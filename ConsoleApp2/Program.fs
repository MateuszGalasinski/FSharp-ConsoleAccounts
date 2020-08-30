open System
open Domain
open Operations
open Auditing
open FileRepository
let sideEffect f g = (f g); g

type Command = AccountOperation of Operation | Exit

let withdrawWithAudit = auditAs allLogger withdraw 
let depositWithAudit = auditAs allLogger deposit 

let displayAccount (account:RatedAccount) = printfn "Account %s\n  Balance: %f\n" (account.Value.Id.ToString()) account.Value.Balance

let tryParseCommand char = 
    match char with 
    | 'd' -> Some (AccountOperation Deposit)
    | 'w' -> Some (AccountOperation Withdraw)
    | 'x' -> Some Exit
    | _ -> None

let tryGetAmount operation = 
    let askForAmount _ = 
        Console.Clear()
        Console.WriteLine "\nHow much: "
        Decimal.TryParse (Console.ReadLine())
    Seq.initInfinite askForAmount
    |> Seq.choose(fun result ->
        match result with
        | true, amount when amount < 0m -> None
        | true, amount -> Some (operation, amount)
        | false, _ -> None)
    |> Seq.head

let processCommand (currentAccount:RatedAccount) (command, amount) =
    let afterAccount = match command with
        | Deposit -> currentAccount |> depositWithAudit command amount
        | Withdraw -> 
            match currentAccount with
            | InCredit account -> account |> withdrawWithAudit command amount
            | Overdrawn _ ->
                printfn "Sorry, you have to pey the debts first!"
                currentAccount
    displayAccount afterAccount
    afterAccount


[<EntryPoint>]
let main argv =
    let openingAccount = 
        Console.WriteLine "Your name, sir?\n"
        let owner = { Name = Console.ReadLine()}
        owner
        |> tryFindTransactionsOnDisk 
        |> Option.map loadAccount
        |> Option.defaultValue (InCredit(CreditAccount {
                Balance = 0m
                Id = Guid.NewGuid()
                Owner = owner
            }))
        |> sideEffect displayAccount

    Seq.initInfinite(fun _ ->
        Console.WriteLine()
        Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
        Console.ReadKey().KeyChar
    )
    |> Seq.choose tryParseCommand
    |> Seq.takeWhile ((<>) Exit)
    |> Seq.choose(fun cmd -> 
        match cmd with
        | Exit -> None
        | AccountOperation o -> Some o)
    |> Seq.map tryGetAmount
    |> Seq.fold processCommand openingAccount
    |> ignore
    0