<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="AssignforExecuion.aspx.cs" Inherits="AssignforExecuion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" Runat="Server">
        <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <br />
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">
            <asp:Button ID="btnBack" runat="server" Text="Go Back" OnClick="btnBack_Click" />
        </div>
        <div class="col-md-2"></div>
    </div>
 <div class="row">
     <div class="col-md-12 text-center">
         <h3>Assign Client to Survey Engineer</h3><br />
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
        <div class="col-md-8 text-left"><span class="label label-primary">Survey Engineer</span><asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" DataTextField="EmployeeName" DataValueField="UserID"></asp:DropDownList></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></div>
        <div class="col-md-2"></div>
    </div>

    <div class="row lineheight"></div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">

            <asp:GridView ID="gvExecutionEngg" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ID" ForeColor="Black" GridLines="Vertical" OnRowDeleting="gvExecutionEngg_RowDeleting" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ButtonType="Button" DeleteText="x" ShowDeleteButton="True" />
                    <asp:BoundField DataField="UserID" HeaderText="Employee ID" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="True"/>
                    <asp:BoundField DataField="Designation" HeaderText="Designation" ReadOnly="True"/>
                    <asp:BoundField DataField="Mobile" HeaderText="Mobile" ReadOnly="True"/>
                    <asp:BoundField DataField="EmailID" HeaderText="Email" ReadOnly="True"/>
                    
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#e7e7e7" Font-Bold="True" ForeColor="Black" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F4F4F4" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>

        </div>
        <div class="col-md-2"></div>
    </div>
</asp:Content>

