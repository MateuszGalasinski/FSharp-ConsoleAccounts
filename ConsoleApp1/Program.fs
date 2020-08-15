// Learn more about F# at http://fsharp.org

open System

[<EntryPoint>]
let main argv =
    let items = argv.Length
    printfn "%d %A" items argv
    0
