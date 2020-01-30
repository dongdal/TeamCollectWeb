Imports Microsoft.AspNet.Identity
Imports Microsoft.Reporting.WebForms
Imports System.Data.SqlClient
Imports System.IO

Public Class NewReport
    Inherits System.Web.UI.Page

    Public Shared ReadOnly Property ConnectionString() As String
        Get
            Return ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        End Get
    End Property

    Private db As New ApplicationDbContext

    Private Function GetCurrentUser() As ApplicationUser
        Dim id = User.Identity.GetUserId
        Dim aspuser = db.Users.Find(id)
        Return aspuser
    End Function

    Private Function ConvertDate(dateConvert As Date) As String
        Dim mydate() = dateConvert.ToString.Split(" ")
        Dim time = mydate(1)
        Dim tempoDateTable() = mydate(0).Split("/")
        'Dim day = dateConvert.Day
        'Dim month = dateConvert.Month
        'Dim year = dateConvert.Year

        Dim jour = tempoDateTable(0)
        Dim mois = tempoDateTable(1)
        Dim annee = tempoDateTable(2)
        'Dim resultDate = annee & "-" & mois & "-" + jour & " " & mydate(1)
        Dim resultDate = annee & "-" & mois & "-" + jour
        Return resultDate
    End Function

    Private Function GetDataSansId(viewName As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} ", viewName)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                'macmd.Parameters.AddWithValue("@id", id_value)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using
        Return matable
    End Function

    Private Function GetData2Param(viewName As String, ByVal parmValue1 As String, ByVal ParaName1 As String, ByVal parmValue2 As String, ByVal ParaName2 As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} Where {1} = @p1 and {2} = @p2 ", viewName, ParaName1, ParaName2)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@p1", parmValue1)
                macmd.Parameters.AddWithValue("@p2", parmValue2)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetDataParam(viewName As String, ByVal parmValue1 As String, ByVal ParaName1 As String, ByVal parmValue2 As String, ByVal ParaName2 As String, ByVal parmValue3 As String, ByVal ParaName3 As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} Where {1} = @p1 and {2} = @p2 and {3} = @p3 ", viewName, ParaName1, ParaName2, ParaName3)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@p1", parmValue1)
                macmd.Parameters.AddWithValue("@p2", parmValue2)
                macmd.Parameters.AddWithValue("@p3", parmValue3)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetDataParam(viewName As String, ByVal parmValue1 As String, ByVal ParaName1 As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} WHERE {1} = @p1 ", viewName, ParaName1)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@p1", parmValue1)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function


    Private Function GetData3Param(viewName As String, ByVal parmValue1 As String, ByVal ParaName1 As String, ByVal parmValue2 As String, ByVal ParaName2 As String, ByVal parmValue3 As String, ByVal ParaName3 As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} Where {1} = @p1 and {2} = @p2 and {3} = @p3 ", viewName, ParaName1, ParaName2, ParaName3)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@p1", parmValue1)
                macmd.Parameters.AddWithValue("@p2", parmValue2)
                macmd.Parameters.AddWithValue("@p3", parmValue3)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetData(viewName As String, ByVal id_value As String, Optional idName As String = "Id") As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} Where {1} = @id ", viewName, idName)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@id", id_value)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetDataHistorique(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String, ByVal IdLechamp As String, ByVal IdValue As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim datefilter = lechampdate & " >= (CONVERT(datetime2, @DateDebut, 120)) AND  " & lechampdate & " <= (CONVERT(datetime2, @DateFin, 120))"
        Dim cmd As String = ""
        cmd = String.Format(" SELECT * FROM {0} Where (" & datefilter & " AND {1} = {2} ) ", viewName, IdLechamp, IdValue)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@DateDebut", datedebutValue + " 00:00:00")
                macmd.Parameters.AddWithValue("@DateFin", datefinValue + " 23:59:59")
                'macmd.Parameters.AddWithValue("@AgenceId", AgenceId)
                'macmd.Parameters.AddWithValue("@p3", IdValue)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetDataOperations(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String, ByVal IdLechamp As String, ByVal IdValue As String, AgenceId As String, AgenceIdField As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim datefilter = lechampdate & " >= (CONVERT(datetime2, @DateDebut, 120)) AND  " & lechampdate & " <= (CONVERT(datetime2, @DateFin, 120))"
        Dim cmd As String = ""

        If Not (String.IsNullOrEmpty(AgenceId)) And Not (User.IsInRole("ADMINISTRATEUR") Or User.IsInRole("SA") Or User.IsInRole("MANAGER")) Then
            cmd = String.Format(" SELECT * FROM {0} Where (" & datefilter & " AND {2} LIKE '" & IdValue & "%'  AND {3} = " & AgenceId & ") ", viewName, lechampdate, IdLechamp, AgenceIdField)
        Else
            If User.IsInRole("ADMINISTRATEUR") Or User.IsInRole("SA") Or User.IsInRole("MANAGER") Then
                cmd = String.Format(" SELECT * FROM {0} Where (" & datefilter & " AND {2} LIKE '" & IdValue & "%' ) ", viewName, lechampdate, IdLechamp)
            Else
                AgenceId = GetCurrentUser.Personne.AgenceId
                cmd = String.Format(" SELECT * FROM {0} Where (" & datefilter & " AND {2} LIKE '" & IdValue & "%'  AND {3} = " & AgenceId & ") ", viewName, lechampdate, IdLechamp, AgenceIdField)
            End If
        End If

        ' Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} = @p1) ", viewName, ParaName1)
        'datefilter = " AND  " & DateFilterFieldName & " >= (CONVERT(datetime2, @DateDebut, 120)) AND  " & DateFilterFieldName & " <= (CONVERT(datetime2, @DateFin, 120)) "

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@DateDebut", datedebutValue + " 00:00:00")
                macmd.Parameters.AddWithValue("@DateFin", datefinValue + " 23:59:59")
                'macmd.Parameters.AddWithValue("@AgenceId", AgenceId)
                'macmd.Parameters.AddWithValue("@p3", IdValue)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetDataExtraFilter(viewName As String, ByVal datedebutValue As String, ByVal datefilter As String, ByVal datefinValue As String, ByVal ExtraFilter As String, ByVal ExtraFilterIdValue As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} Where (" & datefilter & " AND {1} = {2}) ", viewName, ExtraFilter, ExtraFilterIdValue)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@DateDebut", datedebutValue + " 00:00:00")
                macmd.Parameters.AddWithValue("@DateFin", datefinValue + " 23:59:59")
                'macmd.Parameters.AddWithValue("@p3", IdValue)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetData(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String, ByVal IdLechamp As String, ByVal IdValue As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim datefilter = lechampdate & " >= (CONVERT(datetime2, @DateDebut, 120)) AND  " & lechampdate & " <= (CONVERT(datetime2, @DateFin, 120))"
        Dim cmd As String = String.Format(" SELECT * FROM {0} Where (" & datefilter & " AND {2} = @p3) ", viewName, lechampdate, IdLechamp)
        ' Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} = @p1) ", viewName, ParaName1)
        'datefilter = " AND  " & DateFilterFieldName & " >= (CONVERT(datetime2, @DateDebut, 120)) AND  " & DateFilterFieldName & " <= (CONVERT(datetime2, @DateFin, 120)) "

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@DateDebut", datedebutValue + " 00:00:00")
                macmd.Parameters.AddWithValue("@DateFin", datefinValue + " 23:59:59")
                macmd.Parameters.AddWithValue("@p3", IdValue)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetDataIntervalDate(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} >= @p1 AND {1}  <= @p2 ) ", viewName, lechampdate)
        ' Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} = @p1) ", viewName, ParaName1)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@p1", datedebutValue + " 00:00:00")
                macmd.Parameters.AddWithValue("@p2", datefinValue + " 23:59:59")
                macmd.CommandTimeout = 0

                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function LastOperationIntervalDate(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String, ByVal IdLechamp As String, ByVal IdValue As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} Where ( (({1} <= @p1 OR {1} >= @p2) OR ({1} IS NULL)) AND {2} = @p3 ) ", viewName, lechampdate, IdLechamp)
        ' Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} = @p1) ", viewName, ParaName1)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@p1", datedebutValue + " 00:00:00")
                macmd.Parameters.AddWithValue("@p2", datefinValue + " 23:59:59")
                macmd.Parameters.AddWithValue("@p3", IdValue)
                macmd.CommandTimeout = 0

                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function

    Private Function GetDataIntervalDateById(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String, ByVal IdLechamp As String, ByVal IdValue As String) As DataTable
        Dim matable As DataTable = Nothing
        Dim colonne As String = ""

        Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} >= @p1 AND {1}  <= @p2 AND {2} = @p3  ) ", viewName, lechampdate, IdLechamp)
        ' Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} = @p1) ", viewName, ParaName1)

        Using myConnection As New SqlConnection(ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                macmd.Parameters.AddWithValue("@p1", datedebutValue + " 00:00:00")
                macmd.Parameters.AddWithValue("@p2", datefinValue + " 23:59:59")
                macmd.Parameters.AddWithValue("@p3", IdValue)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader
                        matable = New DataTable
                        matable.Load(reader)
                        reader.Close()
                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using
            myConnection.Close()
        End Using

        Return matable
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim type = Request("type")
            Select Case type

                Case "CommissionCollectriceAvecGrilleRemuneration"
                    Dim Mois = Request("Mois")
                    Dim Annee = Request("Annee")
                    Dim AgenceId = Request("AgenceId")

                    ShowReportCommissionCollectriceAvecGrilleRemuneration("CommissionCollectriceAvecGrilleRemuneration", GetDataParam("CommissionCollectriceAvecGrilleRemuneration", AgenceId, "AgenceId", Mois, "Mois", Annee, "Annee"))

                Case "FicheCommissionsCollecteurs"
                    Dim Mois = Request("Mois")
                    Dim Annee = Request("Annee")
                    Dim AgenceId = Request("AgenceId")

                    ShowReportFicheCommissionsCollecteurs("FicheCommissionsCollecteurs", GetDataParam("FicheCommissionsCollecteurs", AgenceId, "AgenceId", Mois, "Mois", Annee, "Annee"))

                Case "FicheCommissionsParPorteFeuille"
                    Dim Mois = Request("Mois")
                    Dim Annee = Request("Annee")
                    Dim AgenceId = Request("AgenceId")

                    ShowReportFicheCommissionsParPorteFeuille("FicheCommissionsParPorteFeuille", GetDataParam("FicheCommissionsParPorteFeuille", AgenceId, "AgenceId", Mois, "Mois", Annee, "Annee"))

                Case "FicheCommissionsParPorteFeuilleSimplifiee"
                    Dim Mois = Request("Mois")
                    Dim Annee = Request("Annee")
                    Dim AgenceId = Request("AgenceId")

                    ShowReportFicheCommissionsParPorteFeuilleSimplifiee("FicheCommissionsParPorteFeuilleSimplifiee", GetDataParam("FicheCommissionsParPorteFeuille", AgenceId, "AgenceId", Mois, "Mois", Annee, "Annee"))

                Case "AgiosParClient"
                    Dim Mois = Request("Mois")
                    Dim Annee = Request("Annee")
                    Dim AgenceId = Request("AgenceId")

                    ShowReportAgiosParClient("AgiosCustomers", GetData3Param("AgiosCustomers", AgenceId, "AgenceId", Mois, "Mois", Annee, "Annee"), AgenceId, Mois, Annee)

                Case "FicheCollecte"
                    Dim JournalCaisseId = Request("JournalCaisseId")
                    ShowReportFicheJournaliere("FicheCollecteJournaliere", GetData("FicheCollecteJournaliere", JournalCaisseId, "JournalCaisseId"))

                Case "ListeClientParCollectrice"
                    Dim CollecteurId = Request("CollecteurId")
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)
                    Dim LeChampDate = "DateCreation"
                    Dim ExtraFilter = "IdCollecteur"
                    Dim datefilter = LeChampDate & " >= (CONVERT(datetime2, @DateDebut, 120)) AND  " & LeChampDate & " <= (CONVERT(datetime2, @DateFin, 120)) "

                    If Not (User.IsInRole("ADMINISTRATEUR") Or User.IsInRole("MANAGER") Or User.IsInRole("SA")) Then
                        datefilter = datefilter & " AND AgenceId=" & AppSession.AgenceId.ToString()
                    End If

                    ShowReportListeClientParCollectrice("ListeClientParCollectrice", GetDataExtraFilter("vListeClientParCollectrice", DateDebut, datefilter, DateFin, ExtraFilter, CollecteurId), DateDebut, DateFin)

                Case "FicheCollecteParPeriode"
                    Dim CollecteurId = Request("CollecteurId")
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)
                    ShowReportFicheJournaliereParPeriode("FicheCollecteJournaliereParPeriode", GetData("FicheCollecteJournaliereParPeriode", DateDebut, "DateOperation", DateFin, "CollecteurId", CollecteurId), DateDebut, DateFin)

                Case "FicheOperationsParPeriode"
                    Dim Operation = Request("Operation")
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)
                    Dim ChampFiltre = "LibelleOperation"
                    Dim AgenceId = Request("AgenceId")
                    'Operation = Operation & "%"
                    ShowReportFicheOperationsParPeriode("FicheOperationsParPeriode", GetDataOperations("FicheOperationsParPeriode", DateDebut, "DateOperation", DateFin, ChampFiltre, Operation, AgenceId, "AgenceId"), DateDebut, DateFin, Operation)


                Case "HistoriqueCollectriceParPeriode"
                    Dim Operation = Request("Operation")
                    Dim CollecteurId = Request("CollecteurId")
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)
                    Dim ChampFiltre = "CollectriceId"
                    Dim AgenceId = AppSession.AgenceId
                    'Operation = Operation & "%"
                    ShowReportHistoriqueCollectriceParPeriode("HistoriqueCollectriceParPeriode", GetDataHistorique("HistoriqueCollectriceParPeriode", DateDebut, "DateOperation", DateFin, ChampFiltre, CollecteurId), DateDebut, DateFin, Operation)

                Case "Clt"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("Customers", GetDataIntervalDate("Customers", DateDebut, "DateCreation", DateFin))
                    Else
                        ShowReportClient("Customers", GetDataIntervalDateById("Customers", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))
                    End If

                Case "CltGlobal"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")
                    ShowReportClient("Customers", GetDataIntervalDateById("Customers", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))


                Case "Colt"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("Collectors", GetDataIntervalDate("Collectors", DateDebut, "DateCreation", DateFin))
                    Else
                        ShowReportClient("Collectors", GetDataIntervalDateById("Collectors", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))
                    End If

                Case "ColtGlobal"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")
                    'showReportClient("Collectors", getDataIntervalDateById("Collectors", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("Collectors", GetDataIntervalDate("Collectors", DateDebut, "DateCreation", DateFin))
                    Else
                        ShowReportClient("Collectors", GetDataIntervalDateById("Collectors", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))
                    End If


                Case "HistoClt"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim ClientId = Request("ClientId")

                    If (String.IsNullOrEmpty(ClientId)) Then
                        ShowReportClient("HistoCustomers", GetDataIntervalDate("HistoCustomers", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("HistoCustomers", GetDataIntervalDateById("HistoCustomers", DateDebut, "DateOperation", DateFin, "Id", ClientId))
                    End If

                Case "HistoCltGlobal"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")
                    'showReportClient("HistoCustomers", getDataIntervalDateById("HistoCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("HistoCustomers", GetDataIntervalDate("HistoCustomers", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("HistoCustomers", GetDataIntervalDateById("HistoCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
                    End If


                Case "HistoColParClt"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim CollecteurId = Request("CollecteurId")
                    Dim ClientId = Request("ClientId")

                    If (String.IsNullOrEmpty(CollecteurId)) Then
                        ShowReportClient("HistoCollectorsDetail", GetDataIntervalDate("HistoCollectorsDetail", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("HistoCollectorsDetail", GetDataIntervalDateById("HistoCollectorsDetail", DateDebut, "DateOperation", DateFin, "Id", CollecteurId))
                    End If

                Case "HistoColParCltGlobal"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim CollecteurId = Request("CollecteurId")
                    Dim AgenceId = Request("AgenceId")
                    'showReportClient("HistoCollectorsDetail", getDataIntervalDateById("HistoCollectorsDetail", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("HistoCollectorsDetail", GetDataIntervalDate("HistoCollectorsDetail", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("HistoCollectorsDetail", GetDataIntervalDateById("HistoCollectorsDetail", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
                    End If


                Case "HistoCol"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim CollecteurId = Request("CollecteurId")

                    If (String.IsNullOrEmpty(CollecteurId)) Then
                        ShowReportClient("HistoCollectors", GetDataIntervalDate("HistoCollectors", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("HistoCollectors", GetDataIntervalDateById("HistoCollectors", DateDebut, "DateOperation", DateFin, "Id", CollecteurId))
                    End If

                Case "HistoColGlobal"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")
                    'showReportClient("HistoCollectors", getDataIntervalDateById("HistoCollectors", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("HistoCollectors", GetDataIntervalDate("HistoCollectors", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("HistoCollectors", GetDataIntervalDateById("HistoCollectors", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
                    End If

                Case "RecetteClt"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim ClientId = Request("ClientId")

                    If (String.IsNullOrEmpty(ClientId)) Then
                        ShowReportClient("IncomeByCustomers", GetDataIntervalDate("IncomeByCustomers", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("IncomeByCustomers", GetDataIntervalDateById("IncomeByCustomers", DateDebut, "DateOperation", DateFin, "Id", ClientId))
                    End If

                Case "RecetteCltGlobal"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")
                    'showReportClient("IncomeByCustomers", getDataIntervalDateById("IncomeByCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("IncomeByCustomers", GetDataIntervalDate("IncomeByCustomers", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("IncomeByCustomers", GetDataIntervalDateById("IncomeByCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
                    End If

                Case "RecetteCol"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim CollecteurId = Request("CollecteurId")

                    If (String.IsNullOrEmpty(CollecteurId)) Then
                        ShowReportClient("IncomeByCollectors", GetDataIntervalDate("IncomeByCollectors", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("IncomeByCollectors", GetDataIntervalDateById("IncomeByCollectors", DateDebut, "DateOperation", DateFin, "Id", CollecteurId))
                    End If

                Case "RecetteColGlobal"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")
                    'showReportClient("IncomeByCollectors", getDataIntervalDateById("IncomeByCollectors", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("IncomeByCustomers", GetDataIntervalDate("IncomeByCustomers", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("IncomeByCustomers", GetDataIntervalDateById("IncomeByCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
                    End If


                Case "HistoAgence"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")
                    'showReportClient("HistoAgence", getDataIntervalDateById("HistoAgence", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("HistoAgence", GetDataIntervalDate("HistoAgence", DateDebut, "DateOperation", DateFin))
                    Else
                        ShowReportClient("HistoAgence", GetDataIntervalDateById("HistoAgence", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
                    End If

                Case "HistoBank"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    ShowReportClient("HistoBank", GetDataIntervalDate("HistoAgence", DateDebut, "DateOperation", DateFin))

                Case "LastOperation"
                    Dim DateDebutInter = Request("DateDebut")
                    Dim DateDebut = ConvertDate(DateDebutInter)
                    Dim DateFinInter = Request("DateFin")
                    Dim DateFin = ConvertDate(DateFinInter)

                    Dim AgenceId = Request("AgenceId")
                    ShowReportClient("LastOperationOfCustomers", LastOperationIntervalDate("LastOperationOfCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))


                    If (IsNothing(AgenceId)) Then
                        ShowReportClient("HistoAgence", GetDataIntervalDate("HistoAgence", DateDebut, "DateOperation", DateFin))
                        ShowReportClient("LastOperationOfCustomers", LastOperationIntervalDate("LastOperationOfCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
                    Else
                        ShowReportClient("LastOperationOfCustomers", LastOperationIntervalDate("LastOperationOfCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
                    End If

            End Select
        End If
    End Sub

    Private Sub ShowReportClient(reportName As String, ds As Object)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsData", ds))
    End Sub

    Private Sub ShowReportCommissionCollectriceAvecGrilleRemuneration(reportName As String, ds As Object)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DsCommissionCollectriceAvecGrilleRemuneration", ds))
    End Sub

    Private Sub ShowReportFicheCommissionsCollecteurs(reportName As String, ds As Object)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DsFicheCommissionsCollecteurs", ds))
    End Sub

    Private Sub ShowReportFicheCommissionsParPorteFeuille(reportName As String, ds As Object)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DsFicheCommissionsParPorteFeuille", ds))
    End Sub

    Private Sub ShowReportFicheCommissionsParPorteFeuilleSimplifiee(reportName As String, ds As Object)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DsFicheCommissionsParPorteFeuilleSimplifiee", ds))
    End Sub

    Private Sub ShowReportAgiosParClient(reportName As String, ds As Object, AgenceId As Long, Mois As Long, Annee As Long)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()

        'définition des paramètres
        Dim P1 As ReportParameter = New ReportParameter("AgenceId", AgenceId)
        Dim P2 As ReportParameter = New ReportParameter("Annee", Annee)
        Dim P3 As ReportParameter = New ReportParameter("Mois", Mois)

        'Ajout des paramètres
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {P1})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {P2})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {P3})

        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsData", ds))
    End Sub

    Private Sub ShowReportFicheJournaliere(reportName As String, ds As Object)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()

        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsData", ds))
    End Sub

    Private Sub ShowReportListeClientParCollectrice(reportName As String, ds As Object, DateDebut As String, DateFin As String)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()

        'définition des paramètres
        Dim LaDateDebut As ReportParameter = New ReportParameter("DateDebut", DateDebut)
        Dim LaDateFin As ReportParameter = New ReportParameter("DateFin", DateFin)
        Dim Utilisateur As ReportParameter = New ReportParameter("Utilisateur", AppSession.NomPrenomUser)

        'Ajout des paramètres
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LaDateDebut})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LaDateFin})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {Utilisateur})


        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DsListeClientParCollectrice", ds))
    End Sub

    Private Sub ShowReportFicheJournaliereParPeriode(reportName As String, ds As Object, DateDebut As String, DateFin As String)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()

        'définition des paramètres
        Dim LaDateDebut As ReportParameter = New ReportParameter("DateDebut", DateDebut)
        Dim LaDateFin As ReportParameter = New ReportParameter("DateFin", DateFin)

        'Ajout des paramètres
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LaDateDebut})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LaDateFin})


        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DsFicheCollecteJournaliereParPeriode", ds))
    End Sub

    Private Sub ShowReportFicheOperationsParPeriode(reportName As String, ds As Object, DateDebut As String, DateFin As String, Operation As String)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()

        'définition des paramètres
        Dim LaDateDebut As ReportParameter = New ReportParameter("DateDebut", DateDebut)
        Dim LaDateFin As ReportParameter = New ReportParameter("DateFin", DateFin)
        Dim LOperation As ReportParameter
        If Operation.Equals("RETRAIT") Then
            LOperation = New ReportParameter("Operation", "RETRAITS")
        ElseIf Operation.Equals("VENTE CARNET") Then
            LOperation = New ReportParameter("Operation", "VENTES DE CARNET")
        End If

        Dim Utilisateur = AppSession.NomPrenomUser

        Dim CurrentUserParam As ReportParameter = New ReportParameter("Utilisateur", Utilisateur)

        Dim Agence = "TOUTES LES AGENCES"
        If Not (User.IsInRole("ADMINISTRATEUR") Or User.IsInRole("SA") Or User.IsInRole("MANAGER")) Then
            Agence = AppSession.AgenceLibelle
        End If
        Dim AgenceParam As ReportParameter = New ReportParameter("Agence", Agence)

        'Ajout des paramètres
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LaDateDebut})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LaDateFin})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LOperation})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {CurrentUserParam})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {AgenceParam})


        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DsFicheOperationsParPeriode", ds))
    End Sub

    Private Sub ShowReportHistoriqueCollectriceParPeriode(reportName As String, ds As Object, DateDebut As String, DateFin As String, Operation As String)
        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
        ReportViewer1.LocalReport.DataSources.Clear()

        'définition des paramètres
        Dim LaDateDebut As ReportParameter = New ReportParameter("DateDebut", DateDebut)
        Dim LaDateFin As ReportParameter = New ReportParameter("DateFin", DateFin)

        Dim Utilisateur = AppSession.NomPrenomUser

        Dim CurrentUserParam As ReportParameter = New ReportParameter("Utilisateur", Utilisateur.ToUpper())

        'Ajout des paramètres
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LaDateDebut})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {LaDateFin})
        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {CurrentUserParam})


        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DsHistoriqueCollectriceParPeriode", ds))
    End Sub


