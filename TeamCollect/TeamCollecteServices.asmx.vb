Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports System.Web.Script.Services
Imports System.Web.Script.Serialization
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Data.Entity
Imports Microsoft.Owin.Security
Imports System.Threading.Tasks
Imports System.Data.Entity.Validation
Imports System.Data.SqlClient
Imports Newtonsoft.Json

' Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante.
<System.Web.Script.Services.ScriptService()>
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class TeamCollecteServices
    Inherits System.Web.Services.WebService

    Public Sub New()
        Me.New(New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(New ApplicationDbContext())))
    End Sub

    Public Sub New(manager As UserManager(Of ApplicationUser))
        UserManager = manager
    End Sub

    Public Property UserManager As UserManager(Of ApplicationUser)

    Private db As New ApplicationDbContext

    Private Property errorClass As New Erreur

    Private Property erreur As String = "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."
    Private Property erreurCollecte As String = "Une erreur est survenue pendant le traitement. Soit le client n'existe pas, soit vous n'êtes pas autorisé à effectuer cette opération, soit les caisses sont fermées; contactez l'administrateur pour plus d'informations."

    <WebMethod()>
     <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Sub LoginNew(username As String, pwd As String, adresseMac As String)
        adresseMac = adresseMac.Replace("-", ":")
        Dim RS As String = "["
        Dim user = UserManager.FindAsync(username, pwd)
        If user.Result IsNot Nothing Then
            'Dim leUser = user.First
            Dim UserPersonneId = user.Result.PersonneId
            Dim LeCollecteur = (From col In db.Personnes.OfType(Of Collecteur)() Where col.Id = UserPersonneId And col.AdrMac.Equals(adresseMac) Select col).ToList

            If (Not LeCollecteur.Count = 0) Then

                Dim journalCaisseExist = (From co In db.Personnes.OfType(Of Collecteur)() Where co.Id = UserPersonneId
                               From jc In db.JournalCaisses Where jc.CollecteurId = co.Id And jc.DateOuverture.HasValue And Not jc.DateCloture.HasValue Select jc).ToList

                If Not journalCaisseExist.Count = 0 Then
                    Dim utser As New BasicUserDatas()
                    utser.Id = user.Result.Id
                    utser.UserName = user.Result.UserName
                    utser.Nom = user.Result.Personne.Nom
                    utser.Prenom = user.Result.Personne.Prenom
                    Context.Response.Write(JsonConvert.SerializeObject(utser, Newtonsoft.Json.Formatting.Indented))
                Else 'If  journalCaisseExist.Count = 0 Then
                    errorClass.exc = "Aucun journal de caisse n'est ouvert pour ce compte."
                    Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
                End If

            Else 'If (LeCollecteur.Count = 0) Then
                errorClass.exc = "Vous n'êtes pas autorisé à vous connecter à partir de cet appareil."
                Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
            End If
        Else
            errorClass.exc = "Votre login et/ou mot de passe est (sont) incorrecte(s)."
            Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
        End If
    End Sub

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Sub GetInfosCLientNew(CodeClient As String)
        Dim RS As String = "["
        Dim clients = (From eachClient In db.Personnes.OfType(Of Client)() Where eachClient.Telephone.Equals(CodeClient) Or eachClient.CodeSecret.Equals(CodeClient) Or
                      eachClient.NumeroCompte.Equals(CodeClient) Select Success = "1", eachClient.Id, eachClient.Nom, eachClient.Prenom, eachClient.Pourcentage,
                      eachClient.Solde, eachClient.NumeroCompte, eachClient.Telephone, eachClient.CodeSecret).ToList

        If Not clients.Count = 0 Then
            Context.Response.Write(JsonConvert.SerializeObject(clients, Newtonsoft.Json.Formatting.Indented))
        Else
            errorClass.exc = "Le code client fourni ne correspond à aucun client."
            Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
        End If
    End Sub

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Sub GetCollecteNew(userId As String, LeNumCompteClient As String, MontantCollecte As Decimal)

        Dim RS As String = "["
        Dim personne = (From pers In db.Users Where pers.Id = userId Select pers).ToList

        If (Not personne.Count = 0) Then
            Dim collecteurId = personne.First.PersonneId
            Dim journalCaisses = (From journal In db.JournalCaisses Where journal.CollecteurId = collecteurId And journal.DateOuverture.HasValue And Not journal.DateCloture.HasValue Select journal).ToList
            Dim clients = (From cli In db.Personnes.OfType(Of Client)() Where (cli.NumeroCompte = LeNumCompteClient Or cli.CodeSecret = LeNumCompteClient Or cli.Telephone = LeNumCompteClient) Select cli).ToList
            Dim users = (From user In db.Users Where user.PersonneId = collecteurId Select user).ToList

            If (Not journalCaisses.Count = 0) And (Not clients.Count = 0) And (Not users.Count = 0) Then
                Dim leClient = clients.First
                Dim leJournalCaisse = journalCaisses.First
                Dim leUser = users.First

                Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
                Dim laDate As Date = Now
                Dim parameterList As New List(Of SqlParameter)()
                parameterList.Add(New SqlParameter("@ClientId", leClient.Id))
                parameterList.Add(New SqlParameter("@CollecteurId", collecteurId))
                parameterList.Add(New SqlParameter("@Montant", MontantCollecte))
                parameterList.Add(New SqlParameter("@DateOperation", laDate))
                parameterList.Add(New SqlParameter("@Pourcentage", leClient.Pourcentage))
                parameterList.Add(New SqlParameter("@EstTraiter", 0))
                parameterList.Add(New SqlParameter("@Etat", False))
                parameterList.Add(New SqlParameter("@DateCreation", Now))
                parameterList.Add(New SqlParameter("@UserId", userId))
                parameterList.Add(New SqlParameter("@JournalCaisseId", leJournalCaisse.Id))
                Dim parameters As SqlParameter() = parameterList.ToArray()

                Try
                    If (db.Database.ExecuteSqlCommand(myInsertQuery, parameters)) Then
                        Dim historiquesMouvements = (From h In db.HistoriqueMouvements Where (h.UserId = userId) Select NomClient = h.Client.Nom, PrenomClient = h.Client.Prenom,
                           NomCollecteur = h.Collecteur.Nom, PrenomCollecteur = h.Collecteur.Prenom, h.Montant, h.Pourcentage, h.DateOperation).ToList

                        Dim historique = historiquesMouvements.ElementAtOrDefault((historiquesMouvements.Count - 1))

                        Context.Response.Write(JsonConvert.SerializeObject(historique, Newtonsoft.Json.Formatting.Indented))
                    Else
                        errorClass.exc = "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur."
                        Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
                    End If

                Catch ex As DbEntityValidationException
                    Util.GetError(ex)
                    errorClass.exc = "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."
                    Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
                Catch ex As Exception
                    Util.GetError(ex)
                    errorClass.exc = "Une erreur est survenue pendant le traitement: veuillez contacter l'administrateur."
                    Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
                End Try
            Else
                errorClass.exc = "Une erreur est survenue pendant le traitement. Soit le client n'existe pas, soit vous n'êtes pas autorisé à effectuer cette opération, soit les caisses sont fermées; contactez l'administrateur pour plus d'informations."
                Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
            End If
        Else
            errorClass.exc = "Vous n'êtes pas autorisé à vous connecter. Veuillez contacter l'administrateur"
            Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
        End If


    End Sub

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Sub GetClientNew(nom As String, prenom As String, sexe As String, cni As String, telephone As String, adresse As String, quartier As String, userId As String, pourcentage As Decimal)

        Dim client As New Client
        client.Nom = nom
        client.Prenom = prenom
        client.Sexe = sexe
        client.CNI = cni
        client.CodeSecret = GetRandomString(nom, prenom)
        client.Telephone = telephone
        client.Adresse = adresse
        client.Quartier = quartier
        client.Pourcentage = pourcentage
        client.UserId = userId
        client.DateCreation = Now
        client.Etat = True
        Dim RS As String = "["
        db.Personnes.Add(client)

        Try
            db.SaveChanges()
            'RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "PrenomClientt" & Chr(34) & ":" & Chr(34) & client.Prenom & Chr(34) & "," & Chr(34) & "NomClient" & Chr(34) & ":" & Chr(34) & client.Nom & Chr(34) & "," & Chr(34) & "CodeClient" & Chr(34) & ":" & Chr(34) & client.CodeSecret & Chr(34) & "}"
            'RS += "]"
            'RS = Replace(RS, "", vbNullString)
            'Context.Response.Write(RS)
            Context.Response.Write(JsonConvert.SerializeObject(client, Newtonsoft.Json.Formatting.Indented))
        Catch ex As DbEntityValidationException
            Util.GetError(ex)
            errorClass.exc = "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur."
            Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
        Catch ex As Exception
            Util.GetError(ex)
            errorClass.exc = "Une erreur est survenue pendant l'exécution de la requête: veuillez contacter l'administrateur."
            Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
        End Try

    End Sub

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Sub GetOperationCollecteurNew(userId As String)
        Dim RS As String = "["
        Dim personne = (From pers In db.Users Where pers.Id = userId Select pers).ToList
        Dim collecteurId = personne.First.PersonneId
        Dim dateToday As Date = Now.Date

        Dim listMvts = (From mvts In db.HistoriqueMouvements Where (mvts.CollecteurId = collecteurId And mvts.DateOperation.Value.Year = dateToday.Year And
                                                                    mvts.DateOperation.Value.Month = dateToday.Month And mvts.DateOperation.Value.Day = dateToday.Day) Select mvts.Id, NomCollecteur = mvts.Collecteur.Nom,
                                                                PrenomCollecteur = mvts.Collecteur.Prenom, NomClient = mvts.Client.Nom, PrenomClient = mvts.Client.Prenom, mvts.Montant, mvts.DateOperation).ToList
        If (Not listMvts.Count = 0) Then
            Context.Response.Write(JsonConvert.SerializeObject(listMvts, Newtonsoft.Json.Formatting.Indented))
        Else
            errorClass.exc = "Vous n'avez effectué aucune action aujourd'hui."
            Context.Response.Write(JsonConvert.SerializeObject(errorClass, Newtonsoft.Json.Formatting.Indented))
        End If
    End Sub

    <WebMethod()>
     <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Sub Login(username As String, pwd As String, adresseMac As String)
        adresseMac = adresseMac.Replace("-", ":")
        Dim RS As String = "["
        Dim user = UserManager.FindAsync(username, pwd)
        If user.Result IsNot Nothing Then
            'Dim leUser = user.First
            Dim UserPersonneId = user.Result.PersonneId
            Dim LeCollecteur = (From col In db.Personnes.OfType(Of Collecteur)() Where col.Id = UserPersonneId And col.AdrMac.Equals(adresseMac) Select col).ToList

            If (Not LeCollecteur.Count = 0) Then

                Dim journalCaisseExist = (From co In db.Personnes.OfType(Of Collecteur)() Where co.Id = UserPersonneId
                               From jc In db.JournalCaisses Where jc.CollecteurId = co.Id And jc.DateOuverture.HasValue And Not jc.DateCloture.HasValue Select jc).ToList

                If Not journalCaisseExist.Count = 0 Then
                    RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & user.Result.Id & Chr(34) & "," & Chr(34) & "UserName" & Chr(34) & ":" & Chr(34) & user.Result.UserName & Chr(34) & "}"
                    RS += "]"
                    RS = Replace(RS, "", vbNullString)
                    Context.Response.Write(RS)
                Else 'If  journalCaisseExist.Count = 0 Then
                    RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Aucun journal de caisse n'est ouvert pour ce compte." & Chr(34) & "}"
                    RS += "]"
                    RS = Replace(RS, "", vbNullString)
                    Context.Response.Write(RS)
                End If

            Else 'If (LeCollecteur.Count = 0) Then
                RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Vous n'êtes pas autorisé à vous connecter à partir de cet appareil." & Chr(34) & "}"
                RS += "]"
                RS = Replace(RS, "", vbNullString)
                Context.Response.Write(RS)
            End If
        Else
            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Votre login et/ou mot de passe est (sont) incorrecte(s)." & Chr(34) & "}"
            RS += "]"
            RS = Replace(RS, "", vbNullString)
            Context.Response.Write(RS)
        End If
    End Sub

    <WebMethod()>
    <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Sub GetInfosCLient(CodeClient As String)
        Dim RS As String = "["
        Dim clients = (From eachClient In db.Personnes.OfType(Of Client)() Where eachClient.Telephone.Equals(CodeClient) Or eachClient.CodeSecret.Equals(CodeClient) Or
                      eachClient.NumeroCompte.Equals(CodeClient) Select eachClient).ToList
        If Not clients.Count = 0 Then
            'Dim client = clients.First
            For Each client In clients
                'RS += "["
                RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "Nom" & Chr(34) & ":" & Chr(34) & client.Nom & " " & client.Prenom & Chr(34) & "," & Chr(34) & _
                    "Compte" & Chr(34) & ":" & Chr(34) & client.NumeroCompte & Chr(34) & "," & Chr(34) & "Telephone" & Chr(34) & ":" & Chr(34) & client.Telephone & Chr(34) & "," & Chr(34) & _
                    "CodeSecret" & Chr(34) & ":" & Chr(34) & client.CodeSecret & Chr(34) & "}"
                RS = Replace(RS, "", vbNullString)
                RS += ","
            Next
            RS = RS.Remove(RS.Length - 1)
            RS += "]"
            Context.Response.Write(RS)

            'RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "Nom" & Chr(34) & ":" & Chr(34) & leClient.Nom & Chr(34) & "," & Chr(34) & "Prenom" & Chr(34) & ":" & Chr(34) & leClient.Prenom & Chr(34) & "," & Chr(34) & "Telephone" & Chr(34) & ":" & Chr(34) & leClient.Telephone & Chr(34) & "}"
            'RS += "]"
            'RS = Replace(RS, "", vbNullString)
            'Context.Response.Write(RS)
        Else
            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Le code client fourni ne correspond à aucun client." & Chr(34) & "}"
            RS += "]"
            RS = Replace(RS, "", vbNullString)
            Context.Response.Write(RS)
        End If
    End Sub

    <WebMethod()>
     <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Sub Retrait(userId As String, LeNumCompteClient As String, MontantCollecte As Decimal)

        Dim RS As String = "["
        Dim personne = (From pers In db.Users Where pers.Id = userId Select pers).ToList

        If (Not personne.Count = 0) Then
            Dim collecteurId = personne.First.PersonneId
            Dim journalCaisses = (From journal In db.JournalCaisses Where journal.CollecteurId = collecteurId And journal.DateOuverture.HasValue And Not journal.DateCloture.HasValue Select journal).ToList
            Dim clients = (From cli In db.Personnes.OfType(Of Client)() Where (cli.NumeroCompte = LeNumCompteClient Or cli.CodeSecret = LeNumCompteClient Or cli.Telephone = LeNumCompteClient) Select cli).ToList
            Dim users = (From user In db.Users Where user.PersonneId = collecteurId Select user).ToList

            If (Not journalCaisses.Count = 0) And (Not clients.Count = 0) And (Not users.Count = 0) Then
                Dim leClient = clients.First
                Dim leJournalCaisse = journalCaisses.First
                Dim leUser = users.First

                If ((leClient.Solde >= (-1 * MontantCollecte))) Then
                    Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
                    Dim laDate As Date = Now
                    Dim parameterList As New List(Of SqlParameter)()
                    parameterList.Add(New SqlParameter("@ClientId", leClient.Id))
                    parameterList.Add(New SqlParameter("@CollecteurId", collecteurId))
                    parameterList.Add(New SqlParameter("@Montant", MontantCollecte))
                    parameterList.Add(New SqlParameter("@DateOperation", laDate))
                    parameterList.Add(New SqlParameter("@Pourcentage", leClient.Pourcentage))
                    parameterList.Add(New SqlParameter("@EstTraiter", 0))
                    parameterList.Add(New SqlParameter("@Etat", False))
                    parameterList.Add(New SqlParameter("@DateCreation", Now))
                    parameterList.Add(New SqlParameter("@UserId", leUser.Id))
                    parameterList.Add(New SqlParameter("@JournalCaisseId", leJournalCaisse.Id))
                    Dim parameters As SqlParameter() = parameterList.ToArray()

                    Try
                        db.Database.ExecuteSqlCommand(myInsertQuery, parameters)
                        'db.SaveChanges()
                        Dim historiquesMouvements = (From h In db.HistoriqueMouvements Select h).ToList
                        Dim historiqueMouvement = historiquesMouvements.LastOrDefault
                        RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "Client" & Chr(34) & ":" & Chr(34) & leClient.Prenom & " " & leClient.Nom & Chr(34) & "," & Chr(34) & _
                            "Collecteur:" & Chr(34) & ":" & Chr(34) & personne.First.Personne.Prenom & " " & personne.First.Personne.Prenom & Chr(34) & "," & Chr(34) & _
                            "Montant" & Chr(34) & ":" & Chr(34) & MontantCollecte & Chr(34) & "," & Chr(34) & "Pourcentage" & Chr(34) & ":" & Chr(34) & leClient.Pourcentage & Chr(34) & "," & Chr(34) & _
                            "DateOperation" & Chr(34) & ":" & Chr(34) & laDate & Chr(34) & "}"
                        RS += "]"
                        RS = Replace(RS, "", vbNullString)
                        Context.Response.Write(RS)
                    Catch ex As DbEntityValidationException
                        Util.GetError(ex)
                        RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & erreur & Chr(34) & "}"
                        RS += "]"
                        RS = Replace(RS, "", vbNullString)
                        Context.Response.Write(RS)
                    Catch ex As Exception
                        Util.GetError(ex)
                        RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & erreur & Chr(34) & "}"
                        RS += "]"
                        RS = Replace(RS, "", vbNullString)
                        Context.Response.Write(RS)
                    End Try
                Else
                    RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Le solde de votre compte (solde= " & leClient.Solde & ") est inférieur au montant demandé." & Chr(34) & "}"
                    RS += "]"
                    RS = Replace(RS, "", vbNullString)
                    Context.Response.Write(RS)
                End If


            Else
                RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & erreurCollecte & Chr(34) & "}"
                RS += "]"
                RS = Replace(RS, "", vbNullString)
                Context.Response.Write(RS)
            End If
        Else
            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Vous n'êtes pas autorisé à vous connecter. Veuillez contacter l'administrateur" & Chr(34) & "}"
            RS += "]"
            RS = Replace(RS, "", vbNullString)
            Context.Response.Write(RS)
        End If


    End Sub

    <WebMethod()>
     <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Sub GetCollecte(userId As String, LeNumCompteClient As String, MontantCollecte As Decimal)

        Dim RS As String = "["
        Dim personne = (From pers In db.Users Where pers.Id = userId Select pers).ToList

        If (Not personne.Count = 0) Then
            Dim collecteurId = personne.First.PersonneId
            Dim journalCaisses = (From journal In db.JournalCaisses Where journal.CollecteurId = collecteurId And journal.DateOuverture.HasValue And Not journal.DateCloture.HasValue Select journal).ToList
            Dim clients = (From cli In db.Personnes.OfType(Of Client)() Where (cli.NumeroCompte = LeNumCompteClient Or cli.CodeSecret = LeNumCompteClient Or cli.Telephone = LeNumCompteClient) Select cli).ToList
            Dim users = (From user In db.Users Where user.PersonneId = collecteurId Select user).ToList

            If (Not journalCaisses.Count = 0) And (Not clients.Count = 0) And (Not users.Count = 0) Then
                Dim leClient = clients.First
                Dim leJournalCaisse = journalCaisses.First
                Dim leUser = users.First

                Dim myInsertQuery As String = "INSERT INTO HistoriqueMouvement (ClientId, CollecteurId, Montant, DateOperation, Pourcentage, EstTraiter, Etat, DateCreation, UserId, JournalCaisseId) VALUES (@ClientId, @CollecteurId, @Montant, @DateOperation, @Pourcentage, @EstTraiter, @Etat, @DateCreation, @UserId, @JournalCaisseId)"
                Dim laDate As Date = Now
                Dim parameterList As New List(Of SqlParameter)()
                parameterList.Add(New SqlParameter("@ClientId", leClient.Id))
                parameterList.Add(New SqlParameter("@CollecteurId", collecteurId))
                parameterList.Add(New SqlParameter("@Montant", MontantCollecte))
                parameterList.Add(New SqlParameter("@DateOperation", laDate))
                parameterList.Add(New SqlParameter("@Pourcentage", leClient.Pourcentage))
                parameterList.Add(New SqlParameter("@EstTraiter", 0))
                parameterList.Add(New SqlParameter("@Etat", False))
                parameterList.Add(New SqlParameter("@DateCreation", Now))
                parameterList.Add(New SqlParameter("@UserId", leUser.Id))
                parameterList.Add(New SqlParameter("@JournalCaisseId", leJournalCaisse.Id))
                Dim parameters As SqlParameter() = parameterList.ToArray()

                Try
                    db.Database.ExecuteSqlCommand(myInsertQuery, parameters)
                    'db.SaveChanges()
                    Dim historiquesMouvements = (From h In db.HistoriqueMouvements Select h).ToList
                    Dim historiqueMouvement = historiquesMouvements.LastOrDefault
                    RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "Client" & Chr(34) & ":" & Chr(34) & leClient.Prenom & " " & leClient.Nom & Chr(34) & "," & Chr(34) & _
                        "Collecteur:" & Chr(34) & ":" & Chr(34) & personne.First.Personne.Prenom & " " & personne.First.Personne.Prenom & Chr(34) & "," & Chr(34) & _
                        "Montant" & Chr(34) & ":" & Chr(34) & MontantCollecte & Chr(34) & "," & Chr(34) & "Pourcentage" & Chr(34) & ":" & Chr(34) & leClient.Pourcentage & Chr(34) & "," & Chr(34) & _
                        "DateOperation" & Chr(34) & ":" & Chr(34) & laDate & Chr(34) & "}"
                    RS += "]"
                    RS = Replace(RS, "", vbNullString)
                    Context.Response.Write(RS)
                Catch ex As DbEntityValidationException
                    Util.GetError(ex)
                    RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & erreur & Chr(34) & "}"
                    RS += "]"
                    RS = Replace(RS, "", vbNullString)
                    Context.Response.Write(RS)
                Catch ex As Exception
                    Util.GetError(ex)
                    RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & erreur & Chr(34) & "}"
                    RS += "]"
                    RS = Replace(RS, "", vbNullString)
                    Context.Response.Write(RS)
                End Try
            Else
                RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & erreurCollecte & Chr(34) & "}"
                RS += "]"
                RS = Replace(RS, "", vbNullString)
                Context.Response.Write(RS)
            End If
        Else
            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Vous n'êtes pas autorisé à vous connecter. Veuillez contacter l'administrateur" & Chr(34) & "}"
            RS += "]"
            RS = Replace(RS, "", vbNullString)
            Context.Response.Write(RS)
        End If


    End Sub

    Public Function GetRandomString(ByVal nom As String, ByVal prenom As String) As String
        Dim isItOk As Boolean = False
        Dim tbl() As String 'le tableau des caracteres
        Dim strx As String = "" 'la chaine qu'on va créer"
        tbl = Split("0,1,2,3,4,5,6,7,8,9", ",") 'Vous pouvez ajouter/suppr des caracteres .
        Dim leCodeSecret As String = ""

        Do
            Randomize()
            For I = 1 To 4
                strx = strx & tbl(Int((UBound(tbl) + 1) * Rnd()))
            Next I

            leCodeSecret = nom.Substring(0, 1).ToUpper
            If Not (String.IsNullOrEmpty(prenom)) Then
                leCodeSecret = leCodeSecret & prenom.Substring(0, 1).ToUpper & strx
            Else
                leCodeSecret = leCodeSecret & nom.Substring(1, 1).ToUpper & strx
            End If
            Dim countCodeSecret = (From cli In db.Personnes.OfType(Of Client)() Where cli.CodeSecret.Equals(leCodeSecret) Select cli).ToList
            isItOk = IIf(countCodeSecret.Count = 0, True, False)
        Loop Until isItOk

        Return leCodeSecret
    End Function

    <WebMethod()>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Sub GetClient(nom As String, prenom As String, sexe As String, cni As String, telephone As String, adresse As String, quartier As String, userId As String, pourcentage As Decimal)

        Dim client As New Client
        client.Nom = nom
        client.Prenom = prenom
        client.Sexe = sexe
        client.CNI = cni
        client.CodeSecret = GetRandomString(nom, prenom)
        client.Telephone = telephone
        client.Adresse = adresse
        client.Quartier = quartier
        client.Pourcentage = pourcentage
        client.UserId = userId
        client.DateCreation = Now
        client.Etat = True
        Dim RS As String = "["
        db.Personnes.Add(client)

        Try
            db.SaveChanges()
            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "PrenomClientt" & Chr(34) & ":" & Chr(34) & client.Prenom & Chr(34) & "," & Chr(34) & "NomClient" & Chr(34) & ":" & Chr(34) & client.Nom & Chr(34) & "," & Chr(34) & "CodeClient" & Chr(34) & ":" & Chr(34) & client.CodeSecret & Chr(34) & "}"
            RS += "]"
            RS = Replace(RS, "", vbNullString)
            Context.Response.Write(RS)
        Catch ex As DbEntityValidationException
            Util.GetError(ex)
            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & ex.ToString & Chr(34) & "}"
            RS += "]"
            RS = Replace(RS, "", vbNullString)
            Context.Response.Write(RS)
        Catch ex As Exception
            Util.GetError(ex)
            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & ex.ToString & Chr(34) & "}"
            RS += "]"
            RS = Replace(RS, "", vbNullString)
            Context.Response.Write(RS)
        End Try

    End Sub

    <WebMethod()>
   <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
    Public Sub GetOperationCollecteur(userId As String)
        Dim RS As String = "["
        Dim personne = (From pers In db.Users Where pers.Id = userId Select pers).ToList
        Dim collecteurId = personne.First.PersonneId
        Dim dateToday As Date = Now.Date

        Dim listMvts = (From mvts In db.HistoriqueMouvements Where (mvts.CollecteurId = collecteurId And mvts.DateOperation.Value.Year = dateToday.Year And
                                                                    mvts.DateOperation.Value.Month = dateToday.Month And mvts.DateOperation.Value.Day = dateToday.Day) Select mvts).ToList
        If (Not listMvts.Count = 0) Then
            For Each mvt In listMvts
                'RS += "["
                RS += "{" & Chr(34) & "CodeOperation" & Chr(34) & ":" & Chr(34) & mvt.Id & Chr(34) & "," & Chr(34) & "Collecteur" & Chr(34) & ":" & Chr(34) & mvt.Collecteur.Prenom & " " & mvt.Collecteur.Nom & Chr(34) & "," & Chr(34) & "Client" & Chr(34) & ":" & Chr(34) & mvt.Client.Prenom & " " & mvt.Client.Nom & Chr(34) & "," & Chr(34) & "Montant" & Chr(34) & ":" & Chr(34) & mvt.Montant.ToString & Chr(34) & "}"
                RS = Replace(RS, "", vbNullString)
                RS += ","
            Next
            RS = RS.Remove(RS.Length - 1)
            RS += "]"
            Context.Response.Write(RS)
        Else
            RS += "{" & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Vous n'avez effectué aucune action aujourd'hui." & Chr(34) & "}"
            RS += "]"
            RS = Replace(RS, "", vbNullString)
            Context.Response.Write(RS)
        End If
    End Sub

    <WebMethod()> _
    Public Function HelloWorld() As String
        Return "Welcome To Team Collecte Web Services"
    End Function

