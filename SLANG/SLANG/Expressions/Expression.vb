
'Expression is what you evaluate for it's value
Public MustInherit Class Expression
    Friend _type As Types
    Public MustOverride Function Evaluate(context As RuntimeContext) As Symbol
    Public MustOverride Function TypeCheck(context As CompilationContext) As Types
    Public Overridable Function Get_Type() As Types
        Return _type
    End Function
End Class