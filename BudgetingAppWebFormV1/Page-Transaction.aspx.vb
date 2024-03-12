Imports BudgetingApp.BLL
Imports BudgetingApp.BLL.DTOs

Public Class Page_Transaction
    Inherits System.Web.UI.Page

    Private Function GetUserIdFromSession() As Integer
        If Session("User") IsNot Nothing AndAlso TypeOf Session("User") Is UserDTO Then
            Dim _userDto As UserDTO = CType(Session("User"), UserDTO)
            Return _userDto.UserID
        Else
            Response.Redirect("Page-Login.aspx")
            Return -1
        End If
    End Function



#Region "Binding Data"
    Sub LoadTransaction()
        Dim _transactionBLL As New BudgetingApp.BLL.TransactionBLL
        Dim results = _transactionBLL.GetAll()
        lvTransaction.DataSource = results
        lvTransaction.DataBind()
    End Sub

    'Sub LoadExepnseIncome()
    '    Dim _transactionBLL As New BudgetingApp.BLL.TransactionBLL
    '    Dim userId As Integer = GetUserIdFromSession()

    '    Dim expense = _transactionBLL.GetUserExpense(userId)
    '    userExpense.Text = "Total Expense " & expense

    '    Dim income = _transactionBLL.GetUserIncome(userId)
    '    userIncome.Text = "Total Income " & income
    'End Sub

    Sub LoadExpenseIncome()
        Dim _transactionBLL As New BudgetingApp.BLL.TransactionBLL
        Dim userId As Integer = GetUserIdFromSession()

        Dim expenseAmount As Decimal = _transactionBLL.GetUserExpense(userId)
        Dim expense As String = FormatCurrency(expenseAmount, 2, TriState.True, TriState.False, TriState.True)

        userExpense.Text = "Total Expense: " & expense


        Dim incomeAmount As Decimal = _transactionBLL.GetUserIncome(userId)
        Dim income As String = FormatCurrency(incomeAmount, 2, TriState.True, TriState.False, TriState.True)

        userIncome.Text = "Total Income: " & income
    End Sub

    Sub LoadExpenseIncomeCard()
        Dim _transactionBLL As New BudgetingApp.BLL.TransactionBLL
        Dim userId As Integer = GetUserIdFromSession()

        Dim expenseAmount As Decimal = _transactionBLL.GetUserExpense(userId)
        Dim formattedExpense As String = String.Format(New System.Globalization.CultureInfo("id-ID"), "{0:C}", expenseAmount)

        Dim incomeAmount As Decimal = _transactionBLL.GetUserIncome(userId)
        Dim formattedIncome As String = String.Format(New System.Globalization.CultureInfo("id-ID"), "{0:C}", incomeAmount)

        incomeValue.InnerText = formattedIncome
        expenseValue.InnerText = formattedExpense
    End Sub




    Sub LoadUserTransaction()
        'Dim _transactionBLL As New TransactionBLL()
        'Dim userId As Integer = 1

        'Dim transactions = _transactionBLL.GetUserTransaction(userId)

        'lvTransaction.DataSource = transactions
        'lvTransaction.DataBind()

        Dim _transactionBLL As New TransactionBLL()

        If Session("User") IsNot Nothing AndAlso TypeOf Session("User") Is UserDTO Then
            'Dim userDto As UserDTO = DirectCast(Session("User"), UserDTO)
            Dim _userDto As UserDTO = CType(Session("User"), UserDTO)
            Dim userId As Integer = _userDto.UserID
            userSession.Text = "UserID: " & userId & ", Username: " & _userDto.Username & ", Email: " & _userDto.Email

            'userId = 1
            Dim transactions = _transactionBLL.GetUserTransaction(userId)

            txtUserID.Text = userId


            lvTransaction.DataSource = transactions
            lvTransaction.DataBind()
        Else
            Response.Redirect("~/Page_Login.aspx")
        End If
    End Sub

#End Region

#Region "Load Create and Edit Data"
    Sub LoadUserWallet()
        Dim _walletBLL As New WalletBLL()
        Dim _userDto As UserDTO = CType(Session("User"), UserDTO)
        Dim userId As Integer = _userDto.UserID

        ddlWalletSource.DataSource = _walletBLL.GetWalletDataByUser(userId)
        ddlWalletSource.DataTextField = "WalletName"
        ddlWalletSource.DataValueField = "WalletID"
        ddlWalletSource.DataBind()
    End Sub

    Sub LoadTransactionType()
        ddlTransactionType.Items.Clear()
        ddlTransactionType.Items.Add(New ListItem("Pengeluaran", "1"))
        ddlTransactionType.Items.Add(New ListItem("Pemasukan", "2"))

        If ViewState("SelectedTransactionType") IsNot Nothing Then
            ddlTransactionType.SelectedValue = ViewState("SelectedTransactionType").ToString()
        End If

        Dim initialTransactionTypeId As Integer = Integer.Parse(ddlTransactionType.SelectedValue)
        LoadTransactionCategory(initialTransactionTypeId)
    End Sub


    Sub LoadTransactionCategory(transactionTypeId As Integer)
        Dim _categoryBLL As New TransactionCategoryBLL()
        ddlTransactionCategory.DataSource = _categoryBLL.GetCategoryNameByType(transactionTypeId)
        ddlTransactionCategory.DataTextField = "Name"
        ddlTransactionCategory.DataValueField = "TransactionCategoryID"
        ddlTransactionCategory.DataBind()
    End Sub