End Class

Public Class Erreur
    Public Property Success As String = "0"
    Public Property exc As String = ""
End Class

Public Class BasicUserDatas
    Public Property Statut As String = "1"
    Public Property Id As String = ""
    Public Property UserName As String = ""
    Public Property Nom As String = ""
    Public Property Prenom As String = ""
End Class

Public Class Operation
    Public Property Id As Long
    Public Property NomCollecteur As String
    Public Property PrenomCollecteur As String
    Public Property NomClient As String
    Public Property PrenomClient As String
    Public Property Montant As String
    Public Property DateOperation As DateTime
End Class

'<WebMethod()>
'   <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
'Public Async Function CollecteurLogin(username As String, pwd As String) As Task
'    Dim RS As String = "["
'    Dim user = Await UserManager.FindAsync(username, pwd)
'    If user IsNot Nothing Then
'        'Dim leUser = user.First
'        Dim UserPersonneId = user.PersonneId
'        Dim journalCaisseExist = (From co In db.Personnes.OfType(Of Collecteur)() Where co.Id = UserPersonneId
'                       From jc In db.JournalCaisses Where jc.CollecteurId = co.Id And jc.DateOuverture.HasValue And Not jc.DateCloture.HasValue Select jc).ToList

'        If Not journalCaisseExist.Count = 0 Then
'            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & user.Id & Chr(34) & "," & Chr(34) & "UserName" & Chr(34) & ":" & Chr(34) & user.UserName & Chr(34) & "}"
'            RS += "]"
'            RS = Replace(RS, "", vbNullString)
'            Context.Response.Write(RS)
'        Else
'            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Aucun journal de caisse n'est ouvert pour ce compte." & Chr(34) & "}"
'            RS += "]"
'            RS = Replace(RS, "", vbNullString)
'            Context.Response.Write(RS)
'        End If
'    Else
'        RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Votre login et/ou mot de passe est (sont) incorrecte(s)." & Chr(34) & "}"
'        RS += "]"
'        RS = Replace(RS, "", vbNullString)
'        Context.Response.Write(RS)
'    End If
'End Function



