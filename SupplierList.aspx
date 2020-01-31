<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="SupplierList.aspx.cs" Inherits="SupplierList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-md-12">

            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SupplierNew.aspx">+new Supplier</asp:HyperLink>

            </div>
        </div>
    <div class="row">
        <div class="col-md-12">
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvSupplier" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                      
                    <asp:BoundField DataField="EmailID" HeaderText="Email " />
                   <asp:HyperLinkField DataNavigateUrlFields="SupplierID" DataNavigateUrlFormatString="SupplierEdit.aspx?id={0}" HeaderText="" Text="Edit" ItemStyle-CssClass="dgcontent"/>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>