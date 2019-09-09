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

type ObjectLocalizator = SymmetricLocalizator<obj>

module ObjectLocalizator =

    let private objFunc (f : 'a -> 'b Option) : (obj -> obj option) =
        fun arg ->
            if arg :? 'a then arg :?> 'a |> Some else None
            |> Option.bind f
            |> Option.map (fun x -> x :> obj)

    let fromLocalizator<'a, 'b> (localizator : Localizator<'a, 'b>)  : ObjectLocalizator =
        Localizator.returnL <| fun inp culture ->
            let func inp = localizator inp culture
            objFunc func inp

    let inline choose2 (l1 : Localizator<_, _>) (l2 : Localizator<_, _>) : ObjectLocalizator =
        Localizator.choose2 (fromLocalizator l1) (fromLocalizator l2)

    let (>+>) = choose2
