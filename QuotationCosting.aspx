<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterCommon.master" AutoEventWireup="true" CodeFile="QuotationCosting.aspx.cs" Inherits="QuotationCosting" %>

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
                <div class="col-md-2 text-left">
                    <span class="label label-primary">Select Currency for Conversation</span><asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>  
                </div>
                <div class="col-md-1 text-left">
                    <span class="label label-primary">Today Rate</span><asp:TextBox ID="txtValue" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtValue_TextChanged"></asp:TextBox>  
                </div>
                
                <div class="col-md-2"></div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8 text-left">
                    Quotation Number:<asp:Label ID="lblQuotationNumber" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-2"></div>
            </div>
            
            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8 text-left">
                    <asp:TextBox ID="txtClientName" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-2"></div>
            </div>
            
            <div class="row lineheight">&nbsp;</div>
            <div class="row">
                
                <div class="col-md-12">
                    <asp:GridView ID="gvFeed" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDeleting="gvFeed_RowDeleting" OnRowCancelingEdit="gvFeed_RowCancelingEdit" OnRowEditing="gvFeed_RowEditing" OnRowUpdating="gvFeed_RowUpdating">
                        <Columns>
                            <asp:BoundField DataField="sno" HeaderText="Sr. No." ReadOnly="True" >
                            <ItemStyle Width="40px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GroupName" HeaderText="Item Group" ReadOnly="True" />
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
                            <asp:TemplateField HeaderText="Supplier Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSupplier" runat="server" Text='<%# Bind("SupplierName") %>' CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("SupplierName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Purchase Cost">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPurchaseCost" runat="server" Text='<%# Bind("PurchaseCost") %>' Width="100" CssClass="form-control"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("PurchaseCost") %>'></asp:Label>
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Cost">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTotalCost" CssClass="form-control" runat="server" Width="94" Text='<%# Bind("QuotedPriceTotal") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("QuotedPriceTotal") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Unit Cost" ReadOnly="True" DataField="QuotedPriceUnit" >
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Currency" HeaderText="Currency" ReadOnly="True">
                            <ControlStyle BackColor="#CCCCFF" />
                            <ItemStyle BackColor="#CCCCFF" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CurrencyUnit" HeaderText="Currency Unit" ReadOnly="True" DataFormatString="{0:N3}" >
                            <ItemStyle BackColor="#CCCCFF" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CurrencyTotal" HeaderText="CurrencyTotal" ReadOnly="True" DataFormatString="{0:N3}" >
                            <ItemStyle BackColor="#CCCCFF" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="Remark" ReadOnly="True" />
                            <%--<asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Delete" Visible="False" />--%>
                           
                            <%--<asp:CommandField ShowEditButton="True" />--%>
                            <asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Quotationupdate.aspx?iid={0}" HeaderText="" Text="Update Cost" ItemStyle-CssClass="btn-primary">
                        <ItemStyle CssClass="dgcontent"></ItemStyle>
                    </asp:HyperLinkField>
                            
                        </Columns>
                    </asp:GridView><br />
                  <%--  <asp:GridView ID="gvitemdisplay" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False" DataKeyNames="ID" OnRowDeleting="gvFeed_RowDeleting" OnRowCancelingEdit="gvFeed_RowCancelingEdit" OnRowEditing="gvFeed_RowEditing" OnRowUpdating="gvFeed_RowUpdating">
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
                    </asp:GridView>--%>
                </div>
                <%--<div class="col-md-2"></div>--%>
            </div>
<div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8">
                    <asp:Button ID="btnFinishSurvey" runat="server" Text="Finish Quotation [New Version]" CssClass="form-control btn-primary" OnClick="btnFinishSurvey_Click" />
                </div>
                <div class="col-md-2"></div>

            </div>
            <br />
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8"><span class="label label-primary">Payment Terms</span>
                    <asp:TextBox ID="txtPaymentTerm" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2"></div>

            </div>
<div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-8 text-center">
                    <asp:Button ID="btnApply" runat="server" Text="Generate PDF File in Selected Currency" CssClass="btn btn-primary" OnClick="btnApply_Click"/>
                </div>
                <div class="col-md-2"></div>

            </div>

       </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

