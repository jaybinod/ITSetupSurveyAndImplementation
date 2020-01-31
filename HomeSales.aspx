<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="HomeSales.aspx.cs" Inherits="HomeSales" %>

<%@ Register Src="~/MarketingTab.ascx" TagPrefix="uc1" TagName="MarketingTab" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
   <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link rel="stylesheet" href="Style/chosen.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
<script type="text/javascript">
        $(function () {
            $('#tblCustomers').footable();
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <uc1:MarketingTab runat="server" ID="MarketingTab" />
       <div class="row">
        <div class="col-md-12">
            Work in progress<br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Repeater ID="gvClient" runat="server" OnItemDataBound="gvClient_ItemDataBound">
                <HeaderTemplate>
                    <table id="tblCustomers" class="footable" border="0" cellpadding="0" cellspacing="0">
                        <thead>
                            <tr>
                                <th data-class="expand">ClientName
                                </th>
                                <th style="display: table-cell;" data-hide="phone">Contact
                                </th>
                                <th style="display: table-cell;" data-hide="phone">Survey Details
                                </th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
<asp:HiddenField ID="hfClientID" runat="server" Value='<%# Eval("ClientID") %>' />
                    <tbody>
                        <tr>
                            <td valign="top">
                                <b><%#Eval("ClientName")%></b><br />
                                <%#Eval("Address1")%>
                                <%#Eval("Address2")%>
                                <%#Eval("City")%> <%#Eval("State")%> <%#Eval("Postalcode")%>
                            </td>
                            <td valign="top">
                                <asp:Repeater runat="server" ID="rpContact" >
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container,"DataItem.PeopleName")%> - <%#DataBinder.Eval(Container,"DataItem.PeopleEmail")%><br />
                                </ItemTemplate>
                            </asp:Repeater>
                                
                            <a href="ClientAddContact.aspx?id=<%#DataBinder.Eval(Container,"DataItem.ClientID")%>">Add/Edit/Delete Contact</a>
                            <br />
                                <b>Created By:
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label></b><br />
                            </td>
                            <td>
                                <asp:Repeater runat="server" ID="rpSurvey" >
                                <ItemTemplate>
                                    Survey Ticket ID <%#DataBinder.Eval(Container,"DataItem.SurveyID")%> - Submitted on: <%#DataBinder.Eval(Container,"DataItem.SubmittedDate","{0:dd-MMM-yyyy}")%><br />
                                    Survey Assigned To : <%# CustomizeMessage(DataBinder.Eval(Container,"DataItem.SurveyEngineer"), "SurveyAssign")%><br />
                                    Survey Started on: <%# CustomizeMessage(DataBinder.Eval(Container,"DataItem.StartDate","{0:dd-MMM-yyyy}"),"SurveyStart")%><br />
                                    Survey Quotation Submitted on: <%# CustomizeMessage(DataBinder.Eval(Container,"DataItem.QuotationSubmittedDate","{0:dd-MMM-yyyy}"), "Quotation")%><br />
                                    Project Engineer : <%# CustomizeMessageEngineer(DataBinder.Eval(Container,"DataItem.SurveyID"))%><br />
                                    Project Finish Date : <%# CustomizeMessage(DataBinder.Eval(Container,"DataItem.ProjectFinishDate","{0:dd-MMM-yyyy}"),"ProjectFinish")%><br />
                            
                            <asp:Label ID="lblStartDate" runat="server" Text=''></asp:Label><br />
                            
                            <asp:Label ID="lblFinishDate" runat="server" Text=''></asp:Label><br />
                                </ItemTemplate>
                            </asp:Repeater>

                               
                            <br />
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>

                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
 
</asp:Content>
