<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
  <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-md-12">
            <asp:Panel ID="loginPanel" runat="server" GroupingText="Use the form below to change your password" CssClass="MyLoginpanel">
                <div class="row">
                    <div class="col-md-4">
                        Current Password:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword"
                            CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Old Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>


                    </div>
                </div>
                <div class="row lineheight">&nbsp;</div>
                <div class="row">
                    <div class="col-md-4">
                        New Password:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword"
                            CssClass="failureNotification" ErrorMessage="New Password is required." ToolTip="New Password is required."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="row lineheight">&nbsp;</div>
                <div class="row">
                    <div class="col-md-4">
                        Confirm New Password:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                            ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword"
                            CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Confirm New Password must match the New Password entry."
                            ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-8">
                        <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                        <asp:Button ID="ChangePasswordPushButton" runat="server"
                            CommandName="ChangePassword" Text="Change Password"
                            ValidationGroup="ChangeUserPasswordValidationGroup"
                            OnClick="ChangePasswordPushButton_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
