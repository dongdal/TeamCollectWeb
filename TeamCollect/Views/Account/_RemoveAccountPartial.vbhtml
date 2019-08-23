@Imports Microsoft.AspNet.Identity
@ModelType ICollection(Of UserLoginInfo)

@If Model.Count > 0
    @<h4>Noms de connexion enregistrés</h4>
    @<table class="table">
        <tbody>
            @For Each account As UserLoginInfo In Model
                @<tr>
                    <td>@account.LoginProvider</td>
                    <td>
                        @If ViewBag.ShowRemoveButton Then
                            using Html.BeginForm("Disassociate", "Account")
                                @Html.AntiForgeryToken()
                                @<div>
                                    @Html.Hidden("loginProvider", account.LoginProvider)
                                    @Html.Hidden("providerKey", account.ProviderKey)
                                    <input type="submit" class="btn btn-default" value="Supprimer" title="Supprimer ce nom de connexion @account.LoginProvider de votre compte" />
                                </div>
                            End Using
                        Else
                            @: &nbsp;
                        End If
                    </td>
                </tr>
            Next
        </tbody>
    </table>
End If
