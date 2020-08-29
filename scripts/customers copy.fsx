type CustomerId = CustomerId of string
type Email = Email of string
type Telephone = Telephone of string
type Address = Address of string
type ContactDetails = 
    | Address of string
    | Telephone of string
    | Email of string
type Customer = 
    {
        CustomerId: CustomerId
        PrimaryContactDetails: ContactDetails
        SecondaryContactDetails: Option<ContactDetails>
    }
type GenuineCustomer = GenuineCustomer of Customer


let createCustomer customerId contactDetails secondaryContactDetails =
    { 
        CustomerId = customerId
        PrimaryContactDetails = contactDetails
        SecondaryContactDetails = secondaryContactDetails
    }
    
let checkIfGenuineCustomer customer = 
    match customer.PrimaryContactDetails with
    | Email e when e.EndsWith "kislev.com" -> Some (GenuineCustomer customer)
    | Telephone _ | Address _ -> Some (GenuineCustomer customer)
    | Email _ -> None

let welcomeMessage (GenuineCustomer customer) =
    printfn "Greetings, %A" customer.CustomerId

[
    (createCustomer
        (CustomerId "C-123")
        (Email "nicki@myemail.com")
        None);
    (createCustomer
        (CustomerId "Jurij")
        (Email "nicki@mkislev.com")
        (Some (Address "Street 11")))
] |> Seq.map checkIfGenuineCustomer
  |> Seq.iter (Option.iter welcomeMessage)
  
    