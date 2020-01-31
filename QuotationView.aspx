<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="QuotationView.aspx.cs" Inherits="QuotationView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
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
                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
                </div>
            </div>
            <%--<div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-2 text-left">
                    <span class="label label-primary">Select Currency for Conversation</span><asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>  
                </div>
                <div class="col-md-1 text-left">
                    <span class="label label-primary">Today Rate</span><asp:TextBox ID="txtValue" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtValue_TextChanged"></asp:TextBox>  
                </div>
                
                <div class="col-md-2"></div>
            </div>--%>
            <br />
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8 text-left">
                    Quotation Number:<asp:Label ID="lblQuotationNumber" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-2"></div>
            </div>

            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8 text-left">
                    <asp:RadioButtonList ID="rblVersion" runat="server" CssClass="padding-5" RepeatDirection="Horizontal" RepeatLayout="Flow"></asp:RadioButtonList> 
                </div>
                <div class="col-md-2"></div>
            </div>

            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8 text-left">
                    <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-8">
                    <div class="row text-center">
                        <div class="col-md-3"><asp:Button ID="btnWithGSTwithDescription" runat="server" Text="With GST & Description" OnClick="btnWithGSTwithDescription_Click" /></div>
                        <div class="col-md-3"><asp:Button ID="btnWithGSTWithoutDescription" runat="server" Text="With GST & Without Description" OnClick="btnWithGSTWithoutDescription_Click" /></div>
                        <div class="col-md-3"><asp:Button ID="btnWithoutGSTWithDescription" runat="server" Text="Without GST & With Description" OnClick="btnWithoutGSTWithDescription_Click" /></div>
                        <div class="col-md-3"><asp:Button ID="btnWithoutGSTWithoutDescription" runat="server" Text="Without GST & Without Description" OnClick="btnWithoutGSTWithoutDescription_Click" /></div>
                    </div>
                </div>
                <div class="col-md-2"></div>
            </div>
            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                <div class="col-md-2">
                </div>
                <div class="col-md-8">
                    <rsweb:ReportViewer ID="rv" runat="server" Width="100%"></rsweb:ReportViewer>
                </div>
                <div class="col-md-2"></div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

