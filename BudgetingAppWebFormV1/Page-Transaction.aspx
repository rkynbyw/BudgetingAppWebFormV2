<%@ Page Title="Transaction" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page-Transaction.aspx.vb" Inherits="BudgetingAppWebFormV1.Page_Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="h3 mb-4 text-gray-800">Transaction Page
    </h1>
    <asp:Label ID="userSession" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="userExpense" runat="server" Text="" Visible="false"></asp:Label><br />
    <asp:Label ID="userIncome" runat="server" Text="" Visible="false"></asp:Label>

    <div class="row">
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-primary shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                Expense
                            </div>
                            <div runat="server" class="h5 mb-0 font-weight-bold text-gray-800 mb-3" id="expenseValue">
                            </div>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-arrow-up fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-primary shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                Income
                            </div>
                            <div runat="server" class="h5 mb-0 font-weight-bold text-gray-800 mb-3" id="incomeValue">
                            </div>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-arrow-down fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="card shadow mb-4">
        <div class="card-header py-3">
            <h6 class="m-0 font-weight-bold text-primary">User Transaction View</h6>
        </div>
        <div class="card-body">
            <asp:Literal ID="ltMessage" runat="server" />
            <div class="row">
                <div class="col-lg-12">
                    <!-- Button trigger modal -->
                    <button id="crtButton" type="button" class="btn btn-success mb-3" data-toggle="modal" data-target="#addTransactionModal">
                        Add New Transaction
                    </button>

                    <!-- Modal -->
                    <%--<div class="modal fade" id="addTransactionModal" tabindex="-1" aria-labelledby="addTransactionModalLabel" aria-hidden="true">--%>
                    <%--<div class="modal fade" id="addTransactionModal" tabindex="-1" role="dialog" aria-labelledby="addTransactionModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">--%>
                    <div class="modal fade" id="addTransactionModal" tabindex="-1" role="dialog" aria-labelledby="addTransactionModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">

                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="addTransactionModalLabel">Add New Transaction</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="container">
                                        <div class="row">
                                            <div class="col-md-12">
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
                                                <asp:Button ID="btnSubmit" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <table class="table table-hover">
                        <thead class="">
                            <tr>
                                <td>Action</td>
                                <td>Date</td>

                                <td>Transaction Type</td>
                                <td>Transaction Category</td>
                                <td>Amount</td>
                                <td>Wallet Source</td>
                                <td>Description</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:ListView ID="lvTransaction" DataKeyNames="TransactionID"
                                OnSelectedIndexChanging="lvTransaction_SelectedIndexChanging"
                                OnSelectedIndexChanged="lvTransaction_SelectedIndexChanged"
                                OnItemCommand="lvTransaction_ItemCommand" runat="server"
                                OnItemDeleting="lvTransaction_ItemDeleting">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="btnEdit" CssClass="btn btn-primary" runat="server" CommandName="Edit" CommandArgument='<%# Eval("TransactionID") %>' Text="Edit" />
                                            <asp:LinkButton ID="btnDelete" CssClass="btn btn-danger" runat="server" CommandName="Delete" CommandArgument="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this item?');" />
                                        </td>
                                        <td><%# Convert.ToDateTime(Eval("Date")).ToString("dd/MM/yyyy") %></td>


                                        <td>
                                            <%# If(Eval("TransactionCategory.TransactionTypeID").ToString() = "1", "Pengeluaran", "Pemasukan") %>
                                        </td>
                                        <td><%# Eval("TransactionCategory.Name") %></td>

                                        <td><%# String.Format(New System.Globalization.CultureInfo("id-ID"), "{0:C}", Eval("Amount")) %></td>

                                        <td><%# Eval("Wallet.WalletType.Name") %></td>
                                        <td><%# Eval("Description") %></td>

                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                        </tbody>

                    </table>

                </div>

            </div>




        </div>
    </div>
</asp:Content>
