using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Net;
using System.IO;
using System.Collections;
using System.IO.Compression;  
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Configuration;

public partial class DashboardNew : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
    SqlConnection conIns = new SqlConnection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["OrgID"] != "" && Request.QueryString["OrgID"] != null)
            {
                string OrgID = "";
                OrgID = Convert.ToString(Request.QueryString["OrgID"]);
                BindUpcomingMeetingEvent();
                BindGVActivity(OrgID);
                pnlActivity.Visible =  true;
            }
            else
            {
                pnlActivity.Visible = false;
                BindUpcomingMeetingEvent();
                BindGVActivity("0");
            }
        }
        else
        {
            //BindGVEditP();
        }
    }

    public void BindGVActivity(string OrgID)
    {
        //con = DB.DynamicConnection();
        if (OrgID != "0" && OrgID != "" && OrgID != null)
        {
            //pnlActivity.Visible = true;
            SqlCommand cmdr = new SqlCommand("Select * from tbl_OrganisationMaster where orgid=" + OrgID, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader dr = cmdr.ExecuteReader();
            if (dr.Read())
            {
                //lblCompany.Text = dr["orgname"].ToString();
                Session["orgID"] = dr["orgid"];
            }
            dr.Close();
            SqlCommand cmdWitAssId = new SqlCommand("select *,convert(varchar(12),ActStartDate,106) as ActStartDateNew,(tbl_UserMaster.FirstName + ' ' + tbl_UserMaster.LastName) as fullname, OM.Orgname,convert(varchar(12),ActDueDate,106) as actduedatenew,(tbl_UserMaster.FirstName + ' ' + tbl_UserMaster.LastName) as fullname from  tbl_ActivityMaster join tbl_UserMaster on tbl_UserMaster.UserId=tbl_ActivityMaster.UserID join tbl_organisationmaster OM on tbl_Activitymaster.orgid=OM.orgid where tbl_ActivityMaster.activestatus =1 and tbl_ActivityMaster.OrgID=@OrgID order by tbl_Activitymaster.ID desc", conn);
            cmdWitAssId.Parameters.Add(new SqlParameter("@OrgID", SqlDbType.Int)).Value = OrgID;
            SqlDataAdapter DaOrgID = new SqlDataAdapter();
            DaOrgID.SelectCommand = cmdWitAssId;
            DataTable DtOrgID = new DataTable();
            DataSet DsOrgID = new DataSet();
            DaOrgID.Fill(DsOrgID);
            DtOrgID = DsOrgID.Tables[0];
            GVActivity.DataSource = DtOrgID;
            GVActivity.DataBind();
        }
        else
        {
            //lblCompany.Text = "All Communication (Click on Company name in LEFT or RIGHT panel to do activity and see filter communication)";
            //pnlActivity.Visible = false;

            SqlCommand cmd = new SqlCommand("select *,convert(varchar(12),ActStartDate,106) as ActStartDateNew,convert(varchar(12),ActDueDate,106) as actduedatenew,(U.FirstName + ' ' + U.LastName) as fullname,  O.Orgname from  tbl_ActivityMaster A,  tbl_UserMaster U,tbl_organisationmaster O where U.UserId=A.UserID and  A.orgid=O.orgid  and A.activestatus =1  and  U.UserID=" + Session["ActiveUserID"] + "  order by A.ActCreateDate desc", conn);
            
            SqlDataAdapter DaAct = new SqlDataAdapter();
            DaAct.SelectCommand = cmd;
            DataTable DtAct = new DataTable();
            DataSet DsAct = new DataSet();
            DaAct.Fill(DsAct);
            DtAct = DsAct.Tables[0];
            GVActivity.DataSource = DtAct;
            GVActivity.DataBind();
        }

           
    }

    

     

    private DataTable GetData(SqlCommand cmd)
    {
        DataTable dt = new DataTable();

        SqlDataAdapter sda = new SqlDataAdapter();
                cmd.Connection = conn;
                conn.Open();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                return dt;         
    }
      
   
    protected void GVActivity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="Edit")
        {               
            
           
        }
    }
    protected void GVActivity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVActivity.PageIndex = e.NewPageIndex;
        BindGVActivity("0");
    }
    protected void btnSubmitTask_Click(object sender, EventArgs e)
    {
        if (txtDueDate.Text.Trim().Length > 0)
        {
            try
            {
                //con = DB.DynamicConnection();
                SqlCommand cmdInsertTask = new SqlCommand("insert into tbl_ActivityMaster (OrgID,ActSubject,ActDueDate,ActSchTime,ActStartDuration,ActEndDuration,NoteNFile,ActTypeId,ActNote,ActFilePath,ActCreateDate,UserID,ActiveStatus,ActWith,DoneStatus,VisibleToGroup,Islike) values (@OrgID,@ActSubject,@ActDueDate,@ActSchTime,@ActStartDuration,@ActEndDuration,@NoteNFile,@ActTypeId,@ActNote,@ActFilePath,getdate(),@UserID,@ActiveStatus,@ActWith,@DoneStatus,@VisibleToGroup,@Islike)", conn);
                cmdInsertTask.Parameters.Add(new SqlParameter("@OrgID", SqlDbType.Int)).Value = Session["orgID"];
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActSubject", SqlDbType.NVarChar)).Value = txtTaskSubject.Text;
                //cmdInsertTask.Parameters.Add(new SqlParameter("ActAssignId", SqlDbType.NVarChar)).Value = DdrUserListTask.SelectedValue.ToString();
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActDueDate", SqlDbType.DateTime)).Value = txtDueDate.Text;
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActSchTime", SqlDbType.NVarChar)).Value = "";
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActStartDuration", SqlDbType.NVarChar)).Value = "";
                cmdInsertTask.Parameters.Add(new SqlParameter("@NoteNFile", SqlDbType.NVarChar)).Value = "";
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActTypeId", SqlDbType.Int)).Value = 7;
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActNote", SqlDbType.NVarChar)).Value = txtTaskDescription.Text;
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActFilePath", SqlDbType.NVarChar)).Value = "";
                cmdInsertTask.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar)).Value = Session["ActiveUserID"];
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActiveStatus", SqlDbType.Bit)).Value = 1;
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActWith", SqlDbType.NVarChar)).Value = "";
                cmdInsertTask.Parameters.Add(new SqlParameter("@ActEndDuration", SqlDbType.NVarChar)).Value = "";

                if (chkTaskDoneF.Checked)
                {
                    cmdInsertTask.Parameters.Add(new SqlParameter("DoneStatus", SqlDbType.Bit)).Value = 1;
                }
                else
                {
                    cmdInsertTask.Parameters.Add(new SqlParameter("DoneStatus", SqlDbType.Bit)).Value = 0;
                }

                cmdInsertTask.Parameters.Add(new SqlParameter("VisibleToGroup", SqlDbType.Int)).Value = 0;
                cmdInsertTask.Parameters.Add(new SqlParameter("Islike", SqlDbType.Bit)).Value = 0;
                conn.Open();
                Int64 insert = cmdInsertTask.ExecuteNonQuery();
                conn.Close();
                if (insert > 0)
                {
                    //ClearAll();
                }
                BindGVActivity(Convert.ToString(Request.QueryString["OrgID"]));
                lblError.Visible = false;
                lblError.Text = "";
            }
            catch (Exception Ex)
            {
                lblError.Visible = true;
                lblError.Text = "Some error while creating Task.";
            }
        }
        else
        {
            lblError.Visible = true;
            lblError.Text = "Due Date Required field for Creating Task.";
        }
    }
    protected void btnSubmitNote_Click(object sender, EventArgs e)
    {
        try
        {
            //con = DB.DynamicConnection();
            SqlCommand cmdInsertNote = new SqlCommand("insert into tbl_ActivityMaster (OrgID,ActSubject,ActTypeId,ActNote,ActCreateDate,UserID,ActiveStatus,DoneStatus) values (@OrgID,@ActSubject,@ActTypeId,@ActNote,getdate(),@UserID,@ActiveStatus,@DoneStatus)", conn);
            cmdInsertNote.Parameters.Add(new SqlParameter("@OrgID", SqlDbType.Int)).Value = Session["OrgID"];
            cmdInsertNote.Parameters.Add(new SqlParameter("@ActSubject", SqlDbType.NVarChar)).Value = "NOTE";

            cmdInsertNote.Parameters.Add(new SqlParameter("@ActTypeId", SqlDbType.Int)).Value = 5;
            cmdInsertNote.Parameters.Add(new SqlParameter("@ActNote", SqlDbType.NVarChar)).Value = txtNote.Text;
            cmdInsertNote.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = Session["ActiveUserID"];
            cmdInsertNote.Parameters.Add(new SqlParameter("@ActiveStatus", SqlDbType.Bit)).Value = 1;            
            cmdInsertNote.Parameters.Add(new SqlParameter("@DoneStatus", SqlDbType.Bit)).Value = 1;
            


            conn.Open();
            Int64 insert = cmdInsertNote.ExecuteNonQuery();
            conn.Close();
            if (insert > 0)
            {
                txtNote.Text = "";
            }
            BindGVActivity(Convert.ToString(Request.QueryString["OrgID"]));
            lblError.Visible = false;
            lblError.Text = "";
        }
        catch (Exception Ex)
        {
            lblError.Visible = true;
            lblError.Text = "Some error while creating Note.";
        }
    }
    protected void btnCreateMeeting_Click(object sender, EventArgs e)
    {
        try
        {
            //con = DB.DynamicConnection();
            SqlCommand cmdInsertMeeting = new SqlCommand("insert into tbl_ActivityMaster (OrgID,ActSubject,ActDueDate,ActSchTime,ActStartDuration,ActEndDuration,ActTypeId,ActNote,ActCreateDate,UserID,ActiveStatus,ActWith,DoneStatus, ActLocation) output Inserted.ID values (@OrgID,@ActSubject,@ActDueDate,@ActSchTime,@ActStartDuration,@ActEndDuration,@ActTypeId,@ActNote,getdate(),@UserID,@ActiveStatus,@ActWith,@DoneStatus,@ActLocation)", conn);
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@OrgID", SqlDbType.NVarChar)).Value = Session["orgID"];
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActSubject", SqlDbType.NVarChar)).Value = txtMeetingSubjectF.Text;
            if (txtMeetDateF.Text=="")
            {
                cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActDueDate", SqlDbType.DateTime)).Value = DBNull.Value;
            }
            else
            {
                cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActDueDate", SqlDbType.DateTime)).Value = DateTime.Parse(txtMeetDateF.Text);
            }
            
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActSchTime", SqlDbType.NVarChar)).Value = DdrMeetingStartTimeF.SelectedValue.ToString();
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActStartDuration", SqlDbType.NVarChar)).Value = DdrMeetingDurationF.SelectedItem.ToString();         
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActTypeId", SqlDbType.Int)).Value = 1;
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActNote", SqlDbType.NVarChar)).Value = txtMeetDescF.Text;
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = Session["ActiveUserID"];
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActiveStatus", SqlDbType.Bit)).Value = 1;
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActWith", SqlDbType.NVarChar)).Value = txtMeetWithF.Text;
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActEndDuration", SqlDbType.NVarChar)).Value = "";
            cmdInsertMeeting.Parameters.Add(new SqlParameter("@ActLocation", SqlDbType.NVarChar)).Value = txtMeetLocationF.Text;
            if (chkMeetingDoneF.Checked)
            {
                cmdInsertMeeting.Parameters.Add(new SqlParameter("@DoneStatus", SqlDbType.Bit)).Value = 1;
            }
            else
            {
                cmdInsertMeeting.Parameters.Add(new SqlParameter("@DoneStatus", SqlDbType.Bit)).Value = 0;
            }
          
            conn.Open();
            int insert = (int)cmdInsertMeeting.ExecuteScalar();
            //Int64 LastId = Convert.ToInt64(cmdInsert.ExecuteScalar());

            conn.Close();

            //con = DB.DynamicConnection();
            SqlCommand cmdDr = new SqlCommand("select * from tbl_tempMeetingParticipant where SessionID='" + Session.SessionID + "'", conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader dr = cmdDr.ExecuteReader();
            while (dr.Read())
            {
                //conIns = DB.DynamicConnection();
                SqlCommand cmdins = new SqlCommand("Insert into tbl_ActParticipant (ActID, Email) values(@ActID, @Email)", conIns);
                if (conIns.State == ConnectionState.Closed)
                    conIns.Open();
                cmdins.Parameters.Add(new SqlParameter("@ActID", insert));
                cmdins.Parameters.Add(new SqlParameter("@Email", dr["Email"].ToString().ToLower().Trim()));
                cmdins.ExecuteNonQuery();
                conIns.Close();
            }
            dr.Close();
            conn.Close();

            SqlCommand cmdDel = new SqlCommand("Delete from tbl_tempMeetingParticipant where SessionID='" + Session.SessionID + "'", conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmdDel.ExecuteNonQuery();
            conn.Close();

            sendMeeting(insert);

            if (insert > 0)
            {
                
                txtMeetingSubjectF.Text = "";
                txtMeetDateF.Text = "";
                txtMeetWithF.Text = "";
                txtMeetLocationF.Text = "";
                txtMeetDescF.Text = "";
                txtMeetWithF.Text = "";
                BindParticipant();
            }
            BindGVActivity(Convert.ToString(Request.QueryString["OrgID"]));
            BindUpcomingMeetingEvent();
            lblError.Visible = false;
            lblError.Text = "";
        }
        catch (Exception Ex)
        {
            lblError.Visible = true;
            lblError.Text = "Some error while creating meeting";
        }
    }
    protected void btnCreateCall_Click(object sender, EventArgs e)
    {
        try
        {
            //con = DB.DynamicConnection();
            SqlCommand cmdInsertCall = new SqlCommand("insert into tbl_ActivityMaster (OrgID,ActSubject,ActDueDate,ActSchTime,ActStartDuration,ActEndDuration,ActTypeId,ActNote,ActCreateDate,UserID,ActiveStatus,ActWith,DoneStatus) values (@OrgID,@ActSubject,@ActDueDate,@ActSchTime,@ActStartDuration,@ActEndDuration,@ActTypeId,@ActNote,getdate(),@UserID,@ActiveStatus,@ActWith,@DoneStatus)", conn);
            cmdInsertCall.Parameters.Add(new SqlParameter("@OrgID", SqlDbType.NVarChar)).Value = Session["orgID"];
            cmdInsertCall.Parameters.Add(new SqlParameter("@ActSubject", SqlDbType.NVarChar)).Value = txtCallSubjectF.Text;            
            if (txtCallDateF.Text == "")
            {
                cmdInsertCall.Parameters.Add(new SqlParameter("@ActDueDate", SqlDbType.DateTime)).Value = DBNull.Value;
            }
            else
            {
                cmdInsertCall.Parameters.Add(new SqlParameter("@ActDueDate", SqlDbType.DateTime)).Value = DateTime.Parse(txtCallDateF.Text);
            }
            cmdInsertCall.Parameters.Add(new SqlParameter("@ActSchTime", SqlDbType.NVarChar)).Value = DdrCalltimeF.SelectedValue.ToString();
            cmdInsertCall.Parameters.Add(new SqlParameter("@ActStartDuration", SqlDbType.NVarChar)).Value = "";            
            cmdInsertCall.Parameters.Add(new SqlParameter("@ActTypeId", SqlDbType.Int)).Value = 9;
            cmdInsertCall.Parameters.Add(new SqlParameter("@ActNote", SqlDbType.NVarChar)).Value = txtCallCommentF.Text;
   
            cmdInsertCall.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = Session["ActiveUserID"];
            cmdInsertCall.Parameters.Add(new SqlParameter("@ActiveStatus", SqlDbType.Bit)).Value = 1;
            cmdInsertCall.Parameters.Add(new SqlParameter("@ActWith", SqlDbType.NVarChar)).Value = txtCallWithF.Text;
            cmdInsertCall.Parameters.Add(new SqlParameter("@ActEndDuration", SqlDbType.NVarChar)).Value = "";


            if (chkCallDoneStatusF.Checked)
            {
                cmdInsertCall.Parameters.Add(new SqlParameter("@DoneStatus", SqlDbType.Bit)).Value = 1;
            }
            else
            {
                cmdInsertCall.Parameters.Add(new SqlParameter("@DoneStatus", SqlDbType.Bit)).Value = 0;
            }
             
            conn.Open();
            Int64 insert = cmdInsertCall.ExecuteNonQuery();
            conn.Close();
            if (insert > 0)
            {
                txtCallCommentF.Text = "";
                txtCallDateF.Text = "";
                txtCallSubjectF.Text = "";
                txtCallWithF.Text = "";
            }
            BindGVActivity(Convert.ToString(Request.QueryString["OrgID"]));
            BindUpcomingMeetingEvent();
            lblError.Visible = false;
            lblError.Text = "";
        }
        catch (Exception Ex)
        {
            lblError.Visible = true;
            lblError.Text = "Some error while creating call";
        }
    }
    protected void BtnEvent_Click(object sender, EventArgs e)
    {
        try
        {
            //conn = DB.DynamicConnection();
            SqlCommand cmdInsertEvent = new SqlCommand("insert into tbl_ActivityMaster (OrgID,ActSubject,ActDueDate,ActStartDuration,ActEndDuration,ActTypeId,ActNote,ActCreateDate,UserID,ActiveStatus,ActWith,DoneStatus,ActLocation,ActStartDate) values (@OrgID,@ActSubject,@ActDueDate,@ActStartDuration,@ActEndDuration,@ActTypeId,@ActNote,getdate(),@UserID,@ActiveStatus,@ActWith,@DoneStatus,@ActLocation,@ActStartDate)", conn);
            cmdInsertEvent.Parameters.Add(new SqlParameter("@OrgID", SqlDbType.NVarChar)).Value = Session["orgID"];
            cmdInsertEvent.Parameters.Add(new SqlParameter("@ActSubject", SqlDbType.NVarChar)).Value = txtEventSubjectF.Text;
            if(txtEventEndDateF.Text=="")
            {
                cmdInsertEvent.Parameters.Add(new SqlParameter("@ActDueDate", SqlDbType.DateTime)).Value = DBNull.Value;
            }
            else
            {
                cmdInsertEvent.Parameters.Add(new SqlParameter("@ActDueDate", SqlDbType.DateTime)).Value = txtEventEndDateF.Text;
            }
            cmdInsertEvent.Parameters.Add(new SqlParameter("@ActStartDuration", SqlDbType.NVarChar)).Value = DdrEventStartTimeF.SelectedItem.ToString();
            cmdInsertEvent.Parameters.Add(new SqlParameter("@ActEndDuration", SqlDbType.NVarChar)).Value = DdrEventEndTimeF.SelectedItem.ToString();
            cmdInsertEvent.Parameters.Add(new SqlParameter("@ActTypeId", SqlDbType.Int)).Value = 3;
            cmdInsertEvent.Parameters.Add(new SqlParameter("@ActNote", SqlDbType.NVarChar)).Value = txtEventCommentF.Text;
            cmdInsertEvent.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = Session["ActiveUserID"];          
            cmdInsertEvent.Parameters.Add(new SqlParameter("@ActiveStatus", SqlDbType.Bit)).Value = 1;
            cmdInsertEvent.Parameters.Add(new SqlParameter("@ActWith", SqlDbType.NVarChar)).Value = "";
            if (chkEventDoneF.Checked)
            {
                cmdInsertEvent.Parameters.Add(new SqlParameter("@DoneStatus", SqlDbType.Bit)).Value = 1;
            }
            else
            {
                cmdInsertEvent.Parameters.Add(new SqlParameter("@DoneStatus", SqlDbType.Bit)).Value = 0;
            }
          
            cmdInsertEvent.Parameters.Add(new SqlParameter("@ActLocation", SqlDbType.NVarChar)).Value = txtEventLocationF.Text;

            if (txtEventStartDateF.Text=="")
            {
                cmdInsertEvent.Parameters.Add(new SqlParameter("@ActStartDate", SqlDbType.DateTime)).Value = DBNull.Value;
            }
            else
            {
                cmdInsertEvent.Parameters.Add(new SqlParameter("@ActStartDate", SqlDbType.DateTime)).Value = DateTime.Parse(txtEventStartDateF.Text);
            }
            

            conn.Open();
            Int64 insert = cmdInsertEvent.ExecuteNonQuery();
            conn.Close();
            if (insert > 0)
            {
                txtEventLocationF.Text = "";
                txtEventEndDateF.Text = "";
                txtEventCommentF.Text = "";
                txtEventStartDateF.Text = "";
                txtEventSubjectF.Text = "";
            }
            BindGVActivity(Convert.ToString(Request.QueryString["OrgID"]));
            BindUpcomingMeetingEvent();
            lblError.Visible = false;
            lblError.Text = "";
        }
        catch (Exception Ex)
        {
            lblError.Visible = true;
            lblError.Text = "Some error while creating Event";
        }
    }
    protected void sendMeeting(int ActID)
    {
        string Subject = txtMeetingSubjectF.Text.Trim();
        string Description = txtMeetDescF.Text.Trim();
        string MeetingWith = txtMeetWithF.Text.Trim();
        string Location =  txtMeetLocationF.Text.Trim();
        DateTime startDateTime;
        //con = DB.DynamicConnection();
        startDateTime = DateTime.Parse(txtMeetDateF.Text + " " + DdrMeetingStartTimeF.Text);
        string hh = DdrMeetingDurationF.Text.Substring(0, 2);
        string mm = DdrMeetingDurationF.Text.Substring(3, 2);
        DateTime dtend = startDateTime.AddHours(double.Parse(hh));
        dtend = dtend.AddMinutes(double.Parse(mm));


        //Declaration a list of attendees
        MailAddressCollection macCollection = new MailAddressCollection();
        macCollection.Add(new MailAddress("meeting@mosindia.in"));

        string qry = "Select * from tbl_ActParticipant where ActID=" + ActID;
        SqlCommand cmd = new SqlCommand(qry, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            macCollection.Add(new MailAddress(dr["Email"].ToString()));
        }
        dr.Close();
        conn.Close();

        try
        {
            MailMessage mmMessage = CreateMeetingRequest(startDateTime, DdrMeetingDurationF.Text, dtend, Subject, Description, Location, MeetingWith, Session["Fullname"].ToString(), Session["emailid"].ToString(), macCollection);
            //Create smtp client
            System.Net.NetworkCredential authenticaionInfo = new System.Net.NetworkCredential("meeting@mosindia.in", "Jmd@7668");
            SmtpClient SmtpMail = new SmtpClient("smtp.gmail.com", 587);
            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.Credentials = authenticaionInfo;
            SmtpMail.EnableSsl = true;
            SmtpMail.Timeout = 900000;
            SmtpMail.Send(mmMessage);
        }
        catch (Exception ex)
        { }
    }

    protected void sendMeetingOneByOne( string ActID, int APID)
    {
        string Subject = txtMeetingSubjectF.Text.Trim();
        string Description = txtMeetDescF.Text.Trim();
        string MeetingWith = txtMeetWithF.Text.Trim();
        string Location = txtMeetLocationF.Text.Trim();
        DateTime startDateTime;
        //con = DB.DynamicConnection();
        startDateTime = DateTime.Parse(txtMeetDateF.Text + " " + DdrMeetingStartTimeF.Text);
        string hh = DdrMeetingDurationF.Text.Substring(0, 2);
        string mm = DdrMeetingDurationF.Text.Substring(3, 2);
        DateTime dtend = startDateTime.AddHours(double.Parse(hh));
        dtend = dtend.AddMinutes(double.Parse(mm));


        //Declaration a list of attendees
        MailAddressCollection macCollection = new MailAddressCollection();
        macCollection.Add(new MailAddress("meeting@mosindia.in"));

        string qry = "Select * from tbl_ActParticipant where  APID="+ APID +"";
        SqlCommand cmd = new SqlCommand(qry, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            macCollection.Add(new MailAddress(dr["Email"].ToString()));
        }
        dr.Close();
        conn.Close();

        try
        {
            MailMessage mmMessage = CreateMeetingRequest(startDateTime, DdrMeetingDurationF.Text, dtend, Subject, Description, Location, MeetingWith, Session["Fullname"].ToString(), Session["emailid"].ToString(), macCollection);
            //Create smtp client
            System.Net.NetworkCredential authenticaionInfo = new System.Net.NetworkCredential("meeting@mosindia.in", "Jmd@7668");
            SmtpClient SmtpMail = new SmtpClient("smtp.gmail.com", 587);
            SmtpMail.UseDefaultCredentials = false;
            SmtpMail.Credentials = authenticaionInfo;
            SmtpMail.EnableSsl = true;
            SmtpMail.Timeout = 900000;
            SmtpMail.Send(mmMessage);
        }
        catch (Exception ex)
        { }
    }

    public static MailMessage CreateMeetingRequest(DateTime dtStart, string Duration, DateTime dtEnd, string strSubject, string strSummary, string strLocation, string strMeetWith, string strOrganizerName, string strOrganizerEmail, MailAddressCollection macAttendeeList)
    {
        //Create an instance of mail message
        MailMessage mmMessage = new MailMessage();
        //  Set up the different mime types contained in the message
        //System.Net.Mime.ContentType typeText = new System.Net.Mime.ContentType("text/plain");
        System.Net.Mime.ContentType typeHTML = new System.Net.Mime.ContentType("text/html");
        System.Net.Mime.ContentType typeCalendar = new System.Net.Mime.ContentType("text/calendar");

        //  Add parameters to the calendar header
        typeCalendar.Parameters.Add("method", "REQUEST");
        typeCalendar.Parameters.Add("name", "meeting.ics");

        ////  Create message body parts in text format
        //string strBodyText = "Type:Meeting\r\nOrganizer: {0}\r\nStart Time:{1}\r\nEnd Time:{2}\r\nTime Zone:{3}\r\nLocation: {4}\r\n\r\n*~*~*~*~*~*~*~*~*~*\r\n\r\n{5}";
        //strBodyText = string.Format(strBodyText, strOrganizerName, dtStart.ToLongDateString() + " " + dtStart.ToLongTimeString(),
        //dtEnd.ToLongDateString() + " " + dtEnd.ToLongTimeString(), System.TimeZone.CurrentTimeZone.StandardName,
        //strLocation, strSummary);
        //AlternateView viewText = AlternateView.CreateAlternateViewFromString(strBodyText, typeText);
        //mmMessage.AlternateViews.Add(viewText);

        //Create the Body in HTML format
        string strBodyHTML = "<HTML>\r\n<HEAD>\r\n<META HTTP-EQUIV=\"Content-Type\" CONTENT=\"text/html; charset=utf-8\">\r\n<TITLE>{0}</TITLE>\r\n</HEAD>\r\n<BODY>\r\n<P><FONT SIZE=2>Type:Meeting<BR>\r\nOrganizer:{1}<BR>\r\nStart Time:{2}<BR>\r\nDuration:{3}<BR>\r\nEnd Time:{4}<BR>\r\nTime Zone:{5}<BR>\r\nMeeting With:{6}<BR>\r\n<BR>\r\n*~*~*~*~*~*~*~*~*~*<BR>\r\n<BR>\r\n{7}<BR>\r\n</FONT>\r\n</P>\r\n\r\n</BODY>\r\n</HTML>";
        strBodyHTML = string.Format(strBodyHTML, strSummary, strOrganizerName, dtStart.ToLongDateString() + " " + dtStart.ToLongTimeString(), Duration,
        dtEnd.ToLongDateString() + " " + dtEnd.ToLongTimeString(), System.TimeZone.CurrentTimeZone.StandardName,
        strMeetWith, strSummary);
        AlternateView viewHTML = AlternateView.CreateAlternateViewFromString(strBodyHTML, typeHTML);
        mmMessage.AlternateViews.Add(viewHTML);

        //Create the Body in VCALENDAR format
        string strCalDateFormat = "yyyyMMddTHHmmssZ";
        string strBodyCalendar = "BEGIN:VCALENDAR\r\nMETHOD:REQUEST\r\nPRODID:Microsoft CDO for Microsoft Exchange\r\nVERSION:2.0\r\nBEGIN:VTIMEZONE\r\nTZID:(GMT-06.00) Central Time (US &amp; Canada)\r\nX-MICROSOFT-CDO-TZID:11\r\nBEGIN:STANDARD\r\nDTSTART:16010101T020000\r\nTZOFFSETFROM:-0500\r\nTZOFFSETTO:-0600\r\nRRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=11;BYDAY=1SU\r\nEND:STANDARD\r\nBEGIN:DAYLIGHT\r\nDTSTART:16010101T020000\r\nTZOFFSETFROM:-0600\r\nTZOFFSETTO:-0500\r\nRRULE:FREQ=YEARLY;WKST=MO;INTERVAL=1;BYMONTH=3;BYDAY=2SU\r\nEND:DAYLIGHT\r\nEND:VTIMEZONE\r\nBEGIN:VEVENT\r\nDTSTAMP:{8}\r\nDTSTART:{0}\r\nSUMMARY:{7}\r\nUID:{5}\r\nATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=\"{9}\":MAILTO:{9}\r\nACTION;RSVP=TRUE;CN=\"{4}\":MAILTO:{4}\r\nORGANIZER;CN=\"{3}\":mailto:{4}\r\nLOCATION:{2}\r\nDTEND:{1}\r\nDESCRIPTION:{7}\\N\r\nSEQUENCE:1\r\nPRIORITY:5\r\nCLASS:\r\nCREATED:{8}\r\nLAST-MODIFIED:{8}\r\nSTATUS:CONFIRMED\r\nTRANSP:OPAQUE\r\nX-MICROSOFT-CDO-BUSYSTATUS:BUSY\r\nX-MICROSOFT-CDO-INSTTYPE:0\r\nX-MICROSOFT-CDO-INTENDEDSTATUS:BUSY\r\nX-MICROSOFT-CDO-ALLDAYEVENT:FALSE\r\nX-MICROSOFT-CDO-IMPORTANCE:1\r\nX-MICROSOFT-CDO-OWNERAPPTID:-1\r\nX-MICROSOFT-CDO-ATTENDEE-CRITICAL-CHANGE:{8}\r\nX-MICROSOFT-CDO-OWNER-CRITICAL-CHANGE:{8}\r\nBEGIN:VALARM\r\nACTION:DISPLAY\r\nDESCRIPTION:REMINDER\r\nTRIGGER;RELATED=START:-PT00H15M00S\r\nEND:VALARM\r\nEND:VEVENT\r\nEND:VCALENDAR\r\n";
        strBodyCalendar = string.Format(strBodyCalendar, dtStart.ToUniversalTime().ToString(strCalDateFormat), dtEnd.ToUniversalTime().ToString(strCalDateFormat),
        strLocation, strOrganizerName, strOrganizerEmail, Guid.NewGuid().ToString("B"), strSummary, strSubject,
        DateTime.Now.ToUniversalTime().ToString(strCalDateFormat), macAttendeeList.ToString());
        AlternateView viewCalendar = AlternateView.CreateAlternateViewFromString(strBodyCalendar, typeCalendar);
        viewCalendar.TransferEncoding = TransferEncoding.SevenBit;
        mmMessage.AlternateViews.Add(viewCalendar);

        //Adress the message
        mmMessage.From = new MailAddress(strOrganizerEmail);
        foreach (MailAddress attendee in macAttendeeList)
        {
            mmMessage.To.Add(attendee);
        }
        mmMessage.To.Add(strOrganizerEmail);
        mmMessage.Subject = strSubject;

        return mmMessage;

    }

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchCustomers(string prefixText, int count)
    {
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = "Data Source=192.168.1.206;Initial Catalog=MOSCRM;User ID=mavericks$whimsclone;Password=jbsSSpk@95461";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select distinct email from tbl_ActParticipant where " +
                "email like @SearchText + '%' order by email";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> customers = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(sdr["email"].ToString());
                    }
                }
                conn.Close();
                return customers;
            }
        }
    }

    protected void BindUpcomingMeetingEvent()
    {
        //lblCompany.Text = "All Communication (Click on Company name in LEFT or RIGHT panel to do activity and see filter communicaiton)";
        //con = DB.DynamicConnection();
        pnlActivity.Visible = false;

        SqlCommand cmd = new SqlCommand("select *,tbl_usermaster.fullname, OM.OrgName from  tbl_ActivityMaster, tbl_UserMaster, tbl_OrganisationMaster OM, tbl_ActivityTypeMaster ATM where tbl_activitymaster.ActTypeID=ATM.ID and tbl_UserMaster.UserId=tbl_ActivityMaster.UserID and tbl_ActivityMaster.UserID=" + Session["ActiveUserID"] + " and tbl_activitymaster.orgid=OM.orgid and tbl_ActivityMaster.activestatus =1 and tbl_ActivityMaster.ActDueDate>='" + DateTime.Now.ToShortDateString() + "' and tbl_ActivityMaster.ActTypeID in (1,3,7,9) order by tbl_ActivityMaster.ActCreateDate desc", conn);

        SqlDataAdapter DaAct = new SqlDataAdapter();
        DaAct.SelectCommand = cmd;
        DataTable DtAct = new DataTable();

        DaAct.Fill(DtAct);

        gvUpcomingMeeting.DataSource = DtAct;
        gvUpcomingMeeting.DataBind();
    }

    protected void btnSubmitFeedBack_Click(object sender, EventArgs e)
    {
        try
        {
            //con = DB.DynamicConnection();
            SqlCommand cmdInsertFeedback = new SqlCommand("insert into tbl_ActivityMaster (OrgID,ActSubject,ActAssignId,ActDueDate,ActSchTime,ActStartDuration,ActEndDuration,NoteNFile,ActTypeId,ActNote,ActFilePath,ActCreateDate,UserID,ActiveStatus,ActWith,DoneStatus,VisibleToGroup,Islike) values (@OrgID,@ActSubject,@ActAssignId,@ActDueDate,@ActSchTime,@ActStartDuration,@ActEndDuration,@NoteNFile,@ActTypeId,@ActNote,@ActFilePath,getdate(),@UserID,@ActiveStatus,@ActWith,@DoneStatus,@VisibleToGroup,@Islike)", conn);
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@OrgID", SqlDbType.Int)).Value = Session["OrgID"];
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActSubject", SqlDbType.NVarChar)).Value = "Feedback";
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActAssignId", SqlDbType.NVarChar)).Value = "";
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActDueDate", SqlDbType.DateTime)).Value = System.DBNull.Value;
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActSchTime", SqlDbType.NVarChar)).Value = "";
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActStartDuration", SqlDbType.NVarChar)).Value = "";
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@NoteNFile", SqlDbType.NVarChar)).Value = "";
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActTypeId", SqlDbType.Int)).Value = 5;
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActNote", SqlDbType.NVarChar)).Value = txtFeedBack.Text;
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActFilePath", SqlDbType.NVarChar)).Value = "";
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int)).Value = Session["ActiveUserID"];
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActiveStatus", SqlDbType.Bit)).Value = 1;
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActWith", SqlDbType.NVarChar)).Value = "";
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@ActEndDuration", SqlDbType.NVarChar)).Value = "";
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@DoneStatus", SqlDbType.Bit)).Value = 0;
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@VisibleToGroup", SqlDbType.Int)).Value = 0;
            cmdInsertFeedback.Parameters.Add(new SqlParameter("@Islike", SqlDbType.Bit)).Value = 0;
            conn.Open();
            Int64 insert = cmdInsertFeedback.ExecuteNonQuery();
            conn.Close();
            if (insert > 0)
            {
                txtFeedBack.Text = "";
            }
            BindGVActivity(Convert.ToString(Request.QueryString["OrgID"]));
            lblError.Visible = false;
            lblError.Text = "";
        }
        catch (Exception Ex)
        {
            lblError.Visible = true;
            lblError.Text = "Some error while creating Note.";
        }
    }




   

    protected void btnAddP_Click(object sender, EventArgs e)
    {
        //if (DB.IsValidEmail(txtContactsSearch.Text) == true)
        //{
        //    con = DB.DynamicConnection();
        //    SqlCommand cmdc = new SqlCommand("Select count(*) from tbl_tempMeetingParticipant where SessionID='" + Session.SessionID + "' and email='" + txtContactsSearch.Text.Trim() + "'", con);
        //    if (con.State == ConnectionState.Closed)
        //        con.Open();
        //    int chkcnt = (int)cmdc.ExecuteScalar();
        //    if (chkcnt > 0)
        //    {
        //        lblErrorP.Text = "Participant already selected for this meeting";
        //        lblErrorP.Visible = true;
        //    }
        //    else
        //    {
        //        lblErrorP.Visible = false;
        //        SqlCommand cmd = new SqlCommand("Insert into tbl_TempMeetingParticipant(SessionID,Email) Values(@SessionID,@Email)", con);
        //        if (con.State == ConnectionState.Closed)
        //            con.Open();
        //        cmd.Parameters.Add(new SqlParameter("@email", txtContactsSearch.Text));
        //        cmd.Parameters.Add(new SqlParameter("@SessionID", Session.SessionID));
        //        cmd.ExecuteNonQuery();
        //        con.Close();
        //        txtContactsSearch.Text = "";
        //        BindParticipant();
        //    }
        //}
    }   

    protected void gvEmailP_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //con = DB.DynamicConnection();
        string email = (string)gvEmailP.DataKeys[e.RowIndex].Value;

        SqlCommand cmdc = new SqlCommand("Delete from tbl_tempMeetingParticipant where SessionID='" + Session.SessionID + "' and email='" + email + "'", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmdc.ExecuteNonQuery();
        conn.Close();
        BindParticipant();
    }




  

    protected void BindParticipant()
    {
        //con = DB.DynamicConnection();
        SqlDataAdapter adpt = new SqlDataAdapter("select * from tbl_tempMeetingParticipant where SessionID='" + Session.SessionID + "'", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        gvEmailP.DataSource = dt;
        gvEmailP.DataBind();
        conn.Close();
    }

     

    protected void GVActivity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //conn = DB.DynamicConnection();
        if(e.Row.RowType == DataControlRowType.DataRow)
        {
            string Actid = ((DataRowView)e.Row.DataItem).Row.ItemArray[0].ToString();
            SqlCommand cmd = new SqlCommand("select A.Email from tbl_ActParticipant A, tbl_ActivityMaster B where A.ActID = B.id and A.ActID="+ Actid +"", conn);
            DataTable Dt = new DataTable();
            Label lblParticipant = e.Row.FindControl("lblParticipant") as Label;
            string EmailList = "";
            StringBuilder SB = new StringBuilder();
            SqlDataAdapter Da = new SqlDataAdapter();
            Da.SelectCommand = cmd;
            Da.Fill(Dt);
            for (int i = 0; i < Dt.Rows.Count; i++ )
            {
                if(i == Dt.Rows.Count - 1)
                {
                    SB.Append("<a href='mailto://" + Dt.Rows[i].ItemArray[0].ToString() + "'>" + Dt.Rows[i].ItemArray[0].ToString() + "</a>");
                }
                else
                {
                    SB.Append("<a href='mailto://" + Dt.Rows[i].ItemArray[0].ToString() + "'>" + Dt.Rows[i].ItemArray[0].ToString() + "</a>, ");
                }
            }

            EmailList = SB.ToString();
            SB.Clear();
            lblParticipant.Text = EmailList;
        }
    }

     
}