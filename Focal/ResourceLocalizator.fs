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

[<RequireQualifiedAccessAttribute>]
module Resource =

    open System.Resources
    open System.Reflection

    let private resources (asm : Assembly) =
        asm.GetManifestResourceNames()

    /// Creates a localizator from `System.Resources.ResourceManager` object.
    let createLocalizator (resourseManager : ResourceManager) : Localizator<string, string> =
        Localizator.returnL <| fun inp culture ->
            try
                resourseManager.GetString(inp, culture) |> Some
            with
            | :? MissingManifestResourceException -> None
            | :? MissingSatelliteAssemblyException -> None

    /// Creates a localizator based on all assembly resources.
    let createAssembleyLocalizator (assembly : Assembly) : Localizator<string, string> =
        Localizator.returnL <| fun inp culture ->
            resources assembly
            |> Seq.map
                (fun x -> new ResourceManager(x, assembly)
                >> createLocalizator)
            |> Localizator.choose
            <|| (inp, culture)
