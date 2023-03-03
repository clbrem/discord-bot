namespace FiascoBot

open System
open Xunit
open MyAssert

module Tests =
    [<Fact>]
    let ``Can Parse Dice`` () =
        match "d4" with
        | Dice.Color a -> Assert.FailWith $"Should not be color, found {a}"
        | Dice.Sides 4 & Dice.Count 1 -> Assert.Pass
        | Dice.Sides n -> Assert.FailWith $"Expected 4, found {n}"
        | _ -> Assert.FailWith "Could Not Parse"

        match "2d4" with
        | Dice.Die a when List.length a = 2 -> Assert.Pass
        | _ -> Assert.FailWith "Could Not Parse"

        match "2wd12" with
        | Dice.Die (a :: _) when a.color = Some White -> Assert.Pass
        | _ -> Assert.FailWith "Could Not Parse"

    [<Fact>]
    let ``Can Roll Dice`` () =
        let rnd = Random()

        match "3d20" with
        | Dice.Die dice -> Dice.rollMany rnd dice |> List.map Dice.rolled |> ignore


        match "3d20 d4 wd6" with
        | Dice.Dice a ->
            a
            |> Dice.rollMany rnd
            |> List.map Dice.sides
            |> Assert.ForEach(
                [ Assert.EqualTo 20
                  Assert.EqualTo 20
                  Assert.EqualTo 20
                  Assert.EqualTo 4
                  Assert.EqualTo 6 ]
            )
