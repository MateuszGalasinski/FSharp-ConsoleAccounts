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
type Operation = Deposit | Withdraw | Exit | Fail
type Transaction =
    {
        Id: Guid
        Amount: decimal
        Operation: Operation
        Timestamp: DateTime
    }
