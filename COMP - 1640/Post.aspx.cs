using COMP___1640.DAL;
using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP___1640
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                var script = "alert(\"ERROR: You must Login first!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                Response.Redirect("Login.aspx");
            }
            else
            {
                //load submitted idea to page
                if (Session["IdeaId"] != null)
                {
                    int id = -1;
                    int.TryParse(Session["IdeaId"].ToString(), out id);


                    if (id > 0)
                    {
                        //Load data to page and clear Session IdeaId
                        LoadIdeaById(id);

                        Session["IdeaId"] = null;
                    }
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        private void LoadIdeaById(int id)
        {
            //Get idea from DB
            var idea = new DataAccess().GetIdeaById(id);

            //Load idea to page
            if (idea != null && idea.Id > 0)
            {
                var user = (PersonalDetails)Session["Login"];
                lbtnUser.InnerHtml = user.Name;
                lblPostedDate.Text = "Posted " + new Common().CalculatePostedDate(idea.PostedDate) + " days ago";
                lblTitle.InnerHtml = idea.Title;
                lblContent.InnerHtml = idea.Details;

                //Category Name
                lblCategory.InnerHtml = new DataAccess().GetCategoryById(idea.CategoryId).Name;
                lblDocumentLink.InnerHtml = string.IsNullOrEmpty(idea.DocumentLink) ? "No Link" : idea.DocumentLink;
                lblAnonymous.InnerHtml = idea.isAnonymous == 0 ? "No" : "Yes";
                lblTotalView.InnerHtml = idea.TotalViews.ToString();
            }
        }
    }
}