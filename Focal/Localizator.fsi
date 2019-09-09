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
  type Localizator<'TKey,'TValue> =
    'TKey -> System.Globalization.CultureInfo -> 'TValue option
  type SymmetricLocalizator<'T> = Localizator<'T,'T>
  module Localizator = begin
    val inline internal returnL : x:Localizator<'a,'b> -> Localizator<'a,'b>
    val id : inp:'a -> culture:System.Globalization.CultureInfo -> 'a option
    val fail : inp:'a -> culture:System.Globalization.CultureInfo -> 'b option
    val token : x:'a -> Localizator<'b,'a>
    val cultureDependedValue :
      value:'a -> culture:System.Globalization.CultureInfo -> Localizator<'b,'a>
    val cultureDependedFunction :
      f:('a -> 'b) ->
        culture:System.Globalization.CultureInfo -> Localizator<'a,'b>
    val bind :
      f:('a -> Localizator<'a0,'b>) ->
        l:Localizator<'a0,'a> -> Localizator<'a0,'b>
    val map : f:('a -> 'b) -> (Localizator<'a0,'a> -> Localizator<'a0,'b>)
    val apply :
      fl:Localizator<'a,('a0 -> 'b)> ->
        l:Localizator<'a,'a0> -> Localizator<'a,'b>
    val inline map2 :
      f:('a -> 'b -> 'c) ->
        l1:Localizator<'d,'a> -> l2:Localizator<'d,'b> -> Localizator<'d,'c>
    val combine :
      l1:Localizator<'a,'b> ->
        l2:Localizator<'c,'d> -> Localizator<('a * 'c),('b * 'd)>
    val choose2 :
      l1:Localizator<'a,'b> -> l2:Localizator<'a,'b> -> Localizator<'a,'b>
    val choose : ls:seq<Localizator<'a,'b>> -> Localizator<'a,'b>
    val transformKey :
      f:('b -> 'a) -> l:Localizator<'a,'a0> -> Localizator<'b,'a0>
    val branch :
      l1:Localizator<'a,'b> ->
        l2:Localizator<'a,'c> -> Localizator<'a,('b * 'c)>
  end
