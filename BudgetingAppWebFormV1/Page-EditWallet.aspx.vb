Imports BudgetingApp.BLL
Imports BudgetingApp.BLL.DTOs

Public Class Page_EditWallet
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
            LoadUserWallet()
        End If
    End Sub

    'Private Sub LoadUserWallet()
    '    If Not String.IsNullOrEmpty(Request.QueryString("WalletID")) AndAlso Integer.TryParse(Request.QueryString("WalletID"), 0) Then
    '        Dim walletID As Integer = Integer.Parse(Request.QueryString("WalletID"))

    '        Dim _walletBL As New WalletBLL

    '        Dim wallet As WalletDTO = _walletBL.GetById(walletID)

    '        If wallet IsNot Nothing Then
    '            txtWalletName.Text = wallet.WalletName
    '            txtInitialBalance.Text = wallet.Balance
    '            txtUserID.Text = wallet.UserID
    '        Else
    '            ltMessage.Text = "Wallet is Null"
    '        End If

    '    End If
    'End Sub

    Private Sub LoadUserWallet()
        'If Not String.IsNullOrEmpty(Request.QueryString("WalletID")) AndAlso Integer.TryParse(Request.QueryString("WalletID"), 0) Then
        Dim walletID As Integer = Integer.Parse(Request.QueryString("WalletID"))

        Dim _walletBL As New WalletBLL

        Dim wallet As WalletDTO = _walletBL.GetById(walletID)

        If wallet IsNot Nothing Then
            txtWalletName.Text = wallet.WalletName
            txtInitialBalance.Text = wallet.Balance.ToString()
            txtWalletID.Text = wallet.WalletID.ToString()
            txtUserID.Text = wallet.UserID
        Else
            ltMessage.Text = "Wallet is Null"
        End If
        'End If
    End Sub


    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs)
        Try
            Dim _walletBLL As New WalletBLL()
            Dim _walletDto As New WalletDTO()

            _walletDto.WalletName = txtWalletName.Text
            _walletDto.Balance = Decimal.Parse(txtInitialBalance.Text)
            _walletDto.WalletID = CInt(txtWalletID.Text)
            _walletDto.UserID = CInt(txtUserID.Text)

            _walletBLL.Update(_walletDto)

            ltMessage.Text = "<span class='alert alert-success'>Wallet edited successfully!</span><br/><br/>"
            Response.Redirect("Page-Wallet")

        Catch ex As Exception
            ltMessage.Text = "<span class='alert alert-danger'>Error: " & ex.Message & "</span><br/><br/>"
        End Try
    End Sub
End Class