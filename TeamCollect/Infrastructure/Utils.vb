Imports System.Data.SqlClient

Public Class Util


    ''' <summary>
    ''' The connection string property that pulls from the web.config
    ''' </summary>
    Public Shared ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        End Get
    End Property

    ''' <summary>
    ''' The connection string property that pulls from the web.config
    ''' </summary>
    Public Shared ReadOnly Property GetPasswordValidityDays() As Integer
        Get
            Return Convert.ToInt64(ConfigurationManager.AppSettings("PasswordValidityDays"))
        End Get
    End Property

    Shared Sub GetError(cex As Exception, modelState As ModelStateDictionary)
        If TypeOf (cex) Is Entity.Validation.DbEntityValidationException Then
            Dim ex = CType(cex, Entity.Validation.DbEntityValidationException)
            Dim strError As String = ""
            For Each val_errors In ex.EntityValidationErrors
                For Each val_error In val_errors.ValidationErrors
                    modelState.AddModelError(val_error.PropertyName, val_error.ErrorMessage)
                    strError &= val_error.ErrorMessage & vbCrLf
                Next
            Next
            modelState.AddModelError("", strError)
        ElseIf TypeOf (cex) Is Exception Then
            Dim ex = cex
            Dim strError As String = ""
            Dim ie = ex
            While ie IsNot Nothing
                If Not ie.Message.StartsWith("Une erreur") Then
                    strError &= ie.Message & "|"
                End If

                ie = ie.InnerException
            End While
            Dim ar_errors = strError.Split("|".ToCharArray)
            For Each val_error In ar_errors
                modelState.AddModelError("", val_error)
            Next
        End If
    End Sub

    Shared Function GetError(cex As Exception) As String
        Dim strError As String = ""
        If TypeOf (cex) Is Entity.Validation.DbEntityValidationException Then
            Dim ex = CType(cex, Entity.Validation.DbEntityValidationException)
            For Each val_errors In ex.EntityValidationErrors
                For Each val_error In val_errors.ValidationErrors
                    strError &= val_error.ErrorMessage & vbCrLf
                Next
            Next

        ElseIf TypeOf (cex) Is Exception Then
            Dim ex = cex
            Dim ie = ex
            While ie IsNot Nothing
                If Not ie.Message.StartsWith("Une erreur") Then
                    strError &= ie.Message & "|"
                End If

                ie = ie.InnerException
            End While
        End If

        Return strError
    End Function

    Shared Function GetModelStateError(ms As ModelStateDictionary) As Object
        Dim query = From state In ms.Values
                    From erreur In state.Errors
                    Select erreur.ErrorMessage

        Dim strError As String = ""
        For Each val_error In query
            strError &= val_error & vbCrLf
        Next
        Return strError
    End Function

    Public Shared Property Db As New ApplicationDbContext

    ''' <summary>
    ''' Fonction permettant de générer le code client de façon aléatoire et de sorte qu'il ne se répète pas
    ''' </summary>
    ''' <param name="AgenceId"></param>
    ''' <param name="SocieteId"></param>
    ''' <returns>String (chaîne de caractères)</returns>
    ''' <remarks></remarks>
    Shared Function GetRandomString(ByVal AgenceId As Long, ByVal SocieteId As Long) As String 'Cette fonction est à revoir
        Dim isItOk As Boolean = False
        Dim tbl() As String 'le tableau des caracteres
        Dim strx As String = "" 'la chaine qu'on va créer"
        tbl = Split("0,1,2,3,4,5,6,7,8,9", ",") 'Vous pouvez ajouter/suppr des caracteres .
        Dim leCodeSecret As String = ""
        'Dim dayOfYear = Now.DayOfYear
        Do
            Randomize()
            For I = 1 To 6
                strx = strx & tbl(Int((UBound(tbl) + 1) * Rnd()))
            Next I
            leCodeSecret = GetPositionAgence(AgenceId, SocieteId) & strx

            Dim countCodeSecret = (From cli In Db.Personnes.OfType(Of Client)() Where (cli.AgenceId = AgenceId And cli.CodeSecret.Equals(leCodeSecret)) Select cli).Count
            isItOk = IIf(countCodeSecret = 0, True, False)
        Loop Until isItOk

        Return leCodeSecret
    End Function

    ''' <summary>
    ''' Fonction permettant de déterminer la position d'un agence dans un groupe d'agence appartenant à une même société
    ''' </summary>
    ''' <param name="agenceId"></param>
    ''' <param name="societeId"></param>
    ''' <returns>String (chaîne de caractères)</returns>
    ''' <remarks></remarks>
    Private Shared Function GetPositionAgence(ByVal agenceId As Long, ByVal societeId As String) As String
        Dim agences = (From ag In Db.Agences Where ag.SocieteId = societeId Select ag).ToList
        Dim position = 0
        For Each agenc In agences
            position += 1
            If agenc.Id = agenceId Then
                Exit For
            End If
        Next
        Dim positionString As String = position
        While positionString.Length < 3
            positionString = "0" & positionString
        End While
        Return positionString
    End Function

End Class