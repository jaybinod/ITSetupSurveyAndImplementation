<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="NewEmployee.aspx.cs" Inherits="NewEmployee" %>

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
         <h3>Add New Employee</h3><br />
         <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
     </div>
     </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left">
            <span class="label label-primary">Employee Name</span><asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Department</span><asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" DataTextField="Department" DataValueField="DepartmentID"></asp:DropDownList></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Email ( Employee Login ID )</span><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Required" CssClass="label-warning"></asp:RequiredFieldValidator>
        </div>
        <div class="col-md-2"></div>
    </div>
    
    <div class="row lineheight">&nbsp;</div>
   
    
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Mobile No</span><asp:TextBox ID="txtmobile" runat="server" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
      <div class="col-md-2"></div>
        <div class="col-md-8 text-left">
            <span class="label label-primary">Designation</span><asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    
    
    
    <%--<div class="row lineheight">&nbsp;</div>
      <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><asp:CheckBox ID="chkAlertNewDealer" Text="Alert for add defaulter Dealer" runat="server" CssClass="form-control"></asp:CheckBox><br /><asp:CheckBox ID="chkExistingDealermodification" Text="Alert for Modification of existing defaulter Dealer" runat="server" CssClass="form-control"></asp:CheckBox></div>
          <div class="col-md-2"></div>
    </div>--%>
    <%--<div class="row lineheight">&nbsp;</div>
      <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><asp:CheckBox ID="chkAlertNewDealer" Text="Alert for add defaulter Dealer" runat="server" CssClass="form-control"></asp:CheckBox><br /><asp:CheckBox ID="chkExistingDealermodification" Text="Alert for Modification of existing defaulter Dealer" runat="server" CssClass="form-control"></asp:CheckBox></div>
          <div class="col-md-2"></div>
    </div>--%>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></div>
        <div class="col-md-2"></div>
    </div>

</asp:Content>

