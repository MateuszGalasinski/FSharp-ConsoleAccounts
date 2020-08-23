module FileRepository
open Domain
open System.IO
open System

let private delimiter = "***"

let private serialize transaction = 
    sprintf "%O%s%s%s%M"
        (transaction.Timestamp.ToString "dd/MM/yyyy") delimiter
        (transaction.Operation.ToString()) delimiter
        transaction.Amount
    
let private deserialize (text:string) (id:string) = 
    text.Split(delimiter)
    |> fun split -> {
        Id = Guid.Parse id
        Timestamp = DateTime.Parse split.[0]
        Operation = Operation.Parse(split.[1])
        Amount = Decimal.Parse split.[2]
        }
            
let private accountsPath = 
    (Directory.CreateDirectory @"accounts" ).FullName
                
let private accountDirectoryDelimiter = '_'

let private findAnyAccountDirectory owner = 
    if (Directory.Exists accountsPath) then
        let dirs = Directory.EnumerateDirectories(accountsPath, sprintf "%s%c*" owner accountDirectoryDelimiter)
        if dirs |> Seq.isEmpty then ""
        else
            DirectoryInfo(Seq.head dirs).FullName
    else
        ""

let findTransactionsOnDisk owner = 
    let accountDir = findAnyAccountDirectory owner
    if accountDir = "" then Guid.NewGuid(), Seq.empty
    else
        Guid(DirectoryInfo(accountDir).Name.Split(accountDirectoryDelimiter).[1]),
        DirectoryInfo(accountDir).EnumerateFiles "*.txt"
        |> Seq.map (fun fileInfo -> deserialize (fileInfo.OpenText().ReadToEnd()) (fileInfo.Name.Split('.').[0]))


let writeTransaction (account:Account) transaction = 
    let dirPath = Path.Combine(accountsPath, sprintf "%s%c%s"  account.Owner.Name accountDirectoryDelimiter (account.Id.ToString()))
    Directory.CreateDirectory dirPath |> ignore
    File.WriteAllText(
        Path.ChangeExtension(
            (dirPath, transaction.Id.ToString()) |> Path.Combine,
            "txt"),
        serialize transaction)
    printf "Written transaction log to: %s" (DirectoryInfo(dirPath).FullName)