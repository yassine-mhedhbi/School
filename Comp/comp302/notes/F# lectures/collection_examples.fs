(* The Set type *)

set ["Albert";"Bowen";"Caroline"]
set [1;3;2]
set [(1,"Prakash");(5,"Quentin");(4,"Rashid")]
(* Sets can be tested for equality
> set [1;2;3] = set [2;1;3;1];;
val it : bool = true
*)
(* You can convert sets to lists but the set will first remove duplicates and rearrange the items.*)
(*
> Set.toList (set [2;4;3;5;1]);;
val it : int list = [1; 2; 3; 4; 5]
> Set.toList (set [1;1;1;2;2]);;
val it : int list = [1; 2]
*)

let profSet = set ["Prakash";"Greg";"Luc";"David"]
Set.add "Doina" profSet// This creates a NEW set, the original set is unchanged
let newProfSet = Set.add "Doina" profSet
Set.contains "Joelle" profSet
Set.isSubset profSet (set ["Prakash";"David"])
Set.isSubset (set ["Prakash";"David"]) profSet //The bigger set is second
Set.minElement profSet
Set.maxElement profSet
Set.count profSet
let rockStars = set ["Jimi";"Mick";"Robert";"David"]
Set.union profSet rockStars
Set.intersect profSet rockStars
Set.difference rockStars profSet
(* There is also Set.map, Set.filter, Set.exists, Set.forall, Set.fold, Set.foldback *)

(* The Map type *)
(* A map from A to B is a finite subset of A, say D and a function m from D to B. *)
(* You can think of maps as tables. *)

let profsAges = Map.ofList [("Prakash",32);("Greg",26);("Luc",36);("David",35)]

let result = Map.find "Prakash" profsAges

type AssocList = Map<string,int>//Map is a type constructor

(* Map.find 32 profsAges gives a type error. *)

Map.find "Einstein" profsAges //Raises an exception
Map.tryFind "Einstein" profsAges //Returns an option
Map.tryFind "Prakash" profsAges
(*  There is also filter, forall, exists, map, fold and foldback in this collection. *)
let older (s:string) n = n+1 //The function expects the key as well
let updatedAges = Map.map older profsAges//The function expects the key as well