End Class

'Public Class NewReport
'    Inherits System.Web.UI.Page

'    Public Shared ReadOnly Property ConnectionString() As String
'        Get
'            Return ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
'        End Get
'    End Property


'    Private Function ConvertDate(dateConvert As Date) As String
'        Dim mydate() = dateConvert.ToString.Split(" ")
'        Dim time = mydate(1)
'        Dim tempoDateTable() = mydate(0).Split("/")
'        'Dim day = dateConvert.Day
'        'Dim month = dateConvert.Month
'        'Dim year = dateConvert.Year

'        Dim jour = tempoDateTable(0)
'        Dim mois = tempoDateTable(1)
'        Dim annee = tempoDateTable(2)
'        'Dim resultDate = annee & "-" & mois & "-" + jour & " " & mydate(1)
'        Dim resultDate = annee & "-" & mois & "-" + jour
'        Return resultDate
'    End Function

'    Private Function GetDataSansId(viewName As String) As DataTable
'        Dim matable As DataTable = Nothing
'        Dim colonne As String = ""

'        Dim cmd As String = String.Format(" SELECT * FROM {0} ", viewName)

'        Using myConnection As New SqlConnection(ConnectionString)
'            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
'                'macmd.Parameters.AddWithValue("@id", id_value)
'                macmd.CommandTimeout = 0
'                Try
'                    myConnection.Open()
'                    Using reader As SqlDataReader = macmd.ExecuteReader
'                        matable = New DataTable
'                        matable.Load(reader)
'                        reader.Close()
'                    End Using
'                Catch ex As Exception
'                    'logMessage(ex.Message)
'                End Try
'            End Using
'            myConnection.Close()
'        End Using
'        Return matable
'    End Function

