module Operations
open Domain

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