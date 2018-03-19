using COMP___1640.DAL;
using COMP___1640.Models;
using System;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP___1640
{
    public partial class WebForm3 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null || Session["TopicId"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            var lstCat = new DataAccess().GetAllCategory();
            if (lstCat != null && lstCat.Count > 0)
            {
                foreach (var x in lstCat)
                {
                    var item = new ListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    };
                    ddlCatgeory.Items.Add(item);
                }
            }
        }

        protected void btnAddIdea_Click(object sender, EventArgs e)
        {
            //validation
            var title = txtTitle.Text;
            var content = txtContent.Text;
            //var tags = txtTag.Text;
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                var script = "alert(\"ERROR: Title and Content are required!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }

            if (!ckbAgreeTerms.Checked)
            {
                var script = "alert(\"Sorry, you must accept the Terms & Conditions before submitting.\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }


            var p = (PersonalDetails)Session["Login"];
            var tpId = int.Parse(Session["TopicId"].ToString());
            //Only add new Idea if not yet pass the Closure Date
            if (!CheckClosureDate(tpId))
            {
                var script = "alert(\"ERROR: You cannot submit idea when the Closure Date already passed!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }

            //populate idea to add to DB
            var idea = new Idea
            {
                CategoryId = int.Parse(ddlCatgeory.SelectedItem.Value),
                topicId = tpId,
                PersonalId = p.Id,
                Title = title.Replace("'", "\""),
                Details = content.Replace("'", "\""),
                DocumentLink = "", //TODO:
                isAnonymous = ckbAnonymous.Checked ? 1 : 0,
                TotalViews = 0,
                PostedDate = DateTime.Today
            };

            var id = new DataAccess().AddIdea(idea);

            if (id > 0)
            {
                var script = "alert(\"SUCCESS: Idea submitted successfully!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                Session["IdeaId"] = id;

                SendEmailToStaff(title, content);

                Response.Redirect("Post.aspx");
            }
            else
            {
                var script = "alert(\"FAILURE: Idea submission failed!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }
        }

        private bool CheckClosureDate(int topicId)
        {
            var tp = new DataAccess().GetTopicById(topicId);
            if (tp == null)
            {
                return false;
            }

            return new Common().CalculateDateRange(tp.ClosureDate) <= 0;
        }

        private void SendEmailToStaff(string title, string content)
        {
            //check role if student - no need
            var user = (PersonalDetails)Session["Login"];
            //var role = new DataAccess().GetRoleById(user.roleId);

            //if (role.Name.ToLower().Contains("student"))
            //{
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("minhduongnhat1996@gmail.com", "1102Nomatkhauhehe");

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add("minhduongnhat1996@gmail.com");
            mailMessage.From = new MailAddress("minhduongnhat1996@gmail.com");
            mailMessage.Subject = "[NEW IDEA SUBMITTED]";
            mailMessage.Body = MailBody(title, content, user.Email);
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mailMessage.IsBodyHtml = true;

            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                return;
            }
            //}
        }

        private string MailBody(string title, string content, string mail)
        {

            var body = "";
            body += string.Format("<p><h3>Title: </h3>{0}</p>", title);
            body += string.Format("<p><h3>Content: </h3>{0}</p>", content);
            body += string.Format("<p><h3>User Email: </h3>{0}</p>", mail);

            return body;
        }
    }
}
