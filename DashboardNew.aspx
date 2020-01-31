<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DashboardNew.aspx.cs" Inherits="DashboardNew" MasterPageFile="~/MainMasterPage.master" %>

<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
        <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/Site.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="Scripts/tabcontent.js" type="text/javascript"></script>
    <link href="Content/tabcontent.css" rel="stylesheet" type="text/css" />
    
    <link href="Content/CSS.css" rel="stylesheet" type="text/css" />
    
    <script src="Scripts/jquery.blockUI.js" type="text/javascript"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>

        <center>
            <table width="100%" cellpadding="2" cellspacing="2">
                <tr>
                    <td align="left">
                     
                         <asp:GridView ID="gvUpcomingMeeting" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="orgname" HeaderText="Company Name" />
                                    <asp:BoundField DataField="ActMastName" HeaderText="Activity Type" />
                                    <asp:BoundField DataField="ActSubject" HeaderText="Subject" />
                                    <asp:BoundField DataField="Actnote" HeaderText="Description" />
                                    <asp:BoundField DataField="ActWith" HeaderText="Meeting With" />
                                    <asp:BoundField DataField="ActDueDate" DataFormatString="{0:dd-MMM hh:mm}" HeaderText="Date" />
                                    <asp:BoundField DataField="ActStartDuration" HeaderText="Duration" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">             
                              <asp:Panel ID="pnlActivity" runat="server" Visible="false" >
                                  
                            <asp:Label ID="lblError" Font-Bold="true" ForeColor="Red" runat="server" Text="Label" Visible="false"></asp:Label><br />
                                 <div style="width: 680px; margin: 0 0px;">
                                      <ul class="tabs" data-persist="true">
                                        <li><a href="#view1">Task</a></li>
                                        <li><a href="#view2">Note</a></li>
                                        <%--<li><a href="#view3">File</a></li>--%>
                                        <li><a href="#view4">Meeting</a></li>
                                        <li><a href="#view5">Call</a></li>
                                        <li><a href="#view6">Event</a></li>
                                        <li id="hideMe" runat="server"  ><a href="#view7" >Feedback</a></li>
                                    </ul>
                                       <div class="tabcontents">
                                        <div id="view1">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left">
                                                    <table cellpadding="1" cellspacing="1" width="100%" border="0">
                                                        <tr>
                                                            <td align="left" class="MyActHeading">Subject</td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTaskSubject" runat="server" CssClass="Mytextbox" Height="27px" Width="500px"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTaskSubject" ErrorMessage="*" ForeColor="Red" ValidationGroup="GrpTask"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="MyActHeading">Task</td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTaskDescription" runat="server" CssClass="Mytextbox" Height="43px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="MyActHeading">Due Date</td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDueDate" runat="server" CssClass="Mytextbox" Height="27px" Width="100px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDueDate"></cc1:CalendarExtender>
                                                                <asp:CheckBox ID="chkTaskDoneF" runat="server" Text="Done" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">&nbsp;</td>
                                                            <td align="left">
                                                                <asp:Button ID="btnSubmitTask" runat="server" CssClass="myButton" OnClick="btnSubmitTask_Click" Text="Submit" ValidationGroup="GrpTask" Width="107px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="left">
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>



                                        </table>

                                    </div>
                                        <div id="view2">
                                        <table width="100%" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <td align="left">
                                                    <asp:TextBox ID="txtNote" runat="server" CssClass="Mytextbox" Width="500px" TextMode="MultiLine" ValidationGroup="GrpNote" Height="70px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ValidationGroup="GrpNote" ControlToValidate="txtNote" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Button ID="btnSubmitNote" runat="server" Text="Submit" ValidationGroup="GrpNote" CssClass="myButton" OnClick="btnSubmitNote_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                        <div id="view4">
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left">
                                                    <table cellpadding="1" cellspacing="1">
                                                        <tr>
                                                            <td align="left" class="MyActHeading">Subject</td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtMeetingSubjectF" runat="server" CssClass="Mytextbox" Height="27px" Width="500px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="MyActHeading">Description</td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtMeetDescF" runat="server" CssClass="Mytextbox" Height="43px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="MyActHeading">Meeting With</td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtMeetWithF" runat="server" CssClass="Mytextbox" Height="27px" Width="500px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="MyActHeading">Location</td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtMeetLocationF" runat="server" CssClass="Mytextbox" Height="27px" Width="500px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="MyActHeading">Date</td>
                                                            <td align="left">
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td align="left" valign="top">
                                                                            <asp:TextBox ID="txtMeetDateF" runat="server" CssClass="Mytextbox" Height="27px" Width="100px"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="txtMeetDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtMeetDateF" />
                                                                        </td>
                                                                        <td align="left" valign="top">
                                                                            <table cellpadding="0" cellspacing="0">
                                                                                <tr>
                                                                                    <td align="left" class="MyActHeading">Time</td>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList ID="DdrMeetingStartTimeF" runat="server" CssClass="MyDropDown" Height="27px">
                                                                                            <asp:ListItem Text="7:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="7:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="7:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="7:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="8:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="8:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="8:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="8:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="9:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="9:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="9:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="9:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="10:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="10:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="10:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="10:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="11:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="11:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="11:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="11:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="12:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="12:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="12:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="12:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="13:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="13:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="13:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="13:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="14:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="14:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="14:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="14:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="15:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="15:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="15:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="15:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="16:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="16:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="16:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="16:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="17:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="17:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="17:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="17:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="18:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="18:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="18:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="18:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="19:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="19:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="19:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="19:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="20:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="20:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="20:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="20:45"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="center" valign="top">
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td align="left" class="MyActHeading">Duration</td>
                                                                                    <td align="left">
                                                                                        <asp:DropDownList ID="DdrMeetingDurationF" runat="server" CssClass="MyDropDown" Height="27px">
                                                                                            <asp:ListItem Text="00:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="00:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="00:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="00:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="01:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="01:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="01:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="01:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="02:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="02:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="02:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="02:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="03:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="03:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="03:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="03:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="04:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="04:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="04:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="04:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="05:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="05:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="05:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="05:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="06:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="06:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="06:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="06:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="07:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="07:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="07:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="07:45"></asp:ListItem>
                                                                                            <asp:ListItem Text="08:00"></asp:ListItem>
                                                                                            <asp:ListItem Text="08:15"></asp:ListItem>
                                                                                            <asp:ListItem Text="08:30"></asp:ListItem>
                                                                                            <asp:ListItem Text="08:45"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td align="center" valign="top">
                                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                                <tr>
                                                                                    <td align="left" class="MyActHeading">
                                                                                        <asp:CheckBox ID="chkMeetingDoneF" runat="server" Text="Done" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="top" class="MyActHeading">Participent</td>
                                                            <td>
                                                               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                <asp:TextBox ID="txtContactsSearch" runat="server" CssClass="Mytextbox" Width="250px"></asp:TextBox>
                                                               <cc1:AutoCompleteExtender ServiceMethod="SearchCustomers"
                                                                    MinimumPrefixLength="1"
                                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                                                                    TargetControlID="txtContactsSearch"
                                                                    ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                                                                </cc1:AutoCompleteExtender> 
                                                                    
                                                                <asp:Button ID="btnAddP" runat="server" Text="Add" OnClick="btnAddP_Click" />
                                                                        <asp:Label ID="lblErrorP" runat="server" ForeColor="#CC3300"></asp:Label>
                                                                        <br />
                                                                        <asp:GridView ID="gvEmailP" Width="100%" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="email" ForeColor="#333333" GridLines="None" OnRowDeleting="gvEmailP_RowDeleting" ShowHeader="False">
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="Email" />
                                                                                <asp:CommandField ShowDeleteButton="True" />
                                                                            </Columns>
                                                                            <EditRowStyle BackColor="#2461BF" />
                                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EFF3FB" />
                                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                                        </asp:GridView>
                                                                        <br />

                                                                 </ContentTemplate>
                                                                    </asp:UpdatePanel>


                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <asp:Button ID="btnCreateMeeting" runat="server" CssClass="myButton" OnClick="btnCreateMeeting_Click" Text="Create" Width="107px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="left" valign="top">
                                                    &nbsp;</td>
                                            </tr>

                                        </table>

                                    </div>
                                        <div id="view5">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left">
                                                        <table cellpadding="1" cellspacing="1">

                                                            <tr>
                                                                <td align="left" class="MyActHeading">Subject</td>
                                                                <td>
                                                                    <asp:TextBox ID="txtCallSubjectF" CssClass="Mytextbox" runat="server" Height="27px" Width="500px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">Comments</td>
                                                                <td style="margin-left: 40px">
                                                                    <asp:TextBox ID="txtCallCommentF" runat="server" CssClass="Mytextbox" Height="43px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">Call With</td>
                                                                <td style="margin-left: 40px">
                                                                    <asp:TextBox ID="txtCallWithF" runat="server" CssClass="Mytextbox" Height="27px" Width="500px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">Date</td>
                                                                <td style="margin-left: 40px" class="auto-style4">
                                                                    <table cellpadding="1" cellspacing="1" width="100%">
                                                                        <tr>
                                                                            <td align="left" valign="top" width="40%">
                                                                                <table cellpadding="0" cellspacing="0">
                                                                                    <tr>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="txtCallDateF" runat="server" CssClass="Mytextbox" Height="27px" Width="100px"></asp:TextBox>
                                                                                            <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCallDateF" PopupPosition="TopRight"></cc1:CalendarExtender>
                                                                                        </td>
                                                                                        <td align="center" valign="top">
                                                                                            <asp:DropDownList ID="DdrCalltimeF" runat="server" CssClass="MyDropDown" Height="27px">
                                                                                                 <asp:ListItem Text="7:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:45"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:CheckBox ID="chkCallDoneStatusF" runat="server" Text="Done" />
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>

                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">&nbsp;</td>
                                                                <td style="margin-left: 40px">
                                                                    <asp:Button ID="btnCreateCall" runat="server" CssClass="myButton" OnClick="btnCreateCall_Click" Text="Create Call" Width="107px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        &nbsp;</td>
                                                </tr>

                                            </table>

                                        </div>
                                        <div id="view6">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left">
                                                        <table cellpadding="1" cellspacing="1">
                                                            <tr>
                                                                <td align="left" class="MyActHeading">Subject</td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEventSubjectF" runat="server" CssClass="Mytextbox" Height="27px" Width="500px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">Comments</td>
                                                                <td align="left" style="margin-left: 40px">
                                                                    <asp:TextBox ID="txtEventCommentF" runat="server" CssClass="Mytextbox" Height="43px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">Location</td>
                                                                <td align="left" style="margin-left: 40px">
                                                                    <asp:TextBox ID="txtEventLocationF" runat="server" CssClass="Mytextbox" Height="27px" Width="500px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">Start</td>
                                                                <td align="left" style="margin-left: 40px">
                                                                    <asp:TextBox ID="txtEventStartDateF" runat="server" CssClass="Mytextbox" Height="27px" Width="100px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEventStartDateF"></cc1:CalendarExtender>
                                                                    <asp:DropDownList ID="DdrEventStartTimeF" runat="server" CssClass="MyDropDown" Height="27px">
                                                                         <asp:ListItem Text="7:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:45"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">End</td>
                                                                <td align="left" style="margin-left: 40px">
                                                                    <asp:TextBox ID="txtEventEndDateF" runat="server" CssClass="Mytextbox" Height="27px" Width="100px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEventEndDateF"></cc1:CalendarExtender>
                                                                    <asp:DropDownList ID="DdrEventEndTimeF" runat="server" CssClass="MyDropDown" Height="27px">
                                                                         <asp:ListItem Text="7:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="7:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="8:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="9:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="10:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="11:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="12:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="13:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="14:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="15:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="16:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="17:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="18:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="19:45"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:00"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:15"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:30"></asp:ListItem>
                                                                                                <asp:ListItem Text="20:45"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:CheckBox ID="chkEventDoneF" runat="server" Text="Done" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="MyActHeading">&nbsp;</td>
                                                                <td align="left" style="margin-left: 40px">
                                                                    <asp:Button ID="BtnEvent" runat="server" CssClass="myButton" OnClick="BtnEvent_Click" Text="Submit" Width="107px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="left">
                                                        &nbsp;</td>
                                                </tr>



                                            </table>

                                        </div>
                                        <div id="view7">
                                            <table width="100%" cellpadding="3" cellspacing="3">
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtFeedBack" runat="server" CssClass="Mytextbox" Width="500px" TextMode="MultiLine" ValidationGroup="GrpFeedBack" Height="70px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ValidationGroup="GrpFeedBack" ControlToValidate="txtFeedBack" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Button ID="Button1" runat="server" Text="Submit" ValidationGroup="GrpFeedBack" CssClass="myButton" OnClick="btnSubmitFeedBack_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                         </div>
                                     </div> 
                                     </asp:Panel>                            
                                <asp:GridView ID="GVActivity" runat="server" Width="100%" AutoGenerateColumns="false" GridLines="None" AlternatingRowStyle-BackColor="#E7E7E7" ShowHeader="false"
                                    HeaderStyle-BackColor="#ffffff" CellPadding="4" CellSpacing="4" AllowPaging="true" PageSize="10"  OnPageIndexChanging="GVActivity_PageIndexChanging" OnRowDataBound="GVActivity_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField>
                                            <ItemTemplate>

                                                <table width="95%" cellpadding="0" cellspacing="0">

                                                    <tr>
                                                        <td align="center">

                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td width="5%" align="left" valign="top">
                                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/task.png" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(7) %>' />
                                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/File.jpg" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(8) %>' />
                                                                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/Note.png" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(5) %>' />
                                                                        <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/Meeting.png" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(1) %>' />
                                                                        <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/Call.png" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(9) %>' />
                                                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/Events.png" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(3) %>' />
                                                                    </td>
                                                                   
                                                                    <td align="left" valign="top" width="60%" style="font-family: Verdana, Tahoma, sans-serif; font-size: 15px; font-weight: bold">
                                                                        <asp:Label ID="Label25" runat="server" Text='<%#Eval("Actsubject") %>' ></asp:Label></td>
                                                                    <td align="center" width="30%" valign="top" style="font-family: Calibri, Helvetica, Arial; font-style: italic; font-size: 12px; color: #545454;">Posted by : <%#Eval("FirstName") %> &nbsp; <%#Eval("LastName") %>   on  <%#Eval("ActCreateDate")%>                                                   
                                                                    </td>
                                                                    
                                                                    <td align="right" valign="top" width="5%">
                                                                        <a href="#" onclick="window.open('EditActivity.aspx?ActId=<%#Eval("ActId")%>'+'&ActtypeId='+<%#Eval("ActTypeId")%> ,'Edit Activity','width=800,height=500')"><img src="images/EditData.png" /></a>
                                                                      <%--  <asp:ImageButton ID="ImageButton1" ImageUrl="images/EditData.png" runat="server" Text="Edit" OnClick="Edit" CommandArgument='<%#Eval("ActId") %>'  />--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">
                                                         <asp:Label ID="Label2" runat="server" Text='<%#Eval("orgname") %>' ></asp:Label> 
                                                                    
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <table width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">
                                                                <asp:Panel ID="PanelMeeting" runat="server" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(1) %>'>                                                                   
                                                                    <tr>
                                                                        <td style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;" align="left">
                                                                            <b style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">Organiser :</b>
                                                                            <asp:Label ID="Label21" runat="server" Text='<%#Eval("FullName") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(1) %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">
                                                                            <b style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">Participants :</b>
                                                                            <asp:Label ID="lblParticipant" runat="server" Text=''></asp:Label>
                                                                             

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;" align="left">
                                                                            <b style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">Scheduled on </b>: 
                                                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("ActDueDateNew") %>'></asp:Label> &nbsp;
                                                                            <span style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">At</span>
                                                                        <asp:Label ID="Label19" runat="server" Text='<%#Eval("ActSchTime") %>'></asp:Label>
                                                                            &nbsp;&nbsp;&nbsp; <b style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">For</b>&nbsp;:&nbsp;<asp:Label ID="Label8" runat="server" Text='<%#Eval("ActStartDuration") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(1) %>'></asp:Label>&nbsp; <span style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">Hrs.</span>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 14px; color: #000000;" align="left">
                                                                            <b style="font-family: Verdana, Tahoma, sans-serif; font-size: 13px;">Meeting with :</b><asp:Label ID="Label20" runat="server" Text='<%#Eval("ActWith") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 14px; color: #000000;">
                                                                            <asp:Label ID="Label9" runat="server" Text='<%#Eval("ActNote") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(1) %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <table width="100%" cellpadding="0" cellspacing="0" style="font-family: Verdana, Tahoma, sans-serif; font-size: 15px;">
                                                                <tr>
                                                                    <td align="left">
                                                                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("ActSubject") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(9) %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <asp:Panel ID="PanelCall" runat="server" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(9) %>'>
                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #000000;" align="left">
                                                                            <b>Scheduled on  </b>
                                                                            <asp:Label ID="Label10" runat="server" Font-Italic="true" Text='<%#Eval("ActDueDate") %>'></asp:Label>

                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 14px; color: #000000;" align="left">
                                                                            <b >Assigned To :</b>
                                                                            <asp:Label ID="Label22" runat="server" Text='<%#Eval("FullName") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(9) %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #000000;" align="left">
                                                                            <asp:Label ID="Label13" runat="server" Text='<%#Eval("ActNote") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(9) %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #000000;">
                                                                            <b>Call with : </b>
                                                                            <asp:Label ID="Label12" runat="server" Text='<%#Eval("ActWith") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </asp:Panel>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center">
                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                <tr>
                                                                    <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #000000;" align="left">
                                                                        <asp:Label ID="Label14" runat="server" Text='<%#Eval("ActSubject") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(3) %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <asp:Panel ID="PanelEvents" runat="server" Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(3) %>'>
                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 14px; color: #000000;" align="left">
                                                                            <b>Assigned To :</b>
                                                                            <asp:Label ID="Label23" runat="server" Text='<%#Eval("FullName") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(3) %>'></asp:Label>
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #000000;" align="left">
                                                                            <b>Start date : </b>
                                                                            <asp:Label ID="Label15" runat="server" Font-Italic="true" Text='<%#Eval("ActStartDateNew") %>'></asp:Label>&nbsp;<asp:Label ID="Label3" runat="server" Font-Italic="true" Text='<%#Eval("ActStartDuration") %>'></asp:Label>


                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #000000;" align="left">
                                                                            <b>End date </b>
                                                                            <asp:Label ID="Label6" runat="server" Font-Italic="true" Text='<%#Eval("ActDueDateNew") %>'></asp:Label>&nbsp;<asp:Label ID="Label18" runat="server" Font-Italic="true" Text='<%#Eval("ActEndDuration") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #000000;">
                                                                            <asp:Label ID="Label16" runat="server" Text='<%#Eval("ActNote") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left" style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #000000;">
                                                                            <b>Location: </b>
                                                                            <asp:Label ID="Label17" runat="server" Text='<%#Eval("ActLocation") %>'></asp:Label>
                                                                        </td>
                                                                    </tr>

                                                                </asp:Panel>
                                                            </table>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #545454;">
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("ActNote") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(5) %>'></asp:Label>
                                                            <asp:TextBox ID="txtActNote" runat="server" Text='<%#Eval("ActNote") %>' Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #545454;">
                                                            <asp:Label ID="Label5" runat="server" Text='<%#Eval("ActNote") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(7) %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                     <%-- <tr>
                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #545454;">
                                                            Due Date : <asp:Label ID="Label4" runat="server" Text='<%#Eval("ActDueDate") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(7) %>'></asp:Label>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="font-family: Calibri, Helvetica, Arial; font-size: 15px; color: #545454;">
                                                            <asp:Image ID="Image7" runat="server" ImageUrl='<%# Eval("ActFilePath","~/Images/FileSmall/{0}") %>' Visible='<%# Convert.ToString(Eval("ActTypeId")) == Convert.ToString(8) %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="line-height: 3px;">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" style="font-family: Verdana; font-size: 14px; color: #267cb2;"></td>
                                                    </tr>
                                                </table>


                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>

                                </asp:GridView>                               
                       </td>
                </tr>
               
            </table>
        </center>
    </div>


     
</asp:Content>

