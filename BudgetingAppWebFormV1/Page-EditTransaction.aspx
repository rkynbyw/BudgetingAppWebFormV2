<%@ Page Title="Edit Transaction" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page-EditTransaction.aspx.vb" Inherits="BudgetingAppWebFormV1.Page_EditTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h3 mb-4 text-gray-800">Update Transaction Page</h1>
    <asp:Label ID="userSession" runat="server" Text=""></asp:Label>


    <div class="card shadow mb-4">
        <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">Update Transaction</h6>
        </div>
        <div class="card-body">
            <asp:Literal ID="ltMessage" runat="server" />
            <div class="row">
                <div class="col-lg-12">
                    <div class="container">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label for="txtTransactionID">Transaction ID</label>
                                    <asp:TextBox ID="txtTransactionID" runat="server" CssClass="form-control" Enabled="false" />
                                </div>
                                <div class="form-group">
                                    <label for="txtDate">Date</label>
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date" />
                                </div>
                                <div class="form-group">
                                    <label for="txtUserID">User ID</label>
                                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" Enabled="false" />
                                </div>
                                <div class="form-group">
                                    <label for="ddlTransactionType">Transaction Type</label>
                                    <asp:DropDownList ID="ddlTransactionType" runat="server"
                                        AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged"
                                        CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="ddlTransactionCategory">Transaction Category</label>
                                    <asp:DropDownList ID="ddlTransactionCategory" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="txtAmount">Amount</label>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" />
                                </div>
                                <div class="form-group">
                                    <label for="ddlWalletSource">Wallet Source</label>
                                    <asp:DropDownList ID="ddlWalletSource" runat="server" CssClass="form-control" />
                                </div>

                                <div class="form-group">
                                    <label for="txtDescription">Description</label>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" />
                                </div>
                                <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
