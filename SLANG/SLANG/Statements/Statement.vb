'Statement is what you Execute for it's Effect
Public MustInherit Class Statement
    Public MustOverride Function Execute(context As RuntimeContext) As Symbol
End Class
