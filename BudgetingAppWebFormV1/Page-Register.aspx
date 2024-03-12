<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Page-Register.aspx.vb" Inherits="BudgetingAppWebFormV1.Page_Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - My Finance App</title>
    <!-- Custom fonts for this template-->
    <link href="~/Startbootstrap/vendor/fontawesome-free/css/all.min.css" rel="stylesheet" runat="server" type="text/css" />
    <link
        href="https://fonts.googleapis.com/css?family=Nunito:200,200i,300,300i,400,400i,600,600i,700,700i,800,800i,900,900i"
        rel="stylesheet" />

    <!-- Custom styles for this template-->
    <link href="~/Startbootstrap/css/sb-admin-2.min.css" rel="stylesheet" runat="server" />
</head>

<body class="bg-gradient-primary">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
        <div class="container">

            <div class="card o-hidden border-0 shadow-lg my-5">
                <div class="card-body p-0">
                    <!-- Nested Row within Card Body -->
                    <div class="row">
                        <div class="col-lg-5 d-none d-lg-block bg-register-image"></div>
                        <div class="col-lg-7">
                            <div class="p-5">
                                <div class="text-center">
                                    <h1 class="h4 text-gray-900 mb-4">Create an Account!</h1>
                                </div>
                                <div>
                                    <asp:Literal ID="ltMessage" runat="server" />
                                    <div class="form-group">
                                        <label for="txtEmail">Email :</label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group">
                                        <label for="txtUsername">Username :</label>
                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                                    </div>
                                    <div class="form-group row">
                                        <div class="col-sm-6 mb-3 mb-sm-0">
                                            <label for="txtPassword">Password :</label>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"/>
                                        </div>
                                        <div class="col-sm-6">
                                            <label for="txtRePassword">RePassword :</label>
                                            <asp:TextBox ID="txtRePassword" runat="server" CssClass="form-control" TextMode="Password"/>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtFullName">Full Name :</label>
                                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                                    </div>
                                    <hr />

                                    <asp:Button ID="btnRegistration" Text="Register" CssClass="btn btn-success btn-sm" runat="server" OnClick="btnRegistration_Click" />


                                </div>

                                <hr />
                                <div class="text-center" visibility="false">
                                    <a class="small" href="forgot-password.html">Forgot Password?</a>
                                </div>
                                <div class="text-center">
                                    <a class="small" href="Page-Login.aspx">Already have an account? Login!</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>


    <%: Scripts.Render("~/Startbootstrap/vendor/bootstrap/js/bootstrap.bundle.min.js") %>
    <%: Scripts.Render("~/Startbootstrap/vendor/jquery-easing/jquery.easing.min.js") %>
    <%: Scripts.Render("~/Startbootstrap/js/sb-admin-2.min.js") %>
</body>

</html>
