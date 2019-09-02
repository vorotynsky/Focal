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

module Choose

open System.Globalization
open Focal
open Xunit
open Focal.ObjectLocalizator

let multyTypeLocalizator() : ObjectLocalizator =
    let strLoc = Localizator.cultureDependedFunction (fun x -> "[LOCALIZED] " + x) CultureInfo.InvariantCulture
    let intLoc = Localizator.cultureDependedFunction ((+) 1) CultureInfo.InvariantCulture
    strLoc >+> intLoc

[<Fact>]
let ``choose2, first localizator: input = "hello", expected "[LOCALIZED] hello" `` () =
    let input = "hello" |> box

    let value =
        input
        |> Localize.getInvariantCulture (multyTypeLocalizator())
        |> string

    Assert.Equal ("[LOCALIZED] hello", value)

[<Fact>]
let ``choose2, second localizator: input = 100, expected 101 `` () =
    let input = 100 |> box

    let value =
        input
        |> Localize.getInvariantCulture (multyTypeLocalizator())
        :?> int

    Assert.Equal (101, value)
