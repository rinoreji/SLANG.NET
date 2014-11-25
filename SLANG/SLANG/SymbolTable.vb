Imports System.Collections

Public Class SymbolTable
    Dim symbols As Hashtable = New Hashtable

    Public Function Add(symbol As Symbol) As Boolean
        symbols(symbol.Name) = symbol
        Return True
    End Function

    Public Function GetSymbol(symbolName As String) As Symbol
        Return CType(symbols(symbolName), Symbol)
    End Function

    Public Sub Assign(symbolName As String, symbol As Symbol)
        symbols(symbolName) = symbol
    End Sub

End Class