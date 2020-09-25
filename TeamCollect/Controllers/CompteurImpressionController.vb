Imports System.Data.Entity
Imports System.Net
Imports PagedList
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.Validation


Namespace TeamCollect
    Public Class CompteurImpressionController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function

        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR,MANAGER")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, dateDebut As String, dateFin As String, CollecteurId As Long?) As ActionResult
            ViewBag.CurrentSort = sortOrder
            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If

            Dim entities = From e In db.CompteurImpressions.Include(Function(h) h.Collectrice).Include(Function(h) h.HistoriqueMouvement).
                               Where(Function(h) h.HistoriqueMouvement.LibelleOperation.Contains("CASH-IN")).ToList

            Dim IsManagerOrAdmin As Boolean = (User.IsInRole("ADMINISTRATEUR") Or User.IsInRole("MANAGER") Or User.IsInRole("SA"))

            If User.IsInRole("CHEFCOLLECTEUR") And Not IsManagerOrAdmin Then
                entities = entities.Where(Function(e) e.Collectrice.Personne.AgenceId = AppSession.AgenceId).ToList()
            End If

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Collectrice.UserName.ToUpper.Contains(searchString.ToUpper) Or e.Collectrice.Personne.Nom.ToUpper.Contains(searchString.ToUpper) Or
                                               e.Collectrice.Personne.Prenom.ToUpper.Contains(searchString.ToUpper) Or e.HistoriqueMouvement.Client.Nom.ToUpper.Contains(searchString.ToUpper) Or
                                               e.HistoriqueMouvement.Client.Prenom.ToUpper.Contains(searchString.ToUpper)).ToList
            End If

            If Not IsNothing(dateDebut) And Not IsNothing(dateFin) Then
                entities = entities.Where(Function(h) h.DatePremiereImpression.Date >= dateDebut And h.DatePremiereImpression.Date <= dateFin).ToList
                ViewBag.dateDebut = dateDebut
                ViewBag.dateFin = dateFin
            Else
                If (IsNothing(dateDebut) And IsNothing(dateFin)) Then
                    entities = entities.Where(Function(h) h.DatePremiereImpression.Date = Now.Date Or h.DatePremiereImpression.Date = Now.Date).ToList
                Else
                    If (IsNothing(dateFin)) Then
                        entities = entities.Where(Function(h) h.DatePremiereImpression.Date >= dateDebut).ToList
                    Else
                        entities = entities.Where(Function(h) h.DatePremiereImpression.Date <= dateFin).ToList
                    End If

                End If
            End If

            If Not IsNothing(CollecteurId) Then
                entities = entities.Where(Function(h) h.Collectrice.PersonneId = CollecteurId.Value).ToList()
            End If

            ViewBag.EnregCount = entities.Count
            ViewBag.CollectriceList = LoadComboBox()
            ViewBag.CollecteurId = CollecteurId
            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            Return View(entities.ToPagedList(pageNumber, pageSize * 2))
        End Function

        Public Function LoadComboBox() As List(Of SelectListItem)
            Dim CollecteurSystem = ConfigurationManager.AppSettings("CollecteurSystemeId") 'Il s'agit de l'identitfiant du collecteur système
            Dim agenceUserConnected = GetCurrentUser().Personne.AgenceId
            Dim Collectrices = (From e In db.Collecteurs Where e.Id <> CollecteurSystem Select e).ToList

            If Not User.IsInRole("ADMINISTRATEUR") Then
                Collectrices = Collectrices.Where(Function(e) e.AgenceId = agenceUserConnected).ToList
            End If
            Dim CollectriceList As New List(Of SelectListItem)
            For Each collectrice In Collectrices
                If String.IsNullOrEmpty(collectrice.Prenom) Then
                    CollectriceList.Add(New SelectListItem With {.Value = collectrice.Id, .Text = collectrice.Nom})
                Else
                    CollectriceList.Add(New SelectListItem With {.Value = collectrice.Id, .Text = collectrice.Nom & " " & collectrice.Prenom})
                End If
            Next
            Return CollectriceList
        End Function

    End Class
End Namespace
