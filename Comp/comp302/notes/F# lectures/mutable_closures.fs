let mutable a = 0
let flip0 () = (a <- (1 - a)); (printf "%i\n" a)

let flop() = let mutable a = 0 in (a <- (1 - a)); (printf "%i\n" a)

type counter = { mutable i: int}

let flip =
  let c:counter = { i = 0 }
  fun () -> (c.i <- 1 - c.i); (printf "%i\n" c.i)


let makeFlipper = fun () ->
  let c:counter = { i = 0 }
  fun () -> (c.i <- 1 - c.i); (printf "%i\n" c.i)

(* Very limited bank account, one can only withdraw. *)
let withDraw = 
  let mutable balance = 100 
  fun amount -> if (amount < balance) 
                then 
                  balance <- balance - amount; (printfn "Balance is %i" balance)
                else
                  printfn "Insufficient funds."

(* Datatype of bank account transactions.*)
type transaction = Withdraw of int | Deposit of int | CheckBalance

(* Bank account generator. *)
let makeAccount(opening_balance: int) =
    let balance = ref opening_balance
    fun (t: transaction) ->
      match t with
        | Withdraw(m) ->  if (!balance > m)
                          then
                            balance := !balance - m
                            printfn "Balance is %i" !balance
                          else
                            printfn "Insufficient funds."
        | Deposit(m) -> (balance := !balance + m; (printf "Balance is %i\n" !balance))
        | CheckBalance -> (printf "Balance is %i\n" !balance)


let Alice = make_account(1000)

let Benoit = make_account(500)

let make_trojan(opening_balance: int) =
    let balance = ref opening_balance
    fun (t: transaction) ->
      match t with
        | Withdraw(m) ->  if (!balance > m)
                          then
                            balance := !balance - m
                            printfn "Balance is %i" !balance
                          else
                            printfn "Insufficient funds."
        | Deposit(m) -> (balance := !balance + m; (printf "Balance is %i\n" !balance))
        | CheckBalance -> (printf "Balance is %i\n" !balance)


(* Monitoring *)
type 'a tagged = Query | Normal of 'a
type 'b answers = Numcalls of int | Ans of 'b

let makeMonitored f =
    let c = ref 0
    fun x ->
      match x with
        | Query -> (Numcalls !c)
        | (Normal y) -> ( c := !c + 1; (Ans (f y)))

(* To avoid the value restriction we have to use a non-polymorphic version. *)
let monLength = makeMonitored (fun (l : int list) -> (List.length l))

let inc n = n + 1

let moninc = makeMonitored(inc)

let taglist l = List.map (fun x -> Normal(x)) l







