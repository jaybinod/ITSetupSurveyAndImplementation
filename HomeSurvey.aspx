<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="HomeSurvey.aspx.cs" Inherits="HomeSurvey" %>

<%@ Register Src="~/SurveyTab.ascx" TagPrefix="uc1" TagName="SurveyTab" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc1:SurveyTab runat="server" id="SurveyTab" />
    <div class="row">
        <div class="col-md-12 table-bordered">
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
        <h4>Survey in Progress</h4>
        </div>
    </div>
    <div class="row">
        
        <div class="col-md-12">
            <asp:GridView ID="gvClient" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" DataKeyNames="SurveyID" OnRowDataBound="gvClient_RowDataBound">
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
                            <br />
                            <b>Sales Employee:
                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("SurveyEngineer") %>'></asp:Label></b>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Survey ">
                        <ItemTemplate>
                                    <asp:HyperLink ID="hlsurvey" runat="server" NavigateUrl='<%#"SurveyPage.aspx?id="+Eval("ClientID", "{0}")+"&sid="+Eval("SurveyID", "{0}")%>' Text=''></asp:HyperLink>]<br />    
                                    <asp:Label ID="lblStartedOn" runat="server" Text='' ></asp:Label>
                                    <asp:Label ID="lblSubmittedForQuotation" runat="server" Text=""></asp:Label> <br />
                                    <asp:Label ID="lblQuotationDate" runat="server" Text=""></asp:Label> <br />
                                    <%--<asp:Label ID="lblProjectStatus" runat="server" Text=""></asp:Label> --%>
                                    
                            <%--<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# Eval("ClientID", "SurveyVerifyPage.aspx?t=n&id={0}") %>' Text="Start New Survey"></asp:HyperLink>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
