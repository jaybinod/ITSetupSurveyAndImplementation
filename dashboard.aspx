<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="dashboard.aspx.cs" Inherits="dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#tblCustomers').footable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row">
        <div class="col-md-12">

            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/NewEmployee.aspx">+new User</asp:HyperLink>

        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <br />
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:Repeater ID="gvEmployee" runat="server">
                <HeaderTemplate>
                    <table id="tblCustomers" class="footable" border="0" cellpadding="0" cellspacing="0">
                        <thead>
                            <tr>
                                <th data-class="expand">Employee Name
                                </th>
                                <th scope="col">Department
                                </th>
                                <th style="display: table-cell;" data-hide="phone">User ID
                                </th>
                                <th style="display: table-cell;" data-hide="phone">Password
                                </th>
                                <th style="display: table-cell;" data-hide="phone">Action
                                </th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tbody>
                        <tr>
                            <td>
                                <%#Eval("EmployeeName")%>
                            </td>
                            <td>
                                <%#Eval("Department")%>
                            </td>
                            <td>
                                <%#Eval("EmailID")%>
                            </td>
                            <td>
                                <%#Eval("Password")%>
                            </td>
                            <td>
                                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#"EditEmployee.aspx?id="+Eval("UserID", "{0}")%>' Text='Edit'></asp:HyperLink>
                            </td>
                        </tr>
                    </tbody>
                </ItemTemplate>

                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <%--<asp:GridView ID="gvEmployee" runat="server" CssClass="table table-bordered table-condensed" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                      
                    <asp:BoundField DataField="EmailID" HeaderText="Email (UserID)" />
                    <asp:BoundField DataField="password" HeaderText="Password" />
                   <asp:HyperLinkField DataNavigateUrlFields="UserID" DataNavigateUrlFormatString="EditEmployee.aspx?id={0}" HeaderText="" Text="Edit" ItemStyle-CssClass="dgcontent">
<ItemStyle CssClass="dgcontent"></ItemStyle>
                    </asp:HyperLinkField>
                </Columns>
            </asp:GridView>--%>
        </div>
    </div>
</asp:Content>
