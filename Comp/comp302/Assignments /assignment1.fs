(* Assignment 1 *) (* Do not edit this line. *)
(* Student name: Yassine Mhedhbi, Id Number: 260673563 *) 

(* In the template below we have written the names of the functions that
you need to define.  You MUST use these names.  If you introduce auxiliary
functions you can name them as you like, except that your names should not
clash with any of the names we are using.  We have also shown the types
that you should have.  Your code MUST compile and must NOT go into infinite
loops.  An assignment like that means you have not tested it.  You will get
ZERO FOR THE ENTIRE ASSIGMENT even if the problem is only with one
question.  If you are not able to get the code to compile and run do not
submit it.  *)

(* module hw1_sol.  Use this if you want to load the file into an interactive session.*)

(* Question 1 *) (* Do not edit this line. *)

let rec sumlist l:float = 
  match l with
    | [] -> 0.0
    | e::li -> e + sumlist li

let rec pairlists twolists:float list =
  match twolists with
    | ([],[]) -> []
    | ([],x::xs) -> failwith "Error -- lists are not of the same length"
    | (x::xs, []) -> failwith "Error -- lists are not of the same length"
    | (x::xs, y::ys) -> x*y::pairlists (xs, ys)

let w_mean weights data = 
  (sumlist (pairlists (data, weights))) / sumlist weights 
  
(* Question 2. *)  (* Do not edit this line. *)

let rec memberof pair = 
  let (item,list) = pair
  match list with
  |x::xs -> if x=item then true
            else memberof (item,xs)
  |[] -> false
    


let rec remove(item, lst) = 
  match item, lst with
    | a,x::xs when a=x -> remove(a,xs)
    | a,x::xs when a<>x -> x::remove(a,xs)
    | _,[] -> []


(* Question 3. *)  (* Do not edit this line. *)

let findMax l = 
  let rec helper(l,m) = 
    match m,l with
    | m,l::ls when l>=m -> helper(ls,l)
    | m,l::ls  -> helper(ls,m)
    | m,[] -> m

  match l with
  | [] -> failwith "Error -- empty list"
  | (x::xs) -> helper(xs,x)

(* Question 4. *)  (* Do not edit this line. *)
  
let rec selsort l = 
  match l with
    | [] -> []
    | lis -> 
      let max = findMax lis
      let rest = remove (max,lis)
      let sortedrest = selsort rest
      max::sortedrest


(* Question 5. *)  (* Do not edit this line. *)

let rec common twolists = 
  let (a,b)=twolists

  let rec helper3 (a,b) =
    match a with
    | [] -> [] // a is empty : no elements can be common so return empty list
    | x::xs -> if memberof (x,b) then x :: helper3 (remove(x,xs),b) // if x is a member of b, keep it
                else helper3 (xs,b) // otherwise continue with remaining elements
  helper3(a,b)

  



(* Question 6. *)   (* Do not edit this line. *)

(* Mergesort requires that you use recursion.  Using isort or
some other sort defeats the whole purpose.*)

let rec split l = 
  let rec helper3 s (xs, ys) =
    match s with
    | x::y::rest -> helper3 rest (x::xs, y::ys)
    | [x] -> (x::xs, ys)
    | _ -> (xs, ys)
  helper3 l ([], [])



let rec merge twolists = 
   match twolists with
   |[],ys -> ys
   |xs,[] -> xs
   |x::xs,y::ys when x<y -> x::merge(xs,y::ys)
   |x::xs,y::ys  -> y::merge(x::xs,ys)
   |[],[] -> []


let rec mergesort l = 
  match l with
  | [] -> []
  | (n::[]) -> n::[] (* Without this you will go into an infinite loop. *)
  | n::ns -> let (l1,l2) = split(n::ns)
             merge(mergesort l1, mergesort l2)



