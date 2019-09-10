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

    /// Symetric localizator with object input and output.
    type ObjectLocalizator = SymmetricLocalizator<obj>

    module ObjectLocalizator = begin

        /// Converts localizator to object localizator.
        val fromLocalizator : localizator:Localizator<'a,'b> -> ObjectLocalizator

        /// Chooses between 2 localizators and converts it to object localizator.
        val inline choose2 :
            l1:Localizator<'a,'b> -> l2:Localizator<'c,'d> -> ObjectLocalizator

        /// Chose2 operator.
        val ( >+> ) : Localizator<'a,'b> -> l2:Localizator<'c,'d> -> ObjectLocalizator
    end