#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LoadExpenseIncomeCard()
            GetUserIdFromSession()
            LoadUserTransaction()
            LoadExpenseIncome()
            LoadUserWallet()
            LoadTransactionType()
            Dim initialTransactionTypeId As Integer = Integer.Parse(ddlTransactionType.SelectedValue)
            LoadTransactionCategory(initialTransactionTypeId)
        End If
    End Sub


    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            Dim userId As Integer = GetUserIdFromSession()

            If userId <> -1 Then
                Dim _transactionBLL As New TransactionBLL()
                Dim _transactionDto As New TransactionDTO()

                _transactionDto.UserID = userId
                _transactionDto.Date = DateTime.Parse(txtDate.Text)
                _transactionDto.Amount = Decimal.Parse(txtAmount.Text)
                _transactionDto.TransactionCategoryID = CInt(ddlTransactionCategory.SelectedValue)
                _transactionDto.Description = txtDescription.Text
                _transactionDto.WalletID = CInt(ddlWalletSource.SelectedValue)

                _transactionBLL.Insert(_transactionDto)

                ltMessage.Text = "<span class='alert alert-success'>Transaction added successfully!</span><br/><br/>"
                LoadUserTransaction()
                LoadExpenseIncomeCard()



                txtDate.Text = String.Empty
                txtAmount.Text = String.Empty
                ddlTransactionCategory.SelectedIndex = 0
                txtDescription.Text = String.Empty
                ddlWalletSource.SelectedIndex = 0
            End If
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub


    Protected Sub ddlTransactionType_SelectedIndexChanged(sender As Object, e As EventArgs)
        ViewState("SelectedTransactionType") = ddlTransactionType.SelectedValue

        Dim selectedTransactionTypeId As Integer = Integer.Parse(ddlTransactionType.SelectedValue)
        LoadTransactionCategory(selectedTransactionTypeId)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ShowModalScript", "$('#addTransactionModal').modal('show');", True)
    End Sub

    Protected Sub lvTransaction_ItemCommand(ByVal sender As Object, ByVal e As ListViewCommandEventArgs)
        If e.CommandName = "Edit" Then

            Dim transactionID As String = e.CommandArgument.ToString()
            Response.Redirect("Page-EditTransaction.aspx?TransactionID=" & transactionID)
        End If

        If e.CommandName = "Delete" Then
            Try
                Dim _transactionBLL As New TransactionBLL()
                _transactionBLL.Delete(CInt(e.CommandArgument.ToString()))
                'lvTransaction.DataBind()
                ltMessage.Text = "<span class='alert alert-success'>Artice deleted successfully</span><br/><br/>"
            Catch ex As Exception
                ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
            End Try

        End If

        ViewState("Command") = e.CommandArgument
    End Sub

    Protected Sub lvTransaction_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ViewState("Command") = "Delete" Then
            Try
                Dim selectedIndex As Integer = lvTransaction.SelectedIndex

                If selectedIndex >= 0 AndAlso selectedIndex < lvTransaction.Items.Count Then
                    Dim transactionID As Integer = CInt(lvTransaction.DataKeys(selectedIndex).Value)

                    Dim _transactionBLL As New TransactionBLL()
                    _transactionBLL.Delete(transactionID)

                    'lvTransaction.DataBind()
                    ltMessage.Text = "<span class='alert alert-success'>Transaction deleted successfully</span><br/><br/>"
                Else
                    ltMessage.Text = "<span class='alert alert-danger'>Invalid selected index</span><br/><br/>"
                End If
            Catch ex As Exception
                ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
            End Try
        End If
    End Sub

    Protected Sub lvTransaction_SelectedIndexChanging(sender As Object, e As ListViewSelectEventArgs)

    End Sub
    Protected Sub lvTransaction_ItemDeleting(ByVal sender As Object, ByVal e As ListViewDeleteEventArgs)

        Try
            Dim selectedIndex As Integer = e.ItemIndex

            If selectedIndex >= 0 AndAlso selectedIndex < lvTransaction.Items.Count Then

                Dim transactionID As Integer = CInt(lvTransaction.DataKeys(selectedIndex).Value)

                Dim _transactionBLL As New TransactionBLL()
                _transactionBLL.Delete(transactionID)

                LoadUserTransaction()
                LoadExpenseIncome()
                LoadUserWallet()
                LoadTransactionType()
                Dim initialTransactionTypeId As Integer = Integer.Parse(ddlTransactionType.SelectedValue)
                LoadTransactionCategory(initialTransactionTypeId)

                lvTransaction.DataBind()

                ltMessage.Text = "<span class='alert alert-success'>Transaction deleted successfully</span><br/><br/>"
            Else
                ltMessage.Text = "<span class='alert alert-danger'>Invalid selected index</span><br/><br/>"
            End If
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub

    Protected Sub lvTransaction_ItemDataBound(ByVal sender As Object, ByVal e As ListViewItemEventArgs)
        If e.Item.ItemType = ListViewItemType.DataItem Then
            Dim dataItem As ListViewDataItem = DirectCast(e.Item, ListViewDataItem)
            Dim transactionType As Integer = Convert.ToInt32(DataBinder.Eval(dataItem.DataItem, "TransactionCategory.TransactionTypeID"))
            Dim row As HtmlTableRow = DirectCast(e.Item.FindControl("yourTableRowID"), HtmlTableRow)

            If transactionType = 1 Then
                row.Attributes("class") = "expense-class"
            ElseIf transactionType = 2 Then
                row.Attributes("class") = "pemasukan-class"
            End If
        End If
    End Sub

End Class

