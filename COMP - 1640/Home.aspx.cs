using COMP___1640.DAL;
using System;
using System.Web.UI;

namespace COMP___1640
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindIdeasToGrid();
        }

        private void BindIdeasToGrid()
        {
            var lstIdeas = new DataAccess().GetAllIdeas();
            lstIdeas.Reverse();
            var list = "";

            if (lstIdeas != null && lstIdeas.Count > 0)
            {
                foreach (var x in lstIdeas)
                {
                    list += string.Format(@"
<div class='col-12'>
	<div class='well'>
		<p>
            <a href='#'>{0}</a> posted:
        </p>
		<h2>
            <a href='#' onclick='ideaOnclick(this)' ideaId='{4}'>{1}</a>
        </h2>
		<p>{2}</p>
		<span class='timestamp'>
			posted {3} days ago.
		</span>
	</div>
</div>
", new DataAccess().GetUserById(x.PersonalId).Name, x.Title, x.Details, new Common().CalculatePostedDate(x.PostedDate), x.Id);
                }
            }



            var html = string.Format(@"
<div class='container body-content col-12'>

	<div class='col-12'>
		<div class='form-group input-group'>
			<input type='text' name='search' id='search' placeholder='Search post' class='form-control' />
			<span class='input-group-btn'>
				<input type='submit' value='Search' class='btn btn-default' data-disable-with='Search' />
			</span>
		</div>
	</div>
	<p class='button'>
		<a href='AddIdea.aspx'>New Post</a>
	</p>


	<div class='center col-12'>
        {0}
	</div>
</div>
", list);

            Response.Write(html);
        }

        [System.Web.Services.WebMethod]
        public static object IdeaClicked(int ideaId)
        {
            Page obj = new Page();
            obj.Session["IdeaId"] = ideaId;

            return null;
        }
    }
}