'    Private Function GetData2Param(viewName As String, ByVal parmValue1 As String, ByVal ParaName1 As String, ByVal parmValue2 As String, ByVal ParaName2 As String) As DataTable
'        Dim matable As DataTable = Nothing
'        Dim colonne As String = ""

'        Dim cmd As String = String.Format(" SELECT * FROM {0} Where {1} = @p1 and {2} = @p2 ", viewName, ParaName1, ParaName2)

'        Using myConnection As New SqlConnection(ConnectionString)
'            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
'                macmd.Parameters.AddWithValue("@p1", parmValue1)
'                macmd.Parameters.AddWithValue("@p2", parmValue2)
'                macmd.CommandTimeout = 0
'                Try
'                    myConnection.Open()
'                    Using reader As SqlDataReader = macmd.ExecuteReader
'                        matable = New DataTable
'                        matable.Load(reader)
'                        reader.Close()
'                    End Using
'                Catch ex As Exception
'                    'logMessage(ex.Message)
'                End Try
'            End Using
'            myConnection.Close()
'        End Using

'        Return matable
'    End Function

'    Private Function GetData3Param(viewName As String, ByVal parmValue1 As String, ByVal ParaName1 As String, ByVal parmValue2 As String, ByVal ParaName2 As String, ByVal parmValue3 As String, ByVal ParaName3 As String) As DataTable
'        Dim matable As DataTable = Nothing
'        Dim colonne As String = ""

