<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="SubmitSurveyConfirmation.aspx.cs" Inherits="SubmitSurveyConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-md-12 text-center text-capitalize text-danger">
            <asp:Label ID="lblMessage" runat="server" Text="" ></asp:Label>
            <asp:Label ID="lblMessage1" runat="server" Text=""></asp:Label>
          </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center text-capitalize text-info">
            Select Back Office: <asp:DropDownList ID="ddlBackOffice" runat="server" ></asp:DropDownList>
            
          </div>
    </div>
    <div class="row">
        <div class="col-md-6 text-right">
            <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" />
        </div>
        <div class="col-md-6 text-left">
            <asp:Button ID="btnNo" runat="server" Text="No" OnClick="btnNo_Click" />
        </div>
        </div>
</asp:Content>
