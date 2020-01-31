<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="AssignForSurvey.aspx.cs" Inherits="AssignForSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-md-12">

            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/SurveyStatus.aspx" CssClass="bg-info img-thumbnail btn">For Survey Status Click Here</asp:HyperLink>

            </div>
        </div>
    <div class="row">
        <div class="col-md-12">
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="gvClient" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="ClientName" HeaderText="Client Name" />
                    <asp:TemplateField HeaderText="Contact">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ContactPerson") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("ContactPerson") %>'></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Email") %>'></asp:Label>--%>
                            <br /><br />
                            <b>Created By: <asp:Label ID="Label4" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label></b><br />
                            <%--<b>Survey Engineer: <asp:Label ID="Label3" runat="server" Text='<%# Bind("SurveyEngineer") %>'></asp:Label></b>--%>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:BoundField DataField="SurveyID" HeaderText="Survey No" />
                  <%-- <asp:HyperLinkField DataNavigateUrlFields="ClientID" DataNavigateUrlFormatString="EditClient.aspx?id={0}" HeaderText="" Text="Edit" ItemStyle-CssClass="dgcontent">
<ItemStyle CssClass="dgcontent"></ItemStyle>
                    </asp:HyperLinkField>--%>
                    <asp:HyperLinkField DataNavigateUrlFields="SurveyID" DataNavigateUrlFormatString="AssignToEngineer.aspx?id={0}" HeaderText="" Text="Assign for Survey" ItemStyle-CssClass="dgcontent">
<ItemStyle CssClass="dgcontent"></ItemStyle>
                    </asp:HyperLinkField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>