'        Dim cmd As String = String.Format(" SELECT * FROM {0} Where {1} = @p1 and {2} = @p2 and {3} = @p3 ", viewName, ParaName1, ParaName2, ParaName3)

'        Using myConnection As New SqlConnection(ConnectionString)
'            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
'                macmd.Parameters.AddWithValue("@p1", parmValue1)
'                macmd.Parameters.AddWithValue("@p2", parmValue2)
'                macmd.Parameters.AddWithValue("@p3", parmValue3)
'                macmd.CommandTimeout = 0
'                Try
'                    myConnection.Open()
'                    Using reader As SqlDataReader = macmd.ExecuteReader
'                        matable = New DataTable
'                        matable.Load(reader)
'                        reader.Close()
'                    End Using
'                Catch ex As Exception
'                    'logMessage(ex.Message)
'                End Try
'            End Using
'            myConnection.Close()
'        End Using

'        Return matable
'    End Function

'    Private Function GetData(viewName As String, ByVal id_value As String, Optional idName As String = "Id") As DataTable
'        Dim matable As DataTable = Nothing
'        Dim colonne As String = ""

'        Dim cmd As String = String.Format(" SELECT * FROM {0} Where {1} = @id ", viewName, idName)

'        Using myConnection As New SqlConnection(ConnectionString)
'            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
'                macmd.Parameters.AddWithValue("@id", id_value)
'                macmd.CommandTimeout = 0
'                Try
'                    myConnection.Open()
'                    Using reader As SqlDataReader = macmd.ExecuteReader
'                        matable = New DataTable
'                        matable.Load(reader)
'                        reader.Close()
'                    End Using
'                Catch ex As Exception
'                    'logMessage(ex.Message)
'                End Try
'            End Using
'            myConnection.Close()
'        End Using

