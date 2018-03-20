using COMP___1640.DAL;
using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
            var db = new DataAccess();
            //Get idea from DB
            var idea = new DataAccess().GetIdeaById(id);

            //Load idea to page
            if (idea != null && idea.Id > 0)
            {
                var user = db.GetUserById(idea.PersonalId);
                lbtnUser.InnerHtml = user.Name;
                lblPostedDate.Text = "Posted " + new Common().CalculateDateRange(idea.PostedDate) + " days ago";
                lblTitle.InnerHtml = idea.Title;
                lblContent.InnerHtml = idea.Details;

                //Category Name
                lblCategory.InnerHtml = db.GetCategoryById(idea.CategoryId).Name;
                lbtnDocumentLink.Text = string.IsNullOrEmpty(idea.DocumentLink) ? "No Link" : idea.DocumentLink;
                lblAnonymous.InnerHtml = idea.isAnonymous == 0 ? "No" : "Yes";
                lblTotalView.InnerHtml = idea.TotalViews.ToString();
            }
        }

        private void LoadCmtByIdea(int ideaId)
        {
            //user must login in order to view the comments, otherwise all the comments will not be displayed
            if (Session["Login"] != null)
            {
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
            var db = new DataAccess();
            var lstCmtUi = new List<CommentUI>();
            //check role if "Student"
            var loggedInRole = db.GetRoleById(roleId);

            foreach (var x in lstCmt)
            {
                var cmtUi = new CommentUI();
                cmtUi.userName = x.isAnonymous ? "Anonymous" : db.GetUserById(x.personId).Name;
                cmtUi.postedDate = new Common().CalculateDateRange(x.postedDate).ToString();
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
            var db = new DataAccess();
            var user = db.GetUserById(personalId);
            var role = db.GetRoleById(user.roleId);

            return role.Name.ToLower().Contains("student");
        }

        protected void btnSubmitCmt_Click(object sender, EventArgs e)
        {
            var cmttxt = txtCmt.Text;

            if (string.IsNullOrEmpty(cmttxt))
            {
                Response.Write("<script>alert('ERROR: You cannot submit the comment without typing anything!!!');</script>");
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

            if (Session["TopicId"] == null)
            {
                Response.Redirect("Topic.aspx");
            }

            var tpId = -1;
            int.TryParse(Session["TopicId"].ToString(), out tpId);
            if (tpId == -1)
            {
                Response.Redirect("Topic.aspx");
            }

            if (CheckFinalClosureDate(tpId))
            {
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
            else
            {
                Response.Write("<script>alert('You cannot submit the comment if the Final Closure Date already passed!!!');</script>");
            }
        }

        public bool CheckFinalClosureDate(int topicId)
        {
            var tp = new DataAccess().GetTopicById(topicId);
            if (tp == null)
            {
                return false;
            }

            return new Common().CalculateDateRange(tp.FinalClosureDate) <= 0;
        }

        protected void lbtnDocumentLink_Click(object sender, EventArgs e)
        {
            if (!lbtnDocumentLink.Text.Equals("No Link"))
            {
                string filePath = lbtnDocumentLink.Text;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
                Response.WriteFile(filePath);
                Response.End();
            }
        }
    }
}