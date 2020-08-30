module Auditing
open System
open Domain
open FileRepository

let formatMessage accountId transaction = 
    sprintf "Account %s\n  Latest transaction %s" (accountId.ToString())
        (sprintf "%M$ %s %s" transaction.Amount (transaction.Operation.ToString()) (transaction.Timestamp.ToString "dd/MM/yyyy"))

let logToConsole (account:Account) transaction = 
    Console.WriteLine (formatMessage account.Id transaction)

let auditAs auditMethod action operationType amount account = 
    let updatedAccount = action amount account
    let transaction = {
        Id = Guid.NewGuid()
        Amount = amount
        Operation = operationType
        Timestamp = DateTime.Now
    }
    auditMethod updatedAccount transaction
    updatedAccount

let allLogger (account:RatedAccount) (transaction:Transaction) = 
    [logToConsole; writeTransaction]
    |> List.iter(fun logger -> logger account.Value transaction)    