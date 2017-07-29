Public Class CXException
    Inherits Exception
    Private mMsg As String
    Private mPath As String
    Sub New(msg As String)
        MyBase.New(msg)
        mMsg = msg
    End Sub

    Sub New(e As Exception)
        MyBase.New(e.Message)
        mMsg = e.Message
        mPath = e.StackTrace
    End Sub

    Public Function GetMsg() As String
        If mMsg = Nothing Then Return Message
        Return mMsg
    End Function

    Public Function GetStackTrace() As String
        If mPath = Nothing Then Return StackTrace
        Return mPath
    End Function

End Class



