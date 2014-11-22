Public Class NumericConstant
    Inherits Expression
    Private _value As Double

    Public Sub New(value As Double)
        _value = value
    End Sub

    Public Overrides Function Evaluate(context As RuntimeContext) As Double
        Return _value
    End Function

    Public Overrides Function ToString() As String
        Return _value.ToString()
    End Function
End Class
