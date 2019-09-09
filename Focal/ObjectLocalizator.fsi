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
type ObjectLocalizator = SymmetricLocalizator<obj>
module ObjectLocalizator = begin
  val private objFunc : f:('a -> Option<'b>) -> arg:obj -> obj option
  val fromLocalizator : localizator:Localizator<'a,'b> -> ObjectLocalizator
  val inline choose2 :
    l1:Localizator<'a,'b> -> l2:Localizator<'c,'d> -> ObjectLocalizator
  val ( >+> ) :
    (Localizator<'a,'b> -> Localizator<'c,'d> -> 'e ->
       System.Globalization.CultureInfo -> obj option)
end
