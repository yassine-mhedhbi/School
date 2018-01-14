type Cell = { data : int; next : RList}
and RList = Cell option ref

let c1 = {data = 1; next = ref None}
let c2 = {data = 2; next = ref (Some c1)}
let c3 = {data = 3; next = ref (Some c2)}
let c5 = {data = 5; next = ref (Some c3)}

(* This converts an RList to an ordinary list. *)
let rec displayList (c : RList) =
  match !c with
    | None -> []
    | Some { data = d; next = l } -> d :: (displayList l)

let cellToRList (c:Cell):RList = ref (Some c)
    
let bigger(x:int, y:int) = (x > y)

let rec insert comp (item: int) (list: RList) =
  match !list with
    | None -> 
        list := Some { data = item ; next = ref None}
    | Some { data = d } when comp (item, d) ->
        let newCell = Some { data = item; next = ref (!list) } in list := newCell
    | Some { next = tail } ->
        insert comp item tail




        

      
