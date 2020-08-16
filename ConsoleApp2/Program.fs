open System
open Domain
open Operations
open Auditing

let consoleAudit = auditAs logToConsole 
let withdrawWithConsole = consoleAudit withdraw 
let depositWithConsole = consoleAudit deposit 

[<EntryPoint>]
let main argv =
    let mutable account:Account = 
        {
            Balance = 0m
            Id = Guid()
            Owner =
            {
                Name = ""
            }
        }
    let amount = 10m
    while true do
        Console.WriteLine "Input Key:"
        let chosenAction = Console.ReadKey().KeyChar
        match chosenAction with
        | 'd' -> account <- account |> depositWithConsole amount
        | 'w' -> account <- account |> withdrawWithConsole amount
        | 'x' -> Environment.Exit 0
        | _ -> ()
    1
