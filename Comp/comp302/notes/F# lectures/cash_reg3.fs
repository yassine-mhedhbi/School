type ArticleCode = string
type ArticleName = string
type NumItems    = int
type Price       = int

type Register    = Map<ArticleCode, ArticleName * Price>

type Info        = NumItems * ArticleName * Price
type InfoSeq     = Info list
type Bill        = InfoSeq * Price

type Item        = ArticleCode * NumItems
type Purchase    = Item list

let rec makeBill (reg: Register) (pur: Purchase) = 
  match pur with
  | []                    -> ([], 0)
  | (acode, nitems)::more -> 
       let (aname, aprice) = Map.find acode reg
       let itemprice = nitems * aprice
       let (infos,sumbill) = makeBill reg more
       ((nitems,aname,itemprice)::infos, itemprice + sumbill)

let reg1 = Map.ofList [("ac1", ("Tylenol", 5)); ("ac2",("Vitamin D", 7)); ("ac3", ("Statin", 35)); ("ac4", ("Nyquil", 12))]

let myPurchase = [("ac3",2);("ac1",3);("ac4",1);("ac2",3)]


