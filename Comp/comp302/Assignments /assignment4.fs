module h
type id = string

type term =
  | Var of id
  | Const of int 
  | Term of id * term list

(* invariant for substitutions: *)
(* no id on a lhs occurs in any term earlier in the list *)
type substitution = (id * term) list

(* check if a variable occurs in a term *)
let rec occurs (x : id) (t : term) : bool = 
  match t with 
  |Term (f,[]) -> false
  |Var e -> e = x
  |Const _ -> false
  |Term (f, n::ns) -> if occurs x n then true
                      else occurs x (Term(f,ns))

(* substitute term s for all occurrences of variable x in term t *)
let rec subst (s : term) (x : id) (t : term) : term = 
  match t with 
  |Var e when  x = e -> s
  |Var e -> Var e
  |Const C -> Const C
  |Term (id,es) -> Term (id,List.map ( subst s x  ) (es)) 




(* apply a substitution right to left; use foldBack *)
let apply (s : substitution) (t : term) : term = 
  List.foldBack (fun (x,s) acc -> subst s x acc) s t 


(* unify one pair *)
let rec unify (s : term) (t : term) : substitution = 
  let arity = List.length
  match s,t with 
  |Var x, Var y -> if x=y then [] else [(x,Var y)]
  |Const a, Const b -> if a = b then [] else failwith "not unifiable: clashing constants"
  |Term(_,_),Const b -> failwith "not unifiable: term constant clash"
  |Const b,Term(_,_) ->failwith "not unifiable: term constant clash"
  |Var x, Const a -> [(x, Const a)]
  |Const a, Var y -> [(y,Const a)]
  |Term(f,s),Term(g,u) -> if f = g && arity s = arity u then unify_list (List.zip s u)
                          else failwith "not unifiable: Head symbol Conflict"
  |Var x, (Term(_,_) as trm) -> if occurs x trm  then failwith "not unifiable: Circularity" 
                                else [(x,trm)]
  |(Term(_,_) as trm), Var x -> if occurs x trm  then failwith "not unifiable: Circularity" 
                                else [(x,trm)]
  

(* unify a list of pairs *)
and unify_list (s : (term * term) list) : substitution = 
    match s with 
    |[] -> []
    |(x,y)::xs -> let a = unify_list xs
                  let b = unify (apply a x) (apply a y)
                  b@a 

(*
Examples
> let t1 = Term("f",[Var "x";Var "y"; Term("h",[Var "x"])]);;
val t1 : term = Term ("f",[Var "x"; Var "y"; Term ("h",[Var "x"])])
> let t2 = Term("f", [Term("g",[Var "z"]); Term("h",[Var "x"]); Var "y"]);;
val t2 : term =
  Term ("f",[Term ("g",[Var "z"]); Term ("h",[Var "x"]); Var "y"])
> let t3 = Term("f", [Var "x"; Var "y"; Term("g", [Var "u"])]);;
val t3 : term = Term ("f",[Var "x"; Var "y"; Term ("g",[Var "u"])])
> unify t1 t2;;
val it : substitution =
  [("x", Term ("g",[Var "z"])); ("y", Term ("h",[Var "x"]))]
> let t4 = Term("f", [Var "x"; Term("h", [Var "z"]); Var "x"]);;
val t4 : term = Term ("f",[Var "x"; Term ("h",[Var "z"]); Var "x"])
>  

val t5 : term = Term ("f",[Term ("k",[Var "y"]); Var "y"; Var "x"])
> unify t4 t5;;
val it : substitution =
  [("x", Term ("k",[Term ("h",[Var "z"])])); ("y", Term ("h",[Var "z"]))]
> unify t5 t4;;
val it : substitution =
  [("x", Term ("k",[Term ("h",[Var "z"])])); ("y", Term ("h",[Var "z"]))]
> apply it t4;;
val it : term =
  Term
    ("f",
     [Term ("k",[Term ("h",[Var "z"])]); Term ("h",[Var "z"]);
      Term ("k",[Term ("h",[Var "z"])])])
> let t6 = Term("f", [Const 2; Var "x"; Const 3]);;
val t6 : term = Term ("f",[Const 2; Var "x"; Const 3])
> let t7 = Term("f", [Const 2; Const 3; Var "y"]);;
val t7 : term = Term ("f",[Const 2; Const 3; Var "y"])
> unify t6 t7;;
val it : substitution = [("x", Const 3); ("y", Const 3)]
> apply it t7;;
val it : term = Term ("f",[Const 2; Const 3; Const 3])
> unify t1 t7;;
System.Exception: not unifiable: term constant clash
....... junk removed .............
Stopped due to error
*)
