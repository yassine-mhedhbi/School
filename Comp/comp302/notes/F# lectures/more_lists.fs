(* There is a built in function @ for append.  This is for illustration only. *)

let rec append(l1,l2) =
  match l1 with
  | [] -> l2
  | x::xs -> x::(append(xs,l2))

(*
> append([1;2;3;4],[9;8;7;6]);;
val it : int list = [1; 2; 3; 4; 9; 8; 7; 6]
*)

(* reverse is also available as a built-in function.*)
let rec rev l =
  match l with
  | [] -> []
  | x::xs -> rev(xs) @ [x] // Must write [x], not just x.  Can't use :: to attach to the end.

(* This implementation is O(n^2), where n is the length of the list.  That's very inefficient. *)
(* Here is a better implementation running in O(n).  It uses tail recursion with an extra
parameter where the partial answers are accumulated. *)

let rev2 l =
  let rec helper(l1,l2) = 
    match l1 with
    | [] -> l2
    | x::xs -> helper(xs, x::l2)
  helper(l,[])

(* Illustrating pattern matching with the result of a recursive call. *)
let rec sumProd l =
  match l with
  | [] -> (0,1)
  | x::xs -> 
        let (s,p) = sumProd xs
        (x+s,x*p)
(*
val sumProd : l:int list -> int * int
> sumProd [1..5];;
val it : int * int = (15, 120)
*)

(* The next few examples are fundamental and illustrate the use of higher-order functions.*)
(* The examples are all built-in primitives in F#; these examples are for illustration only.*)

let rec mymap f l =
  match l with 
  | [] -> []
  | (x::xs) -> (f x) :: (mymap f xs)
(*
val mymap : f:('a -> 'b) -> l:'a list -> 'b list
> let inc n = n + 1;;
val inc : n:int -> int
> mymap inc [0;1;2;3];;
val it : int list = [1; 2; 3; 4]
> mymap inc;;
val it : (int list -> int list) = <fun:it@41-3>
*)

let rec myfilter test lst =
  match lst with
  | [] -> []
  | x::xs -> if (test x) then x::(myfilter test xs) else (myfilter test xs)
(*
val it : (int list -> int list) = <fun:it@41-3>
val myfilter : test:('a -> bool) -> lst:'a list -> 'a list
> let odd n = (n % 2) = 1;;
val odd : n:int -> bool
> myfilter odd [1..19];;
val it : int list = [1; 3; 5; 7; 9; 11; 13; 15; 17; 19]
*)

let rec myforall test l =
  match l with
  | [] -> true
  | x::xs -> if (test x) then (myforall test xs) else false
(*
myforall odd [1..17];;
val it : bool = false
> myforall odd [1..2..17];;
val it : bool = true
*)

(* A remarkably versatile function. v plays the role of an initial value.*)
let rec myfold f v l =
  match l with 
  | [] -> v
  | x::xs -> myfold f (f v x) xs
(*
val myfold : f:('a -> 'b -> 'a) -> v:'a -> l:'b list -> 'a
> let adder acc item = acc + item;;
val adder : acc:int -> item:int -> int
> let sumList = myfold adder 0 [1..10];;
val sumList : int = 55

> (@);;
val it : ('a list -> 'a list -> 'a list) = <fun:clo@68-1>
> let glueLists ll = myfold (@) [] ll;;
val glueLists : ll:'a list list -> 'a list
> glueLists  [[1..5];[6..10];[11..15];[16..20]];;
val it : int list =
  [1; 2; 3; 4; 5; 6; 7; 8; 9; 10; 11; 12; 13; 14; 15; 16; 17; 18; 19; 20]
*)

(* This is similar but a bit clumsier. *)
let rec myreduce f l =
  match l with
  | [] -> failwith "The input list was empty"
  | x::[] -> x
  | x1::x2::[] -> (f x1 x2)
  | x1::x2::xs -> f (f x1 x2) (myreduce f xs)
(*
val myreduce : f:('a -> 'a -> 'a) -> l:'a list -> 'a
> myreduce (+) [1];;
val it : int = 1
> myreduce (+) [1;2];;
val it : int = 3
> myreduce (+) [1;2;3;4];;
val it : int = 10
*)
