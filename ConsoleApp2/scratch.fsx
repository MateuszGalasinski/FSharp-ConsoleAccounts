#load "Domain.fs"
open Domain
open System

let text = "08/30/2020 18:39:06***Deposit***123.23"
text.Split("***")
|> fun split -> {
        Id = Guid.Parse "0e85116e-6555-4082-99c7-bcbf79d18d7e"
        Timestamp = DateTime.Parse(split.[0], Globalization.CultureInfo.InvariantCulture)
        Operation = Operation.Parse(split.[1])
        Amount = Decimal.Parse(split.[2], Globalization.CultureInfo.InvariantCulture)
    }