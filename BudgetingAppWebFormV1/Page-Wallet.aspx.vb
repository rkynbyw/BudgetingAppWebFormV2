Imports BudgetingApp.BLL
Imports BudgetingApp.BLL.DTOs

Public Class Page_Wallet
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

    Private Function GetTotalBalance() As Decimal
        Dim _walletBLL As New BudgetingApp.BLL.WalletBLL
        Dim userId As Integer = GetUserIdFromSession()

        Dim totalBalance As Decimal = _walletBLL.GetTotalBalance(userId)
        Dim formattedTotalBalance As String = String.Format(New System.Globalization.CultureInfo("id-ID"), "{0:C}", totalBalance)

        totalBalanceValue.InnerText = formattedTotalBalance
    End Function

#Region "Binding Data"
    Sub LoadWallet()
        Dim _walletBLL As New BudgetingApp.BLL.WalletBLL
        Dim results = _walletBLL.GetAll()
        'lvWallet.DataSource = results
        'lvWallet.DataBind()


        Dim _userDto As UserDTO = CType(Session("User"), UserDTO)
        results = _walletBLL.GetWalletDataByUser(_userDto.UserID)
        rptWallet.DataSource = results
        rptWallet.DataBind()
    End Sub

    Sub LoadWalletType()
        Dim _wallettypeBLL As New BudgetingApp.BLL.WalletTypeBLL
        Dim results = _wallettypeBLL.GetAll()

        'lvWalletType.DataSource = results
        'lvWalletType.DataBind()
    End Sub

    Sub BindWalletTypeDD()
        Dim _wallettypeBLL As New BudgetingApp.BLL.WalletTypeBLL
        Dim results = _wallettypeBLL.GetAll()

        ddWalletType.DataSource = results
        ddWalletType.DataTextField = "Name"
        ddWalletType.DataValueField = "WalletTypeID"
        ddWalletType.DataBind()
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            GetTotalBalance()
            GetUserIdFromSession()
            LoadWallet()
            BindWalletTypeDD()
        End If
    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Try
            Dim userId As Integer = GetUserIdFromSession()

            Dim _walletBLL As New WalletBLL()
            Dim _walletDTO As New WalletDTO()

            _walletDTO.WalletName = txtWalletName.Text
            _walletDTO.Balance = Decimal.Parse(txtInitialBalance.Text)
            _walletDTO.UserID = userId

            _walletBLL.Insert(_walletDTO)

            ltMessage.Text = "<span class='alert alert-success'>Wallet added successfully!</span><br/><br/>"
            LoadWallet()

            txtWalletName.Text = String.Empty
            txtInitialBalance.Text = String.Empty
        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Wallet add failed!</span><br/><br/>"
        End Try

    End Sub

    Protected Sub rptWallet_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs) Handles rptWallet.ItemCommand
        Dim _walletBLL As New BudgetingApp.BLL.WalletBLL
        If e.CommandName = "Edit" Then
            ' Handle edit command here
            Dim walletID As Integer = Convert.ToInt32(e.CommandArgument)

            Try
                Response.Redirect("Page-EditWallet?WalletID=" & walletID)
            Catch ex As Exception

            End Try


        ElseIf e.CommandName = "Delete" Then
            Try
                'Dim walletID As Integer = Convert.ToInt32(e.CommandArgument)
                _walletBLL.Delete(CInt(e.CommandArgument.ToString()))
                LoadWallet()
                ltMessage.Text = "<span class='alert alert-success'>Wallet deleted successfully</span><br/><br/>"
            Catch ex As Exception
                ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
            End Try
        End If


    End Sub
End Class