'        Return matable
'    End Function

'    Private Function GetDataIntervalDate(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String) As DataTable
'        Dim matable As DataTable = Nothing
'        Dim colonne As String = ""

'        Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} >= @p1 AND {1}  <= @p2 ) ", viewName, lechampdate)
'        ' Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} = @p1) ", viewName, ParaName1)

'        Using myConnection As New SqlConnection(ConnectionString)
'            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
'                macmd.Parameters.AddWithValue("@p1", datedebutValue + " 00:00:00")
'                macmd.Parameters.AddWithValue("@p2", datefinValue + " 23:59:59")
'                macmd.CommandTimeout = 0

'                Try
'                    myConnection.Open()
'                    Using reader As SqlDataReader = macmd.ExecuteReader
'                        matable = New DataTable
'                        matable.Load(reader)
'                        reader.Close()
'                    End Using
'                Catch ex As Exception
'                    'logMessage(ex.Message)
'                End Try
'            End Using
'            myConnection.Close()
'        End Using

'        Return matable
'    End Function

'    Private Function LastOperationIntervalDate(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String, ByVal IdLechamp As String, ByVal IdValue As String) As DataTable
'        Dim matable As DataTable = Nothing
'        Dim colonne As String = ""

'        Dim cmd As String = String.Format(" SELECT * FROM {0} Where ( (({1} <= @p1 OR {1} >= @p2) OR ({1} IS NULL)) AND {2} = @p3 ) ", viewName, lechampdate, IdLechamp)
'        ' Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} = @p1) ", viewName, ParaName1)

