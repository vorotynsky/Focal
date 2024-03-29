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


module TypeLocalizator

open System.Globalization
open Focal
open Focal.Operators
open Xunit

type 'a NamedValue(name : string, value : 'a) =
    [<Localize>] member val Name = name with get, set
    [<Localize>] member val Value = value with get, set

    member val UnlocalizableValue = value with get, set

[<Fact>]
let ``NamedValue localize with unlocalizable property: success``() =
    let input = new NamedValue<int>("hello", 5)

    let localizator : SymmetricLocalizator<int NamedValue> =
        let strLoc = Localizator.cultureDependedFunction (fun x -> "[LOCALIZED] " + x) CultureInfo.InvariantCulture
        let intLoc = Localizator.cultureDependedFunction ((+) 1) CultureInfo.InvariantCulture
        TypeLocalizator.typeLocalizator (strLoc >+> intLoc)

    let value = input |> Localize.getInvariantCulture localizator

    Assert.Equal (5, value.UnlocalizableValue)
    Assert.Equal ("[LOCALIZED] hello", value.Name)
    Assert.Equal (6, value.Value)

[<Fact>]
let ``NamedValue Localize to undeterminated culture: None``() =
    let input = new NamedValue<int>("hello", 5)
    let culture = CultureInfo.CreateSpecificCulture "ru-RU"
    
    let localizator : SymmetricLocalizator<int NamedValue> =
        let strLoc = Localizator.cultureDependedFunction (fun x -> "[LOCALIZED] " + x) (CultureInfo.CreateSpecificCulture "en-GB")
        let intLoc = Localizator.cultureDependedFunction ((+) 1) (CultureInfo.CreateSpecificCulture "en-GB")
        TypeLocalizator.typeLocalizator (strLoc >+> intLoc)

    let value = localizator input culture

    Assert.Equal (None, value)

[<Fact>]
let ``NamedValue localize with the missing localzator: None`` () =
    let input = new NamedValue<int>("hello", 5)
    let localizator =
        Localizator.cultureDependedFunction (fun x -> "[LOCALIZED] " + x) (CultureInfo.InvariantCulture)
        |> ObjectLocalizator.fromLocalizator
        |> TypeLocalizator.typeLocalizator<int NamedValue>

    let value = input |> Localize.toInvariantCulture localizator

    Assert.Equal (None, value)

