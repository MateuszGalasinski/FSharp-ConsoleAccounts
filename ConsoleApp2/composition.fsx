[<Measure>] type Gb

type Disk = 
| HardDisk of RPM:int * Platters:int
| SolidState
| MMC of Pins:int

let aHardDisk = HardDisk(RPM=123, Platters=7)
let disk2 = HardDisk(300, 8)
(250, 7) |> HardDisk
MMC 5
let ssd = SolidState

let seek disk = 
    match disk with
    | HardDisk _ -> "Loud"
    | MMC _ -> "Slow"
    | SolidState -> "Your data, sir."
seek ssd