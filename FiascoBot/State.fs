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
    
type Detail = {
    key: int
    description: string
    detail: Detail option
}

type Relationship = {
    key: int
    description: string
    detail: Detail option
    players: Set<Player> 
}
type Scenario =
    | Relationship of Relationship
    | Need of Detail
    | Location of Detail
    | Object of Detail
        
type State = {
    players: Map<string, Player * DicePool>
    scenario: Set<Scenario>
}
