module Hw2

(* Assignment 2 *) (* Do not edit this line. *)
(* Student name: Yassine Mhedhbi, Id Number: 260673563 *) (* Edit this line. *)

(* In the template below we have written the names of the functions that
you need to define.  You MUST use these names.  If you introduce auxiliary
functions you can name them as you like, except that your names should notp
clash with any of the names we are using.  We have also shown the types
that you should have.  It is OK to change a "rec" declaration and put the
recursive function inside a helper if you want to.  Your code MUST compile
and must NOT go into infinite loops.  An assignment like that means you
have not tested it.  You will get ZERO FOR THE ENTIRE ASSIGMENT even if the
problem is only with one question.  If you are not able to get the code to
compile and run do not submit it.  *)

(* Question 1 *) 

let deriv (f, dx: float) = fun x -> ((f(x + dx) - f(x))/dx)

let rec newton(f,guess:float,tol:float,dx:float) = 
  let rec update (f,guess:float,dx:float) = 
    f guess / deriv (f,dx) guess

  if  abs (f guess) < tol then guess 
  else  newton (f,guess - update (f,guess ,dx),tol, dx)  

 //For testing 
let make_cubic(a:float,b,c) = fun x -> (x*x*x + a * x*x + b*x + c)
let ans = newton(make_cubic(2.0,-3.0,1.0),0.0,0.0001,0.0001)


let root = newton(sin,5.0,0.0001,0.0001)


(* Question 2 *)

type term = Term of float * int
type poly = Poly of (float * int) list

exception EmptyList

let rec zero(p)=
    match p with 
    |[]->[]
    |(x,y)::xs -> if  x<> 0.0 then (x,y)::zero(xs)
                    else zero(xs)


let multiplyPolyByTerm(Term (c,e):term, Poly p:poly):poly =
  let mult (x,y) =
    (c*x, e+y) 
  let map = List.map
                          
  if List.isEmpty p then raise EmptyList
  else 
      let s = zero (map mult p) 
      if List.isEmpty s then Poly [(0.0,0)]
      else Poly s 

//-------------------------------------------------------

let addTermToPoly(Term (c,e):term, Poly p:poly):poly =

  let rec add (Term (c,e):term ,Poly p:poly)=
    match p with 
    |[] -> [(c,e)]
    |(x,y)::xs when e > y -> let s = (x,y)::xs 
                             ((c,e)::s)
    |(x,y)::xs when e = y ->  ((c+x,e)::xs)
    |(x,y)::xs -> (x,y)::add(Term (c,e), Poly xs)

  if List.isEmpty p then raise EmptyList
  else let s = add  (Term (c,e),Poly p)
       if List.isEmpty s then Poly [(0.0,0)]
       else Poly s 
       
  
//-----------------------------------------------------

                
let addPolys(Poly p1:poly, Poly p2:poly):poly = 


  let rec adds(Poly p1:poly, Poly p2:poly) = 
    match p2 with 
    |[]-> p1
    |(x,y)::xs -> let s = addTermToPoly (Term (x,y), Poly p1)
                  adds(s, Poly xs)
                  
  if List.isEmpty p1 || List.isEmpty p2 then raise EmptyList
  else  let s = adds (Poly p2, Poly p1)
        if List.isEmpty s then Poly [(0.0,0)]
        else  Poly s

//--------------------------------------------------------

let multPolys(Poly p1:poly, Poly p2:poly) =

  let rec mults(Poly p1:poly, Poly p2:poly) = 
    match p2 with
    | [] ->Poly  [(0.0,0)]
    | (x,y)::xs  -> addPolys(multiplyPolyByTerm(Term (x,y),Poly p1),mults(Poly p1,Poly xs)) 
  
  if List.isEmpty p1 || List.isEmpty p2 then raise EmptyList
  else 
    let (Poly p) = mults(Poly p1,Poly p2)
    let pprime = zero p
    if List.isEmpty pprime then Poly [(0.0,0)]
    else Poly pprime
  

let exp(b:float, e:int) =
  let rec helper(b:float, e:int, a: float) =
    if (b = 0.0) then 0.0
    elif (e = 0) then a
    elif (e % 2 = 1) then helper(b,e-1, b*a)
    else helper(b*b,e/2,a)
  helper(b,e,1.0)

let evalTerm (v:float) (Term (c,e):term) = if (e=0) then c else c * exp(v,e)
//----------------------------------------------------
let rec evalPoly(Poly p:poly,v:float):float = 

  if List.isEmpty p then raise EmptyList
  else
    match p with 
    |[]->0.0
    |(x,s)::xs -> let t = Term (x,s)
                  evalTerm v t + evalPoly(Poly xs,v)


//--------------------------
let diffPoly (Poly p) = 

  let rec diff (Poly p)=
    match p with 
    |[]-> []
    |(x,y)::xs -> if y<>0 then (x*float y,y-1)::diff(Poly xs)
                  else (0.0,y)::diff(Poly xs)

  if List.isEmpty p then raise EmptyList
  else 
       let s = zero (diff(Poly p))
       if List.isEmpty s then Poly [(0.0,0)]
       else Poly s

(* Question 3 *)
type Exptree =
  | Const of int 
  | Var of string 
  | Add of Exptree * Exptree 
  | Mul of Exptree * Exptree

type Bindings = (string * int) list

(* exception notFound *)

let rec lookup(name:string, env: Bindings) = 

  let helper (key,value) = 
    key = name 

  let result =   List.tryFind helper env 

  match result with 
  |None -> None
  |Some (key,value)-> Some value  


