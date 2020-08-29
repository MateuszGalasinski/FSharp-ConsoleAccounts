let getCreditLimit customer = 
    match customer with
    | "medium", 1 -> 500
    | "good", 0 | "good", 1 -> 750
    | "good", years when years < 3 -> years*750
    | "good", _ -> 2000
    | _, years when years < 3 -> 250
getCreditLimit ("good", 3)

type Customer =
    {
        Balance: int
        Name: string
    }
let getStatus (customer:Customer) = 
    match customer with 
    | { Name = name } when name = "Jason" -> sprintf "Good job!"
    | _ -> sprintf "Who are you?"
let (customer:Customer) = { Balance = 0; Name = "asd" }
let customers = [| customer; customer; customer|]
getStatus { Balance = 0; Name = "Jason"}

let rec length list = 
    match list with
    | [] -> 0
    | _ :: tail -> 1 + length tail