'<WebMethod()>
'   <ScriptMethod(UseHttpGet:=True, ResponseFormat:=ResponseFormat.Json)>
'Public Sub LoginOld(username As String, pwd As String)
'    Dim RS As String = "["

'    Dim user = (From u In db.Users.Where(Function(e) e.UserName = username))
'    user = user.Where(Function(e) e.CodeSecret = pwd)

'    'Dim appUser = UserManager.FindAsync(model.UserName, model.Password)
'    If Not user.Count = 0 Then
'        Dim leUser = user.First
'        Dim UserPersonneId = leUser.PersonneId
'        Dim journalCaisseExist = (From co In db.Personnes.OfType(Of Collecteur)() Where co.Id = UserPersonneId
'                       From jc In db.JournalCaisses Where jc.CollecteurId = co.Id And jc.DateOuverture.HasValue And Not jc.DateCloture.HasValue Select jc).ToList

'        If Not journalCaisseExist.Count = 0 Then

'            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "1" & Chr(34) & "," & Chr(34) & "Id" & Chr(34) & ":" & Chr(34) & leUser.Id & Chr(34) & "," & Chr(34) & "UserName" & Chr(34) & ":" & Chr(34) & leUser.UserName & Chr(34) & "}"
'            RS += "]"
'            RS = Replace(RS, "", vbNullString)
'            Context.Response.Write(RS)
'        Else
'            RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Aucun journal de caisse n'est ouvert pour ce compte." & Chr(34) & "}"
'            RS += "]"
'            RS = Replace(RS, "", vbNullString)
'            Context.Response.Write(RS)
'        End If
'    Else
'        RS += "{" & Chr(34) & "Success" & Chr(34) & ":" & Chr(34) & "0" & Chr(34) & "," & Chr(34) & "exc" & Chr(34) & ":" & Chr(34) & "Votre login et/ou mot de passe est (sont) incorrecte(s)." & Chr(34) & "}"
'        RS += "]"
'        RS = Replace(RS, "", vbNullString)
'        Context.Response.Write(RS)
'    End If
'End Sub


'Dim result = JsonConvert.SerializeObject(listMvts, Newtonsoft.Json.Formatting.Indented)
'Dim listOperations As New List(Of Operation)()
'listOperations = JsonConvert.DeserializeObject(Of List(Of Operation))(result)
'Context.Response.Write(JsonConvert.SerializeObject(listOperations, Newtonsoft.Json.Formatting.Indented))