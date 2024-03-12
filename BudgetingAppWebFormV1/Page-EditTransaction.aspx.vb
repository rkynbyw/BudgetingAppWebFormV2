Imports BudgetingApp.BLL
Imports BudgetingApp.BLL.DTOs

Public Class Page_EditTransaction
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetUserIdFromSession()
            LoadTransactionData()
        End If
    End Sub

    Private Sub LoadTransactionData()
        If Not String.IsNullOrEmpty(Request.QueryString("TransactionID")) AndAlso Integer.TryParse(Request.QueryString("TransactionID"), 0) Then
            Dim transactionID As Integer = Integer.Parse(Request.QueryString("TransactionID"))

            Dim _transactionBLL As New TransactionBLL()
            Dim transaction As TransactionDTO = _transactionBLL.GetById(transactionID)

            LoadUserWallet()
            ddlWalletSource.SelectedValue = transaction.WalletID

            If transaction IsNot Nothing Then
                txtTransactionID.Text = transaction.TransactionID.ToString()
                txtDate.Text = transaction.Date.ToString("yyyy-MM-dd")
                txtUserID.Text = transaction.UserID.ToString()
                txtAmount.Text = transaction.Amount.ToString()
                txtDescription.Text = transaction.Description

                ddlTransactionType.Items.Add(New ListItem("Pengeluaran", "1"))
                ddlTransactionType.Items.Add(New ListItem("Pemasukan", "2"))
                If transaction.TransactionCategoryID <= 6 Then
                    ddlTransactionType.SelectedValue = "1"
                Else
                    ddlTransactionType.SelectedValue = "2"
                End If
                ddlTransactionType.DataBind()

                Dim initialTransactionTypeId As Integer = Integer.Parse(ddlTransactionType.SelectedValue)
                LoadTransactionCategory(initialTransactionTypeId)
                ddlTransactionCategory.SelectedValue = transaction.TransactionCategoryID
            Else
                Response.Redirect("Page-Error.aspx")
            End If
        End If
    End Sub


    Sub LoadUserWallet()
        Dim _walletBLL As New WalletBLL()
        Dim _userDto As UserDTO = CType(Session("User"), UserDTO)
        Dim userId As Integer = _userDto.UserID

        ddlWalletSource.DataSource = _walletBLL.GetWalletDataByUser(userId)
        ddlWalletSource.DataTextField = "WalletName"
        ddlWalletSource.DataValueField = "WalletID"
        ddlWalletSource.DataBind()
    End Sub

    Sub LoadTransactionCategory(transactionTypeId As Integer)
        Dim _categoryBLL As New TransactionCategoryBLL()
        ddlTransactionCategory.DataSource = _categoryBLL.GetCategoryNameByType(transactionTypeId)
        ddlTransactionCategory.DataTextField = "Name"
        ddlTransactionCategory.DataValueField = "TransactionCategoryID"
        ddlTransactionCategory.DataBind()
    End Sub



    Protected Sub ddlTransactionType_SelectedIndexChanged(sender As Object, e As EventArgs)
        ViewState("SelectedTransactionType") = ddlTransactionType.SelectedValue

        Dim selectedTransactionTypeId As Integer = Integer.Parse(ddlTransactionType.SelectedValue)
        LoadTransactionCategory(selectedTransactionTypeId)
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Try
            Dim _transactionBLL As New TransactionBLL()
            Dim _transactionDto As New TransactionDTO()

            _transactionDto.TransactionID = CInt(txtTransactionID.Text)
            _transactionDto.UserID = CInt(txtUserID.Text)
            _transactionDto.Date = DateTime.Parse(txtDate.Text)
            _transactionDto.Amount = Decimal.Parse(txtAmount.Text)
            _transactionDto.TransactionCategoryID = CInt(ddlTransactionCategory.SelectedValue)
            _transactionDto.Description = txtDescription.Text
            _transactionDto.WalletID = CInt(ddlWalletSource.SelectedValue)

            _transactionBLL.Update(_transactionDto)

            ltMessage.Text = "<span class='alert alert-success'>Transaction edited successfully!</span><br/><br/>"

            Response.Redirect("Page-Transaction")

        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try

    End Sub


End Class