//----------------------------------------------------------



let rec insert(name:string, value: int, b: Bindings) = 
  match b with
  | [] -> [(name,value)]
  | (x,y)::xs -> if x<name then (x,y)::(insert (name,value, xs)) else (name,value)::b
                                           
let rec eval(exp : Exptree, env:Bindings) = 
  match exp with 
  |Const x -> Some x
  |Var str -> lookup(str,env)
  |Add(x,y) -> let a=eval (x,env) 
               let b=eval (y,env)
               match (a,b) with 
               |(Some x,Some y) -> Some(x+y)
               |_ -> None
  |Mul(x,y) -> let a=eval (x,env) 
               let b=eval (y,env)
               match (a,b) with 
               |(Some x,Some y) -> Some(x*y)
               |_ -> None


 //For testing 

let env:Bindings = [("a",3);("b",4);("c",5)]                                

let exp1 = Add(Const 3, Const 4)
let exp2 = Add(Const 3, Var "b")
let exp3 = Add(Var "c", Var "b")
let exp4 = Mul(exp3,exp2)
let exp5 = Add(Var "d",exp3)
let env2 = insert("b",10,env)
let result = eval(exp4,env2)




(* Question 4 *)

type Team = string
type Goals = Goals of int
type Points = Points of int
type Fixture = Team * Team  
type Result = ((Team * Goals) * (Team * Goals))
type Table = Map<Team,Points>
    
let league =
  ["Chelsea"; "Spurs"; "Liverpool"; "ManCity"; "ManUnited"; "Arsenal"; "Everton"; "Leicester"]

let pointsMade (r: Result) = 
  let ((home, Goals x),(away, Goals y)) = r
  if x>y then ((home, Points 3),(away, Points 0))
  elif x<y then ((home, Points 0),(away, Points 3))
  else ((home, Points 1),(away, Points 1))

let initEntry (name:Team) = (name, Points 0)
           
let initializeTable l = Map.ofList (List.map initEntry l)

let weekend1:Result list = [(("Chelsea", Goals 2),("Spurs", Goals 1)); (("Liverpool", Goals 3),("ManCity", Goals 2));(("ManUnited", Goals 1),("Arsenal", Goals 4));(("Everton", Goals 1),("Leicester", Goals 5))]

let weekend2:Result list = [(("Chelsea", Goals 5),("Arsenal", Goals 0)); (("Spurs", Goals 3),("ManCity",Goals 2)); (("ManUnited", Goals 1),("Liverpool", Goals 0));(("Everton", Goals 3),("Leicester", Goals 5))]

let s = [weekend2;weekend1]

let updateTable(t:Table,r:Result):Table = 
  let ((home, Points p1),(away, Points p2)) =  pointsMade(r)
  let (Points oldHome) = Map.find home t 
  let newHome = Points (oldHome + p1) 
  let res = Map.add home newHome t
  let (Points oldAway) = Map.find away t 
  let newAway = Points (oldAway + p2) 
  Map.add away newAway res

  

let rec weekendUpdate(t:Table,rl: Result list): Table = 
  match rl with 
  |[] -> t
  |r::rs -> weekendUpdate(updateTable(t,r),rs)

let rec seasonUpdate(t:Table, sll:Result list list) : Table = 
  match sll with
  |[]-> t
  |r::rs ->seasonUpdate(weekendUpdate(t,r),rs)


let less((s1,n1):Team * Points, (s2,n2):Team * Points) =  
  let (Points p1) = n1
  let (Points p2) = n2 
  p1 < p2

let rec myinsert item lst =
  match lst with
  | [] -> [item]
  | x::xs -> if less(item,x) then x::(myinsert item xs) else item::lst

let rec isort lst =
  match lst with
  | [] -> []
  | x::xs -> myinsert x (isort xs)

let showStandings (t:Table) = isort (Map.toList t)
                                                  
(* Question 5 *)

type Destination = City of string
type RoadMap = Roads of Map<Destination, Set<Destination>>

let roadData = [
  "Andulo", ["Bibala"; "Cacolo"; "Dondo"]
  "Bibala", ["Andulo"; "Dondo"; "Galo"]
  "Cacolo", ["Andulo"; "Dondo"]
  "Dondo",  ["Andulo"; "Bibala"; "Cacolo"; "Ekunha"; "Funda"]
  "Ekunha", ["Dondo"; "Funda"]
  "Funda",  ["Dondo"; "Ekunha"; "Galo"; "Kuito"]
  "Galo",   ["Bibala"; "Funda"; "Huambo"; "Jamba"]
  "Huambo", ["Galo"]
  "Jamba",  ["Galo"]
  "Kuito",  ["Ekunha"; "Funda"]
]

let rec makeRoadMap data = 
  let rec helper(data, acc)=
    match data with 
    |[]-> acc
    |(key,value)::tail -> let valSet = Set.ofList (List.map City value)
                          let res =Map.add (City key) valSet acc
                          helper(tail,res)
  Roads (helper(data,Map.empty)) 
  
  


let rec upToManySteps (Roads r) n startCity = 
  let fromCity (Roads r) found acc cur =
    Set.union acc (Set.difference r.[cur] found)
  let rec helper rs n found des = 
      if n = 0 then found
      else let newDes = Set.fold (fromCity rs found) Set.empty des 
           let next = Set.union newDes found 
           helper rs (n-1) next newDes

  let start = Set[startCity]
  helper (Roads r) n start start

