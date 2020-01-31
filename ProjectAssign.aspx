<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="ProjectAssign.aspx.cs" Inherits="ProjectAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-md-12">
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <br />PENDING FOR PROJECT IMPLEMENTATION
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvClient" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" DataKeyNames="ClientID" OnRowDataBound="gvClient_RowDataBound">
                <Columns>
                    <%--<asp:BoundField DataField="ClientName" HeaderText="Client Name" />--%>
                    <asp:TemplateField HeaderText="Project Details">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("ClientName") %>' Font-Bold="True"></asp:Label><br />
                           <%-- <asp:Label ID="Label1" runat="server" Text='<%# Eval("ContactPerson","Contact Person: {0}") %>'></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Email") %>'></asp:Label>--%>
                            <br />
                            <br />
                            <b>Sales Employee:
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label></b>
                            <br />
                            <b>Survey Engineer:
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("SurveyEngineer") %>'></asp:Label></b>
                            <br />
                            <b>Project Engineer:
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("ProjectEngineer") %>'></asp:Label></b>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="List of Project(s)">
                        <ItemTemplate>
                            <asp:Repeater ID="rpopen" runat="server">
                                <ItemTemplate>
                                    Survey No : [<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#"AssignforExecuion.aspx?id="+Eval("ClientID", "{0}")+"&sid="+Eval("SurveyID", "{0}")%>' Text='<%#Eval("SurveyID","{0}")+" - "+ Eval("StartDate","{0:dd-MM-yyyy}")%>'></asp:HyperLink>]<br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <br />
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
