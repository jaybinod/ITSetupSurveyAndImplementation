<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="SurveyVerifyPage.aspx.cs" Inherits="SurveyVerifyPage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
            <br /><br />
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <%--<asp:Button ID="btnBack" runat="server" Text="Go Back" OnClick="btnBack_Click" />--%>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <%--<h3>Site Survey</h3><br />--%>
                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label><br /><br />
                    <asp:Label ID="lblMessage1" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
                </div>
            </div><br/><br /><br />
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4 text-center">
                    <asp:Button ID="btnYes" runat="server" Text="Yes" Width="100" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-4 text-center">
                    <asp:Button ID="btnNo" runat="server" Text="No" Width="100" OnClick="btnNo_Click" />
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row lineheight">&nbsp;</div>
    <br /><br /><br />
</asp:Content>

