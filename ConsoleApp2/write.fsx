#load "Domain.fs" 
#load "Operations.fs" 
#load "Auditing.fs" 
open System.IO
open System
open Domain
open Operations
open Auditing


let withdrawWithConsole = auditAs logToConsole withdraw
let depositWithConsole = auditAs logToConsole deposit

account
|> withdrawWithConsole 14m
|> depositWithConsole 20m

//let writeToTomorrow = writeToFile (DateTime.UtcNow.Date.AddDays 1.0)

//Directory.GetCurrentDirectory()
//|> Directory.GetCreationTime
//|> Console.WriteLine