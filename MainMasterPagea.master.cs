using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class MainMasterPagea : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Session["Admin"] = null;
         
        //Control cntrl = HeadLoginView.FindControl("HeadLoginName");
        ////LinkButton lnkHeadLoginStatus = HeadLoginView.FindControl("lnkHeadLoginStatus") as  LinkButton;
        //HyperLink hyprHeadLoginStatus = HeadLoginView.FindControl("hyprHeadLoginStatus") as HyperLink;
        //HtmlAnchor MyAnchor = HeadLoginView.FindControl("MyAnchor") as HtmlAnchor;
        ////
        //if (Session["UserFullName"] != null)
        //{
        //    //Control view2 = HeadLoginView.LoggedInTemplate.InstantiateIn(view1);
        //   // messageLabel2.Text = Convert.ToString(Session["UserFullName"]);
        //    hyprHeadLoginStatus.Text = "Log Out";
        //    MyAnchor.InnerText = Convert.ToString(Session["UserFullName"]);

        //}
        //else
        //{
        //    MyAnchor.InnerText = "";
        //    hyprHeadLoginStatus.Text = "Log In";

        //}
    }
}
