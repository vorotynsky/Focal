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
    module Localize = begin

        /// Applies current culture to localizator.
        val toCurrentCulture    : l:Localizator<'a,'b> -> ('a -> 'b option)
        /// Applies UI culture to localizator.
        val toUICulture         : l:Localizator<'a,'b> -> ('a -> 'b option)
        /// Applies invariant culture to localizator.
        val toInvariantCulture  : l:Localizator<'a,'b> -> ('a -> 'b option)

        /// Applies current culture to localizator and gets its result.
        val getCurrentCulture   : l:Localizator<'a,'b> -> ('a -> 'b)
        /// Applies UI culture to localizator and gets its result.
        val getUICulture        : l:Localizator<'a,'b> -> ('a -> 'b)
        /// Applies invariant culture to localizator and gets its result.
        val getInvariantCulture : l:Localizator<'a,'b> -> ('a -> 'b)

        /// Returns result of localizator or default value if localizator coudn't return.
        val getOrDefaultCurrentCulture   : l:Localizator<'a,'b> -> value:'b -> ('a -> 'b)
        /// Returns result of localizator or default value if localizator coudn't return.
        val getOrDefaultUICulture        : l:Localizator<'a,'b> -> value:'b -> ('a -> 'b)
        /// Returns result of localizator or default value if localizator coudn't return.
        val getOrDefaultInvariantCulture : l:Localizator<'a,'b> -> value:'b -> ('a -> 'b)

        /// Returns result of localizator or input value if localizator coudn't return.
        val getOrTokenCurrentCulture     : l:SymmetricLocalizator<'a> -> ('a -> 'a)
        /// Returns result of localizator or input value if localizator coudn't return.
        val getOrTokenUICulture          : l:SymmetricLocalizator<'a> -> ('a -> 'a)
        /// Returns result of localizator or input value if localizator coudn't return.
        val getOrTokenInvariantCulture   : l:SymmetricLocalizator<'a> -> ('a -> 'a)
    end
