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

module RecordLocalizator

open System.Globalization
open Focal
open Xunit
open Focal.ObjectLocalizator

type 'a Point = { X : 'a; Y : 'a } 
type 'a NamedValue = { Name : string; Value : 'a } 

[<Fact>]
let ``int pair: success``() =
    let input = { X = 5; Y = 10 }

    let iobjLocalizator = ObjectLocalizator.fromLocalizator <| Localizator.cultureDependedFunction ((+) 1) CultureInfo.InvariantCulture
    let localizator : SymmetricLocalizator<int Point> = TypeLocalizator.recordLocalizator iobjLocalizator

    let value = input |> Localize.getInvariantCulture localizator

    Assert.Equal ({ X = 6; Y = 11 }, value)

[<Fact>]
let ``string & int: success`` () =
    let input = { Name = "hello"; Value = 5 }

    let tupleLocalizator =
        let strLoc = Localizator.cultureDependedFunction (fun x -> "[LOCALIZED] " + x) CultureInfo.InvariantCulture
        let intLoc = Localizator.cultureDependedFunction ((+) 1) CultureInfo.InvariantCulture
        TypeLocalizator.recordLocalizator (strLoc >+> intLoc)

    let value = input |> Localize.getInvariantCulture (tupleLocalizator)

    Assert.Equal ({ Name ="[LOCALIZED] hello"; Value = 6 }, value)

[<Fact>]
let ``string & int Localize to undeterminated culture: None``() =
    let input = { Name = "hello"; Value = 5 }
    let culture = CultureInfo.CreateSpecificCulture "ru-RU"
    
    let localizator =
        let strLoc = Localizator.cultureDependedFunction (fun x -> "[LOCALIZED] " + x) (CultureInfo.CreateSpecificCulture "en-GB")
        let intLoc = Localizator.cultureDependedFunction ((+) 1) (CultureInfo.CreateSpecificCulture "en-GB")
        TypeLocalizator.recordLocalizator (strLoc >+> intLoc)

    let value = localizator input culture

    Assert.Equal (None, value)


