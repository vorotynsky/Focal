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

    /// The Attribute for mark properties and fields for localization.
    [<System.AttributeUsage(System.AttributeTargets.Property ||| System.AttributeTargets.Field)>]
    type LocalizeAttribute =
        class
            inherit System.Attribute
            new : unit -> LocalizeAttribute
        end

    [<RequireQualifiedAccessAttribute>]
    module TypeLocalizator = begin

        /// Creates localizator for record type based on existing localizator.
        val recordLocalizator : baseLoaclizator:ObjectLocalizator -> SymmetricLocalizator<'T>

        /// Creates localizator for tuple based on existing localizator.
        val tupleLocalizator : baseLocalizator:ObjectLocalizator -> SymmetricLocalizator<'T>

        /// Creates localizator for type based on existing localizator.
        ///
        /// Localize only fields and properties with `LocalizeAttribute`.
        val typeLocalizator : baseLocalizator:ObjectLocalizator -> SymmetricLocalizator<'T>
    end
