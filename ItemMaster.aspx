<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="ItemMaster" %>

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

            <div class="row">
                <%--<div class="col-md-1"></div>--%>
                <div class="col-md-2">
                    <asp:DropDownList ID="ddlGroup" runat="server" placeholder="Select Item Group" CssClass="form-control" DataTextField="GroupName" DataValueField="GroupID" AutoPostBack="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-2 text-left">
                    <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" MaxLength="20" placeholder="Item Name" OnTextChanged="txtItemName_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <asp:TextBox ID="txtBrand" runat="server" CssClass="form-control" MaxLength="20" placeholder="Brand" OnTextChanged="txtBrandName_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <asp:TextBox ID="txtPartNumber" runat="server" CssClass="form-control" MaxLength="20" placeholder="Part Number" OnTextChanged="txtPartNumber_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" MaxLength="20" placeholder="Description" OnTextChanged="txtDescription_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" MaxLength="10" placeholder="Unit Nos/Mtr/Doz"></asp:TextBox>
                </div>
            </div>
            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                <div class="col-md-2 text-left">

                    <asp:TextBox ID="txtHSN" runat="server" CssClass="form-control" MaxLength="10" placeholder="HSN Code"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <asp:TextBox ID="txtCGSTPercentage" runat="server" CssClass="form-control" MaxLength="10" placeholder="CGST %(i.e. 0,2.5,6,9,...)"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <asp:TextBox ID="txtSGSTPercentage" runat="server" CssClass="form-control" MaxLength="10" placeholder="SGST %(i.e. 0,2.5,6,9,...)"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left">
                    <asp:TextBox ID="txtIGSTPercentage" runat="server" CssClass="form-control" MaxLength="10" placeholder="IGST %(i.e. 0,5,12,18,...)"></asp:TextBox>
                </div>
                <div class="col-md-1 text-left">
                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="btn-primary form-control" OnClick="btnSubmit_Click" />
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row lineheight">&nbsp;</div>

            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <asp:GridView ID="gvItemList" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered">
                        <Columns>
                            <asp:BoundField DataField="itemName" HeaderText="Item" />
                            <asp:BoundField DataField="Unit" HeaderText="Unit" />
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

