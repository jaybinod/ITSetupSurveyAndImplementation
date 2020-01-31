<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="ClientAddContact.aspx.cs" Inherits="ClientAddContact" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-md-12 text-center">
            <h3>Add New Contact for Company <asp:Label ID="lblClient" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label></h3>
            <br />
            <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
        </div>
    </div>

    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Contact Person</span><asp:TextBox ID="txtContact" runat="server" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>

    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left"><span class="label label-primary">Designation</span><asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left">
            <span class="label label-primary">Email</span><asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
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
        <div class="col-md-8 text-left"><span class="label label-primary">Role</span><asp:TextBox ID="txtRole" runat="server" CssClass="form-control"></asp:TextBox></div>
        <div class="col-md-2"></div>
    </div>
    <div class="row lineheight">&nbsp;</div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8 text-left">
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        </div>
        <div class="col-md-2"></div>
    </div>
    <div class="row">
        <div class="col-md-2"></div>
        <div class="col-md-8">

            <asp:GridView ID="gvOrgContact" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PeopleID" ForeColor="Black" GridLines="Vertical" OnRowCancelingEdit="gvOrgContact_RowCancelingEdit" OnRowDeleting="gvOrgContact_RowDeleting" OnRowEditing="gvOrgContact_RowEditing" OnRowUpdating="gvOrgContact_RowUpdating" Width="100%">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ButtonType="Button" DeleteText="x" ShowDeleteButton="True" />
                    <asp:BoundField DataField="PeopleID" HeaderText="Contact ID" ReadOnly="True" Visible="False" />
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtKPname" runat="server" CssClass="Mytextbox" Text='<%# Eval("PeopleName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("PeopleName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Designation">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtKPDesignation" runat="server" CssClass="Mytextbox" Text='<%# Bind("PeopleDesignation") %>' Width="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("PeopleDesignation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Contact No">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtKPContact" runat="server" CssClass="Mytextbox" Text='<%# Bind("PeopleContactNo") %>' Width="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("PeopleContactNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtKPEmail" runat="server" CssClass="Mytextbox" Text='<%# Bind("PeopleEmail") %>' Width="100"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("PeopleEmail") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Role">
                        <EditItemTemplate>
                            <asp:TextBox ID="txtKPRole" runat="server" CssClass="Mytextbox" Text='<%# Bind("PeopleRole") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("PeopleRole") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True">
                        <ItemStyle Width="140px" />
                    </asp:CommandField>
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

