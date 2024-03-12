<%@ Page Title="Edit Wallet" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page-EditWallet.aspx.vb" Inherits="BudgetingAppWebFormV1.Page_EditWallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                                <div class="form-group" hidden>
                                    <label for="txtUserID">Wallet ID</label>
                                    <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" Enabled="false"/>
                                </div>
                                <div class="form-group">
                                    <label for="txtWalletID">Wallet ID</label>
                                    <asp:TextBox ID="txtWalletID" runat="server" CssClass="form-control" Enabled="false" />
                                </div>
                                <div class="form-group">
                                    <label for="txtWalletName">Wallet Name</label>
                                    <asp:TextBox ID="txtWalletName" runat="server" CssClass="form-control" placeholder="Enter Wallet Name" />
                                </div>
                                <div class="form-group">
                                    <label for="txtInitialBalance">Balance</label>
                                    <asp:TextBox ID="txtInitialBalance" runat="server" CssClass="form-control" placeholder="Enter Initial Balance" />
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
