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
type LocalizeAttribute =
  class
    inherit System.Attribute
    new : unit -> LocalizeAttribute
  end
module TypeLocalizator = begin
  val private getMembersWithAttribute :
    atr:System.Attribute -> t:System.Type -> seq<System.Reflection.MemberInfo>
  val private copyObj : obj:'a -> 'a
  val private iterByPredicate :
    predicate:('a -> bool) -> func:('a -> unit) -> (seq<'a> -> bool)
  val private editProperties :
    localizeFunc:(obj -> obj option) ->
      members:seq<System.Reflection.MemberInfo> -> obj:'a -> bool
  val private editFields :
    localizeFunc:(obj -> obj option) ->
      members:seq<System.Reflection.MemberInfo> -> obj:'a -> bool
  val recordLocalizator :
    baseLoaclizator:ObjectLocalizator -> SymmetricLocalizator<'T>
  val tupleLocalizator :
    baseLocalizator:ObjectLocalizator -> SymmetricLocalizator<'T>
  val typeLocalizator :
    baseLocalizator:ObjectLocalizator -> SymmetricLocalizator<'T>
end
