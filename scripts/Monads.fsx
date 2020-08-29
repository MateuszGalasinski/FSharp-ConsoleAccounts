[<Measure>] type Years
// { "Drivers" :
// [ { "Name" : "Fred Smith", "SafetyScore": 550, "YearPassed" : 1980 },
// { "Name" : "Jane Dunn", "YearPassed" : 1980 } ] }
type Driver =
    {
        Name: string
        SafetyScore: Option<int>
        YearPassed: int<Years>
    }
let calculateAnnualPremiumUsd driver =
    match driver.SafetyScore with
    | Some 0 -> 250
    | Some score when score < 0 -> 400
    | Some score when score > 0 -> 150
    | None -> 300
[
    {Name = "Fred"; SafetyScore = Some 550; YearPassed = 1980<Years>};
    {Name = "Jane"; SafetyScore = None; YearPassed = 1980<Years>};
] |> Seq.map calculateAnnualPremiumUsd

let drivers = 
    [
        {Name = "Fred"; SafetyScore = Some 550; YearPassed = 1980<Years>};
        {Name = "Jane"; SafetyScore = None; YearPassed = 1980<Years>};
    ]
let tryFindCutomer id = if id = 10 then Some drivers.[0] else None
let getSafetyScore customer = customer.SafetyScore
let score = tryFindCutomer 10 |> Option.bind getSafetyScore

drivers.[0].SafetyScore |> Option.filter(fun s -> s > 300 )

let isHighScore score = score > 300
let driverWithHighSafetyScore driver = (driver.SafetyScore |> Option.filter isHighScore).IsSome
drivers |> Seq.filter driverWithHighSafetyScore