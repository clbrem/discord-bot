namespace FiascoBot

type Player = {
    name: string
    discriminator: string
    id: uint64 
}
type DicePool = Set<Dice>
module DicePool =
    let add a : DicePool -> DicePool= Set.add a
    let flush: DicePool -> DicePool = fun _ -> Set.empty
    let remove a : DicePool -> DicePool = Set.remove a
    let empty: DicePool = Set.empty
    
type Detail<'T> = {
    key: int
    description: string
    detail: 'T
}
type Detail = Detail<unit>

type Relationship<'T> = {
    key: int
    description: string
    detail: 'T
    players: Set<Player> 
}
type Scenario<'T> =
    | Relationship of Relationship<'T>
    | Need of Detail<'T>
    | Location of Detail<'T>
    | Object of Detail<'T>
        
type State = {
    players: Map<string, Player * DicePool>
    dice: DicePool
    scenario: Set<Scenario<Detail option>>
}

module State =
    let addPlayer player state : State =
        { state with
           players = state.players |> Map.add player.discriminator (player, DicePool.empty) 
        }
    let dropPlayer player state : State =
        { state with
           players = state.players |> Map.remove player.discriminator  
        }
    
    
        
