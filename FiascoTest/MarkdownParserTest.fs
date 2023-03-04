namespace FiascoBot
open System
open System.IO
open Markdig.Syntax
open Xunit
open MyAssert
open Markdig
open System.Linq
open Reader


module MarkdownTest =
    let file = File.ReadAllText "./Modules/Sample.md"
    [<Fact>]
    let ``Can Read Markdown File``() =                
        let rec loop =
            function
            | Header category
               :: Ordered (
                   Item ( _,
                       Text title
                       :: Ordered(
                           Item(i, Text x:: _ ) :: _ 
                           ) :: _
                       ) :: _
                   )
               :: _-> Assert.FailWith $"Item {i} is {x}"          
            | _ :: a -> loop a
            | _ -> Assert.FailWith "Couldn't Find It"
        Markdown.Parse file
        |> List.ofSeq
        |> loop 
                
        
        
        

                
            
            
            
        
        



