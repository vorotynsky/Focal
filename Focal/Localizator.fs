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

open System
open System.Globalization

type Localizator<'TKey, 'TValue> = 'TKey -> CultureInfo -> 'TValue option

module Localizator =

    /// function for hiliting support
    let inline internal returnL x : Localizator<_, _> = x

    let id : Localizator<_, _> =
        fun inp culture -> Some inp

    let fail : Localizator<_, _> =
        fun inp culture -> None

    let token x : Localizator<_, _> =
        returnL <| fun inp culture -> Some x

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

    let combine l1 l2 =
        map2 (fun x y -> x, y) l1 l2

    let choose (ls : Localizator<_, _> seq) : Localizator<_, _> =
         returnL <| fun inp culture ->
            ls
            |> Seq.map (fun l -> l inp culture)
            |> Seq.filter Option.isSome
            |> Seq.tryHead
            |> Option.flatten
