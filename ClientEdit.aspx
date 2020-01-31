<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="ClientEdit.aspx.cs" Inherits="ClientEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
        <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
     <div class="col-md-12 text-center">
         <h3>Add New Client</h3><br />
         <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
     </div>
     </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left">
            <span class="label label-primary">Client Name</span><asp:TextBox ID="txtClientName" runat="server" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Address Line 1</span><asp:TextBox ID="txtAddress1" MaxLength="50" runat="server" CssClass="form-control" ></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Address Line 2</span><asp:TextBox ID="txtAddress2" MaxLength="50" runat="server" CssClass="form-control" ></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">City</span><asp:TextBox ID="txtCity" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">State</span><asp:TextBox ID="txtState" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Postal Code</span><asp:TextBox ID="txtPostalCode" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Country</span><asp:TextBox ID="txtCountry" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">GST No</span><asp:TextBox ID="txtGSTNo" runat="server" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>

    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></div>
        <div class="col-md-2"></div>
    </div>

</asp:Content>

