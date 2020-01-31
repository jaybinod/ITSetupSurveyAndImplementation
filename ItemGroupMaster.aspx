<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="ItemGroupMaster.aspx.cs" Inherits="ItemGroupMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
        <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
           
 
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-5 text-left">
            <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control" MaxLength="30" placeholder="Group Name" AutoPostBack="True"></asp:TextBox></div>
        <div class="col-md-1 text-left"><asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn-primary form-control" OnClick="btnSubmit_Click" />
            </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    
    <div class="row">
        <div class="col-md-1"></div>
        <div class="col-md-10">
            <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-1"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

