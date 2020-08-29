#load "../Domain.fs" 
#load "Operations.fs" 
#load "FileRepository.fs" 
#load "Auditing.fs" 
open System
open Domain
open Operations
open Auditing
open FileRepository

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


let loadAccount owner accountId transactions = 
    let initAccount : Account = { Id = accountId
                                  Balance = 0m
                                  Owner = owner}
    transactions
    |> Seq.sortBy (fun t -> t.Timestamp)
    |> Seq.fold (fun account txn -> 
        match txn.Operation with 
        | Operation.Deposit -> deposit txn.Amount account |> fst
        | Operation.Withdraw -> withdraw txn.Amount account |> fst
        | Operation.Exit -> account
        | Operation.Fail -> account
        ) initAccount

loadAccount { Name = "ASD" } (Guid.NewGuid()) [
    {
        Id = Guid.NewGuid()
        Amount = 12.3123m
        Operation = Operation.Deposit
        Timestamp = DateTime.Now
    };
    {
        Id = Guid.NewGuid()
        Amount = 9.2m
        Operation = Operation.Exit
        Timestamp = DateTime.Now
    };
    {
        Id = Guid.NewGuid()
        Amount = 22.2m
        Operation = Operation.Withdraw
        Timestamp = DateTime.Now
    };
]

#load "FileRepository.fs" 
open FileRepository
snd (findTransactionsOnDisk "MG") |> Seq.iter (fun t -> printf "%s" (t.ToString()))