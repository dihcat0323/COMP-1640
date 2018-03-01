using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP___1640
{
    public partial class Layout : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //update nav bar if session has personalDetails
            if (Session["Login"] != null)
            {
                var p = (PersonalDetails)Session["Login"];
                lbtnUser.Text = p.Name;
                lbtnUser.Attributes.Add("href", "Home.aspx");
            }
            else
            {
                lbtnUser.Text = "Login";
                lbtnUser.Attributes.Add("href", "Login.aspx");
            }
        }
    }
}