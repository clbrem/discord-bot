namespace FiascoBot
open Markdig
open Markdig.Syntax

module Reader =
    type Pattern<'T> = Block -> 'T option         
    let (|Header|_|) : Pattern<string> =
        function
        | :? HeadingBlock as a ->
            Some (a.Inline.FirstChild.ToString())
        | _ -> None
    let (|Ordered|_|) : Pattern<Block list> =
        function
        | :? ListBlock as l when l.IsOrdered ->            
            l.Descendants<Block>() |> List.ofSeq |> Some
        | _ -> None
    let (|Item|_|) : Pattern<int * Block list> =
        function
        | :? ListItemBlock as l ->
            Some (l.Order, l.Descendants<Block>() |> List.ofSeq)
        | _ -> None
    let (|Text|_|) : Pattern<string> =
        function
        | :? ParagraphBlock as p ->
            p.Inline.FirstChild.ToString() |> Some             
        | _ -> None
    
    
    
    