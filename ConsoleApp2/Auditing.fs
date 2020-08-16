module Auditing
open System
open Domain
open Operations
open FileRepository

let formatMessage (account:Account) transaction = 
    sprintf "Account %s\n   Balance: %f \n  Latest transaction %s" (account.Id.ToString()) account.Balance
        (sprintf "%M$ %s %s" transaction.Amount (transaction.Operation.ToString()) (transaction.Timestamp.ToString "dd/MM/yyyy"))

let logToConsole (account:Account) transaction = 
    Console.WriteLine (formatMessage account transaction)

let auditAs auditMethod operation amount (account:Account) = 
    let result = operation amount account
    if fst result = account then
        auditMethod <|| result
    else
        auditMethod <|| result
    fst result

let consoleAudit = auditAs logToConsole 
let withdrawWithConsole = consoleAudit withdraw 
let depositWithConsole = consoleAudit deposit 

let fileAudit = auditAs writeTransaction 
let withdrawWithFile = fileAudit withdraw 
let depositWithFile = fileAudit deposit 