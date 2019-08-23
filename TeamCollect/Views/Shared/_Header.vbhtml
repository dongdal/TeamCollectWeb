@If Request.IsAuthenticated = True Then
    @<div id="header">
        <nav class="navbar navbar-default" role="navigation">
            <div class="navbar-header" style="text-align: center">
                <a class="navbar-brand" href="#">
                    @*TeamCollect.<span class="slogan">mF</span>*@
                    <img src="~/img/logo.png" width="100" height="100" alt="" class="image img-responsive" />
                </a>
            </div>
            <div id="navbar-no-collapse" class="navbar-no-collapse">
                <ul class="nav navbar-nav">
                    <li>
                        <!--Sidebar collapse button-->
                        <a href="#" class="collapseBtn leftbar"><i class="s16 minia-icon-list-3"></i></a>
                    </li>

                </ul>
                @If Request.IsAuthenticated = True Then


                    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With {.id = "logoutForm", .class = "navbar-right"})
                        @Html.AntiForgeryToken()

                        @<ul class="nav navbar-right usernav">
                            <li>
                                <a href="javascript:document.getElementById('logoutForm').submit()" style="padding:7px 15px;">
                                    <i class="s16 icomoon-icon-exit"></i>
                                    <span class="txt"> Logout</span>
                                </a>
                            </li>
                        </ul>
                        @<ul class="nav navbar-right">
    <li><h4 style="color: #304a85"><i class="s16 icomoon-icon-office"> Welcome: <b>@User.Identity.Name </b></i> <span class="txt"></span></h4> </li>
    <li><a href="@Url.Action("Manage", "Account")" title="Gérer"><i class="fa fa-gear fa-fw"></i> Changer mon mot de passe</a></li>
</ul>
                    End Using
                End If

            </div>
            <!-- /.nav-collapse -->
        </nav>
        <!-- /navbar -->
    </div>
End If
<!-- / #header -->
