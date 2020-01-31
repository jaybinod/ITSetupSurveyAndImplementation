<%@ page title="" language="C#" masterpagefile="~/MainMasterCommon.master" autoeventwireup="true" CodeFile="Surveydetails.aspx.cs" inherits="Surveydetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <link rel="stylesheet" href="Style/chosen.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    
            <br />
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <%--<asp:Button ID="btnBack" runat="server" Text="Go Back" OnClick="btnBack_Click" />--%>
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-12 text-center">
                    <%--<h3>Site Survey</h3><br />--%>
                    <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10 text-left">
                    <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-md-1"></div>
            </div>
            <div class="row lineheight">&nbsp;</div>

     
            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-10">
                    <asp:GridView ID="gvFeed" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="ID" >
                        <Columns>
                            <asp:CommandField ButtonType="Button" DeleteText="x" ShowDeleteButton="True" />
                            <asp:BoundField DataField="sno" HeaderText="Serial Number" ReadOnly="True" />
                            <asp:BoundField DataField="GroupName" HeaderText="Item Group" ReadOnly="true"/>
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Quantity">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQty" runat="server" Text='<%# Bind("Quantity") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Unit" HeaderText="Unit" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Remark">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNote" runat="server" Text='<%# Bind("Remark") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:CommandField ShowEditButton="True" />
                            
                        </Columns>
                    </asp:GridView><br />
                    <asp:GridView ID="gvitemdisplay" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False" DataKeyNames="ID" >
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
                                                     
                            <asp:BoundField DataField="GroupName" HeaderText="Item Group" />
                                                     
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-md-1"></div>
            </div>

            <script src="Scripts/jquery.min.js" type="text/javascript"></script>
		<script src="Scripts/chosen.jquery.js" type="text/javascript"></script>
		<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

</asp:Content>

