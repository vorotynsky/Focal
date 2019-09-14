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
    /// A function that convert key (input) to value (output) depending on `CultureInfo`.
    type Localizator<'TKey,'TValue> =
        'TKey -> System.Globalization.CultureInfo -> 'TValue option

    /// A loclizator with same key and value types.
    type SymmetricLocalizator<'T> = Localizator<'T,'T>

    [<RequireQualifiedAccessAttribute>]
    module Localizator = begin

        val inline internal returnL : x:Localizator<'a,'b> -> Localizator<'a,'b>

        /// The localizator that returns token
        val id : SymmetricLocalizator<'a>

        /// The localizator that fails (returns None).
        val fail : Localizator<'a, 'b>

        /// The localizator that returns `x`. Independent on an input value.
        val token : x:'a -> Localizator<'b,'a>

        /// A localizator that returns `value` if input culture is `culture`.
        val cultureDependedValue : value:'a -> culture:System.Globalization.CultureInfo -> Localizator<'b,'a>

        /// A localizator based on function `f` what execute on input if input culture is `culture`.
        val cultureDependedFunction : f:('a -> 'b) -> culture:System.Globalization.CultureInfo -> Localizator<'a,'b>

        /// Invokes a function on an localizator value that itself yields an localizator.
        ///
        /// Begining of the monad.
        val bind : f:('a -> Localizator<'inp,'b>) -> l:Localizator<'inp,'a> -> Localizator<'inp,'b>

        /// Transforms an localizator's output by using a specified mapping function.
        val map : f:('a -> 'b) -> (Localizator<'inp,'a> -> Localizator<'inp,'b>)

        /// Gets a function from localizator by mapping it to function on localizators.
        val apply : fl:Localizator<'inp,('a -> 'b)> -> l:Localizator<'inp,'a> -> Localizator<'inp,'b>

        /// Transforms an localizators' output by using a specified mapping function.
        val inline map2 :
            f:('a -> 'b -> 'c) -> l1:Localizator<'inp,'a> -> l2:Localizator<'inp,'b> -> Localizator<'inp,'c>

        /// Combine 2 localizators into one.
        val combine :
            l1:Localizator<'ia,'a> -> l2:Localizator<'ib,'b> -> Localizator<('ia * 'ib),('a * 'b)>

        /// Returns a localizator that a contains successful result.
        val choose2 :
            l1:Localizator<'a,'b> -> l2:Localizator<'a,'b> -> Localizator<'a,'b>

        /// Returns the first localizator that contains a successful result.
        val choose : ls:seq<Localizator<'a,'b>> -> Localizator<'a,'b>

        /// Transforms an localizator's input by using a specified mapping function.
        val transformKey : f:('b -> 'a) -> l:Localizator<'a,'v> -> Localizator<'b,'v>

        /// Combine 2 localizators with the same input type into one.
        val branch : l1:Localizator<'a,'b> -> l2:Localizator<'a,'c> -> Localizator<'a,('b * 'c)>
    end
