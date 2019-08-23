
Imports System
Imports System.Collections.Generic

Partial Public Class TransfertClientPorteFeuille
    Public Property Id As Long
    Public Property PorteFeuilleSourceId As Long
    Public Property PorteFeuilleDestinationId As Long
    Public Property ClientId As Long
    Public Property Etat As Boolean = True
    Public Property DateCreation As DateTime = Now


    Public Overridable Property Client As Client
    Public Overridable Property PorteFeuilleSource As PorteFeuille
    Public Overridable Property PorteFeuilleDestination As PorteFeuille

    Public Property UserId As String
    Public Overridable Property User As ApplicationUser

End Class
