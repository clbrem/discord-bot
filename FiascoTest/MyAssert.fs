module MyAssert


type Assert() =
    inherit Xunit.Assert()
    static member Pass =
        Assert.True(true)
    static member FailWith msg =
        Assert.True(false, msg)
    static member Some check =
        function
            | Some item -> check item
            | None -> Assert.FailWith "Expected Some value, found None."
    static member ForEach asserts items=        
        List.iteri (
            fun i asserter ->
                Seq.item i items |> asserter
                ) asserts
    static member EqualTo<'T> (expected:'T) item =
        Assert.Equal(expected, item)
    
