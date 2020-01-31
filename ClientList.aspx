<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="ClientList.aspx.cs" Inherits="ClientList" %>

<%@ Register Src="~/MarketingTab.ascx" TagPrefix="uc1" TagName="MarketingTab" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link rel="stylesheet" href="Style/chosen.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    
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
            List of Client<br />
        </div>
    </div>
<div class="row">
    <div class="col-md-12">
        <asp:DropDownList data-placeholder="Search Client Name..." runat="server" ID="listClient"  CssClass="chzn-select form-control" DataTextField="ClientName" DataValueField="ClientID" AutoPostBack="True" OnSelectedIndexChanged="listClient_SelectedIndexChanged">
		</asp:DropDownList>

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
                                <th style="display: table-cell;" data-hide="phone">
                                </th>
                                <th style="display: table-cell;" data-hide="phone">Action
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
                            <td valign="top">
                                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#"ClientEdit.aspx?id="+Eval("ClientID", "{0}")%>' Text='Edit'></asp:HyperLink>
                            </td>
                            <td valign="top">
                                <asp:HyperLink ID="btnSubmit" runat="server" NavigateUrl='<%#"SubmitSurveyConfirmation.aspx?cid="+Eval("ClientID", "{0}")%>' Text='Submit for Survey'></asp:HyperLink>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>

                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <%--<asp:GridView ID="gvClient" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" DataKeyNames="ClientID" OnRowDeleting="gvClient_RowDeleting" OnRowDataBound="gvClient_RowDataBound">
                
                <Columns>
                    <asp:BoundField DataField="ClientName" HeaderText="Client Name" />
                    <asp:TemplateField HeaderText="Contact">
                        
                        <ItemTemplate>
                            <asp:Repeater runat="server" ID="rpContact">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container,"DataItem.PeopleName")%> - <%#DataBinder.Eval(Container,"DataItem.PeopleEmail")%><br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <br />
                            <a href="ClientAddContact.aspx?id=<%#DataBinder.Eval(Container,"DataItem.ClientID")%>">Add/Edit/Delete Contact</a>
                            <br />
                            <b>Created By:
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label></b><br />
                          
                        </ItemTemplate>
                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Survey Details">
                        <ItemTemplate>
                            Survey ID:
                            <asp:Label ID="lblSurveyID" runat="server" Text=''></asp:Label><br />
                            Submitted on
                            <asp:Label ID="lblSubmittedDate" runat="server" Text=''></asp:Label><br />
                            
                            <asp:Label ID="lblStartDate" runat="server" Text=''></asp:Label><br />
                            
                            <asp:Label ID="lblFinishDate" runat="server" Text=''></asp:Label><br />
                            <br />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:HyperLinkField DataNavigateUrlFields="ClientID" DataNavigateUrlFormatString="ClientEdit.aspx?id={0}" HeaderText="" Text="Edit" ItemStyle-CssClass="btn-primary">
                        <ItemStyle CssClass="dgcontent"></ItemStyle>
                    </asp:HyperLinkField>
                    <asp:CommandField  DeleteText="Submit for Survey" ShowDeleteButton="True" ButtonType="Button" />
                </Columns>
            </asp:GridView>--%>
        </div>
    </div>
<%--    <div class="row">
        <div class="col-md-12">
            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" DataKeyNames="ClientID" OnRowCommand="gvClient_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ClientName" HeaderText="Clihgyent Name" />
                    <asp:TemplateField HeaderText="Contact">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ContactPerson") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ContactPerson") %>'></asp:Label><br />
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                            <br />
                            <br />
                            <b>Created By:
                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label></b><br />
                            <b>Survey Engineer:
                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("SurveyEngineer") %>'></asp:Label></b>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Survey Details">
                        <ItemTemplate>
                            Survey ID:
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("SurveyID") %>'></asp:Label>
                            Submitted on
                            <asp:Label ID="Label6" runat="server" Text='<%# ReformatDate(Eval("SubmittedDate", "{0}")) %>'></asp:Label><br />
                            Survey Started on:
                            <asp:Label ID="Label7" runat="server" Text='<%# ReformatDate(Eval("StartDate", "{0}")) %>'></asp:Label><br />
                            Survey Finished on:
                            <asp:Label ID="Label8" runat="server" Text='<%# ReformatDate(Eval("FinishDate", "{0}")) %>'></asp:Label><br />
                            <br />
                            <%--<br />
                            <b>Created By: <asp:Label ID="Label4" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label></b><br />
                            <b>Survey Engineer: <asp:Label ID="Label3" runat="server" Text='<%# Bind("SurveyEngineer") %>'></asp:Label></b>--%>
                     <%--   </ItemTemplate>
                    </asp:TemplateField>

                    <asp:HyperLinkField DataNavigateUrlFields="ClientID" DataNavigateUrlFormatString="EditClient.aspx?id={0}" HeaderText="" Text="Edit" ItemStyle-CssClass="dgcontent">
                        <ItemStyle CssClass="dgcontent"></ItemStyle>
                    </asp:HyperLinkField>--%>
                    <%-- <asp:HyperLinkField DataNavigateUrlFields="ClientID" DataNavigateUrlFormatString="AssignToEngineer.aspx?id={0}" HeaderText="" Text="Assign for Survey" ItemStyle-CssClass="dgcontent">
<ItemStyle CssClass="dgcontent"></ItemStyle>
                    </asp:HyperLinkField>--%>
                    <%--<asp:ButtonField ButtonType="Button" Text="Submit for Survey" ControlStyle-CssClass="btn-primary"></asp:ButtonField>
                </Columns>
            </asp:GridView>
        </div>
    </div>--%>
        <%--<script src="Scripts/jquery.min.js" type="text/javascript"></script>--%>
		<script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

</asp:Content>
