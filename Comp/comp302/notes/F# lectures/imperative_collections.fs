List.iter (fun x -> printf "%i\n" x ) [1..5]
List.iteri (fun n -> fun x -> let m = n+1 in printf "Value at position %i is  %i\n" m x ) [15..-2..1]

let S = Set.ofList ["alpha";"beta";"gamma";"delta"]

Set.iter (fun x -> let m = String.length x in printf "The length of %s is %i\n" x m) S

type intBinTree = Leaf of int | Node of intBinTree * int * intBinTree


let rec preIter f t =
  match t with
  | Leaf n -> f n
  | Node(left, x, right) -> (f x) ; (preIter f left); (preIter f right)

let rec postIter f t =
  match t with
  | Leaf n -> f n
  | Node(left, x, right) -> (postIter f left); (postIter f right); (f x)

let rec inIter f t =
  match t with
  | Leaf n -> f n
  | Node(left, x, right) -> (inIter f left); (f x); (inIter f right)

let t0 = Node(Leaf 3, 10, Leaf 17)
let t1 = Node(t0, 20, Node(Leaf 22, 25, Leaf 27))

preIter (fun n -> printfn "%i\n" n) t1
postIter (fun n -> printfn "%i\n" n) t1
inIter  (fun n -> printfn "%i\n" n) t1


type 'a ListTree = Node of 'a * ('a ListTree list)

let t3 = Node("x",[])
let t4 = Node("y",[])
let t5 = Node("z",[])
let t6 = Node("u",[])
let t7 = Node("v",[])
let t8 = Node("xx",[t3;t4])
let t9 = Node("yy",[t4;t5;t6;t7])
let t10 = Node("abc",[t8;t9;t6])
let t11 = Node("pqr",[t10;t7;t9])
let rec depthFirstIter f (Node(x,ts)) =
  (printf "\n"); (f x); (List.iter (depthFirstIter f) ts)

depthFirstIter (fun x -> printf " %s " x) t11

open System.Collections.Generic

let qt = Queue<int> ()
(* We could also have done
let qt = new Queue<int> ()
*)

qt.Enqueue 1
qt.Enqueue 2
qt.Enqueue 3
qt.Count
let n = qt.Dequeue
(* This does not produce an item as you may think.  Its type is
val n : (unit -> int) 
So in order to get an integer one has to apply it to () *)

qt.Dequeue ()
(*
> val it : int = 1
*)
let x = n ()
(* "n" has been defined to dequeue from qt so we will get
val x : int = 2
and also
>qt.Count;;
val it : int = 1
*)
 
qt.Peek //lets you see the first item without removing it.


  
