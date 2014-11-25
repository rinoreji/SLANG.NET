Public Class Symbol

    Public Name As String
    Public Type As Types
    Public StringValue As String
    Public BoolValue As Boolean
    Public NumericValue As Double

    Sub New()
    End Sub

    Private Sub New(name As String, type As Types)
        Me.Name = name
        Me.Type = type
    End Sub

    Sub New(name As String, value As String)
        Me.New(name, Types.StringType)
        Me.StringValue = value
    End Sub

    Sub New(name As String, value As Boolean)
        Me.New(name, Types.BooleanType)
        Me.BoolValue = value
    End Sub

    Sub New(name As String, value As Double)
        Me.New(name, Types.NumericType)
        Me.NumericValue = value
    End Sub
End Class
