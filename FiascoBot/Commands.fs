namespace FiascoBot
open System
open DSharpPlus.CommandsNext
open DSharpPlus.CommandsNext.Attributes
open DSharpPlus.Entities
open System.Threading.Tasks

type FiascoBot() =    
  inherit BaseCommandModule()   
    [<Command "roll">]
  let roll(ctx: CommandContext): Task =
        task {
            do! ctx.TriggerTypingAsync() 
            let rng = Random()
            let emoji = DiscordEmoji.FromName(ctx.Client, ":game_die:").ToString()
            let! _ =
                rng.Next(1,7)
                |> sprintf "%s rolled %i" emoji
                |> ctx.RespondAsync
            return ()
        }  