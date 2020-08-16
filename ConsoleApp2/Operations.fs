module Operations
open Domain
open System

let private createTransaction amount operation = 
    {
        Id = Guid.NewGuid()
        Amount = amount
        Operation = operation
        Timestamp = DateTime.Now
    }

let deposit amount account = 
    {   account with
            Balance = account.Balance + amount
    },
    createTransaction amount Operation.Deposit

let withdraw amount account = 
    if account.Balance >= amount then
        {   account with
                Balance = account.Balance - amount
        }, 
        createTransaction amount Operation.Withdraw
    else 
        account,
        createTransaction 0m Operation.Fail