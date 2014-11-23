
Public Class PrintStatement
    Inherits Statement

    Friend Write As Action(Of String)
    Private _expression As Expression

    Sub New(expression As Expression)
        Write = Sub(value As String)
                    Console.Write(value)
                End Sub
        _expression = expression
    End Sub

    Public Overrides Function Execute(context As RuntimeContext) As Boolean
        Write(_expression.Evaluate(context).ToString())
        Return True
    End Function
End Class

Public Class PrintLineStatement
    Inherits PrintStatement

    Sub New(expression As Expression)
        MyBase.New(expression)
        Write = Sub(value As String)
                    Console.WriteLine(value)
                End Sub
    End Sub
End Class
