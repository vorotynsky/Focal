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

open FSharp.Reflection
open System
open System.IO
open System.Reflection
open System.Runtime.Serialization.Formatters.Binary
open System.Runtime.Serialization

[<AttributeUsage(AttributeTargets.Property ||| AttributeTargets.Field)>]
type LocalizeAttribute() = inherit Attribute()

[<RequireQualifiedAccessAttribute>]
module TypeLocalizator = 

    let private getMembersWithAttribute atr (t : Type) = 
        t.GetMembers()
        |> Seq.filter (fun x -> x.GetCustomAttributes() |> Seq.contains atr)

    let private copyObj (obj : 'a) =
        let formatter = new BinaryFormatter()
        formatter.Context <- new StreamingContext(StreamingContextStates.Clone)
        using (new MemoryStream()) (fun ms -> 
            formatter.Serialize(ms, obj)
            ms.Position <- 0L
            formatter.Deserialize(ms) :?> 'a)

    let private iterByPredicate predicate func =
        let doF p v = if p then func v
        Seq.map (fun x -> x, (predicate x))
        >> Seq.fold (fun s (v, p) -> doF p v; p && s) true
        

    let private editProperties (localizeFunc : obj -> obj option) (members : MemberInfo seq) obj  =
        members
        |> Seq.choose (fun x -> if x :? PropertyInfo then Some (x :?> PropertyInfo) else None)
        |> Seq.filter (fun pi -> pi.CanRead && pi.CanWrite)
        |> Seq.map (fun x -> x, (localizeFunc (x.GetValue (obj))))
        |> iterByPredicate (fun (_, v) -> v.IsSome) (fun (pi, v) -> pi.SetValue(obj, Option.get v))

    let private editFields (localizeFunc : obj -> obj option) (members : MemberInfo seq) obj  =
        members
        |> Seq.choose (fun x -> if x :? FieldInfo then Some (x :?> FieldInfo) else None)
        |> Seq.map (fun x -> x, (localizeFunc (x.GetValue (obj))))
        |> iterByPredicate (fun (_, v) -> v.IsSome) (fun (pi, v) -> pi.SetValue(obj, Option.get v))

    let recordLocalizator<'T> (baseLoaclizator : ObjectLocalizator) : SymmetricLocalizator<'T> =
        if not <| FSharpType.IsRecord (typeof<'T>) then invalidOp "The generic parameter isn't a record. "
        Localizator.returnL <| fun inp culture ->
            let values =
                FSharpValue.GetRecordFields inp
                |> Array.map (fun x -> baseLoaclizator x culture)
            if Array.contains None values then None
            else
                values
                |> Array.map Option.get
                |> fun x -> FSharpValue.MakeRecord (typeof<'T>, x)
                :?> 'T
                |> Some

    let tupleLocalizator<'T> (baseLocalizator : ObjectLocalizator) : SymmetricLocalizator<'T> =
        if not <| FSharpType.IsTuple (typeof<'T>) then invalidOp "The generic parametr isn't a tuple. "
        Localizator.returnL <| fun inp culture ->
            let values =
                FSharpValue.GetTupleFields inp
                |> Array.map (fun x -> baseLocalizator x culture)
            if Array.contains None values then None
            else
                values
                |> Array.map Option.get
                |> fun x -> FSharpValue.MakeTuple (x, typeof<'T>)
                :?> 'T
                |> Some

    let typeLocalizator<'T> (baseLocalizator : ObjectLocalizator) : SymmetricLocalizator<'T> =
        let members = getMembersWithAttribute (new LocalizeAttribute()) typeof<'T>
        Localizator.returnL <| fun inp culture ->
            let func inp = baseLocalizator inp culture
            let newInp = copyObj inp
            let (&&&) f g = fun a b c -> (f a b c) && (g a b c)
            if (editFields &&& editProperties) func members newInp then Some newInp else None
