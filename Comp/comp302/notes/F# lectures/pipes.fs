(* Examples using pipes and other composition operators *)

(* The normal use of map *)

let map = List.map

let inc n = n + 1

map inc [1..5]
(* val it : int list = [2; 3; 4; 5; 6] *)

(* But you can construct the argument first and then pass it to the function using |>
This is called the pipe forward operator.  x |> f is the same as f x
The expression below is the same as the one just above. *)

[1..5] |> map inc
(* val it : int list = [2; 3; 4; 5; 6]  *)

(* This is very useful if you want to apply a sequence of functions to an expression. *)

[1..5] |> map (fun n -> n * n) |> List.fold (+) 0
(* val it : int = 55 *)

(* The forward composition operator is written >> *)
(* f >> g is the same as fun x -> g(f(x))  *)

((map (fun n -> n * n)) >> (List.fold (+) 0)) [1..5]
(* val it : int = 55 *)

(* You will get a type error if you omit the outermost parens.  Why? *)

(* There is a backward pipe operator written as <|.  f <| x is the same as f x.
Hey!  Why on Earth do we need that?  We can do that without any special operator.
It is, however, useful in changing precedence without using parens. *)

List.fold (+) 0 <| map (fun n -> n * n)  [1..5]
(* val it : int = 55 *)

(* There is also a backward composition
operator <<.  (f << g) is the same as fun x -> f (g x). *)

let square n = n * n
let negate n = -n

(square >> negate) 5  // val it : int = -25
(square << negate) 5  // val it : int = 25



