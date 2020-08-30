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
type CreditAccount = CreditAccount of Account
type RatedAccount = 
    | InCredit of CreditAccount
    | Overdrawn of Account
    member this.Value =
        match this with
        | InCredit (CreditAccount account) -> account
        | Overdrawn account -> account   

type Operation = Deposit | Withdraw with
    static member Parse (s:string) = 
        match s with
        | "Deposit" -> Deposit
        | "Withdraw" -> Withdraw
        | _ -> failwith (sprintf "Cannot parse %s to Operation union type." s)
type Transaction =
    {
        Id: Guid
        Amount: decimal
        Operation: Operation
        Timestamp: DateTime
    }
