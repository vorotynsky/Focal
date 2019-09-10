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

module Localize =

    open Focal
    open System.Globalization

    let inline private reverseLocalizator (localizator : Localizator<_, _>) =
        fun culture inp -> localizator inp culture

    let toCurrentCulture l =
        reverseLocalizator l CultureInfo.CurrentCulture

    let toUICulture l =
        reverseLocalizator l CultureInfo.CurrentUICulture

    let toInvariantCulture l =
        reverseLocalizator l CultureInfo.InvariantCulture

    let getCurrentCulture l =
        toCurrentCulture l >> Option.get

    let getUICulture l =
        toUICulture l >> Option.get

    let getInvariantCulture l =
        toInvariantCulture l >> Option.get

    let inline private getOrDefault l value = Localizator.choose [l; Localizator.token value]

    let getOrDefaultCurrentCulture l value =
        getOrDefault l value |> toCurrentCulture >> Option.get

    let getOrDefaultUICulture l value =
        getOrDefault l value |> toUICulture >> Option.get

    let getOrDefaultInvariantCulture l value =
        getOrDefault l value |> toInvariantCulture >> Option.get

    let inline private getOrToken l = Localizator.choose [l; Localizator.id]

    let getOrTokenCurrentCulture l =
        getOrToken l |> toCurrentCulture >> Option.get

    let getOrTokenUICulture l =
        getOrToken l |> toUICulture >> Option.get

    let getOrTokenInvariantCulture l =
        getOrToken l |> toInvariantCulture >> Option.get
