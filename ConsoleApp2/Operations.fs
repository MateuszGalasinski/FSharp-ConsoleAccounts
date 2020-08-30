module Operations
open Domain

let classifyAccount (account:Account) = 
    if account.Balance >= 0m then
        InCredit(CreditAccount account)
    else
        Overdrawn account

let deposit amount account = 
    let account = 
        match account with 
        | InCredit (CreditAccount account) -> account
        | Overdrawn account -> account
    { account with Balance = account.Balance + amount }
    |> classifyAccount

let withdraw amount (CreditAccount account) = 
    { account with Balance = account.Balance - amount } 
    |> classifyAccount

let loadAccount (owner, accountId, transactions) = 
    let initAccount = InCredit(CreditAccount { 
        Id = accountId
        Balance = 0m
        Owner = owner})
    transactions
    |> Seq.sortBy (fun t -> t.Timestamp)
    |> Seq.fold (fun account txn -> 
        match txn.Operation, account with 
        | Deposit, _ -> deposit txn.Amount account
        | Withdraw, InCredit account -> withdraw txn.Amount account
        | Withdraw, Overdrawn _ -> account
        ) initAccount
