namespace FiascoBot
open System
open System.Text.RegularExpressions

type DiceColor =
    | White
    | Black
type Dice =
    {
      sides: int
      color: DiceColor option
      rolled : int option
     }

module Dice =  
  let diceRegex = Regex("d(\d\d?)", RegexOptions.IgnoreCase)
  let colorRegex = Regex("([wb])d", RegexOptions.IgnoreCase)
  let countRegex = Regex("^\d+")
  let (|Matched|) (a: Group) =
      a.Value
  let (|Sides|_|) input =
      match diceRegex.Match(input).Groups |> List.ofSeq with
      | _ :: b :: _ -> System.Convert.ToInt32 b.Value |> Some
      | _ -> None
  let (|Color|_|) input =
      match colorRegex.Match(input).Groups |> List.ofSeq with
      | _ :: Matched "w" :: _ -> Some White
      | _ :: Matched "b" :: _ -> Some Black
      | _ -> None
  let (|Count|) input =
      let m = countRegex.Match(input) 
      if m.Success then
          System.Convert.ToInt32 m.Value 
      else
          1
  let (|Die|) =
      function
      | Count n & Color a & Sides m ->
          {
              sides = m
              color = Some a
              rolled = None
          }|>List.replicate n
      | Count n & Sides m ->
          {
              sides = m
              color = None
              rolled = None 
          }|>List.replicate n
      | _ ->
          List.empty
  let (|Dice|) (input: string) =
      input.Split()      
      |> Seq.map (function |Die a -> a)
      |> List.concat
  let roll (random: Random) (dice: Dice)=
     { dice with
         rolled = Some (random.Next(1, dice.sides))          
     }
  let rollMany (random: Random)  =
      roll random
      |> List.map
  
  let rolled (dice: Dice) =
      dice.rolled
  let sides (dice:Dice) =
      dice.sides
  let color (dice: Dice) =
      dice.color