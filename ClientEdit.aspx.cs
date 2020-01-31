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

public partial class ClientEdit : System.Web.UI.Page
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
            txtClientName.Text = dr["ClientName"].ToString().Trim();
            txtAddress1.Text = dr["Address1"].ToString().Trim();
            txtAddress2.Text = dr["Address2"].ToString().Trim();
            txtCity.Text = dr["city"].ToString().Trim();
            //txtPinCode.Text = dr["pincode"].ToString().Trim();
            txtState.Text = dr["State"].ToString().Trim();
            txtPostalCode.Text = dr["PostalCode"].ToString().Trim();
            txtCountry.Text = dr["Country"].ToString().Trim();
            txtGSTNo.Text = dr["GSTNo"].ToString().Trim();

        }
        conn.Close();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ClientList.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //bool email = false;
        //try
        //{
        //    MailMessage msg = new MailMessage();
        //    msg.To.Add(txtEmail.Text);
        //    email = true;
        //}
        //catch
        //{
        //    email = false;
        //}
        //if (email == true)
        //{
            SqlCommand fmdfinde = new SqlCommand("Select count(*) from ClientMaster where upper(ltrim(rtrim(ClientName)))=@ClientName and ltrim(rtrim(ClientID))!='"+ViewState["cid"]+"'", conn);
            fmdfinde.Parameters.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar, txtClientName.Text.Trim().Length)).Value = txtClientName.Text.Trim().ToUpper();

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            int chkd = (int)fmdfinde.ExecuteScalar();
            conn.Close();
            if (chkd > 0)
            {
                lblMessage.Text = "Mentioned Company Name already exist";
            }
            else
            {
                if (txtClientName.Text.Length > 0)
                {
                        string ClientID = System.Guid.NewGuid().ToString().ToUpper().Trim();
                        SqlCommand cmd = new SqlCommand("Update [dbo].[ClientMaster] set ClientName=@ClientName, Address1=@Address1, Address2=@Address2, City=@City, State=@State, PostalCode=@PostalCode, Country=@Country, GSTNo=@GSTNo where clientID='"+ViewState["cid"]+"'", conn);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
           cmd.Parameters.Add(new SqlParameter("@ClientName", SqlDbType.NVarChar, txtClientName.Text.Trim().Length)).Value = txtClientName.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Address1", SqlDbType.NVarChar, txtAddress1.Text.Trim().Length)).Value = txtAddress1.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Address2", SqlDbType.NVarChar, txtAddress2.Text.Trim().Length)).Value = txtAddress2.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, txtCity.Text.Trim().Length)).Value = txtCity.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, txtState.Text.Trim().Length)).Value = txtState.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@PostalCode", SqlDbType.NVarChar, txtPostalCode.Text.Trim().Length)).Value = txtPostalCode.Text.Trim();
                        //cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 8)).Value = EmployeeID.Substring(0,8);
                        //cmd.Parameters.Add(new SqlParameter("@Contact", SqlDbType.NVarChar, txtContactPerson.Text.Trim().Length)).Value = txtContactPerson.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar, txtCountry.Text.Trim().Length)).Value = txtCountry.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@GSTNo", SqlDbType.NVarChar, txtGSTNo.Text.Trim().Length)).Value = txtGSTNo.Text.Trim();
                    
                    cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    
                    SqlCommand cmdCF = new SqlCommand("Select count(*) from ClientServiceMaster where ClientID=@ClientID and EmployeeID=@EmployeeID", conn);
                    cmdCF.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
                    cmdCF.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar, Session["UserID"].ToString().Trim().Length)).Value = Session["UserID"].ToString().Trim();
                    int cexist = (int)cmdCF.ExecuteScalar();
                    if(cexist<=0)
                    {
                        SqlCommand cmdR = new SqlCommand("Insert into ClientServiceMaster (ClientID, EmployeeID) Values(@ClientID, @EmployeeID)", conn);
                        cmdR.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
                        cmdR.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar, Session["UserID"].ToString().Trim().Length)).Value = Session["UserID"].ToString().Trim();
                        cmdR.ExecuteNonQuery();
                        conn.Close();
                        lblMessage.Text = "Client added sucessfully";
                        //EmployeeEmail(EmployeeID.Substring(0, 8));
                        refreshField();
                    }
                    //else
                    //{
                    //    lblMessage.Text = "Dealer already Existing";
                    //    //refreshField();
                    //}
                }
                else
                {
                    lblMessage.Text = "Client Name should not be Empty";
                }

        }
        //}
        //else
        //{
        //    lblMessage.Text = "Please enter valid email address";
        //}
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
        txtClientName.Text = "";
        txtAddress1.Text = "";
        txtAddress2.Text = "";
        txtCity.Text = "";
        txtState.Text = "";
        txtCountry.Text = "";
        txtPostalCode.Text = "";
        txtGSTNo.Text = "";
        //chkAlertNewDealer.Checked = false;
        //chkExistingDealermodification.Checked = false;
    }
}