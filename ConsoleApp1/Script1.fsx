open System.IO
open System.Text
open System

let greetPerson name age = 
    sprintf "Hello, %s. You are %d years old" name age
let greeting = greetPerson "Fred" 25

let countWords (text:string) = 
    text.Split ' '
    |> Array.length
countWords "asd qw sddsv1231 -o-awef"
let asd (text:string) = 
    text.Split ' '
    |> Array.length
    |> fun x -> File.WriteAllText("file", sprintf "%i %s" x text)
asd "qwe asd sdf";;