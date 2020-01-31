<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="Quotation.aspx.cs" Inherits="Quotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-md-3">
            <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ClientNew.aspx">+ New Client</asp:HyperLink>--%>
        </div>
        <div class="col-md-3">
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Quotation.aspx" CssClass="img-thumbnail bg-success">Quotation Pending(<asp:Label ID="lblqpending" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>
        </div>
        <div class="col-md-3">
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/QuotationSubmitted.aspx" CssClass="img-thumbnail bg-success">Quotation Submitted(<asp:Label ID="lblqSubmitted" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>
        </div>
        <div class="col-md-3">
            <%--<asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/CompletedProject.aspx">List of Completed Project(<asp:Label ID="lblCompleted" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <br />PENDING FOR QUOTATION
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvClient" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" DataKeyNames="ClientID">
                <Columns>
                    <%--<asp:BoundField DataField="ClientName" HeaderText="Client Name" />--%>
                    <asp:TemplateField HeaderText="Project Details">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("ClientName") %>' Font-Bold="True"></asp:Label><br />
                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Eval("ContactPerson","Contact Person: {0}") %>'></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Email") %>'></asp:Label>--%>
                            <br />
                            <br />
                            <b>Sales Employee:
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("SalesPerson") %>'></asp:Label></b>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="List of Survey for Quotation ">
                                <ItemTemplate>
                                    Survey No : [<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"Quotationcosting.aspx?sid="+Eval("SurveyID", "{0}")+"&id="+Eval("ClientID", "{0}")%>' Text='<%#Eval("SurveyID","{0}")+" - "+ Eval("SubmittedDate","{0:dd-MM-yyyy}")%>'></asp:HyperLink>]<br />
                                </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
