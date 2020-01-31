<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="QuotationUpdate.aspx.cs" Inherits="QuotationUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link rel="stylesheet" href="Style/chosen.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:Button ID="btnBack" runat="server" Text="Go Back" OnClick="btnBack_Click" CssClass="btn btn-primary"/>
                </div>
                <div class="col-md-2"></div>
            </div>
    <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <%--<h3>Site Survey</h3><br />--%>
                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-3 text-left">
                    <span class="label label-primary">Item Group</span><asp:Label ID="lblGroup" runat="server" CssClass="form-control"></asp:Label>  
                </div>
                <div class="col-md-3 text-left">
                    <span class="label label-primary">Item Name</span><asp:Label ID="lblItem" runat="server" CssClass="form-control"></asp:Label>  
                </div>
                <div class="col-md-1 text-left">
                    <span class="label label-primary">Quantity</span><asp:Label ID="lblQuantity" runat="server" CssClass="form-control"></asp:Label>  
                </div>
                <div class="col-md-1 text-left">
                    <span class="label label-primary">Unit</span><asp:Label ID="lblUnit" runat="server" CssClass="form-control"></asp:Label>  
                </div>
                <div class="col-md-1"></div>
            </div>
            <br />

            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-3 text-left">
                    <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="chzn-select form-control" DataTextField="SupplierName" DataValueField="SupplierID"></asp:DropDownList>
                </div>
                <div class="col-md-2 text-left"><span class="label label-primary">Purchase Cost</span>
                    <asp:TextBox ID="txtPurchaseCost" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtPurchaseCost_TextChanged" ></asp:TextBox>
                </div>
                <div class="col-md-2 text-left"><span class="label label-primary">Sale Cost</span>
                    <asp:TextBox ID="txtTotalValue" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TotalValue_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-2 text-left"><span class="label label-primary">Unit Cost</span>
                    <asp:TextBox ID="txtUnitCost" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1 text-left"><span class="label label-primary">Margin %</span>
                    <asp:TextBox ID="txtMargin" runat="server" CssClass="form-control" OnTextChanged="txtMargin_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-1"></div>
            </div>
    <br />
    <div class="row">
        <div class="col-md-1"></div>
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
        <div class="col-md-1"></div>
        </div>        
            <div class="row lineheight">&nbsp;</div>
          
<div class="row">
                <div class="col-md-5"></div>
                <div class="col-md-2">
                    <asp:Button ID="btnFinishSurvey" runat="server" Text="Submit" CssClass="form-control btn-primary" OnClick="btnFinishSurvey_Click" />
                </div>
                <div class="col-md-5"></div>

            </div>
                <script src="Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

</asp:Content>

