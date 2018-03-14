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
            return new SqlConnection("Data Source=.;Initial Catalog=IdeasCampaignManager_ver2;Integrated Security=False;User Id=sa;Password=abc@12345;MultipleActiveResultSets=True");
        }
        #region PersonalDetail
        public PersonalDetails CheckLogIn(string email, string pass)
        {
            var query = string.Format("SELECT * FROM PersonalDetail WHERE p_Email = '{0}' AND p_Pass = '{1}'", email, pass);
            var conn = Connect();
            try
            {
                conn.Open();
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                var u = new PersonalDetails();

                while (reader.Read())
                {
                    u.Id = int.Parse(reader["p_ID"].ToString());
                    u.roleId = int.Parse(reader["r_ID"].ToString());
                    u.departmentId = int.Parse(reader["dp_ID"].ToString());
                    u.Name = reader["p_Name"].ToString();
                    u.Email = reader["p_Email"].ToString();
                    u.Pass = reader["p_Pass"].ToString();
                    u.Details = reader["p_Detail"].ToString();
                }

                conn.Close();
                return u;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }

        public PersonalDetails GetUserById(int id)
        {
            var query = string.Format("SELECT * FROM PersonalDetail WHERE p_ID = {0}", id);
            var conn = Connect();
            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var u = new PersonalDetails();

                    while (reader.Read())
                    {
                        u.Id = int.Parse(reader["p_ID"].ToString());
                        u.roleId = int.Parse(reader["r_ID"].ToString());
                        u.departmentId = int.Parse(reader["dp_ID"].ToString());
                        u.Name = reader["p_Name"].ToString();
                        u.Email = reader["p_Email"].ToString();
                        u.Pass = reader["p_Pass"].ToString();
                        u.Details = reader["p_Detail"].ToString();
                    }

                    conn.Close();
                    return u;
                }

                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }
        #endregion

        #region Topic
        public int AddTopic(Topic tp)
        {
            //Check logic Closure Date and Final Closure Date
            if ((DateTime.Parse(tp.ClosureDate.ToString()) - DateTime.Parse(tp.FinalClosureDate.ToString())).TotalDays > 0)
            {
                return -1;
            }

            if ((DateTime.Parse(tp.PostedDate.ToString()) - DateTime.Parse(tp.ClosureDate.ToString())).TotalDays > 0)
            {
                return -1;
            }

            var id = -1;
            var query = string.Format(@"INSERT INTO Topic (t_Name, t_Details, t_PostedDate, t_ClosureDate, t_FinalClosureDate)
output INSERTED.t_ID
VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
tp.Name, tp.Details, tp.PostedDate, tp.ClosureDate, tp.FinalClosureDate);

            var conn = Connect();

            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                id = (int)cmd.ExecuteScalar();

                conn.Close();
                return id;
            }
            catch (Exception ex)
            {
                conn.Close();
                return -1;
            }
        }

        public Topic GetTopicById(int id)
        {
            var tp = new Topic();
            var query = string.Format("SELECT * FROM Topic WHERE t_ID = {0}", id);

            var conn = Connect();

            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tp.Id = id;
                    tp.Name = reader["t_Name"].ToString();
                    tp.Details = reader["t_Details"].ToString();
                    tp.PostedDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["t_PostedDate"].ToString()) ? "" : reader["t_PostedDate"].ToString());
                    tp.ClosureDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["t_ClosureDate"].ToString()) ? "" : reader["t_ClosureDate"].ToString());
                    tp.FinalClosureDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["t_FinalClosureDate"].ToString()) ? "" : reader["t_FinalClosureDate"].ToString());
                }

                conn.Close();
                return tp;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }

        public List<Topic> GetAllTopics()
        {
            var lstTp = new List<Topic>();
            var query = string.Format("SELECT * FROM Topic");

            var conn = Connect();

            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var tp = new Topic();
                    tp.Id = int.Parse(reader["t_ID"].ToString());
                    tp.Name = reader["t_Name"].ToString();
                    tp.Details = reader["t_Details"].ToString();
                    tp.PostedDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["t_PostedDate"].ToString()) ? "" : reader["t_PostedDate"].ToString());
                    tp.ClosureDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["t_ClosureDate"].ToString()) ? "" : reader["t_ClosureDate"].ToString());
                    tp.FinalClosureDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["t_FinalClosureDate"].ToString()) ? "" : reader["t_FinalClosureDate"].ToString());

                    lstTp.Add(tp);
                }

                conn.Close();
                return lstTp;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }
        #endregion

        #region Idea
        public int AddIdea(Idea idea)
        {
            var id = -1;
            var query = string.Format(@"INSERT INTO Idea (c_ID, p_ID, i_Title, i_Details, DocumentLink, i_IsAnonymous, TotalViews, i_PostedDate) 
output INSERTED.i_ID 
VALUES ({0}, {1}, '{2}', '{3}', '{4}', {5}, {6}, '{7}')",
idea.CategoryId, idea.PersonalId, idea.Title, idea.Details, idea.DocumentLink, idea.isAnonymous, idea.TotalViews, idea.PostedDate);

            var conn = Connect();

            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                id = (int)cmd.ExecuteScalar();

                conn.Close();
                return id;
            }
            catch (Exception ex)
            {
                conn.Close();
                return -1;
            }
        }

        public Idea GetIdeaById(int id)
        {
            var idea = new Idea();
            var query = string.Format("SELECT * FROM Idea WHERE i_ID = {0}", id);

            var conn = Connect();

            try
            {
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
                    idea.PostedDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["i_PostedDate"].ToString()) ? "" : reader["i_PostedDate"].ToString());
                    //idea.ClosureDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["i_ClosureDate"].ToString()) ? "" : reader["i_ClosureDate"].ToString());
                }
                conn.Close();
                return idea;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }

        public List<Idea> GetIdeasByTopic(int tpId)
        {
            var lstIdea = new List<Idea>();
            var query = string.Format("SELECT * FROM Idea WHERE t_ID = {0}", tpId);

            var conn = Connect();

            try
            {
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
                        idea.PostedDate = Convert.ToDateTime(string.IsNullOrEmpty(reader["i_PostedDate"].ToString()) ? "" : reader["i_PostedDate"].ToString());

                        lstIdea.Add(idea);
                    }

                    conn.Close();
                    return lstIdea;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }
        #endregion

        #region Category
        public List<Category> GetAllCategory()
        {
            var query = string.Format("SELECT * FROM Category");
            var lstCat = new List<Category>();

            var conn = Connect();

            try
            {
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
                    return lstCat;
                }
                else
                {
                    conn.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }

        public Category GetCategoryById(int id)
        {
            var c = new Category();
            var query = string.Format("SELECT * FROM Category WHERE c_ID = {0}", id);

            var conn = Connect();
            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c.Id = int.Parse(reader["c_ID"].ToString());
                        c.Name = reader["c_Name"].ToString();
                        c.Description = reader["c_Description"].ToString();
                    }

                    conn.Close();
                    return c;
                }

                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }

        public bool AddCategory(Category cat)
        {
            var query = string.Format("INSERT INTO Category(c_Name, c_Description) VALUES('{0}', '{1}')", cat.Name, cat.Description);

            var conn = Connect();
            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var stt = cmd.ExecuteNonQuery() == 1;

                conn.Close();
                return stt;
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool UpdateCategory(Category cat)
        {
            var stt = false;
            var query = string.Format("UPDATE Category SET c_Name = '{0}', c_Description = '{1}' WHERE c_ID = {2}", cat.Name, cat.Description, cat.Id);

            var conn = Connect();

            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                stt = cmd.ExecuteNonQuery() == 1;

                conn.Close();
                return stt;
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool DeleteCategory(int id)
        {
            var stt = false;
            var query = string.Format("DELETE Category WHERE c_ID = {0}", id);

            var conn = Connect();

            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                stt = cmd.ExecuteNonQuery() == 1;

                conn.Close();
                return stt;
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }
        #endregion

        #region Role
        public Role GetRoleById(int id)
        {
            var r = new Role();
            var query = string.Format("SELECT * FROM Role WHERE r_ID = {0}", id);

            var conn = Connect();
            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        r.Id = int.Parse(reader["r_ID"].ToString());
                        r.Name = reader["r_Name"].ToString();
                        r.Description = reader["r_Description"].ToString();
                    }
                    conn.Close();
                    return r;
                }

                conn.Close();
                return null;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }

        public List<Role> GetAllRoles()
        {
            var lstRoles = new List<Role>();
            var query = string.Format("SELECT * FROM Role");

            var conn = Connect();
            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var r = new Role();
                    r.Id = int.Parse(reader["r_ID"].ToString());
                    r.Name = reader["r_Name"].ToString();
                    r.Description = reader["r_Description"].ToString();

                    lstRoles.Add(r);
                }
                conn.Close();
                return lstRoles;
            }
            catch (Exception ex)
            {
                conn.Close();
                return null;
            }
        }

        public bool AddRole(Role role)
        {
            var query = string.Format("INSERT INTO Role(r_Name, r_Description) VALUES('{0}', '{1}')", role.Name, role.Description);

            var conn = Connect();
            try
            {
                conn.Open();

                var cmd = new SqlCommand(query, conn);
                var stt = cmd.ExecuteNonQuery() == 1;

                conn.Close();
                return stt;
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        public bool UpdateRole(Role r)
        {
            var stt = false;
            var query = string.Format("UPDATE Role SET r_Name = '{0}', r_Description = '{1}' WHERE r_ID = {2}", r.Name, r.Description, r.Id);

            var conn = Connect();

            try
            {
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

            var conn = Connect();
            try
            {
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
                conn.Close();
                return null;
            }
        }
        #endregion

    }
}