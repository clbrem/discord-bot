open System.IO
open System.Threading.Tasks
open DSharpPlus.EventArgs
open Microsoft.Extensions.Configuration
open DSharpPlus
open FiascoBot
open Microsoft.Extensions.Logging
open DSharpPlus.SlashCommands
open DSharpPlus.SlashCommands.EventArgs

let event = EventId(42, "Bot x1")
[<CLIMutable>]
type Secrets =
    { token: string }
let config() =
    ConfigurationBuilder()
        .AddUserSecrets<Secrets>(false)
        .Build()
let execute =
    fun (client: SlashCommandsExtension) (item: SlashCommandExecutedEventArgs) ->
        client.Client.Logger.LogInformation($"{item.Context.Member.Id}"); Task.CompletedTask
let ready (sender: DiscordClient) _ =
    sender.Logger.LogInformation(event, "Starting Up!")
    Task.CompletedTask
let guild (sender: DiscordClient) (e: GuildCreateEventArgs) =
    sender.Logger.LogInformation(event, $"Guild Available {e.Guild.Name}")
    Task.CompletedTask
let error (sender: DiscordClient) (e: SocketErrorEventArgs) =
    sender.Logger.LogError(event, $"Error: {e.Exception.Message}")
    Task.CompletedTask
let clientError (sender: DiscordClient) (e: ClientErrorEventArgs) =
    sender.Logger.LogError(event, $"Error: {e.Exception.Message}")
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
        client.add_SocketErrored(error)
        client.add_ClientErrored(clientError)

        let commands = client.UseSlashCommands()
        do commands.RegisterCommands<FiascoBot>()
        commands.add_SlashCommandExecuted(execute)
        do! client.ConnectAsync()        
        do! Task.Delay(-1)
    }
    
[<EntryPoint>]
let main args =
    (program args).GetAwaiter().GetResult()
    1
    
    
    