'        Using myConnection As New SqlConnection(ConnectionString)
'            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
'                macmd.Parameters.AddWithValue("@p1", datedebutValue + " 00:00:00")
'                macmd.Parameters.AddWithValue("@p2", datefinValue + " 23:59:59")
'                macmd.Parameters.AddWithValue("@p3", IdValue)
'                macmd.CommandTimeout = 0

'                Try
'                    myConnection.Open()
'                    Using reader As SqlDataReader = macmd.ExecuteReader
'                        matable = New DataTable
'                        matable.Load(reader)
'                        reader.Close()
'                    End Using
'                Catch ex As Exception
'                    'logMessage(ex.Message)
'                End Try
'            End Using
'            myConnection.Close()
'        End Using

'        Return matable
'    End Function

'    Private Function GetDataIntervalDateById(viewName As String, ByVal datedebutValue As String, ByVal lechampdate As String, ByVal datefinValue As String, ByVal IdLechamp As String, ByVal IdValue As String) As DataTable
'        Dim matable As DataTable = Nothing
'        Dim colonne As String = ""

'        Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} >= @p1 AND {1}  <= @p2 AND {2} = @p3  ) ", viewName, lechampdate, IdLechamp)
'        ' Dim cmd As String = String.Format(" SELECT * FROM {0} Where ({1} = @p1) ", viewName, ParaName1)

