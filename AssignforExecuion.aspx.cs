using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

public partial class AssignforExecuion : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
    //string pwd="";
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["userType"].ToString().Trim() == "Super")
    //        {
    //            this.MasterPageFile = "~/MainMasterPagea.master";
    //        }
    //        else
    //        {
    //            this.MasterPageFile = "~/" + Session["MasterPage"];
    //        }
    //    }
    //    catch
    //    {
    //        Response.Redirect("default.aspx");
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            try
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                if ((bool)Session["Validate"] == true)
                {
                    
                    try
                    {
                        
                        string sid = Request.QueryString["sid"].ToString();
                        Session["sid"] = sid;
                    }
                    catch
                    {
                        Response.Redirect(ViewState["RefUrl"].ToString());
                    }

                    //int QID = int.Parse(Session["qid"].ToString());
                    int SID = int.Parse(Session["sid"].ToString());
                    EngineerEmployee();
                    ddlEmployee.SelectedValue = "";
                    CallProjectData(SID);
                    getEmpContact();
                }
            }
            catch(SqlException ex)
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void EngineerEmployee()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from EmployeeUserMaster EUM, DepartmentMaster DM where EUM.DepartmentID=DM.DepartmentID and ltrim(rtrim(DM.category))='Project' order by EmployeeName", conn);
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        conn.Close();
        dt.Rows.Add();
        dt.Rows[dt.Rows.Count - 1]["UserID"] = "";
        dt.Rows[dt.Rows.Count - 1]["EmployeeName"] = "(Select)";
        DataView dv = dt.DefaultView;
        dv.Sort = "EmployeeName asc";
        DataTable sortedDT = dv.ToTable();

        ddlEmployee.DataSource = sortedDT;
        ddlEmployee.DataBind();
    }

    protected void CallProjectData(int SurveyID)
    {
        string query = "Select *  from ClientMaster CM, SurveyClientItemHead SCIH where CM.ClientID=SCIH.ClientID and SCIH.SurveyID=" + SurveyID;

        SqlCommand cmd = new SqlCommand(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.Read())
        {
            txtClientName.Text = dr["ClientName"].ToString().Trim();

            //ddlEmployee.SelectedValue = dr["ProjectEngineerID"].ToString().Trim();
        }
        conn.Close();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        object refUrl = ViewState["RefUrl"];
        if (refUrl != null)
            Response.Redirect((string)refUrl);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlEmployee.Text.Length > 0)
        {
            SqlCommand fmdfinde = new SqlCommand("Select count(*) from ProjectImplementationPeople where UserID=@UserID and SurveyID=" + Session["sid"], conn);
            fmdfinde.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar, ddlEmployee.SelectedValue.ToString().Trim().Length)).Value = ddlEmployee.SelectedValue.ToString().Trim();

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            int chkd = (int)fmdfinde.ExecuteScalar();
            conn.Close();
            if (chkd > 0)
            {
                lblMessage.Text = "Mentioned Employee already exist for this survey";
            }
            else
            {


                //string EmployeeID = System.Guid.NewGuid().ToString().ToUpper().Trim();
                SqlCommand cmd = new SqlCommand("Insert Into [ProjectImplementationPeople] (SurveyID, UserID) Values(@SurveyID, @UserID)", conn);

                cmd.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar, ddlEmployee.SelectedValue.ToString().Trim().Length)).Value = ddlEmployee.SelectedValue.ToString().Trim();
                cmd.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int)).Value = Session["sid"];
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                conn.Close();
                lblMessage.Text = "Project Assigned to Engineer for Implementation";
                EmployeeEmail(int.Parse(Session["sid"].ToString()));
                getEmpContact();
            }
        }
        else
        {
            lblMessage.Text = "Employee Name should not be Empty";
        }


    }

    protected void gvExecutionEngg_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)gvExecutionEngg.Rows[e.RowIndex];

        int autoid = Int32.Parse(gvExecutionEngg.DataKeys[e.RowIndex].Value.ToString());
        //con = DB.DynamicConnection();
        SqlCommand cmd = new SqlCommand("Delete from ProjectImplementationPeople where ID=" + autoid, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        getEmpContact();
    }

    protected void getEmpContact()
    {
        //con = DB.DynamicConnection();
        SqlCommand cmd = new SqlCommand("Select PIP.ID, EUM.* from ProjectImplementationPeople PIP, EmployeeUserMaster EUM where PIP.UserID=EUM.UserID and SurveyID=" + Session["sid"], conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        gvExecutionEngg.DataSource = cmd.ExecuteReader();
        gvExecutionEngg.DataBind();
        conn.Close();
    }


    protected void EmployeeEmail(int SurveyID)
    {
        string mailbody = "";

        MailMessage msg = new MailMessage();
        msg.BodyEncoding = System.Text.ASCIIEncoding.ASCII;
        msg.IsBodyHtml = true;
        msg.Priority = MailPriority.Normal;
        //msg.To.Add("binod@mosindia.co.in");
        //msg.To.Add(txtEmail.Text.Trim());
        
        msg.Bcc.Add("jaybinod@gmail.com");
        //msg.Bcc.Add("amangoenka@gmail.com");
        msg.From = new MailAddress("noreply@infrasol.com");
        mailbody = "<html>";
        mailbody = mailbody + "<body>";
        mailbody = mailbody + "Dear "+ddlEmployee.SelectedItem.Text.Trim()+",<br><br>"+Session["EmployeeName"].ToString()+", has assigned you new client "+txtClientName.Text.Trim()+" for Implementation.<br><br>Please call on below mentioned number and proceed for Implementation<br><br>";
        //mailbody = mailbody + "Designation : " + txtDesignation.Text.Trim();
        //mailbody = mailbody + "<br>Deprtment : " + ddlDepartment.SelectedItem.Text.Trim();
        ////mailbody = mailbody + "<br>Address : " + txtAddress.Text.Trim();
        //mailbody = mailbody + "<br>City : " + txtCity.Text.Trim();
        //mailbody = mailbody + "<br>State : " + txtState.Text.Trim();
        //mailbody = mailbody + "<br>Postal Code : " + txtPinCode.Text.Trim();
        //mailbody = mailbody + "<br>Mobile : " + txtmobile.Text.Trim();
        //mailbody = mailbody + "<br><b>Email/User ID : " + txtEmail.Text.Trim();
        //mailbody = mailbody + "<br>Password : " + pwd;
        
        mailbody = mailbody + "</b></body></html>";
        msg.Subject = "New Project assigned for implementation: "+txtClientName.Text.Trim();
        msg.Body = mailbody;

        //Smaple 2
        //System.Net.NetworkCredential authenticaionInfo = new System.Net.NetworkCredential("binod@mosindia.co.in", "regency");
        //SmtpClient SmtpMail = new SmtpClient("smtpout.secureserver.net");
        //SmtpMail.UseDefaultCredentials = false;
        //SmtpMail.Credentials = authenticaionInfo;


        //------------------------------------------------
        //Sample 1
        // SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net");
        SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net");
        //SmtpClient SmtpMail = new SmtpClient("smtp.socketlabs.com");
        //SmtpMail.UseDefaultCredentials = true;
        //NetworkCredential NC = new NetworkCredential("jaybinod", "CVtbXIu5RdS0");
        //SmtpMail.Credentials = NC;
        //--------------------------------------

        //SmtpMail.Send(msg);
        lblMessage.Text = "Email Confirmation sended to Survey Engineer";
        //RefreshData();

    }

    protected void refreshField()
    {
        //txtEmployeeName.Text = "";
        //txtEmail.Text = "";
        //txtDesignation.Text = "";
        ddlEmployee.SelectedItem.Text = "(Select)";
        //txtCity.Text = "";
        //txtContactPerson.Text = "";
        //txtmobile.Text = "";
        //txtPinCode.Text = "";
        //txtLandline.Text = "";
        //chkAlertNewDealer.Checked = false;
        //chkExistingDealermodification.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["RefUrl"].ToString());
    }
}