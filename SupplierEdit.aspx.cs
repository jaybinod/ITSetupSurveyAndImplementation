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

public partial class SupplierEdit : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

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
        SqlCommand cmd = new SqlCommand("Select * from SupplierMaster where SupplierID='" + ViewState["cid"]+"'", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txtSupplierName.Text = dr["SupplierName"].ToString().Trim();
            txtAddress.Text = dr["Address"].ToString().Trim();
            
            txtCity.Text = dr["city"].ToString().Trim();
            //txtPinCode.Text = dr["pincode"].ToString().Trim();
            txtContact.Text = dr["ContactPerson"].ToString().Trim();
            txtmobile.Text = dr["Mobile"].ToString().Trim();
            txtEmail.Text = dr["EmailID"].ToString().Trim();
        }
        conn.Close();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SupplierList.aspx");
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
            SqlCommand fmdfinde = new SqlCommand("Select count(*) from SupplierMaster where upper(ltrim(rtrim(SupplierName)))=@SupplierName and ltrim(rtrim(SupplierID))!='"+ViewState["cid"]+"'", conn);
            fmdfinde.Parameters.Add(new SqlParameter("@SupplierName", SqlDbType.NVarChar, txtSupplierName.Text.Trim().Length)).Value = txtSupplierName.Text.Trim().ToUpper();

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
                if (txtSupplierName.Text.Length > 0)
                {
                        string ClientID = System.Guid.NewGuid().ToString().ToUpper().Trim();
                        SqlCommand cmd = new SqlCommand("Update [dbo].[SupplierMaster] set SupplierName=@SupplierName, Address=@Address, City=@City, ContactPerson=@ContactPerson, EmailID=@Email, Mobile=@Mobile where SupplierID='" + ViewState["cid"] + "'", conn);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        //cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
                        cmd.Parameters.Add(new SqlParameter("@SupplierName", SqlDbType.NVarChar, txtSupplierName.Text.Trim().Length)).Value = txtSupplierName.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, txtAddress.Text.Trim().Length)).Value = txtAddress.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, txtCity.Text.Trim().Length)).Value = txtCity.Text.Trim();

                        cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar, txtContact.Text.Trim().Length)).Value = txtContact.Text.Trim();
                        //cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 8)).Value = EmployeeID.Substring(0,8);
                        //cmd.Parameters.Add(new SqlParameter("@Contact", SqlDbType.NVarChar, txtContactPerson.Text.Trim().Length)).Value = txtContactPerson.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.NVarChar, txtmobile.Text.Trim().Length)).Value = txtmobile.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, txtEmail.Text.Trim().Length)).Value = txtEmail.Text.Trim();
                        //cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit)).Value = true;    
                        //cmd.Parameters.Add(new SqlParameter("@Landline", SqlDbType.NVarChar, txtLandline.Text.Trim().Length)).Value = txtLandline.Text.Trim();

                        //cmd.Parameters.Add(new SqlParameter("@NewDealerAdded", SqlDbType.Bit)).Value = chkAlertNewDealer.Checked;
                        //cmd.Parameters.Add(new SqlParameter("@ExistingDealerModification", SqlDbType.Bit)).Value = chkExistingDealermodification.Checked;
                        //string SupplierID = cmd.ExecuteScalar();
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        Response.Redirect("Supplierlist.aspx");   
                    //SqlCommand cmdCF = new SqlCommand("Select count(*) from ClientServiceMaster where ClientID=@ClientID and EmployeeID=@EmployeeID", conn);
                    //cmdCF.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
                    //cmdCF.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar, Session["UserID"].ToString().Trim().Length)).Value = Session["UserID"].ToString().Trim();
                    //int cexist = (int)cmdCF.ExecuteScalar();
                    //if(cexist<=0)
                    //{
                    //    SqlCommand cmdR = new SqlCommand("Insert into ClientServiceMaster (ClientID, EmployeeID) Values(@ClientID, @EmployeeID)", conn);
                    //    cmdR.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
                    //    cmdR.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar, Session["UserID"].ToString().Trim().Length)).Value = Session["UserID"].ToString().Trim();
                    //    cmdR.ExecuteNonQuery();
                    //    conn.Close();
                    //    lblMessage.Text = "Client added sucessfully";
                    //    //EmployeeEmail(EmployeeID.Substring(0, 8));
                    //    refreshField();
                    //}
                    //else
                    //{
                    //    lblMessage.Text = "Dealer already Existing";
                    //    //refreshField();
                    //}
                }
                else
                {
                    lblMessage.Text = "Supplier Name should not be Empty";
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
        txtSupplierName.Text = "";
        txtEmail.Text = "";
        txtCity.Text = "";
        //ddlDepartment.SelectedItem.Text = "(Select)";
        //txtCity.Text = "";
        txtContact.Text = "";
        txtmobile.Text = "";
        txtEmail.Text = "";
        //txtLandline.Text = "";
        //chkAlertNewDealer.Checked = false;
        //chkExistingDealermodification.Checked = false;
    }
}