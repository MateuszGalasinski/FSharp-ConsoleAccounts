open System.Net
open System.Windows.Forms
open System


type Webpage = 
    {
        Uri: Uri
        Content: string
        Metadata: string
    }
let check = 
    let page = 
        {
            Uri = Uri "http://fsharp.org"
            Content = "asd"
            Metadata = String.Empty
        }
    let webpage =
        {
            page with
                Metadata = "qwdqf"
        }
    let page =
        {
            webpage with
                Metadata = "qwdqf"
        }
    1
let browser = 
    let downloadPage uri = (new WebClient()).DownloadString(Uri uri)
    new WebBrowser(ScriptErrorsSuppressed = true,
                   Dock = DockStyle.Fill,
                   DocumentText = downloadPage "http://fsharp.org")
let form = new Form(Text = "Hello")
form.Controls.Add browser
form.Show()