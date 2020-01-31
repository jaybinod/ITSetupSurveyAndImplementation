<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MarketingTab.ascx.cs" Inherits="MarketingTab" %>
<div class="row">
        <div class="col-md-3 text-center">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ClientNew.aspx"  CssClass="bg-success img-thumbnail btn" Width="100%">+ New Client</asp:HyperLink>
        </div>
        <div class="col-md-3 text-center">
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/ClientList.aspx" CssClass="bg-info img-thumbnail btn" Width="100%">List of Client(<asp:Label ID="lblLoc" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>
        </div>
        <div class="col-md-3 text-center">
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/homesales.aspx" CssClass="bg-danger img-thumbnail btn" Width="100%">Work in Progress(<asp:Label ID="lblwip" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>
        </div>
        <div class="col-md-3 text-center">
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/CompletedProject.aspx" CssClass="bg-warning img-thumbnail btn" Width="100%">List of Completed Project(<asp:Label ID="lblCompleted" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>
        </div>
    </div>