'        Using myConnection As New SqlConnection(ConnectionString)
'            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
'                macmd.Parameters.AddWithValue("@p1", datedebutValue + " 00:00:00")
'                macmd.Parameters.AddWithValue("@p2", datefinValue + " 23:59:59")
'                macmd.Parameters.AddWithValue("@p3", IdValue)
'                macmd.CommandTimeout = 0
'                Try
'                    myConnection.Open()
'                    Using reader As SqlDataReader = macmd.ExecuteReader
'                        matable = New DataTable
'                        matable.Load(reader)
'                        reader.Close()
'                    End Using
'                Catch ex As Exception
'                    'logMessage(ex.Message)
'                End Try
'            End Using
'            myConnection.Close()
'        End Using

'        Return matable
'    End Function


'    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
'        If Not IsPostBack Then
'            Dim type = Request("type")
'            Select Case type

'                Case "AgiosParClient"
'                    Dim Mois = Request("Mois")
'                    Dim Annee = Request("Annee")
'                    Dim AgenceId = Request("AgenceId")

'                    showReportAgiosParClient("AgiosCustomers", getData3Param("AgiosCustomers", AgenceId, "AgenceId", Mois, "Mois", Annee, "Annee"), AgenceId, Mois, Annee)

'                Case "FicheCollecte"
'                    Dim JournalCaisseId = Request("JournalCaisseId")
'                    showReportFicheJournaliere("FicheCollecteJournaliere", getData("FicheCollecteJournaliere", JournalCaisseId, "JournalCaisseId"))
'                Case "Clt"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")

'                    If (IsNothing(AgenceId)) Then
'                        showReportClient("Customers", getDataIntervalDate("Customers", DateDebut, "DateCreation", DateFin))
'                    Else
'                        showReportClient("Customers", getDataIntervalDateById("Customers", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))
'                    End If

'                Case "CltGlobal"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("Customers", getDataIntervalDateById("Customers", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))


'                Case "Colt"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")

'                    If (IsNothing(AgenceId)) Then
'                        showReportClient("Collectors", getDataIntervalDate("Collectors", DateDebut, "DateCreation", DateFin))
'                    Else
'                        showReportClient("Collectors", getDataIntervalDateById("Collectors", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))
'                    End If

'                Case "ColtGlobal"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("Collectors", getDataIntervalDateById("Collectors", DateDebut, "DateCreation", DateFin, "AgenceId", AgenceId))


'                Case "HistoClt"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim ClientId = Request("ClientId")

