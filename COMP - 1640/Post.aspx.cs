using COMP___1640.DAL;
using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace COMP___1640
{
    public partial class WebForm2 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdeaId"] != null)
            {
                int id = -1;
                int.TryParse(Session["IdeaId"].ToString(), out id);

                if (id > 0)
                {
                    //Load data to page and clear Session IdeaId
                    LoadIdeaById(id);
                    LoadCmtByIdea(id);
                }
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }

        private void LoadIdeaById(int id)
        {
            //Get idea from DB
            var idea = new DataAccess().GetIdeaById(id);

            //Load idea to page
            if (idea != null && idea.Id > 0)
            {
                var user = new DataAccess().GetUserById(idea.PersonalId);
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

        private void LoadCmtByIdea(int ideaId)
        {
            //user must login in order to view the comments, otherwise all the comments will not be displayed
            if (Session["Login"] != null)
            {
                //Response.Redirect("Login.aspx");

                var user = (PersonalDetails)Session["Login"];

                var cmt = new DataAccess().GetCommentsByIdea(ideaId);
                var cmtUi = PopulateComments(cmt, user.roleId);
                var jsonSerialiser = new JavaScriptSerializer();
                cmtUi.Reverse();
                var script = "bindComments('" + jsonSerialiser.Serialize(cmtUi) + "')";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }

        private List<CommentUI> PopulateComments(List<Comment> lstCmt, int roleId)
        {
            var lstCmtUi = new List<CommentUI>();
            //check role if "Student"
            var loggedInRole = new DataAccess().GetRoleById(roleId);

            foreach (var x in lstCmt)
            {
                var cmtUi = new CommentUI();
                cmtUi.userName = x.isAnonymous ? "Anonymous" : new DataAccess().GetUserById(x.personId).Name;
                cmtUi.postedDate = new Common().CalculatePostedDate(x.postedDate).ToString();
                cmtUi.Details = x.Details;

                //logic check for "Only student can view comments submitted by other students. Staff can view all type of comments"
                if (loggedInRole.Name.ToLower().Contains("student") && !IsCommentedByStudent(x.personId))
                {

                }
                else
                {
                    lstCmtUi.Add(cmtUi);
                }
            }

            return lstCmtUi;
        }

        //check if the comment is submitted by "student" role
        private bool IsCommentedByStudent(int personalId)
        {
            var user = new DataAccess().GetUserById(personalId);
            var role = new DataAccess().GetRoleById(user.roleId);

            return role.Name.ToLower().Contains("student");
        }

        protected void btnSubmitCmt_Click(object sender, EventArgs e)
        {
            var cmttxt = txtCmt.Text;

            if (string.IsNullOrEmpty(cmttxt))
            {
                var script = "alert(\"ERROR: You cannot submit the comment without typing anything!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }

            if (Session["IdeaId"] == null)
            {
                Response.Redirect("Home.aspx");
            }

            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            //add comment
            var ideaId = int.Parse(Session["IdeaId"].ToString());
            var user = (PersonalDetails)Session["Login"];
            var cmt = new Comment();
            cmt.ideaId = ideaId;
            cmt.isAnonymous = ckbAnonymous.Checked;
            cmt.postedDate = DateTime.Today;
            cmt.Details = cmttxt;
            cmt.personId = user.Id;

            var stt = new DataAccess().AddComment(cmt);

            //redirect to this page
            if (stt)
            {
                Response.Redirect("Post.aspx");
            }
        }
    }
}