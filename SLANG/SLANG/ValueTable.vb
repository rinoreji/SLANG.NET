Public Structure ValueTable
    Public Token As Tokens
    Public Value As String

    Public Sub New(token As Tokens, value As String)
        Me.Token = token
        Me.Value = value
    End Sub
End Structure
