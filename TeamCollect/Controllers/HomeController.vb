Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports PagedList
Imports System.Web.SessionState
Imports System.Web.Script.Serialization
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports TeamCollect.My.Resources
Imports System.Data.Entity.Validation
Imports System.IO
Imports Newtonsoft.Json
Imports System.Data.SqlClient


Public Class HomeController
    Inherits BaseController

    Private db As New ApplicationDbContext

    <LocalizedAuthorize(Roles:="CHEFCOLLECTEUR,ADMINISTRATEUR,MANAGER")>
    Function Index(sortOrder As String, currentFilter As String, searchString As String, page As Integer?, AgenceId As Long?) As ActionResult

        Dim cmd As String = String.Format("select top (5) SUM(Montant) as Montant,SUM(PartBANK) as PartBANK,SUM(PartCLIENT) as PartCLIENT,Min(Libelle) as Agence, Min(Nom) as Nom,  Min(Prenom) as Prenom,  Min(Sexe) as Sexe  from dbo.IncomeByCollectors group by Id order by Montant Desc")
        Dim cmdtopagence As String = String.Format("select SUM(Montant) as Montant,SUM(PartBANK) as PartBANK,SUM(PartCLIENT) as PartCLIENT,Min(Libelle) as Agence, AgenceId from dbo.HistoAgence Where Id <> 70146 group by AgenceId order by Montant Desc")
        Dim cmdgenre As String = String.Format("SELECT count(*) as Nombre, min(sexe) as Genre FROM HistoCustomers group by Sexe  order by Genre")

        Dim messtats = New List(Of Dashbord)
        Dim mondashbord = New Dashbord
        Dim Listdemastat = New List(Of StatCollecteur)
        Dim Listdemastatagence = New List(Of StatAgence)
        Dim Listdemastatgenre = New List(Of StatGenre)

        Using myConnection As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString)
            Using macmd As SqlCommand = New SqlCommand(cmd, myConnection)
                'macmd.Parameters.AddWithValue("@id", id_value)
                macmd.CommandTimeout = 0
                Try
                    myConnection.Open()
                    Using reader As SqlDataReader = macmd.ExecuteReader

                        For Each item In reader
                            Dim mastat = New StatCollecteur

                            mastat.Montant = item("Montant").ToString
                            mastat.PartBANK = item("PartBANK").ToString
                            mastat.PartCLIENT = item("PartCLIENT").ToString
                            mastat.Agence = item("Agence").ToString
                            mastat.Nom = item("Nom").ToString
                            mastat.Prenom = item("Prenom").ToString
                            mastat.Sexe = item("Sexe").ToString

                            Listdemastat.Add(mastat)
                        Next

                        reader.Close()

                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using

            '-----------top agence---------------
            Using macmdag As SqlCommand = New SqlCommand(cmdtopagence, myConnection)
                'macmd.Parameters.AddWithValue("@id", id_value)
                macmdag.CommandTimeout = 0
                Try
                    Using reader2 As SqlDataReader = macmdag.ExecuteReader
                        For Each item In reader2
                            Dim mastatagence = New StatAgence

                            mastatagence.Montant = item("Montant").ToString
                            mastatagence.PartBANK = item("PartBANK").ToString
                            mastatagence.PartCLIENT = item("PartCLIENT").ToString
                            mastatagence.Agence = item("Agence").ToString

                            Listdemastatagence.Add(mastatagence)
                        Next

                        reader2.Close()

                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using

            '-----------stat genre ---------------
            Using macmdgenre As SqlCommand = New SqlCommand(cmdgenre, myConnection)
                'macmd.Parameters.AddWithValue("@id", id_value)
                macmdgenre.CommandTimeout = 0
                Try
                    Using reader3 As SqlDataReader = macmdgenre.ExecuteReader
                        Dim i = 0
                        For Each item In reader3
                            Dim mastatgenre = New StatGenre
                            If (i = 0) Then
                                mastatgenre.NombreF = item("Nombre").ToString
                                mastatgenre.NombreM = 0
                                mastatgenre.Genre = item("Genre").ToString
                            Else
                                mastatgenre.NombreM = item("Nombre").ToString
                                mastatgenre.NombreF = 0
                                mastatgenre.Genre = item("Genre").ToString
                            End If
                           

                            Listdemastatgenre.Add(mastatgenre)
                            i = i + 1
                        Next

                        reader3.Close()

                    End Using
                Catch ex As Exception
                    'logMessage(ex.Message)
                End Try
            End Using

            myConnection.Close()
        End Using

        mondashbord.StatCollecteurListObject = Listdemastat.ToList
        mondashbord.StatAgenceListObject = Listdemastatagence.ToList
        mondashbord.StatGenreListObject = Listdemastatgenre.ToList
        messtats.Add(mondashbord)
   
        Return View("Index", messtats.ToList)

    End Function

    'Function Map() As ActionResult

    '    Return View()
    'End Function

    'Function Contact() As ActionResult
    '    ViewData("Message") = "Your contact page."

    '    Return View()
    'End Function
End Class
