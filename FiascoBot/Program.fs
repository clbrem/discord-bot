open System
open System.Threading.Tasks
open Discord
open Discord.WebSocket
open Microsoft.Extensions.Configuration
[<CLIMutable>]
type Secrets =
    { token: string }
let config() =
    ConfigurationBuilder()
        .AddUserSecrets<Secrets>(false)
        .Build()
let log (msg: LogMessage) =
    printfn $"{msg.ToString()}"
    Task.CompletedTask        
let program _ =
    let config = config()
    let token = config["token"]
    
    task {
        use client = new DiscordSocketClient()
        do client.add_Log(log)
        do! client.LoginAsync(TokenType.Bot, token)
        do! client.StartAsync()
        do! Task.Delay(-1)   
    }
    
    




[<EntryPoint>]
let main args =
    (program args).GetAwaiter().GetResult()
    0
    
    
    