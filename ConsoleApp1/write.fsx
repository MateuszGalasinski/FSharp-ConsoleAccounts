open System.IO
open System

type Customer =
    {
        Name: string
    }

type Account = 
    {
        Id: Guid
        Balance: decimal
        Owner: Customer
    }
let account = 
    {
        Id = Guid()
        Balance = decimal 0.0
        Owner = 
            {
                Name = ""
            }
    }

let deposit amount account = 
    {   account with
            Balance = account.Balance + amount
    }

let withdraw amount account = 
    if account.Balance >= amount then
        {   account with
                Balance = account.Balance - amount
        }
    else 
        account

let formatMessage account message = 
    sprintf "Account %s: %s" (account.Id.ToString()) message

let logToFile (account:Account) message = 
    let dirPath = Path.Combine("C:\temp\learnfs\capstone2", account.Owner.Name)
    Directory.CreateDirectory dirPath |> ignore
    File.WriteAllText(
        Path.ChangeExtension(
            Path.Combine(dirPath, account.Id.ToString()),
            "txt"),
        formatMessage account message)

let logToConsole (account:Account) message = 
    formatMessage account message |> Console.WriteLine

//let writeToTomorrow = writeToFile (DateTime.UtcNow.Date.AddDays 1.0)

//Directory.GetCurrentDirectory()
//|> Directory.GetCreationTime
//|> Console.WriteLine
