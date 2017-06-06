<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="WebExample.Frontend.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
     <!-- Cascade Style Sheets -->
    <link href="~/Content/login.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Javascript Scripts -->
    <script src="<%: ResolveUrl("~/Scripts/jquery-3.1.1.min.js") %>" type="text/javascript"></script>
    <script src="<%: ResolveUrl("~/Scripts/bootstrap.min.js") %>" type="text/javascript"></script>
</head>
<body>
    <div class="container">
        <div class="row">

            <div class="col-xs-10 col-sm-6 col-md-4 col-lg-4 col-md-offset-4 col-sm-offset-3 col-xs-offset-1 col-lg-offset-4">
                <h1 class="text-center login-title"><%=Resources.Resource.Saludos %></h1>
                <div class="account-wall">
                    <img alt="" class="profile-img" src="https://lh5.googleusercontent.com/-b0-k99FZlyE/AAAAAAAAAAI/AAAAAAAAAAA/eu7opA4byxI/photo.jpg?sz=120" />
                    <form class="form-signin" id="form1" runat="server">
                        <asp:DropDownList ID="ddlLanguages" runat="server" AutoPostBack="true" CssClass="form-control">
                            <asp:ListItem Text="<%$Resources:Resource, Español %>" Value="es" />
                            <asp:ListItem Text="<%$Resources:Resource, Ingles %>" Value="en-us" />
                        </asp:DropDownList>
                        <asp:TextBox ID="txtIdentity" runat="server" type="text" class="form-control" placeholder="Email" required autofocus></asp:TextBox>
                        <asp:TextBox ID="txtPassword" runat="server" type="password" class="form-control" placeholder="Password" required></asp:TextBox>
                        <asp:Button ID="btnLogin" runat="server" Text="<%$Resources:Resource, Ingresar %>" class="btn btn-lg btn-primary btn-block" OnClick="btnLogin_Click" />
                        <label class="checkbox pull-left">
                            <input type="checkbox" value="remember-me" />
                            Remember me
                        </label>
                        <a href="#" class="pull-right need-help">Need help? 
                        </a><span class="clearfix"></span>
                    </form>
                </div>
                <a href="#" class="text-center new-account">Create an account </a>
            </div>
        </div>
    </div>
</body>
</html>
