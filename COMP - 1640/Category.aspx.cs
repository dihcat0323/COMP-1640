using COMP___1640.DAL;
using COMP___1640.Entity;
using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP___1640
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                //Check if account is Admin category
                var user = (PersonalDetails)Session["Login"];
                var r = new DataAccess().GetRoleById(user.roleId);

                if (r.Name.Contains("Manager") && r.Name.Contains("QA"))
                {

                }
                else
                {
                    Response.Redirect("Topic.aspx");
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static CategoryEntity LoadData(int currentPage, int pagesize)
        {
            var db = new DataAccess();
            var categoryEntity = new CategoryEntity();

            var lstCategories = db.GetAllCategory();
            lstCategories.Reverse();

            var lstCategoryUi = new List<Category>();
            foreach (var x in lstCategories)
            {
                var r = new Category();
                r.Id = x.Id;
                r.Name = x.Name;
                r.Description = x.Description.Replace("\"", "'").Replace(Environment.NewLine, " ");

                lstCategoryUi.Add(r);
            }
            categoryEntity.ListCategories = lstCategoryUi;

            return categoryEntity;
        }

        [System.Web.Services.WebMethod]
        public static object CategoryClicked(int categoryId)
        {
            var category = new DataAccess().GetCategoryById(categoryId);

            return category;
        }

        [System.Web.Services.WebMethod]
        public static object Client_AddCategory(string name, string description)
        {
            var category = new Category
            {
                Name = name,
                Description = description
            };

            var stt = new DataAccess().AddCategory(category);

            return stt;
        }

        [System.Web.Services.WebMethod]
        public static object Client_UpdateCategory(int id, string name, string description)
        {
            var category = new Category
            {
                Id = id,
                Name = name,
                Description = description
            };

            var stt = new DataAccess().UpdateCategory(category);

            return stt;
        }

        [System.Web.Services.WebMethod]
        public static object Client_DeleteCategory(int id)
        {
            //var cat = new DataAccess().GetCategoryById(id);
            //if (cat != null)
            //{
            //    return false;
            //}

            var stt = new DataAccess().DeleteCategory(id);

            return stt;
        }
    }
}