module MyAssert


type Assert() =
    inherit Xunit.Assert()
    static member Pass =
        Assert.True(true)
    static member FailWith msg =
        Assert.True(false, msg)
    
