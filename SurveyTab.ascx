<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SurveyTab.ascx.cs" Inherits="SurveyTab" %>
    <div class="row">
        <div class="col-md-4 text-center">
            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/HomeSurvey.aspx" CssClass="img-thumbnail bg-success btn">Survey in Progress (<asp:Label ID="lblprogress" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>
        </div>
        <div class="col-md-4 text-center">
            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/HomeSurveyPendingToStart.aspx" CssClass="img-thumbnail bg-danger btn">Survey Pending to Start (<asp:Label ID="lblpending" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>
        </div>
        
        <div class="col-md-4 text-center">
            <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/HomeSurveyCompleted.aspx" CssClass="img-thumbnail bg-warning btn">List of Completed Survey(<asp:Label ID="lblCompleted" runat="server" Text="Label"></asp:Label>)</asp:HyperLink>
        </div>
    </div>