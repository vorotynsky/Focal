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
    val inline private reverseLocalizator :
      localizator:Localizator<'a,'b> ->
        culture:System.Globalization.CultureInfo -> inp:'a -> 'b option
    val toCurrentCulture : l:Localizator<'a,'b> -> ('a -> 'b option)
    val toUICulture : l:Localizator<'a,'b> -> ('a -> 'b option)
    val toInvariantCulture : l:Localizator<'a,'b> -> ('a -> 'b option)
    val getCurrentCulture : l:Localizator<'a,'b> -> ('a -> 'b)
    val getUICulture : l:Localizator<'a,'b> -> ('a -> 'b)
    val getInvariantCulture : l:Localizator<'a,'b> -> ('a -> 'b)
    val inline private getOrDefault :
      l:Localizator<'a,'b> ->
        value:'b -> ('a -> System.Globalization.CultureInfo -> 'b option)
    val getOrDefaultCurrentCulture :
      l:Localizator<'a,'b> -> value:'b -> ('a -> 'b)
    val getOrDefaultUICulture : l:Localizator<'a,'b> -> value:'b -> ('a -> 'b)
    val getOrDefaultInvariantCulture :
      l:Localizator<'a,'b> -> value:'b -> ('a -> 'b)
    val inline private getOrToken :
      l:Localizator<'a,'a> ->
        ('a -> System.Globalization.CultureInfo -> 'a option)
    val getOrTokenCurrentCulture : l:Localizator<'a,'a> -> ('a -> 'a)
    val getOrTokenUICulture : l:Localizator<'a,'a> -> ('a -> 'a)
    val getOrTokenInvariantCulture : l:Localizator<'a,'a> -> ('a -> 'a)
  end
