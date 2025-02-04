﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Page-Login.aspx.vb" Inherits="BudgetingAppWebFormV1.Page_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login - My Finance App</title>
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
            <!-- Outer Row -->
            <div class="row justify-content-center">
                <div class="col-xl-10 col-lg-12 col-md-9">
                    <div class="card o-hidden border-0 shadow-lg my-5">
                        <div class="card-body p-0">
                            <!-- Nested Row within Card Body -->
                            <div class="row">
                                <div class="col-lg-6 d-none d-lg-block bg-login-image">
                                    <img src="https://play-lh.googleusercontent.com/27oDxUYHG9xiHxgktqespIj16pilDpimWsuJY0UDMl3mpAn9P2kGodn8Rr1ejNvULw" alt="Alternate Text" max-widht="50%"/>
                                </div>
                                <div class="col-lg-6" style="padding-left: 4rem">
                                    <div class="p-5">
                                        <div class="text-center">
                                            <h1 class="h4 text-gray-900 mb-4">My Finance App!</h1>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtUsername" placeholder="Enter Username..." CssClass="form-control form-control-user" runat="server" />
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtPassword" TextMode="Password"
                                                CssClass="form-control form-control-user" placeholder="Password" runat="server" />
                                        </div>
                                        <hr />
                                        <asp:Button ID="btnLogin" OnClick="btnLogin_Click"
                                            Text="Login" CssClass="btn btn-primary btn-user btn-block" runat="server" />
                                        <asp:Literal ID="ltMessage" runat="server" />
                                        <hr />
                                        <div class="text-center">
                                            <a class="small" href="forgot-password.html">Forgot Password?</a>
                                        </div>
                                        <div class="text-center">
                                            <a class="small" href="Page-Register.aspx">Create an Account!</a>
                                        </div>
                                    </div>
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
            <%: Scripts.Render("https://cdnjs.cloudflare.com/ajax/libs/jquery.tablesorter/2.31.3/js/jquery.tablesorter.min.js") %>
        <%: Scripts.Render("~/Startbootstrap/vendor/datatables/jquery.dataTables.min.js") %>
        <%: Scripts.Render("~/Startbootstrap/vendor/datatables/dataTables.bootstrap4.min.js") %>
</body>
</html>
