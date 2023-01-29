namespace FiascoBot
open System
open DSharpPlus.SlashCommands
open DSharpPlus.Entities
open System.Threading.Tasks

type FiascoBot() =    
  inherit ApplicationCommandModule()

           
  [<SlashCommand("roll", "Roll the dice!")>]
  member _.Roll(
      ctx: InteractionContext,
      [<Option("dice", "Append a list of dice to roll: d6 d20 wd4 bd1")>]
      dice: string
      ): Task =
        task {            
            let rng = Random()
            let emoji =
                function
                    |Some White ->                        
                        DiscordEmoji.FromName(ctx.Client, ":black_square_button:").ToString()
                    | Some Black ->
                        DiscordEmoji.FromName(ctx.Client, ":white_square_button:").ToString()
                    | None ->
                        DiscordEmoji.FromName(ctx.Client, ":game_die:").ToString()                
            let d = match dice with Dice.Dice a -> a
            
            let! _ =
                Dice.rollMany rng d                
                |> List.map (fun d ->(sprintf "%s %i" (emoji d.color) d.rolled.Value))   
                |> String.concat " "
                |> ctx.CreateResponseAsync
            return ()
        }  