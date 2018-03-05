using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace COMP___1640.DAL
{
    public class DataAccess
    {
        public static SqlConnection Connect()
        {
            //return new SqlConnection(WebConfigurationManager.AppSettings["ConnectionString"]);
            return new SqlConnection("Data Source=.;Initial Catalog=IdeasCampaignManager;Integrated Security=False;User Id=sa;Password=abc@12345;MultipleActiveResultSets=True");
        }

        public PersonalDetails CheckLogIn(string email, string pass)
        {
            var query = string.Format("SELECT * FROM PersonalDetail WHERE p_Email = '{0}' AND p_Pass = '{1}'", email, pass);
            try
            {
                var conn = Connect();
                conn.Open();
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    return new PersonalDetails
                    {
                        Id = int.Parse(reader["p_ID"].ToString()),
                        roleId = int.Parse(reader["r_ID"].ToString()),
                        departmentId = int.Parse(reader["dp_ID"].ToString()),
                        Name = reader["p_Name"].ToString(),
                        Email = reader["p_Email"].ToString(),
                        Pass = reader["p_Pass"].ToString(),
                        Details = reader["p_Detail"].ToString()
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PersonalDetails GetUserById(int id)
        {
            var query = string.Format("SELECT * FROM PersonalDetail WHERE p_ID = {0}", id);
            try
            {
                var conn = Connect();
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new PersonalDetails
                        {
                            Id = int.Parse(reader["p_ID"].ToString()),
                            roleId = int.Parse(reader["r_ID"].ToString()),
                            departmentId = int.Parse(reader["dp_ID"].ToString()),
                            Name = reader["p_Name"].ToString(),
                            Email = reader["p_Email"].ToString(),
                            Pass = reader["p_Pass"].ToString(),
                            Details = reader["p_Detail"].ToString()
                        };
                    }
                }

                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int AddIdea(Idea idea)
        {
            var id = -1;
            var query = string.Format(@"INSERT INTO Idea (c_ID, p_ID, i_Title, i_Details, DocumentLink, i_IsAnonymous, TotalViews, i_PostedDate, i_ClosureDate) 
output INSERTED.i_ID 
VALUES ({0}, {1}, '{2}', '{3}', '{4}', {5}, {6}, '{7}', '{8}')",
idea.CategoryId, idea.PersonalId, idea.Title, idea.Details, idea.DocumentLink, idea.isAnonymous, idea.TotalViews, idea.PostedDate, idea.ClosureDate);

            try
            {
                var conn = Connect();
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                id = (int)cmd.ExecuteScalar();

                conn.Close();
                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }


        public List<Category> GetAllCategory()
        {
            var query = string.Format("SELECT * FROM Category");
            var lstCat = new List<Category>();
            try
            {
                var conn = Connect();
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var cat = new Category
                        {
                            Id = int.Parse(reader["c_ID"].ToString()),
                            Name = reader["c_Name"].ToString(),
                            Description = reader["c_Description"].ToString()
                        };
                        lstCat.Add(cat);
                    }
                }
                else
                {
                    return null;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return lstCat;
        }

        public Category GetCategoryById(int id)
        {
            var query = string.Format("SELECT * FROM Category WHERE c_ID = {0}", id);
            try
            {
                var conn = Connect();
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return new Category
                        {
                            Id = int.Parse(reader["c_ID"].ToString()),
                            Name = reader["c_Name"].ToString(),
                            Description = reader["c_Description"].ToString()
                        };
                    }
                }

                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Idea GetIdeaById(int id)
        {
            var idea = new Idea();
            var query = string.Format("SELECT * FROM Idea WHERE i_ID = {0}", id);

            try
            {
                var conn = Connect();
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    idea.Id = id;
                    idea.CategoryId = int.Parse(reader["c_ID"].ToString());
                    idea.PersonalId = int.Parse(reader["p_ID"].ToString());
                    idea.Title = reader["i_Title"].ToString();
                    idea.Details = reader["i_Details"].ToString();
                    idea.DocumentLink = reader["DocumentLink"].ToString();
                    idea.isAnonymous = bool.Parse(reader["i_IsAnonymous"].ToString()) ? 1 : 0;
                    idea.TotalViews = int.Parse(reader["TotalViews"].ToString());
                    idea.PostedDate = Convert.ToDateTime(String.IsNullOrEmpty(reader["i_PostedDate"].ToString()) ? "" : reader["i_PostedDate"].ToString());
                    idea.ClosureDate = Convert.ToDateTime(String.IsNullOrEmpty(reader["i_ClosureDate"].ToString()) ? "" : reader["i_ClosureDate"].ToString());
                }
                return idea;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Idea> GetAllIdeas()
        {
            var lstIdea = new List<Idea>();
            var query = string.Format("SELECT * FROM Idea");

            try
            {
                var conn = Connect();
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var idea = new Idea();
                        idea.Id = int.Parse(reader["i_ID"].ToString());
                        idea.CategoryId = int.Parse(reader["c_ID"].ToString());
                        idea.PersonalId = int.Parse(reader["p_ID"].ToString());
                        idea.Title = reader["i_Title"].ToString();
                        idea.Details = reader["i_Details"].ToString();
                        idea.DocumentLink = reader["DocumentLink"].ToString();
                        idea.isAnonymous = bool.Parse(reader["i_IsAnonymous"].ToString()) ? 1 : 0;
                        idea.TotalViews = int.Parse(reader["TotalViews"].ToString());
                        idea.PostedDate = Convert.ToDateTime(String.IsNullOrEmpty(reader["i_PostedDate"].ToString()) ? "" : reader["i_PostedDate"].ToString());
                        idea.ClosureDate = Convert.ToDateTime(String.IsNullOrEmpty(reader["i_ClosureDate"].ToString()) ? "" : reader["i_ClosureDate"].ToString());

                        lstIdea.Add(idea);
                    }
                }
                else
                {
                    return null;
                }

                conn.Close();
                return lstIdea;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}