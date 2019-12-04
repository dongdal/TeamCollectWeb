Public Class AppSession

    Shared Property UserId As String
        Get
            Return HttpContext.Current.Session("UserId")
        End Get
        Set(value As String)
            HttpContext.Current.Session("UserId") = value
        End Set
    End Property

    Shared Property PasswordExpiredDate As DateTime
        Get
            Return HttpContext.Current.Session("PasswordExpiredDate")
        End Get
        Set(value As DateTime)
            HttpContext.Current.Session("PasswordExpiredDate") = value
        End Set
    End Property

    Shared Property NomPrenomUser As String
        Get
            Return HttpContext.Current.Session("NomPrenomUser")
        End Get
        Set(value As String)
            HttpContext.Current.Session("NomPrenomUser") = value
        End Set
    End Property

    Shared Property UserName As String
        Get
            Return HttpContext.Current.Session("UserName")
        End Get
        Set(value As String)
            HttpContext.Current.Session("UserName") = value
        End Set
    End Property

    Shared Property PersonneId As String
        Get
            Return HttpContext.Current.Session("PersonneId")

        End Get
        Set(value As String)
            HttpContext.Current.Session("PersonneId") = value
        End Set
    End Property

    Shared Property CodeSecret As String
        Get
            Return HttpContext.Current.Session("CodeSecret")

        End Get
        Set(value As String)
            HttpContext.Current.Session("CodeSecret") = value
        End Set
    End Property

    Shared Property AgenceId As Long
        Get
            Return HttpContext.Current.Session("AgenceId")

        End Get
        Set(value As Long)
            HttpContext.Current.Session("AgenceId") = value
        End Set
    End Property

    'Shared Property LastExercices As Dictionary(Of Integer, Integer)
    '    Get
    '        Return HttpContext.Current.Session("LastExercices")
    '    End Get
    '    Set(value As Dictionary(Of Integer, Integer))
    '        HttpContext.Current.Session("LastExercices") = value
    '    End Set
    'End Property

End Class
