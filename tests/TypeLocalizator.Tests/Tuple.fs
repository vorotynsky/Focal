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

module TupleLocalizator

open System.Globalization
open Focal
open Xunit
open Focal.ObjectLocalizator

[<Fact>]
let ``int pair: success``() =
    let input = 5, 10

    let iobjLocalizator = ObjectLocalizator.fromLocalizator <| Localizator.cultureDependedFunction ((+) 1) CultureInfo.InvariantCulture
    let localizator : SymmetricLocalizator<int * int> = TypeLocalizator.tupleLocalizator iobjLocalizator

    let value = input |> Localize.getInvariantCulture localizator

    Assert.Equal ((6, 11), value)

[<Fact>]
let ``string * int: success`` () =
    let input = "hello", 5

    let tupleLocalizator =
        let strLoc = Localizator.cultureDependedFunction (fun x -> "[LOCALIZED] " + x) CultureInfo.InvariantCulture
        let intLoc = Localizator.cultureDependedFunction ((+) 1) CultureInfo.InvariantCulture
        TypeLocalizator.tupleLocalizator (strLoc >+> intLoc)

    let value = input |> Localize.getInvariantCulture (tupleLocalizator)

    Assert.Equal (("[LOCALIZED] hello", 6), value)

[<Fact>]
let ``string * int Localize to undeterminated culture: None``() =
    let input = "hello", 5
    let culture = CultureInfo.CreateSpecificCulture "ru-RU"
    
    let localizator =
        let strLoc = Localizator.cultureDependedFunction (fun x -> "[LOCALIZED] " + x) (CultureInfo.CreateSpecificCulture "en-GB")
        let intLoc = Localizator.cultureDependedFunction ((+) 1) (CultureInfo.CreateSpecificCulture "en-GB")
        TypeLocalizator.tupleLocalizator (strLoc >+> intLoc)

    let value = localizator input culture

    Assert.Equal (None, value)
