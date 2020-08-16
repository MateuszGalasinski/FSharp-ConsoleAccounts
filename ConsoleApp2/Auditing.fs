module Auditing
open System.IO
open System
open Domain
open Operations

let formatMessage account message = 
    sprintf "Account %s|balance:%f : %s" (account.Id.ToString()) account.Balance message

let logToFile (account:Account) message = 
    let dirPath = Path.Combine("C:\temp\learnfs\capstone2", account.Owner.Name)
    Directory.CreateDirectory dirPath |> ignore
    File.WriteAllText(
        Path.ChangeExtension(
            Path.Combine(dirPath, account.Id.ToString()),
            "txt"),
        formatMessage account message)

let logToConsole (account:Account) message = 
    Console.WriteLine (formatMessage account message)
    
let account = 
    {
        Id = Guid()
        Balance = decimal 46.0
        Owner = 
            {
                Name = "Test"
            }
    }
let auditAs auditMethod operation (amount:decimal) (account:Account) = 
    let result = operation amount account
    if result = account then
        auditMethod result ("Operation was rejected :(")
    else
        auditMethod result (sprintf "Operation %s success! Amount %M" (operation.GetType().Name) amount)
    result
