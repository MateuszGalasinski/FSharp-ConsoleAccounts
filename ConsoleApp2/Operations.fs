module Operations
open Domain

let deposit amount account = 
    {   account with
            Balance = account.Balance + amount
    }, Deposit

let withdraw amount account = 
    if account.Balance >= amount then
        {   account with
                Balance = account.Balance - amount
        }, Withdraw
    else 
        account, Fail

let loadAccount owner accountId transactions = 
    let initAccount : Account = { Id = accountId
                                  Balance = 0m
                                  Owner = owner}
    transactions
    |> Seq.sortBy (fun t -> t.Timestamp)
    |> Seq.fold (fun account txn -> 
        match txn.Operation with 
        | Deposit -> deposit txn.Amount account |> fst
        | Withdraw -> withdraw txn.Amount account |> fst
        | Exit -> account
        | Fail -> account
        ) initAccount
