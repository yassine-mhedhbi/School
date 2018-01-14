
let x = 1
let y = 2
x + y

(* let x = 1
     let y = 2
     x + y *)

let result = 
  let x = 1
  let y = 2
  x + y

let x = 1
let x = 2
x + x

let x = 1 in
  let y = 2 in
    x + y

let x = 1 in
  let x = 2 in
    x + x

let x = 1 in
  let x = 2 in
  printf "x is %i\n" x

let x = 1 in
  let y = x in
    let x = 2 in
      x + y

let result =
  let x = 1
  let y = x
  let x = 2
  y

let result = 
  let x = 1 
  let x = 2
  x

let x = 10 in
  let square n = n * n
  square x


(* The first interesting example.  The function remembers the definition of x!  *)
let result2 =
  let x = 1 in
  let f = fun u -> (printf "Inside f, x is %i\n" x);(u + x) in
  let x = 2 in
  (printf "x is %i\n" x);f x

(*
> x is 2
Inside f, x is 1
val result2 : int = 3
*)

(* Lightweight syntax. *)
let result3 =
  let x = 1
  let f = fun u -> (u + x)
  let x = 2
  f x
  
let result4 =
  let x = 1
  let y = let x = 3 in x
  (printf "x is %i\n" x); let x = 2  in (printf "Now x is %i\n" x);y

(*
> x is 1
Now x is 2
val result4 : int = 3
*)

let x = 1 in
  let y = (let u = 3 in u + x) in
    let x = 2 in
    x + y

(*
> val it : int = 6
*)

let x = 1 in
  let f = (let u = 3 in (fun y -> ();u + y + x)) in
    let x = 2 in
    f x
(*
> val it : int = 6
*)
 
let x = 1 in
  let f = (let u = 3 in (fun y -> u + y + x)) in
    let x = u in
    f x
  
(*
error FS0039: The value or constructor 'u' is not defined
*)

let x = 1 in
  let f = (let u = 3 in (fun y -> (printf "Now u is %i\n" u);u + y + x)) in
    let x = 2 in
    f x
(*
> Now u is 3
val it : int = 6
*)


  
  

  
    
    
