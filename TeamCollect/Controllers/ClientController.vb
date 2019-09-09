Imports System.Data.Entity
Imports System.Net
Imports PagedList
Imports Microsoft.AspNet.Identity
Imports System.Data.Entity.Validation
Imports System.Data.SqlClient
Imports System.Data.OleDb


Namespace TeamCollect
    Public Class ClientController
        Inherits BaseController

        Private db As New ApplicationDbContext

        Private Function GetCurrentUser() As ApplicationUser
            Dim id = User.Identity.GetUserId
            Dim aspuser = db.Users.Find(id)
            Return aspuser
        End Function


        Public Sub LoadComboTransfert(evm As TransfertViewModel, clientId As Long)
            Dim userAgenceId = GetCurrentUser.Personne.AgenceId
            Dim listPorteFeuille = db.PorteFeuilles.Where(Function(m) m.Collecteur.AgenceId = userAgenceId).ToList

            Dim listPorteF As New List(Of SelectListItem)
            For Each item In listPorteFeuille
                listPorteF.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle})
            Next

            Dim LeClient = db.Clients.Find(clientId)
            evm.IDsPorteFeuille = listPorteF
            evm.ClientId = clientId
            evm.Client = LeClient
            evm.PorteFeuilleSourceId = LeClient.PorteFeuilleId
            evm.PorteFeuille = LeClient.PorteFeuille
        End Sub

        ' GET: /Collecteur/Create
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR")>
        Function Transfert(ClientId As Long) As ActionResult
            Dim entityVM As New TransfertViewModel

            LoadComboTransfert(entityVM, ClientId)
            Return View(entityVM)
        End Function


        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR")>
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Transfert(ByVal entityVM As TransfertViewModel) As ActionResult
            If ModelState.IsValid Then
                Dim ClientId = entityVM.ClientId
                Dim client = db.Clients.Find(ClientId)
                Dim UserId = GetCurrentUser.Id
                If IsNothing(client) Then
                    ModelState.AddModelError("PorteFeuilleId", "Le client n'existe pas")
                    LoadComboTransfert(entityVM, ClientId)
                    Return View(entityVM)
                End If

                'mise a jour du solde du client
                client.PorteFeuilleId = entityVM.PorteFeuilleId
                db.Entry(client).State = EntityState.Modified
                db.SaveChanges()
                'on enregistre la trace
                Dim Trans As New TransfertClientPorteFeuille
                Trans.ClientId = ClientId
                Trans.UserId = UserId
                Trans.PorteFeuilleSourceId = entityVM.PorteFeuilleSourceId
                Trans.PorteFeuilleDestinationId = entityVM.PorteFeuilleId
                Trans.DateCreation = Now
                Trans.Etat = True
                db.TransfertClientPorteFeuille.Add(Trans)
                db.SaveChanges()

                Return RedirectToAction("IndexAgence")
            End If
            LoadComboTransfert(entityVM, entityVM.ClientId)
            Return View(entityVM)
        End Function

        ' GET:/Rapport
        Function ListeClient() As ActionResult
            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")
            ViewBag.UserAgenceId = getCurrentUser.Personne.AgenceId
            Return View()
        End Function

        Function ListeClientGlobal() As ActionResult
            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listPersonne = db.Agences.OfType(Of Agence)().ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listPersonne2.ToList
            Return View()
        End Function

        Public Sub LoadCombo(evm As ImportClientViewModel)
            Dim userAgenceId = getCurrentUser.Personne.AgenceId
            Dim listPersonne = db.Personnes.OfType(Of Collecteur).Where(Function(e) e.AgenceId = userAgenceId).ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Nom & " " & item.Prenom})
            Next
            evm.IDsCollecteur = listPersonne2
        End Sub
        ' GET: /Collecteur/Create
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR")>
        Function Import() As ActionResult
            Dim entityVM As New ImportClientViewModel
            LoadCombo(entityVM)
            Return View(entityVM)
        End Function


        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR")>
        Function Import(VientDeForm As Boolean?) As ActionResult

            'juste pour un petit test
            'If (1 = 1) Then
            '    ViewBag.Error = "true"
            '    ViewBag.Msg = "Message d'ereur"
            '    Dim eVM1 As New ImportClientViewModel
            '    LoadCombo(eVM1)
            '    Return View(eVM1)
            'End If


            Dim userAgenceId = getCurrentUser.Personne.AgenceId
            'Dim userId = getCurrentUser.Personne.Id
            Dim userId = getCurrentUser.Id
            If (VientDeForm = True) Then
                'Try
                Dim fileExtension As String = System.IO.Path.GetExtension(Request.Files("Fichier").FileName)
                If fileExtension = ".xls" OrElse fileExtension = ".xlsx" Then
                    Dim ds As New DataSet()
                    If Request.Files("Fichier").ContentLength > 0 Then

                        Dim fileLocation As String = Server.MapPath("~/Importation/") + Request.Files("Fichier").FileName

                        Dim excelConnection As New OleDbConnection
                        Dim excelConnection1 As New OleDbConnection

                        Try

                            If System.IO.File.Exists(fileLocation) Then
                                System.IO.File.Delete(fileLocation)
                            End If
                            Request.Files("Fichier").SaveAs(fileLocation)
                            Dim excelConnectionString As String = String.Empty
                            excelConnectionString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & fileLocation) + ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=2"""
                            'connection String pour l'extension xls.
                            If fileExtension = ".xls" Then
                                excelConnectionString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & fileLocation) + ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
                                'connection String pour l'extension xlsx.
                            ElseIf fileExtension = ".xlsx" Then
                                excelConnectionString = (Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & fileLocation) + ";Extended Properties=""Excel 12.0;HDR=Yes;IMEX=1"""
                            End If
                            'Create Connection pour Excel
                            excelConnection = New OleDbConnection(excelConnectionString)
                            excelConnection.Open()
                            Dim dt As New DataTable()
                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)
                            If dt Is Nothing Then
                                Return Nothing
                            End If

                            Dim excelSheets As [String]() = New [String](dt.Rows.Count - 1) {}
                            Dim t As Integer = 0
                            'recuperation des données excel .
                            For Each row As DataRow In dt.Rows
                                Dim Tempo = row("TABLE_NAME").ToString()
                                If Tempo.EndsWith("$") Then
                                    excelSheets(t) = row("TABLE_NAME").ToString()
                                    t += 1
                                End If
                            Next
                            excelConnection1 = New OleDbConnection(excelConnectionString)
                            Dim query As String = String.Format("Select * from [{0}]", excelSheets(0))
                            Using dataAdapter As New OleDbDataAdapter(query, excelConnection1)
                                dataAdapter.Fill(ds)
                            End Using

                            'recuperation du portefeuille ID
                            Dim CollecteurId As Long = CType(Request("CollecteurId"), Long)
                            Dim portefeuille = (From p In db.PorteFeuilles Where p.CollecteurId = CollecteurId Select p).ToArray
                            Dim PortefeuilleId As Long = 0
                            If (portefeuille.Count > 0) Then
                                PortefeuilleId = portefeuille.FirstOrDefault.Id
                            End If
                            '--------------------------
                            For i As Integer = 6 To ds.Tables(0).Rows.Count - 1
                                Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
                                'Dim monsolde As Decimal = CType(ds.Tables(0).Rows(i)(7).ToString, Decimal)
                                Dim NomPrenom = Replace(ds.Tables(0).Rows(i)(1).ToString, ",", ".")
                                'Dim Solde = Replace(ds.Tables(0).Rows(i)(2).ToString, ",", ".")
                                Dim Solde = ds.Tables(0).Rows(i)(2).ToString
                                Dim Adresse = ds.Tables(0).Rows(i)(3).ToString
                                Dim Telephone = ds.Tables(0).Rows(i)(4).ToString
                                Dim Sexe = ds.Tables(0).Rows(i)(5).ToString
                                'on cast d'abord le solde
                                Dim SoldeCast As Double = 0
                                Double.TryParse(Solde, SoldeCast)
                                Solde = CType(Solde, Double)

                                'chargement des donne provenant du fichier excel
                                Dim entityVM As New ClientViewModel With {
                                    .DateCreation = Now,
                                    .Nom = NomPrenom,
                                    .Quartier = Adresse,
                                    .Solde = Solde, 'SoldeCast '
                                    .Sexe = Sexe,
                                    .Telephone = Telephone,
                                    .AgenceId = userAgenceId,
                                    .Etat = 1
                                }

                                '--------------------------
                                Dim entities = (From e In db.Personnes.OfType(Of Client)() Select e)
                                Dim list As List(Of Personne) = db.Personnes.OfType(Of Personne)().ToList
                                entityVM.Id = list.ElementAt(list.Count - 1).Id + 1
                                Dim monwebser = New TeamCollecteServices
                                Dim CLT As Client = entityVM.getEntity
                                CLT.CodeSecret = monwebser.GetRandomString(CLT.Nom, CLT.Prenom)
                                CLT.PorteFeuilleId = PortefeuilleId
                                CLT.UserId = userId
                                CLT.Etat = True
                                db.Personnes.Add(CLT)
                                Try
                                    db.SaveChanges()
                                    'Return RedirectToAction("Index")
                                    'ici on creer une premiere collect
                                    Dim ClientId = CLT.Id
                                    Dim Montant = CLT.Solde
                                    'Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId Select J).ToArray

                                    Dim CollecteurId2 = ConfigurationManager.AppSettings("CollecteurSystemeId")

                                    '3- on recupere le journal caisse et on enregistre dans mouvement historique
                                    Dim LesJournalCaisse = (From J In db.JournalCaisses Where J.CollecteurId = CollecteurId2 Select J).ToArray
                                    Dim JCID = LesJournalCaisse.FirstOrDefault.Id
                                    Dim LibOperation As String = "Solde Initial- au" & DateTime.Now.ToString

                                    Dim parameterList As New List(Of SqlParameter)()
                                    parameterList.Add(New SqlParameter("@ClientId", ClientId))
                                    parameterList.Add(New SqlParameter("@CollecteurId", CollecteurId2))
                                    parameterList.Add(New SqlParameter("@Montant", +Montant))
                                    parameterList.Add(New SqlParameter("@DateOperation", Now))
                                    parameterList.Add(New SqlParameter("@Pourcentage", 0))
                                    parameterList.Add(New SqlParameter("@MontantRetenu", 0))
                                    parameterList.Add(New SqlParameter("@EstTraiter", 0))
                                    parameterList.Add(New SqlParameter("@Etat", False))
                                    parameterList.Add(New SqlParameter("@DateCreation", Now))
                                    parameterList.Add(New SqlParameter("@UserId", userId))
                                    parameterList.Add(New SqlParameter("@JournalCaisseId", JCID))
                                    parameterList.Add(New SqlParameter("@LibelleOperation", LibOperation))
                                    Dim parameters As SqlParameter() = parameterList.ToArray()
                                    Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, MontantRetenu, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId, LibelleOperation) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @MontantRetenu, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId, @LibelleOperation)"
                                    db.Database.ExecuteSqlCommand(myInsertQuery, parameters)

                                Catch ex As DbEntityValidationException
                                    Util.GetError(ex, ModelState)
                                Catch ex As Exception
                                    Util.GetError(ex, ModelState)
                                End Try

                                'Dim queryPersonne = "Insert into Personne (CodeSecret,Nom,Prenom,Sexe,CNI,Telephone,Adresse,Quartier,Etat,DateCreation,UserId,AgenceId) VALUES('" & ds.Tables(0).Rows(i)(1) & "','" & ds.Tables(0).Rows(i)(1) & "','" & ds.Tables(0).Rows(i)(1) & "')"
                                ''Decimal.TryParse(ds.Tables(0).Rows(i)(7).ToString, monsolde)
                                'Dim querySolde = "Update Client SET Solde=" & monsolde & " from Client,Personne where Client.Id = Personne.Id AND  NumeroCompte='" & ds.Tables(0).Rows(i)(1) & "' AND CNI='" & ds.Tables(0).Rows(i)(2) & "'"
                                'Dim queryChangeEtat = "Update HistoriqueMouvement SET EstTraiter = 2, DateTraitement ='" + Now.Date + "' from HistoriqueMouvement, Client where HistoriqueMouvement.ClientId = Client.Id  AND  NumeroCompte='" + ds.Tables(0).Rows(i)(1) + "' AND EstTraiter = 1"
                                'con.Open()
                                'Dim cmdPersonne As New SqlCommand(querySolde, con)
                                'Dim cmd2 As New SqlCommand(queryChangeEtat, con)
                                'cmd.ExecuteNonQuery()
                                'cmd2.ExecuteNonQuery()
                                'con.Close()
                            Next

                            excelConnection.Close()
                            excelConnection.Dispose()
                            excelConnection1.Close()
                            excelConnection1.Dispose()
                            Return RedirectToAction("IndexAgence", "Client")
                        Catch ex As SqlException
                            ViewBag.Error = "true"
                            ViewBag.Msg = ex.Message

                            excelConnection.Close()
                            excelConnection.Dispose()
                            excelConnection1.Close()
                            excelConnection1.Dispose()
                        Catch ex As Exception
                            ViewBag.Error = "true"
                            ViewBag.Msg = ex.Message

                            excelConnection.Close()
                            excelConnection.Dispose()
                            excelConnection1.Close()
                            excelConnection1.Dispose()
                            'Finally
                            '    excelConnection.Close()
                            '    excelConnection.Dispose()
                            '    excelConnection1.Close()
                            '    excelConnection1.Dispose()
                        End Try
                    Else

                    End If
                Else

                End If

            End If

            Dim eVM As New ImportClientViewModel
            LoadCombo(eVM)
            Return View(eVM)
        End Function



        ' GET: /Collecteur/
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, AgenceId As Long?) As ActionResult
            'Dim exercices = db.Exercices.Include(Function(e) e.User)
            'Return View(exercices.ToList())

            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If
            ViewBag.CurrentFilter = searchString

            Dim entities = From e In db.Personnes.OfType(Of Client)().ToList

            If (AgenceId.HasValue) Then
                entities = From e In db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = AgenceId).ToList
            Else
                entities = entities.ToList
            End If

            Dim ListClientsRecheches As New List(Of Client)

            If Not String.IsNullOrEmpty(searchString) Then
                For Each item In entities.ToList
                    If String.IsNullOrEmpty(item.Telephone) Then
                        If (item.Nom.ToUpper.Contains(searchString.ToUpper) Or item.Sexe.ToUpper.Contains(searchString.ToUpper)) Then
                            ListClientsRecheches.Add(item)
                        End If
                    Else
                        If (item.Nom.ToUpper.Contains(searchString.ToUpper) Or item.Sexe.ToUpper.Contains(searchString.ToUpper) Or item.Telephone.ToUpper.Contains(searchString.ToUpper)) Then
                            ListClientsRecheches.Add(item)
                        End If
                    End If
                Next
                'entities = entities.Where(Function(e) e.Nom.ToUpper.Contains(searchString.ToUpper) Or e.Sexe.ToUpper.Contains(searchString.ToUpper) Or e.Telephone.ToUpper.Contains(searchString.ToUpper))
                entities = ListClientsRecheches.ToList
            End If

            'If Not String.IsNullOrEmpty(searchString) Then
            '    entities = entities.Where(Function(e) e.Nom.ToUpper.Contains(searchString.ToUpper) Or e.Prenom.ToUpper.Contains(searchString.ToUpper) Or e.Sexe.ToUpper.Contains(searchString.ToUpper) Or e.Telephone.ToUpper.Contains(searchString.ToUpper) Or e.Adresse.ToUpper.Contains(searchString.ToUpper) Or e.Quartier.ToUpper.Contains(searchString.ToUpper) Or e.CodeSecret.ToUpper.Contains(searchString.ToUpper))
            'End If

            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            '----------------on recupère la liste des agences pour filtrer---------------
            Dim listPersonne = db.Agences.OfType(Of Agence)().ToList
            Dim listPersonne2 As New List(Of SelectListItem)
            For Each item In listPersonne
                listPersonne2.Add(New SelectListItem With {.Value = item.Id, .Text = item.Societe.Libelle & ":-- [" & item.Libelle & "] --"})
            Next
            ViewBag.lesagences = listPersonne2.ToList

            Return View("Index", entities.ToPagedList(pageNumber, pageSize))
        End Function

        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function IndexAgence(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            'Dim exercices = db.Exercices.Include(Function(e) e.User)
            'Return View(exercices.ToList())
            ViewBag.CurrentSort = sortOrder
            ViewBag.CodeParm = If(sortOrder = "Code", "Code_desc", "Code")
            ViewBag.NomParm = If(sortOrder = "Nom", "Nom_desc", "Nom")
            ViewBag.PrenomParm = If(sortOrder = "Prenom", "Prenom_desc", "Prenom")
            ViewBag.SexeParm = If(sortOrder = "Sexe", "Sexe_desc", "Sexe")
            ViewBag.CNIParm = If(sortOrder = "CNI", "CNI_desc", "CNI")
            ViewBag.TelParm = If(sortOrder = "Tel", "Tel_desc", "Tel")
            ViewBag.Tel2Parm = If(sortOrder = "Tel2", "Tel2_desc", "Tel2")
            ViewBag.QuartierParm = If(sortOrder = "Quartier", "Quartier_desc", "Quartier")
            ViewBag.AdresseParm = If(sortOrder = "Adresse", "Adresse_desc", "Adresse")
            ViewBag.SoldeParm = If(sortOrder = "Solde", "Solde_desc", "Solde")
            ViewBag.SoldeDispoParm = If(sortOrder = "SoldeDispo", "SoldeDispo_desc", "SoldeDispo")

            ViewBag.CurrentSort = sortOrder



            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If
            ViewBag.CurrentFilter = searchString

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId

            Dim entities = From e In db.Clients.Where(Function(i) i.AgenceId = userAgenceId)

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Nom.ToUpper.Contains(searchString.ToUpper) Or e.Prenom.ToUpper.Contains(searchString.ToUpper) Or
                                              e.Sexe.ToUpper.Contains(searchString.ToUpper) Or e.Telephone.ToUpper.Contains(searchString.ToUpper) Or e.Telephone2.ToUpper.Contains(searchString.ToUpper) Or
                                              e.Adresse.ToUpper.Contains(searchString.ToUpper) Or e.Quartier.ToUpper.Contains(searchString.ToUpper) Or
                                              e.Telephone2.ToUpper.Contains(searchString.ToUpper) Or e.CNI.ToUpper.Contains(searchString.ToUpper) Or
                                              e.CodeSecret.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count


            Select Case sortOrder

                Case "Code"
                    entities = entities.OrderBy(Function(e) e.CodeSecret)
                Case "Code_desc"
                    entities = entities.OrderByDescending(Function(e) e.CodeSecret)

                Case "Nom"
                    entities = entities.OrderBy(Function(e) e.Nom)
                Case "Nom_desc"
                    entities = entities.OrderByDescending(Function(e) e.Nom)

                Case "Prenom"
                    entities = entities.OrderBy(Function(e) e.Prenom)
                Case "Prenom_desc"
                    entities = entities.OrderByDescending(Function(e) e.Prenom)

                Case "Sexe"
                    entities = entities.OrderBy(Function(e) e.Sexe)
                Case "Sexe_desc"
                    entities = entities.OrderByDescending(Function(e) e.Sexe)

                Case "Tel"
                    entities = entities.OrderBy(Function(e) e.Telephone)
                Case "Tel_desc"
                    entities = entities.OrderByDescending(Function(e) e.Telephone)

                Case "Tel2"
                    entities = entities.OrderBy(Function(e) e.Telephone2)
                Case "Tel2_desc"
                    entities = entities.OrderByDescending(Function(e) e.Telephone2)

                Case "CNI"
                    entities = entities.OrderBy(Function(e) e.CNI)
                Case "CNI_desc"
                    entities = entities.OrderByDescending(Function(e) e.CNI)

                Case "Quartier"
                    entities = entities.OrderBy(Function(e) e.Quartier)
                Case "Quartier_desc"
                    entities = entities.OrderByDescending(Function(e) e.Quartier)

                Case "Adresse"
                    entities = entities.OrderBy(Function(e) e.Adresse)
                Case "Adresse_desc"
                    entities = entities.OrderByDescending(Function(e) e.Adresse)

                Case "Solde"
                    entities = entities.OrderBy(Function(e) e.Solde)
                Case "Solde_desc"
                    entities = entities.OrderByDescending(Function(e) e.Solde)

                Case "SoldeDispo"
                    entities = entities.OrderBy(Function(e) e.SoldeDisponible)
                Case "SoldeDispo_desc"
                    entities = entities.OrderByDescending(Function(e) e.SoldeDisponible)

                Case Else
                    entities = entities.OrderBy(Function(e) e.DateCreation)
                    Exit Select
            End Select

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)


            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")

            'Return View("IndexAgence", entities.ToPagedList(pageNumber, pageSize))

            Return View(entities.ToPagedList(pageNumber, pageSize))
        End Function

        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function IndexActivite(sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            'Dim exercices = db.Exercices.Include(Function(e) e.User)
            'Return View(exercices.ToList())

            ViewBag.CurrentSort = sortOrder

            If Not String.IsNullOrEmpty(searchString) Then
                page = 1
            Else
                searchString = currentFilter
            End If
            ViewBag.CurrentFilter = searchString

            Dim userAgenceId = GetCurrentUser.Personne.AgenceId


            Dim entities = From e In db.Personnes.OfType(Of Client)().Where(Function(i) i.AgenceId = userAgenceId).ToList

            If Not String.IsNullOrEmpty(searchString) Then
                entities = entities.Where(Function(e) e.Nom.ToUpper.Contains(searchString.ToUpper) Or e.Prenom.ToUpper.Contains(searchString.ToUpper) Or e.Sexe.ToUpper.Contains(searchString.ToUpper) Or e.Telephone.ToUpper.Contains(searchString.ToUpper) Or e.Adresse.ToUpper.Contains(searchString.ToUpper) Or e.Quartier.ToUpper.Contains(searchString.ToUpper))
            End If
            ViewBag.EnregCount = entities.Count

            Dim pageSize As Integer = ConfigurationManager.AppSettings("PageSize")
            Dim pageNumber As Integer = If(page, 1)

            ViewBag.dateDebut = Now.Date.ToString("d")
            ViewBag.dateFin = Now.Date.ToString("d")

            Return View("IndexAgence", entities.ToPagedList(pageNumber, pageSize))
        End Function

        ' GET: /Collecteur/Details/5
        <LocalizedAuthorize(Roles:="ADMINISTRATEUR")>
        Function Details(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim collecteur As Collecteur = db.Personnes.Find(id)
            If IsNothing(collecteur) Then
                Return HttpNotFound()
            End If
            Return View(collecteur)
        End Function

        Private Sub LoadComboBox(entityVM As ClientViewModel)
            Dim AgenceUserConnected = GetCurrentUser.Personne.AgenceId
            Dim PorteFeuilles = (From portefeuille In db.PorteFeuilles Where portefeuille.Collecteur.AgenceId = AgenceUserConnected Select portefeuille).OrderBy(Function(e) e.Libelle)
            Dim LesPorteFeuilles As New List(Of SelectListItem)
            For Each item In PorteFeuilles
                LesPorteFeuilles.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle})
            Next

            Dim SecteurActivites = (From sectAct In db.SecteurActivites Select sectAct).OrderBy(Function(e) e.Libelle)
            Dim LesSecteurActivites As New List(Of SelectListItem)
            For Each item In SecteurActivites
                LesSecteurActivites.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle})
            Next

            Dim Agences = (From agenc In db.Agences Select agenc).OrderBy(Function(e) e.Libelle)
            Dim LesAgences As New List(Of SelectListItem)
            For Each item In Agences
                LesAgences.Add(New SelectListItem With {.Value = item.Id, .Text = item.Libelle})
            Next

            entityVM.LesSecteursActivite = LesSecteurActivites
            entityVM.IDsAgence = LesAgences
            entityVM.LesPorteFeuilles = LesPorteFeuilles

        End Sub

        ' GET: /Collecteur/Create
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Create() As ActionResult
            'ViewBag.activeMenu = Resource.MenuDashBoard
            Dim entityVM As New ClientViewModel
            LoadComboBox(entityVM)
            Return View(entityVM)
        End Function

        ' POST: /Collecteur/Create
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Create(ByVal entityVM As ClientViewModel) As ActionResult
            LoadComboBox(entityVM)
            If ModelState.IsValid Then
                entityVM.AgenceId = GetCurrentUser.Personne.AgenceId
                entityVM.CodeSecret = Util.GetRandomString(entityVM.AgenceId, GetCurrentUser.Personne.Agence.SocieteId)
                entityVM.DateCreation = Now
                entityVM.UserId = GetCurrentUser.Id
                db.Clients.Add(entityVM.getEntity)
                Try
                    db.SaveChanges()
                    Return RedirectToAction("IndexAgence")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If
            Return View(entityVM)
        End Function

        ' GET: /Collecteur/Edit/5
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Edit(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim client As Client = db.Clients.Find(id)
            If IsNothing(client) Then
                Return HttpNotFound()
            End If
            Dim entityVM As New ClientViewModel(client)
            LoadComboBox(entityVM)
            Return View(entityVM)
        End Function

        ' POST: /Collecteur/Edit/5
        'Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        'plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Edit(ByVal entityVM As ClientViewModel) As ActionResult
            LoadComboBox(entityVM)
            If ModelState.IsValid Then
                db.Entry(entityVM.getEntity).State = EntityState.Modified
                Try
                    db.SaveChanges()
                    Return RedirectToAction("IndexAgence")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If
            Return View(entityVM)
        End Function


        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function EditActivite(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, Etat As Long, ClientId As Long) As ActionResult

            If ModelState.IsValid Then
                Dim entity = db.Clients.Find(ClientId)

                If (Etat = 0) Then
                    entity.Etat = True
                End If
                If (Etat = 1) Then
                    entity.Etat = False
                End If

                Try
                    db.Entry(entity).State = EntityState.Modified
                    db.SaveChanges()
                    Return RedirectToAction("IndexAgence")
                Catch ex As DbEntityValidationException
                    Util.GetError(ex, ModelState)
                Catch ex As Exception
                    Util.GetError(ex, ModelState)
                End Try
            End If

            Return View()
        End Function

        ' GET: /Collecteur/Delete/5
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function Delete(ByVal id As Long?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim collecteur As Client = db.Personnes.Find(id)
            If IsNothing(collecteur) Then
                Return HttpNotFound()
            End If
            Return View(collecteur)
        End Function

        ' POST: /Collecteur/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR")>
        Function DeleteConfirmed(ByVal id As Long) As ActionResult
            Dim collecteur As Client = db.Personnes.Find(id)
            db.Personnes.Remove(collecteur)
            Try
                db.SaveChanges()
                Return RedirectToAction("Index")
            Catch ex As DbEntityValidationException
                Util.GetError(ex, ModelState)
            Catch ex As Exception
                Util.GetError(ex, ModelState)
            End Try

            Return View()
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