'                    If (String.IsNullOrEmpty(ClientId)) Then
'                        showReportClient("HistoCustomers", getDataIntervalDate("HistoCustomers", DateDebut, "DateOperation", DateFin))
'                    Else
'                        showReportClient("HistoCustomers", getDataIntervalDateById("HistoCustomers", DateDebut, "DateOperation", DateFin, "Id", ClientId))
'                    End If

'                Case "HistoCltGlobal"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("HistoCustomers", getDataIntervalDateById("HistoCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))


'                Case "HistoColParClt"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim CollecteurId = Request("CollecteurId")
'                    Dim ClientId = Request("ClientId")

'                    If (String.IsNullOrEmpty(CollecteurId)) Then
'                        showReportClient("HistoCollectorsDetail", getDataIntervalDate("HistoCollectorsDetail", DateDebut, "DateOperation", DateFin))
'                    Else
'                        showReportClient("HistoCollectorsDetail", getDataIntervalDateById("HistoCollectorsDetail", DateDebut, "DateOperation", DateFin, "Id", CollecteurId))
'                    End If

'                Case "HistoColParCltGlobal"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim CollecteurId = Request("CollecteurId")
'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("HistoCollectorsDetail", getDataIntervalDateById("HistoCollectorsDetail", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))


'                Case "HistoCol"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim CollecteurId = Request("CollecteurId")

'                    If (String.IsNullOrEmpty(CollecteurId)) Then
'                        showReportClient("HistoCollectors", getDataIntervalDate("HistoCollectors", DateDebut, "DateOperation", DateFin))
'                    Else
'                        showReportClient("HistoCollectors", getDataIntervalDateById("HistoCollectors", DateDebut, "DateOperation", DateFin, "Id", CollecteurId))
'                    End If

'                Case "HistoColGlobal"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("HistoCollectors", getDataIntervalDateById("HistoCollectors", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

'                Case "RecetteClt"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim ClientId = Request("ClientId")

'                    If (String.IsNullOrEmpty(ClientId)) Then
'                        showReportClient("IncomeByCustomers", getDataIntervalDate("IncomeByCustomers", DateDebut, "DateOperation", DateFin))
'                    Else
'                        showReportClient("IncomeByCustomers", getDataIntervalDateById("IncomeByCustomers", DateDebut, "DateOperation", DateFin, "Id", ClientId))
'                    End If

'                Case "RecetteCltGlobal"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("IncomeByCustomers", getDataIntervalDateById("IncomeByCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

'                Case "RecetteCol"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim CollecteurId = Request("CollecteurId")

'                    If (String.IsNullOrEmpty(CollecteurId)) Then
'                        showReportClient("IncomeByCollectors", getDataIntervalDate("IncomeByCollectors", DateDebut, "DateOperation", DateFin))
'                    Else
'                        showReportClient("IncomeByCollectors", getDataIntervalDateById("IncomeByCollectors", DateDebut, "DateOperation", DateFin, "Id", CollecteurId))
'                    End If

'                Case "RecetteColGlobal"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("IncomeByCollectors", getDataIntervalDateById("IncomeByCollectors", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))


'                Case "HistoAgence"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("HistoAgence", getDataIntervalDateById("HistoAgence", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))

'                Case "HistoBank"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    showReportClient("HistoBank", getDataIntervalDate("HistoAgence", DateDebut, "DateOperation", DateFin))

'                Case "LastOperation"
'                    Dim DateDebutInter = Request("DateDebut")
'                    Dim DateDebut = ConvertDate(DateDebutInter)
'                    Dim DateFinInter = Request("DateFin")
'                    Dim DateFin = ConvertDate(DateFinInter)

'                    Dim AgenceId = Request("AgenceId")
'                    showReportClient("LastOperationOfCustomers", LastOperationIntervalDate("LastOperationOfCustomers", DateDebut, "DateOperation", DateFin, "AgenceId", AgenceId))
'            End Select
'        End If
'    End Sub

'    Private Sub ShowReportClient(reportName As String, ds As Object)
'        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
'        ReportViewer1.LocalReport.DataSources.Clear()
'        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsData", ds))
'    End Sub

'    Private Sub ShowReportAgiosParClient(reportName As String, ds As Object, AgenceId As Long, Mois As Long, Annee As Long)
'        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
'        ReportViewer1.LocalReport.DataSources.Clear()

'        'définition des paramètres
'        Dim P1 As ReportParameter = New ReportParameter("AgenceId", AgenceId)
'        Dim P2 As ReportParameter = New ReportParameter("Annee", Annee)
'        Dim P3 As ReportParameter = New ReportParameter("Mois", Mois)

'        'Ajout des paramètres
'        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {P1})
'        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {P2})
'        ReportViewer1.LocalReport.SetParameters(New ReportParameter() {P3})

'        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsData", ds))
'    End Sub

'    Private Sub ShowReportFicheJournaliere(reportName As String, ds As Object)
'        ReportViewer1.LocalReport.ReportPath = Path.Combine(Server.MapPath("~/Report/Template"), reportName & ".rdlc")
'        ReportViewer1.LocalReport.DataSources.Clear()

'        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("dsData", ds))
'    End Sub


'End Class