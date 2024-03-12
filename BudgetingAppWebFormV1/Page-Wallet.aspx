<%@ Page Title="Wallet" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page-Wallet.aspx.vb" Inherits="BudgetingAppWebFormV1.Page_Wallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1 class="h3 mb-4 text-gray-800">Wallet and Balance Page
    </h1>

    <div class="card shadow mb-4">
        <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">User Balance View</h6>
        </div>
        <div class="card-body">
            <asp:Literal ID="ltMessage" runat="server" />

            <div>
                <div class="row" hidden>
                    <div class="col-4">
                        <div class="form-group">
                            <label for="ddWalletType">Filter by Wallet Type</label>
                            <asp:DropDownList ID="ddWalletType" runat="server"
                                CssClass="form-control mb-1">
                            </asp:DropDownList>
                            <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary" runat="server" Text="Find" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-xl-3 col-md-6 mb-4">
                    <div class="card border-left-primary shadow h-100 py-2">
                        <div class="card-body">
                            <div class="row no-gutters align-items-center">
                                <div class="col mr-2">
                                    <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                        Total Balance
                                    </div>
                                    <div runat="server" class="h5 mb-0 font-weight-bold text-gray-800 mb-3" id="totalBalanceValue">
                                    </div>
                                </div>
                                <div class="col-auto">
                                    <i class="fas fa-money-bill fa-2x text-gray-300"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container-fluid">
                <button id="crtButton" type="button" class="btn btn-success mb-3" data-toggle="modal" data-target="#addWalletModal">
                    Create New Wallet
                </button>
                <div class="modal fade" id="addWalletModal" tabindex="-1" role="dialog" aria-labelledby="addWalletModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">

                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="addWalletModalLabel">Add New Transaction</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="container">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtWalletName">Wallet Name</label>
                                                <asp:TextBox ID="txtWalletName" runat="server" CssClass="form-control" placeholder="Enter Wallet Name" />
                                            </div>
                                            <div class="form-group">
                                                <label for="txtInitialBalance">Balance</label>
                                                <asp:TextBox ID="txtInitialBalance" runat="server" CssClass="form-control" placeholder="Enter Initial Balance" />
                                            </div>

                                            <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <asp:Repeater ID="rptWallet" runat="server" OnItemCommand="rptWallet_ItemCommand">
                        <ItemTemplate>
                            <div class="col-xl-3 col-md-6 mb-4">
                                <div class="card border-left-primary shadow h-100 py-2">
                                    <div class="card-body">
                                        <div class="row no-gutters align-items-center">
                                            <div class="col mr-2">
                                                <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                                    <%# Eval("WalletName") %>
                                                </div>
                                                <div class="h5 mb-0 font-weight-bold text-gray-800 mb-3">
                                                    <%# String.Format(New System.Globalization.CultureInfo("id-ID"), "{0:C}", Eval("Balance")) %>
                                                </div>
                                                <asp:LinkButton ID="btnEdit" CssClass="btn btn-primary btn-sm" runat="server" CommandName="Edit"
                                                    CommandArgument='<%# Eval("WalletID") %>' Text="Edit" />
                                                <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger btn-sm " runat="server" CommandName="Delete"
                                                    Text="Delete"
                                                    CommandArgument='<%# Eval("WalletID") %>'
                                                    OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                                            </div>
                                            <div class="col-auto">
                                                <i class="fas fa-wallet fa-2x text-gray-300"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>


        </div>
    </div>

</asp:Content>
