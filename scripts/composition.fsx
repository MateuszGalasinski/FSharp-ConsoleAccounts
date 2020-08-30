[<Measure>] type Gb

type Disk = 
| HardDisk of RPM:int * Platters:int
| SSD
| MMC of Pins:int

let aHardDisk = HardDisk(RPM=123, Platters=7)
let disk2 = HardDisk(300, 8)
(250, 7) |> HardDisk
MMC 5
let ssd = SSD

// let seek disk = 
//     match disk with
//     | HardDisk _ -> "Loud"
//     | MMC _ -> "Slow"
//     | SolidState -> "Your data, sir."
// seek ssd
let describe disk = 
    match disk with 
    | SSD -> "SSD"
    | MMC 1 -> "Single pin MMC"
    | MMC pins when pins < 5 -> sprintf "MMC with few"
    | MMC pins -> sprintf "MMC with %i" pins
    | HardDisk(5400, _) -> "Slow hard disk"
    | HardDisk(_, 7) -> "7 spindles"
    | HardDisk _ -> "A hard disk, indeed"
[SSD; HardDisk(5400,2); HardDisk(5400,7); HardDisk(123,7); MMC(1); MMC(3); MMC(6)]
|> Seq.iter (describe >> printf "%s\n")