<%@ Page Language="C#" MasterPageFile="~/PlannerDefault.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="head">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

        <div class="row">
            <div class="col-md-12">
                <asp:Panel ID="loginPanel" runat="server" GroupingText="Account Information" CssClass="MyLoginpanel">
                    <div class="row">
                        <div class="col-md-4">
                            Username/email ID
                        </div>
                    <%--</div>
                    <div class="row">--%>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-1 text-left"></div>--%>
                    </div>

                    <div class="row lineheight">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-4">
                            Password:
                        </div>
                   <%-- </div>
                    <div class="row">--%>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        </div>
                        <%--<div class="col-md-1 text-left"></div>--%>
                    </div>
                    <div class="row lineheight">&nbsp;</div>
                    <div class="row">
                        <div class="col-md-4">
                            
                        </div>
                        <div class="col-md-8">
                            <asp:Button ID="btnLogin" runat="server" OnClick="LoginButton_Click" Text="Log In" CssClass="btn btn-primary"/>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12"><a href="ForgotPassword.aspx">Forgot Password?</a>
                            <asp:Label ID="lblError" runat="server" ForeColor="#CC3300"></asp:Label>
                        </div>
                    </div>
                    <br />
                </asp:Panel>
            </div>
    </div>
</asp:Content>
