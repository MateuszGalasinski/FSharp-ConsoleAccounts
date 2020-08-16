open System
open Domain
open Operations
open Auditing

let depositAudit = depositWithFile
let withdrawAudit = withdrawWithFile

let isValidCommand char =
    [ 'd'; 'w'; 'x']
    |> Seq.contains char

let getAmount (command:char) = 
    if ['d'; 'w'] |> Seq.contains command then
        Console.WriteLine "\nSpecify amount: "
        (command, Decimal.Parse (Console.ReadLine()))
    else
        (command, 0m)

let processCommand currentAccount (command, amount) = 
    match command with
    | 'd' -> currentAccount |> depositAudit amount
    | 'w' -> currentAccount |> withdrawAudit amount
    | 'x' -> Environment.Exit 0; currentAccount
    | _ -> currentAccount

[<EntryPoint>]
let main argv =
    let openingAccount = 
        { Owner = {Name = "MG"}; Balance = 0m; Id = Guid.Empty}

    seq {
        while true do
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            yield Console.ReadKey().KeyChar
    }
    |> Seq.filter isValidCommand
    |> Seq.takeWhile (fun cmd -> cmd <> 'x')
    |> Seq.map getAmount
    |> Seq.fold processCommand openingAccount
    |> ignore
    0