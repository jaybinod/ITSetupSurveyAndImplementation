<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/PlannerDefault.master" AutoEventWireup="true"
    CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="Head">
  <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-md-12">
            <asp:Panel ID="loginPanel" runat="server" GroupingText="Use the form below to retrive your user ID/Password" CssClass="MyLoginpanel">
                <div class="row">
                    <div class="col-md-4">
                        Registered Email ID:
                    </div>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtuserEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="txtuserEmail"
                            CssClass="failureNotification" ErrorMessage="Required." ValidationGroup="ChangeUserPasswordValidationGroup"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtuserEmail" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>

                    </div>
                </div>
             <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-8">
                        <asp:Label ID="lblMessage" runat="server" CssClass="alert-info" Text=""></asp:Label>
                        </div>
                 </div>
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-8">
                           <asp:Button ID="ForgotPushButton" runat="server"
                            CommandName="ChangePassword" Text="Submit"
                            ValidationGroup="ChangeUserPasswordValidationGroup"
                            OnClick="ChangePasswordPushButton_Click" />
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
