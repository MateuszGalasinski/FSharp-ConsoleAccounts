module FileRepository
open Domain
open System.IO

let serialize transaction = 
    sprintf "%O***%s***%M"
        (transaction.Timestamp.ToString "dd/MM/yyyy")
        (transaction.Operation.ToString())
        transaction.Amount

let writeTransaction (account:Account) (transaction:Transaction) = 
    let dirPath = Path.Combine("C:\\temp\\learnfs\\capstone2", account.Id.ToString() |> sprintf "%s-%s" account.Owner.Name)
    Directory.CreateDirectory dirPath |> ignore
    (
        Path.ChangeExtension(
            (dirPath, transaction.Id.ToString()) |> Path.Combine,
            "txt"),
        serialize transaction
    )|> File.WriteAllText