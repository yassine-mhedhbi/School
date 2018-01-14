let rec powerlist l =
  match l with
    | [] -> [[]]
    | h::t ->
        [ for x in (powerlist t) ->
          h::x] @ (powerlist t)

let rec powerlist2 l =
  match l with
  | [] -> [[]]
  | x::xs -> 
     let p = powerlist xs
     (List.map (fun u -> x::u) p) @ p


let rec powerset (s : Set<int>) = 
  if Set.isEmpty s then Set.add (Set.empty) Set.empty
  else 
    let i = s.MaximumElement 
    let s2 = Set.remove i s
    let p = powerset s2
    Set.union (Set.map (fun x -> Set.add i x) p) p

   
