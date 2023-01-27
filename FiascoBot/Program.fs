open System.Threading.Tasks
open DSharpPlus.EventArgs
open Microsoft.Extensions.Configuration
open DSharpPlus
open DSharpPlus.CommandsNext
open FiascoBot
open Microsoft.Extensions.Logging
let event = EventId(42, "Bot x1")
[<CLIMutable>]
type Secrets =
    { token: string }
let config() =
    ConfigurationBuilder()
        .AddUserSecrets<Secrets>(false)
        .Build()

let ready (sender: DiscordClient) _ =
    sender.Logger.LogInformation(event, "Starting Up!")
    Task.CompletedTask
let guild (sender: DiscordClient) (e: GuildCreateEventArgs) =
    sender.Logger.LogInformation(event, $"Guild Available {e.Guild.Name}")
    Task.CompletedTask
let program _ =
    let config = config()
    let token = config["token"]
    let discord = DiscordConfiguration(
        Token = token,
        TokenType = TokenType.Bot,
        Intents = DiscordIntents.AllUnprivileged,
        AutoReconnect = true,
        MinimumLogLevel = LogLevel.Information
    )    
    task {
        use client = new DiscordClient(discord)
        client.add_Ready(ready)
        client.add_GuildAvailable(guild)
        let commandsConfig = CommandsNextConfiguration (
            StringPrefixes = ["/"],
            EnableMentionPrefix = true,
            EnableDms = true
            )                
        let commands = client.UseCommandsNext(commandsConfig)
        do commands.RegisterCommands<FiascoBot>()
        do! client.ConnectAsync()        
        do! Task.Delay(-1)
    }
    
[<EntryPoint>]
let main args =
    (program args).GetAwaiter().GetResult()
    1
    
    
    