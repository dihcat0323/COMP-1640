using COMP___1640.DAL;
using COMP___1640.Models;
using System;

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
                lbtnUser.Attributes.Add("href", "Topic.aspx");
            }
            else
            {
                lbtnUser.Text = "Login";
                lbtnUser.Attributes.Add("href", "Login.aspx");
            }
        }
    }
}