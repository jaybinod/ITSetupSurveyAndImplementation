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

public partial class ClientAddContact : System.Web.UI.Page
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
                if ((bool)Session["Validate"] == true)
                {
                    ViewState["cid"] = Request.QueryString["id"].ToString();
                    CallData();
                    getContact();
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    //protected void Department()
    //{
    //    SqlDataAdapter adpt = new SqlDataAdapter("Select * from Departmentmaster order by department", conn);
    //    //if (conn.State == ConnectionState.Closed)
    //    //    conn.Open();
    //    DataTable dt = new DataTable();
    //    adpt.Fill(dt);
    //    conn.Close();
    //    ddlDepartment.DataSource = dt;
    //    ddlDepartment.DataBind();
    //}
    protected void CallData()
    {
        SqlCommand cmd = new SqlCommand("Select * from ClientMaster where ClientID='" + ViewState["cid"]+"'", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            lblClient.Text = dr["ClientName"].ToString().Trim();
            //txtRole.Text = dr["Address"].ToString().Trim();
            
            ////txtCity.Text = dr["city"].ToString().Trim();
            ////txtPinCode.Text = dr["pincode"].ToString().Trim();
            //txtContact.Text = dr["ContactPerson"].ToString().Trim();
            //txtmobile.Text = dr["Mobile"].ToString().Trim();
            //txtEmail.Text = dr["Email"].ToString().Trim();
            

        }
        conn.Close();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClientList.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool email = false;
        try
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(txtEmail.Text);
            email = true;
        }
        catch
        {
            email = false;
        }
        if (email == true)
        {
            SqlCommand fmdfinde = new SqlCommand("Select count(*) from tbl_OrgKeyPeople where upper(ltrim(rtrim(PeopleName)))=@PeopleName and ltrim(rtrim(ClientID))!='"+ViewState["cid"]+"'", conn);
            fmdfinde.Parameters.Add(new SqlParameter("@PeopleName", SqlDbType.NVarChar, txtContact.Text.Trim().Length)).Value = txtContact.Text.Trim().ToUpper();

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            int chkd = (int)fmdfinde.ExecuteScalar();
            conn.Close();
            if (chkd > 0)
            {
                lblMessage.Text = "Mentioned Contact Name already exist";
            }
            else
            {
                if (txtContact.Text.Length > 0)
                {
                    SqlCommand cmdInsertOrgPeople = new SqlCommand("insert into tbl_OrgKeyPeople (ClientId,PeopleName,PeopleDesignation,PeopleContactNo,PeopleEmail, PeopleRole) values (@ClientId,@PeopleName,@PeopleDesignation,@PeopleContactNo,@PeopleEmail, @PeopleRole)", conn);
                    cmdInsertOrgPeople.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar)).Value = ViewState["cid"];
                    cmdInsertOrgPeople.Parameters.Add(new SqlParameter("@PeopleName", SqlDbType.NVarChar)).Value = txtContact.Text.Trim();
                    cmdInsertOrgPeople.Parameters.Add(new SqlParameter("@PeopleDesignation", SqlDbType.NVarChar)).Value = txtDesignation.Text.Trim();
                    cmdInsertOrgPeople.Parameters.Add(new SqlParameter("@PeopleContactNo", SqlDbType.NVarChar)).Value = txtmobile.Text.Trim();
                    cmdInsertOrgPeople.Parameters.Add(new SqlParameter("@PeopleEmail", SqlDbType.NVarChar)).Value = txtEmail.Text.Trim();
                    cmdInsertOrgPeople.Parameters.Add(new SqlParameter("@PeopleRole", SqlDbType.NVarChar)).Value = txtRole.Text.Trim();
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    cmdInsertOrgPeople.ExecuteNonQuery();
                    conn.Close();
                    getContact();
                }
                else
                {
                    lblMessage.Text = "Contact Name should not be Empty";
                }

            }
        }
        else
        {
            lblMessage.Text = "Please enter valid email address";
        }
        //Response.Redirect("dashboard.aspx");
    }

    //protected void EmployeeEmail(string pwd)
    //{
    //    string mailbody = "";

    //    MailMessage msg = new MailMessage();
    //    msg.BodyEncoding = System.Text.ASCIIEncoding.ASCII;
    //    msg.IsBodyHtml = true;
    //    msg.Priority = MailPriority.Normal;
    //    //msg.To.Add("binod@mosindia.co.in");
    //    msg.To.Add(txtEmail.Text.Trim());
        
    //    msg.Bcc.Add("jaybinod@gmail.com");
    //    //msg.Bcc.Add("amangoenka@gmail.com");
    //    msg.From = new MailAddress("noreply@infrasol.com");
    //    mailbody = "<html>";
    //    mailbody = mailbody + "<body>";
    //    mailbody = mailbody + "Dear "+txtEmployeeName.Text.Trim()+",<br><br>Your Details are as under with login ID and password<br><br>";
    //    mailbody = mailbody + "Designation : " + txtDesignation.Text.Trim();
    //    mailbody = mailbody + "<br>Deprtment : " + ddlDepartment.SelectedItem.Text.Trim();
    //    //mailbody = mailbody + "<br>Address : " + txtAddress.Text.Trim();
    //    //mailbody = mailbody + "<br>City : " + txtCity.Text.Trim();
    //    //mailbody = mailbody + "<br>State : " + txtState.Text.Trim();
    //    //mailbody = mailbody + "<br>Postal Code : " + txtPinCode.Text.Trim();
    //    mailbody = mailbody + "<br>Mobile : " + txtmobile.Text.Trim();
    //    mailbody = mailbody + "<br><b>Email/User ID : " + txtEmail.Text.Trim();
    //    mailbody = mailbody + "<br>Password : " + pwd;
        
    //    mailbody = mailbody + "</b></body></html>";
    //    msg.Subject = "Login Details";
    //    msg.Body = mailbody;

    //    //Smaple 2
    //    //System.Net.NetworkCredential authenticaionInfo = new System.Net.NetworkCredential("binod@mosindia.co.in", "regency");
    //    //SmtpClient SmtpMail = new SmtpClient("smtpout.secureserver.net");
    //    //SmtpMail.UseDefaultCredentials = false;
    //    //SmtpMail.Credentials = authenticaionInfo;


    //    //------------------------------------------------
    //    //Sample 1
    //    // SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net");
    //    SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net");
    //    //SmtpClient SmtpMail = new SmtpClient("smtp.socketlabs.com");
    //    //SmtpMail.UseDefaultCredentials = true;
    //    //NetworkCredential NC = new NetworkCredential("jaybinod", "CVtbXIu5RdS0");
    //    //SmtpMail.Credentials = NC;
    //    //--------------------------------------

    //    //SmtpMail.Send(msg);
    //    lblMessage.Text = "Registration Confirmation sended to Employee Email Address";
    //    //RefreshData();

    //}

    protected void refreshField()
    {
        txtDesignation.Text = "";
        //txtEmail.Text = "";
        txtRole.Text = "";
        //ddlDepartment.SelectedItem.Text = "(Select)";
        //txtCity.Text = "";
        txtContact.Text = "";
        txtmobile.Text = "";
        txtEmail.Text = "";
        //txtLandline.Text = "";
        //chkAlertNewDealer.Checked = false;
        //chkExistingDealermodification.Checked = false;
    }

    protected void getContact()
    {
        //con = DB.DynamicConnection();
        SqlCommand cmd = new SqlCommand("Select * from tbl_OrgKeyPeople OKP where ClientID='" + ViewState["cid"]+"'", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        gvOrgContact.DataSource = cmd.ExecuteReader();
        gvOrgContact.DataBind();
        conn.Close();
    }
    protected void gvOrgContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvOrgContact.EditIndex = -1;
        getContact();
    }
    protected void gvOrgContact_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvOrgContact.EditIndex = e.NewEditIndex;
        getContact();
    }
    protected void gvOrgContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = (GridViewRow)gvOrgContact.Rows[e.RowIndex];

        int autoid = Int32.Parse(gvOrgContact.DataKeys[e.RowIndex].Value.ToString());
        TextBox tPName = (TextBox)row.FindControl("txtKPname");
        TextBox tPDesignation = (TextBox)row.FindControl("txtKPDesignation");
        TextBox tPContact = (TextBox)row.FindControl("txtKPContact");
        TextBox tPEmail = (TextBox)row.FindControl("txtKPEmail");
        TextBox tPRole = (TextBox)row.FindControl("txtKPRole");
        //con = DB.DynamicConnection();
        SqlCommand cmd = new SqlCommand("Update tbl_OrgKeyPeople set PeopleName=@PeopleName, PeopleDesignation=@PeopleDesignation, PeopleContactNo=@PeopleContactNo, PeopleEmail=@PeopleEmail, PeopleRole=@PeopleRole where PeopleID=" + autoid, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();

        cmd.Parameters.Add("@PeopleName", SqlDbType.NVarChar).Value = tPName.Text.Trim();
        cmd.Parameters.Add("@PeopleDesignation", SqlDbType.NVarChar).Value = tPDesignation.Text.Trim();
        cmd.Parameters.Add("@PeopleContactNo", SqlDbType.NVarChar).Value = tPContact.Text.Trim();
        cmd.Parameters.Add("@PeopleEmail", SqlDbType.NVarChar).Value = tPEmail.Text.Trim();
        cmd.Parameters.Add("@PeopleRole", SqlDbType.NVarChar).Value = tPRole.Text.Trim();

        cmd.ExecuteNonQuery();

        gvOrgContact.EditIndex = -1;
        getContact();
    }
    protected void gvOrgContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = (GridViewRow)gvOrgContact.Rows[e.RowIndex];

        int autoid = Int32.Parse(gvOrgContact.DataKeys[e.RowIndex].Value.ToString());
        //con = DB.DynamicConnection();
        SqlCommand cmd = new SqlCommand("Delete from tbl_OrgKeyPeople where PeopleID=" + autoid, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        getContact();
    }
}