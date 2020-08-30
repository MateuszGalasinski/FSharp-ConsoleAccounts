module FileRepository
open Domain
open System.IO
open System

[<AutoOpen>]
module Serialization =
    let private delimiter = "***"
    let serialize transaction = 
        sprintf "%O%s%s%s%s"
            (transaction.Timestamp.ToString(Globalization.CultureInfo.InvariantCulture)) delimiter
            (transaction.Operation.ToString()) delimiter
            (transaction.Amount.ToString(Globalization.CultureInfo.InvariantCulture))
    let deserialize (id:string) (text:string) = 
        text.Split(delimiter)
        |> fun split -> {
                Id = Guid.Parse id
                Timestamp = DateTime.Parse(split.[0], Globalization.CultureInfo.InvariantCulture)
                Operation = Operation.Parse(split.[1])
                Amount = Decimal.Parse(split.[2], Globalization.CultureInfo.InvariantCulture)
            }
            
let private accountsPath = 
    (Directory.CreateDirectory @"accounts" ).FullName

let private accountDirectoryDelimiter = '_'

let private tryFindAnyAccountDirectory owner = 
    match (Directory.Exists accountsPath) with
    | true ->
        let dirs = Directory.EnumerateDirectories(accountsPath, sprintf "%s%c*" owner.Name accountDirectoryDelimiter)
        match (dirs |> Seq.toList) with
        | [] -> None
        | dir :: _ -> Some (DirectoryInfo(dir).FullName)
    | false -> None

let tryFindTransactionsOnDisk owner =
    let deserializeFile (fileInfo:FileInfo) = fileInfo.OpenText().ReadToEnd() |> deserialize (fileInfo.Name.Split('.').[0])
    let deserializeDirectory path = 
        owner,
        Guid(DirectoryInfo(path).Name.Split(accountDirectoryDelimiter).[1]),
        DirectoryInfo(path).EnumerateFiles "*.txt"|> Seq.map deserializeFile 
    tryFindAnyAccountDirectory owner |> Option.map deserializeDirectory

let writeTransaction account transaction = 
    let dirPath = Path.Combine(accountsPath, sprintf "%s%c%s"  account.Owner.Name accountDirectoryDelimiter (account.Id.ToString()))
    Directory.CreateDirectory dirPath |> ignore
    File.WriteAllText(
        Path.ChangeExtension(
            (dirPath, transaction.Id.ToString()) |> Path.Combine,
            "txt"),
        serialize transaction)