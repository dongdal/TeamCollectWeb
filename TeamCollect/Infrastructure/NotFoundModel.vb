Public Class NotFoundModel
    Inherits HandleErrorInfo
    Public Sub New(exception As Exception, controllerName As String, actionName As String)
        MyBase.New(exception, controllerName, actionName)
    End Sub
    Public Property RequestedUrl() As String
        Get
            Return m_RequestedUrl
        End Get
        Set(value As String)
            m_RequestedUrl = Value
        End Set
    End Property
    Private m_RequestedUrl As String
    Public Property ReferrerUrl() As String
        Get
            Return m_ReferrerUrl
        End Get
        Set(value As String)
            m_ReferrerUrl = Value
        End Set
    End Property
    Private m_ReferrerUrl As String
End Class