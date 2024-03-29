// Copyright 2019 Vorotynsky Maxim
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Focal

open System.Globalization

type Localizator<'TKey, 'TValue> = 'TKey -> CultureInfo -> 'TValue option
type SymmetricLocalizator<'T> = Localizator<'T, 'T>

[<RequireQualifiedAccessAttribute>]
module Localizator =

    /// function for hiliting support
    let inline internal returnL x : Localizator<_, _> = x

    let id : Localizator<_, _> =
        fun inp culture -> Some inp

    let fail : Localizator<_, _> =
        fun inp culture -> None

    let token x : Localizator<_, _> =
        returnL <| fun inp culture -> Some x

    let cultureDependedValue value culture : Localizator<_, _> =
        returnL <| fun _ c -> if culture = c then Some value else None
        
    let cultureDependedFunction f culture : Localizator<_, _> =
        returnL <| fun inp c -> if culture = c then Some (f inp) else None

    let bind (f : 'a -> Localizator<_, 'b>) (l : Localizator<_, 'a>) : Localizator<_, 'b> =
        returnL <| fun inp culture ->
            fun x -> (f x) inp culture
            |> Option.bind
            <| l inp culture

    let map f : Localizator<_, 'a> -> Localizator<_, 'b> =
        bind (f >> token)

    let apply (fl : Localizator<_, 'a -> 'b>) (l : Localizator<_, 'a>) : Localizator<_, 'b> =
        returnL <| fun inp culture ->
            fun f ->
                l inp culture
                |> Option.map f
            |> Option.bind
            <| fl inp culture

    let inline map2 f l1 l2 : Localizator<_, _> =
        let (<!>) = map
        let (<*>) = apply
        f <!> l1 <*> l2

    let combine (l1 : Localizator<_, _>) (l2 : Localizator<_, _>) : Localizator<_, _> =
        returnL <| fun (inp1, inp2) culture ->
            match (l1 inp1 culture), (l2 inp2 culture) with
            | Some r1, Some r2 -> Some (r1, r2)
            | _ -> None               

    let choose2 (l1 : Localizator<_, _>) (l2 : Localizator<_, _>) : Localizator<_, _> =
        returnL <| fun inp culture ->
            match l1 inp culture, l2 inp culture with
            | Some x, _ -> Some x
            | _, x -> x

    let choose (ls : Localizator<_, _> seq) : Localizator<_, _> =
         Seq.reduce choose2 ls

    let transformKey (f : 'b -> 'a) (l : Localizator<_, _>) : Localizator<_, _> =
        f >> l

    let branch l1 l2 : Localizator<_, _> =
        combine l1 l2
        |> transformKey (fun k -> k, k)
