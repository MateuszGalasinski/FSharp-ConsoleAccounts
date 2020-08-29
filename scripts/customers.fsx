type CustomerId = CustomerId of string
type Email = Email of string
type Telephone = Telephone of string
type Address = Address of string
type Customer = 
    {
        CustomerId: CustomerId
        Email: Email
        Telephone: Telephone
        Address: Address
    }


let createCustomer customerId email telephone address =
    { 
        CustomerId = customerId
        Email = email
        Telephone = telephone
        Address = address
    }

createCustomer 
    (CustomerId "C-123")
    (Email "nicki@myemail.com")
    (Telephone "029-293-23")
    (Address "1 The Street")
    