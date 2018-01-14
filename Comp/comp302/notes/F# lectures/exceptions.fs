let solve(a,b,c) =
  let disc = (b * b - 4.0 * a * c)
  if  disc < 0.0 || a = 0.0 then
    failwith "The discriminant is negative or a was zero."
  else
    ((-b + sqrt(disc))/(2.0*a),(-b - sqrt(disc))/(2.0*a));;

exception Solve

let solve_quad(a,b,c) =
  let disc = (b * b - 4.0 * a * c)
  if  disc < 0.0 || a = 0.0 then
    raise Solve
  else
    ((-b + sqrt(disc))/(2.0*a),(-b - sqrt(disc))/(2.0*a));;
    

let solve_text(a,b,c) = string(solve_quad(a,b,c));;

let solve_catch(a,b,c) = 
  try
    string(solve_quad(a,b,c))
  with
  | Solve -> "No real solutions";;

(* This produces a type error. 
let bad_solve(a,b,c) =
  try
    solve_quad(a,b,c)
  with
  | Solve -> "No real solutions";;
*)

exception NegDisc
exception AisZero

let solve_robust(a,b,c) =
  try
    let disc = (b * b - 4.0 * a * c)
    if  a = 0.0 then
      raise AisZero
    elif disc < 0.0 then
      raise NegDisc
    else
      ((-b + sqrt(disc))/(2.0*a),(-b - sqrt(disc))/(2.0*a))
  with
  | AisZero -> (printfn "This is not a quadratic you idiot!")
               (- c / b, - c / b)
  | NegDisc -> (printfn "The roots are complex.  The real and imaginary parts are:")
               let disc =  (b * b - 4.0 * a * c)
               let realpart = -b/(2.0 * a)
               let imagpart = (sqrt(-disc))/(2.0 * a)
               (realpart,imagpart);;

(* Built-in exceptions *)

let basic n = if n = 0 then 0 else failwith "N was not 0"

let fact n =
  let rec helper (n,m) =
    if (n = 0) then m else helper(n-1,n*m)
  if n < 0 then invalidArg "n" "cannot be negative"
  else helper(n,1);;

let gethead (l : int list) =
  match l with
    | [] -> nullArg "l" "cannot be the empty list"
    | x :: xs -> x;;

let divide(n,m) =
  if m = 0 then invalidOp "tried to divide by 0" else n / m;;

let ConvertToString (l: 'a seq) = System.String.Join(",", l)

(* Using exceptions for backtracking. *)

exception Change

let makeChange coinTypes amt = 
  let rec helper(coins: int list, amt:int) : int list =
    match (coins, amt) with
    | (_,0) -> []
    | ([],_) -> raise Change
    | (coin::rest,amt) -> 
       try
         if 
           (coin > amt) 
         then 
           helper(rest,amt) 
         else 
           coin::(helper(coins, amt - coin))
       with 
         | Change -> helper(rest,amt)

  try
    let C = helper(coinTypes,amt) in
      ("Return the following coins: " + (ConvertToString C) + "\n")
      
    with
      | Change -> "Sorry, I cannot make change.\n"

  
   

  
