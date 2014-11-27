Imports System.IO
Imports SLANG

Module Module1

    Sub Main()
        PrintHeader()

        Console.ForegroundColor = ConsoleColor.DarkGreen
        Console.WriteLine("Prealoading program 1 from file...")
        Console.WriteLine(">> Reading source code")

        Console.ForegroundColor = ConsoleColor.Gray
        Dim sourceCode = File.ReadAllText(Path.Combine(
                                          Environment.CurrentDirectory,
                                          "scripts",
                                          "IfAndWhile.sl"))
        Console.WriteLine(sourceCode)
        Console.WriteLine(ControlChars.NewLine)

        Console.ForegroundColor = ConsoleColor.DarkGreen
        Console.WriteLine(">> Parsing source code")

        Dim statements As Statement()
        Try
            Dim compileContext = New CompilationContext()
            Dim slangCompiler = New RDParser(sourceCode)
            statements = slangCompiler.Parse(compileContext)
            Console.WriteLine(">> Parsing successful ..." + ControlChars.NewLine)
            Console.ForegroundColor = ConsoleColor.DarkGreen
            Console.WriteLine(">> Executing" + ControlChars.NewLine)
            Try
                Dim rContext = New RuntimeContext()
                For Each statement In statements
                    statement.Execute(rContext)
                Next
                Console.WriteLine(ControlChars.NewLine + ">> Completed!" + ControlChars.NewLine)
            Catch ex As Exception
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine(">> Exception while executing, Details")
                Console.WriteLine(ex.Message)
            End Try

        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine(">> Exception while parsing, Details")
            Console.WriteLine(ex.Message)
        End Try

        Console.ReadLine()
    End Sub

    Sub PrintHeader()
        Console.ForegroundColor = ConsoleColor.Green
        Console.WriteLine(ControlChars.NewLine)
        Console.WriteLine(ControlChars.NewLine)
        Console.WriteLine("*********************************************")
        Console.WriteLine("*********************************************")
        Console.WriteLine("*********** S L A N G in action *************")
        Console.WriteLine("*********************************************")
        Console.WriteLine("*********************************************")
        Console.WriteLine(ControlChars.NewLine)
        Console.ForegroundColor = ConsoleColor.White
    End Sub

End Module
