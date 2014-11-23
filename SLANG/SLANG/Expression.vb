
'Expression is what you evaluate for it's value
Public MustInherit Class Expression
    Public MustOverride Function Evaluate(context As RuntimeContext) As Double
End Class
