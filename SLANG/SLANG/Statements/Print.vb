
Public Class Print
    Inherits Statement

    Friend Write As Action(Of String)
    Private _expression As Expression

    Sub New(expression As Expression)
        Write = Sub(value As String)
                    Console.Write(value)
                End Sub
        _expression = expression
    End Sub

    Public Overrides Function Execute(context As RuntimeContext) As Symbol
        Dim symbol = _expression.Evaluate(context)
        Dim data As String = String.Empty
        Select Case symbol.Type
            Case Types.StringType
                data = symbol.StringValue
            Case Types.NumericType
                data = symbol.NumericValue.ToString()
            Case Types.BooleanType
                data = symbol.BoolValue.ToString().ToUpperInvariant()
        End Select
        Write(data)
        Return Nothing
    End Function
End Class

Public Class PrintLine
    Inherits Print

    Sub New(expression As Expression)
        MyBase.New(expression)
        Write = Sub(value As String)
                    Console.WriteLine(value)
                End Sub
    End Sub
End Class
