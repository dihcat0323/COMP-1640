using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace COMP___1640.DAL
{
    public class DataAccess
    {
        public static SqlConnection Connect()
        {
            //return new SqlConnection(WebConfigurationManager.AppSettings["ConnectionString"]);
            return new SqlConnection("Data Source=.;Initial Catalog=IdeasCampaignManager;Integrated Security=False;User Id=sa;Password=abc@12345;MultipleActiveResultSets=True");
        }
        #region PersonalDetail
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

                conn.Close();
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
        #endregion

        #region Idea
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
                conn.Close();
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
        #endregion

        #region Category
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
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    return null;
                }
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
        #endregion

        #region Role
        public Role GetRoleById(int id)
        {
            var query = string.Format("SELECT * FROM Role WHERE r_ID = {0}", id);
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
                        return new Role
                        {
                            Id = int.Parse(reader["r_ID"].ToString()),
                            Name = reader["r_Name"].ToString(),
                            Description = reader["r_Description"].ToString()
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
        #endregion

        #region Comment
        public bool AddComment(Comment cmt)
        {
            var stt = false;
            var query = string.Format("INSERT INTO Comment (I_ID, p_ID, cmt_Detail, cmt_IsAnonymous, cmt_PostedDate) VALUES ({0}, {1}, '{2}', '{3}', '{4}')",
                cmt.ideaId, cmt.personId, cmt.Details, cmt.isAnonymous.ToString().ToLower(), cmt.postedDate);

            try
            {
                var conn = Connect();
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                stt = cmd.ExecuteNonQuery() == 1;

                conn.Close();
                return stt;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Comment> GetCommentsByIdea(int ideaId)
        {
            var lstCmt = new List<Comment>();
            var query = string.Format("SELECT * FROM Comment WHERE I_ID = {0}", ideaId);

            try
            {
                var conn = Connect();
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var cmt = new Comment();
                    cmt.Id = int.Parse(reader["cmt_ID"].ToString());
                    cmt.ideaId = int.Parse(reader["I_ID"].ToString());
                    cmt.personId = int.Parse(reader["p_ID"].ToString());
                    cmt.Details = reader["cmt_Detail"].ToString();
                    cmt.isAnonymous = bool.Parse(reader["cmt_IsAnonymous"].ToString());
                    cmt.postedDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["cmt_PostedDate"].ToString()) ? "" : reader["cmt_PostedDate"].ToString());

                    lstCmt.Add(cmt);
                }
                conn.Close();
                return lstCmt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}