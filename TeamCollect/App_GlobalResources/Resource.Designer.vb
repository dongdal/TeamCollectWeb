﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Ce code a été généré par un outil.
'     Version du runtime :4.0.30319.42000
'
'     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
'     le code est régénéré.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System


'Cette classe a été générée automatiquement par la classe StronglyTypedResourceBuilder
'à l'aide d'un outil, tel que ResGen ou Visual Studio.
'Pour ajouter ou supprimer un membre, modifiez votre fichier .ResX, puis réexécutez ResGen
'avec l'option /str ou régénérez votre projet VS.
'''<summary>
'''  Une classe de ressource fortement typée destinée, entre autres, à la consultation des chaînes localisées.
'''</summary>
<Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0"),  _
 Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
 Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
Public Class Resource
    
    Private Shared resourceMan As Global.System.Resources.ResourceManager
    
    Private Shared resourceCulture As Global.System.Globalization.CultureInfo
    
    <Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")>  _
    Friend Sub New()
        MyBase.New
    End Sub
    
    '''<summary>
    '''  Retourne l'instance ResourceManager mise en cache utilisée par cette classe.
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Public Shared ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
        Get
            If Object.ReferenceEquals(resourceMan, Nothing) Then
                Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("TeamCollect.Resource", GetType(Resource).Assembly)
                resourceMan = temp
            End If
            Return resourceMan
        End Get
    End Property
    
    '''<summary>
    '''  Remplace la propriété CurrentUICulture du thread actuel pour toutes
    '''  les recherches de ressources à l'aide de cette classe de ressource fortement typée.
    '''</summary>
    <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Public Shared Property Culture() As Global.System.Globalization.CultureInfo
        Get
            Return resourceCulture
        End Get
        Set
            resourceCulture = value
        End Set
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Annee.
    '''</summary>
    Public Shared ReadOnly Property Annee() As String
        Get
            Return ResourceManager.GetString("Annee", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Annuler la clôture.
    '''</summary>
    Public Shared ReadOnly Property AnnulerCloture() As String
        Get
            Return ResourceManager.GetString("AnnulerCloture", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Voulez-vous vraiment annuler cette opération?.
    '''</summary>
    Public Shared ReadOnly Property AnnulerOperationBody() As String
        Get
            Return ResourceManager.GetString("AnnulerOperationBody", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Annuler une opération.
    '''</summary>
    Public Shared ReadOnly Property AnnulerOperationTitle() As String
        Get
            Return ResourceManager.GetString("AnnulerOperationTitle", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Modifier.
    '''</summary>
    Public Shared ReadOnly Property Btn_Edit() As String
        Get
            Return ResourceManager.GetString("Btn_Edit", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Supprimer.
    '''</summary>
    Public Shared ReadOnly Property BtnDelete() As String
        Get
            Return ResourceManager.GetString("BtnDelete", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Informations.
    '''</summary>
    Public Shared ReadOnly Property BtnDetails() As String
        Get
            Return ResourceManager.GetString("BtnDetails", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Opération annulée.
    '''</summary>
    Public Shared ReadOnly Property CancelingOperation() As String
        Get
            Return ResourceManager.GetString("CancelingOperation", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Annulation Confirmée!!!.
    '''</summary>
    Public Shared ReadOnly Property CancelingProcess() As String
        Get
            Return ResourceManager.GetString("CancelingProcess", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Annulation du retrait!!.
    '''</summary>
    Public Shared ReadOnly Property CancelingRetrait() As String
        Get
            Return ResourceManager.GetString("CancelingRetrait", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Catégorie.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemuneration() As String
        Get
            Return ResourceManager.GetString("CategorieRemuneration", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Veuillez sélectionner une catégorie.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemunerationCombo() As String
        Get
            Return ResourceManager.GetString("CategorieRemunerationCombo", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Ajouter une nouvelle catégorie.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemunerationCreate() As String
        Get
            Return ResourceManager.GetString("CategorieRemunerationCreate", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Supprimer une catégorie.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemunerationDelete() As String
        Get
            Return ResourceManager.GetString("CategorieRemunerationDelete", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Informations sur la catégorie.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemunerationDetail() As String
        Get
            Return ResourceManager.GetString("CategorieRemunerationDetail", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Modifier une catégorie.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemunerationEdit() As String
        Get
            Return ResourceManager.GetString("CategorieRemunerationEdit", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Catégorie.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemunerationLibelle() As String
        Get
            Return ResourceManager.GetString("CategorieRemunerationLibelle", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Grille de rémunération.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemunerationList() As String
        Get
            Return ResourceManager.GetString("CategorieRemunerationList", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Gestion des catégories.
    '''</summary>
    Public Shared ReadOnly Property CategorieRemunerationManage() As String
        Get
            Return ResourceManager.GetString("CategorieRemunerationManage", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à :Cette Information est Obligatoire.
    '''</summary>
    Public Shared ReadOnly Property champ_Manquant() As String
        Get
            Return ResourceManager.GetString("champ_Manquant", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Impossible de changer le mot de passe.
    '''</summary>
    Public Shared ReadOnly Property ChangePasswordImpossible() As String
        Get
            Return ResourceManager.GetString("ChangePasswordImpossible", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Clôturer la caisse.
    '''</summary>
    Public Shared ReadOnly Property CloturerCaisse() As String
        Get
            Return ResourceManager.GetString("CloturerCaisse", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Voulez-vous vraiment clôturer cette caisse?.
    '''</summary>
    Public Shared ReadOnly Property CloturerCaisseBody() As String
        Get
            Return ResourceManager.GetString("CloturerCaisseBody", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Collectrice.
    '''</summary>
    Public Shared ReadOnly Property CollectriceId() As String
        Get
            Return ResourceManager.GetString("CollectriceId", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Commission Minimale.
    '''</summary>
    Public Shared ReadOnly Property CommissionMinimale() As String
        Get
            Return ResourceManager.GetString("CommissionMinimale", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Voulez-vous vraiment supprimer cet élément?.
    '''</summary>
    Public Shared ReadOnly Property ConfirmDelete() As String
        Get
            Return ResourceManager.GetString("ConfirmDelete", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Confirmer retrait.
    '''</summary>
    Public Shared ReadOnly Property ConfirmerRetrait() As String
        Get
            Return ResourceManager.GetString("ConfirmerRetrait", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Formulaire de création....
    '''</summary>
    Public Shared ReadOnly Property CreateForm() As String
        Get
            Return ResourceManager.GetString("CreateForm", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Votre Format de date n&apos;est pas correct! (jj/mois/annee).
    '''</summary>
    Public Shared ReadOnly Property Dataexpression_Error() As String
        Get
            Return ResourceManager.GetString("Dataexpression_Error", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Date création.
    '''</summary>
    Public Shared ReadOnly Property DateCreation() As String
        Get
            Return ResourceManager.GetString("DateCreation", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Date impression.
    '''</summary>
    Public Shared ReadOnly Property DatePremiereImpression() As String
        Get
            Return ResourceManager.GetString("DatePremiereImpression", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à La valeur saisie est hors limite et ne peut être enregistrée!.
    '''</summary>
    Public Shared ReadOnly Property DecimalMaxValue() As String
        Get
            Return ResourceManager.GetString("DecimalMaxValue", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à La valeur saisie doit être comprise entre {0} et {1}..
    '''</summary>
    Public Shared ReadOnly Property DecimalMaxValuePercent() As String
        Get
            Return ResourceManager.GetString("DecimalMaxValuePercent", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Votre Format de données n&apos;est pas correct! (00,00).
    '''</summary>
    Public Shared ReadOnly Property decimalType_error() As String
        Get
            Return ResourceManager.GetString("decimalType_error", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Supprimer.
    '''</summary>
    Public Shared ReadOnly Property Delete() As String
        Get
            Return ResourceManager.GetString("Delete", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Demande de retrait.
    '''</summary>
    Public Shared ReadOnly Property DemandeRetrait() As String
        Get
            Return ResourceManager.GetString("DemandeRetrait", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Formulaire de modification....
    '''</summary>
    Public Shared ReadOnly Property EditForm() As String
        Get
            Return ResourceManager.GetString("EditForm", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Erreur lors de l&apos;enregistrement veuillez essayez à nouveau!.
    '''</summary>
    Public Shared ReadOnly Property ErreurDenreg() As String
        Get
            Return ResourceManager.GetString("ErreurDenreg", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Erreur!!!.
    '''</summary>
    Public Shared ReadOnly Property ErreurTitle() As String
        Get
            Return ResourceManager.GetString("ErreurTitle", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Une erreur est survenue lors du traitement de votre demande..
    '''</summary>
    Public Shared ReadOnly Property ErrorProcess() As String
        Get
            Return ResourceManager.GetString("ErrorProcess", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Votre recherche.
    '''</summary>
    Public Shared ReadOnly Property Find() As String
        Get
            Return ResourceManager.GetString("Find", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Vous êtes connecté en tant que: .
    '''</summary>
    Public Shared ReadOnly Property GestUser_ConnectedAs() As String
        Get
            Return ResourceManager.GetString("GestUser_ConnectedAs", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à La connexion externe a été supprimée..
    '''</summary>
    Public Shared ReadOnly Property GestUser_DeleteConection() As String
        Get
            Return ResourceManager.GetString("GestUser_DeleteConection", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Formulaire de modification du mot de passe.
    '''</summary>
    Public Shared ReadOnly Property GestUser_EditPasswordForm() As String
        Get
            Return ResourceManager.GetString("GestUser_EditPasswordForm", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Une erreur s&apos;est produite..
    '''</summary>
    Public Shared ReadOnly Property GestUser_ErrorOccured() As String
        Get
            Return ResourceManager.GetString("GestUser_ErrorOccured", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Gestion du compte.
    '''</summary>
    Public Shared ReadOnly Property GestUser_ManageAccount() As String
        Get
            Return ResourceManager.GetString("GestUser_ManageAccount", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Le nouveau mot de passe ne peut être identique ou être constitué à partir de l&apos;ancien mot de passe..
    '''</summary>
    Public Shared ReadOnly Property GestUser_NewPasswordContainOldPassword() As String
        Get
            Return ResourceManager.GetString("GestUser_NewPasswordContainOldPassword", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Votre mot de passe a été défini..
    '''</summary>
    Public Shared ReadOnly Property GestUser_PwdDefine() As String
        Get
            Return ResourceManager.GetString("GestUser_PwdDefine", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Votre mot de passe a été modifié..
    '''</summary>
    Public Shared ReadOnly Property GestUser_PwdModify() As String
        Get
            Return ResourceManager.GetString("GestUser_PwdModify", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Mot de passe actuel.
    '''</summary>
    Public Shared ReadOnly Property GestUserActualPwd() As String
        Get
            Return ResourceManager.GetString("GestUserActualPwd", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Mot de passe.
    '''</summary>
    Public Shared ReadOnly Property GestUserPwd() As String
        Get
            Return ResourceManager.GetString("GestUserPwd", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Confirmer le mot de passe.
    '''</summary>
    Public Shared ReadOnly Property GestUserPwdConf() As String
        Get
            Return ResourceManager.GetString("GestUserPwdConf", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Le mot de passe et le mot de passe de confirmation ne correspondent pas..
    '''</summary>
    Public Shared ReadOnly Property GestUserPwdConfMatch() As String
        Get
            Return ResourceManager.GetString("GestUserPwdConfMatch", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Confirmer le nouveau mot de passe.
    '''</summary>
    Public Shared ReadOnly Property GestUserPwdConfNew() As String
        Get
            Return ResourceManager.GetString("GestUserPwdConfNew", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas..
    '''</summary>
    Public Shared ReadOnly Property GestUserPwdConfNewMatch() As String
        Get
            Return ResourceManager.GetString("GestUserPwdConfNewMatch", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à La chaîne {0} doit comporter au moins {2} caractères..
    '''</summary>
    Public Shared ReadOnly Property GestUserPwdLength() As String
        Get
            Return ResourceManager.GetString("GestUserPwdLength", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Mémoriser le mot de passe ?.
    '''</summary>
    Public Shared ReadOnly Property GestUserPwdMemo() As String
        Get
            Return ResourceManager.GetString("GestUserPwdMemo", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Nouveau mot de passe.
    '''</summary>
    Public Shared ReadOnly Property GestUserPwdNew() As String
        Get
            Return ResourceManager.GetString("GestUserPwdNew", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Selectionnez des Roles pour l&apos;utilisateur.
    '''</summary>
    Public Shared ReadOnly Property GestUserUserRoles() As String
        Get
            Return ResourceManager.GetString("GestUserUserRoles", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Grille de rémuneration.
    '''</summary>
    Public Shared ReadOnly Property GrilleRemuneration() As String
        Get
            Return ResourceManager.GetString("GrilleRemuneration", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Opération.
    '''</summary>
    Public Shared ReadOnly Property HistoriqueMouvementId() As String
        Get
            Return ResourceManager.GetString("HistoriqueMouvementId", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Libelle.
    '''</summary>
    Public Shared ReadOnly Property Libelle() As String
        Get
            Return ResourceManager.GetString("Libelle", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Profil Manager.
    '''</summary>
    Public Shared ReadOnly Property ManagerProfile() As String
        Get
            Return ResourceManager.GetString("ManagerProfile", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Message d&apos;alerte.
    '''</summary>
    Public Shared ReadOnly Property MessageAlerte() As String
        Get
            Return ResourceManager.GetString("MessageAlerte", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Mois.
    '''</summary>
    Public Shared ReadOnly Property Mois() As String
        Get
            Return ResourceManager.GetString("Mois", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Le montant entré est invalide. Veuillez corriger l&apos;erreur..
    '''</summary>
    Public Shared ReadOnly Property MontantInvalid() As String
        Get
            Return ResourceManager.GetString("MontantInvalid", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Nombre d&apos;impression.
    '''</summary>
    Public Shared ReadOnly Property NombreImpression() As String
        Get
            Return ResourceManager.GetString("NombreImpression", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Votre Format de données n&apos;est pas correct! (Ex: 123; 50354).
    '''</summary>
    Public Shared ReadOnly Property NumericType_error() As String
        Get
            Return ResourceManager.GetString("NumericType_error", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Le champ {0} doit être composé au minimum de 8 caractères et avoir des minuscules, majuscules, chiffres et caractères spéciaux (#$^+=!*()@%&amp;)..
    '''</summary>
    Public Shared ReadOnly Property PasswordStrength() As String
        Get
            Return ResourceManager.GetString("PasswordStrength", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Pourcentage Commission.
    '''</summary>
    Public Shared ReadOnly Property PourcentageCommission() As String
        Get
            Return ResourceManager.GetString("PourcentageCommission", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Salaire de base.
    '''</summary>
    Public Shared ReadOnly Property SalaireDeBase() As String
        Get
            Return ResourceManager.GetString("SalaireDeBase", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Statut Existant.
    '''</summary>
    Public Shared ReadOnly Property StatutExistant() As String
        Get
            Return ResourceManager.GetString("StatutExistant", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à La chaîne {0} doit comporter au maximum {1} caractères..
    '''</summary>
    Public Shared ReadOnly Property StringLength() As String
        Get
            Return ResourceManager.GetString("StringLength", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Opération effectuée avec succès..
    '''</summary>
    Public Shared ReadOnly Property SuccessOperation() As String
        Get
            Return ResourceManager.GetString("SuccessOperation", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Le retrait a été effectué avec succès..
    '''</summary>
    Public Shared ReadOnly Property SuccessRetrait() As String
        Get
            Return ResourceManager.GetString("SuccessRetrait", resourceCulture)
        End Get
    End Property
    
    '''<summary>
    '''  Recherche une chaîne localisée semblable à Succes!!!.
    '''</summary>
    Public Shared ReadOnly Property SuccessTitle() As String
        Get
            Return ResourceManager.GetString("SuccessTitle", resourceCulture)
        End Get
    End Property
End Class
