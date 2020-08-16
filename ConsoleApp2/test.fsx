#load "Domain.fs" 
#load "Operations.fs" 
#load "Auditing.fs" 
open System.IO
open System
open Domain
open Operations
open Auditing

let consoleAudit = auditAs logToConsole 
let withdrawWithConsole = consoleAudit withdraw 
let depositWithConsole = consoleAudit deposit 

let openingAccount = 
    { Owner = {Name = "MG"}; Balance = 0m; Id = Guid.Empty}

let isValidCommand char =
    [ 'd'; 'w'; 'x']
    |> Seq.contains char

let isStopCommand command =
    command = 'x'

let generateAmount command =
    match command with
    | 'd' -> ('d', 50m)
    | 'w' -> ('w', 25m)
    | _ -> ('x', 0m)

let processCommand currentAccount (command, amount) = 
    match command with
    | 'd' -> currentAccount |> depositWithConsole amount
    | 'w' -> currentAccount |> withdrawWithConsole amount
    | 'x' -> Environment.Exit 0; account
    | _ -> currentAccount

let account = 
    let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x']

    commands
    |> Seq.filter isValidCommand
    |> Seq.takeWhile (not << isStopCommand)
    |> Seq.map generateAmount
    |> Seq.fold processCommand openingAccount