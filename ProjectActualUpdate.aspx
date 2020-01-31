<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="ProjectActualUpdate.aspx.cs" Inherits="ProjectActualUpdate" %>

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
                    Actual Implementation update
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
                <div class="col-md-2"></div>
                <div class="col-md-8 text-left">
                    <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2"></div>
            </div>
            
            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:GridView ID="gvFeed" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDeleting="gvFeed_RowDeleting" OnRowCancelingEdit="gvFeed_RowCancelingEdit" OnRowEditing="gvFeed_RowEditing" OnRowUpdating="gvFeed_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="sno" HeaderText="Serial Number" ReadOnly="True" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Quantity">
                                <EditItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Unit" HeaderText="Unit" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Utilized Quantity">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActualUtilisation" CssClass="form-control" runat="server" Text='<%# Bind("ActualUtilisation") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ActualUtilisation") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Implemented Date">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtImplementedDate" CssClass="form-control" runat="server" Text='<%# Bind("UtilizeUpdateDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtImplementedDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtImplementedDate">
            </asp:CalendarExtender>

                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("UtilizeUpdateDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" />
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="Unit Cost" ReadOnly="True" DataField="QuotedPriceUnit" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Remark" HeaderText="Remark" ReadOnly="True" />
                            <%--<asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" Visible="False" />--%>
                           
                            <asp:CommandField ShowEditButton="True" />
                            
                        </Columns>
                    </asp:GridView><br />
                    <asp:GridView ID="gvitemdisplay" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDeleting="gvFeed_RowDeleting" OnRowCancelingEdit="gvFeed_RowCancelingEdit" OnRowEditing="gvFeed_RowEditing" OnRowUpdating="gvFeed_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="sno" HeaderText="Serial Number" ReadOnly="True" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Quantity">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Unit" HeaderText="Unit" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Remark">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNote" runat="server" Text='<%# Bind("Remark") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" Visible="False" />
                                                     
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-md-2"></div>
            </div>
<div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:Button ID="btnFinishSurvey" runat="server" Text="Project Completed | Click Here" CssClass="form-control btn-primary" OnClick="btnFinishSurvey_Click" />
                </div>
                <div class="col-md-2"></div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

