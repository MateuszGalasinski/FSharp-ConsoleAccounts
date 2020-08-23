namespace Domain
open System

type Customer =
    {
        Name: string
    }
type Account = 
    {
        Id: Guid
        Balance: decimal
        Owner: Customer
    }
type Operation = Deposit | Withdraw | Exit | Fail with
    static member Parse (s:string) = 
        match s with
        | "Deposit" -> Deposit
        | "Withdraw" -> Withdraw
        | "Exit" -> Exit
        | "Fail" -> Fail
        | _ -> failwith (sprintf "Cannot parse %s to Operation union type." s)
type Transaction =
    {
        Id: Guid
        Amount: decimal
        Operation: Operation
        Timestamp: DateTime
    }
