using ArtHandler.Model;
using ArtHandler.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ArtHandler
{
    public class DAL_QuestionAnswer
    {

        ResourceFileManager objRes;

        public DAL_QuestionAnswer()
        {
            objRes = new ResourceFileManager(System.Web.HttpContext.Current.Session[Constants.ARTUSERLANG].ToString());
        }

        public bool CheckUserQuestionAnswer(string userId, int questionId, string answer)
        {
            try
            {
                int result = 0;

                using (MySqlConnection con =  MySqlConnector.OpenConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(Constants.SP_USERQUESTIONANSWER, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("user_id", userId));
                        cmd.Parameters.Add(new MySqlParameter("questionid", questionId));
                        cmd.Parameters.Add(new MySqlParameter("uanswer", answer));
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

                if (result != 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public bool InsertQuestionAnswer(string userId, int questionId, string answer, bool isDeleted, DateTime createdDate, DateTime deletedDate)
        {
            try
            {

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(Constants.SP_INSERTUSERQUESTIONANSWER, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("userid", userId));
                        cmd.Parameters.Add(new MySqlParameter("questionid", questionId));
                        cmd.Parameters.Add(new MySqlParameter("answer", answer));
                        cmd.Parameters.Add(new MySqlParameter("isDeleted", isDeleted));
                        cmd.Parameters.Add(new MySqlParameter("createdDate", createdDate));
                        cmd.Parameters.Add(new MySqlParameter("deletedDate", deletedDate));
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public bool DeleteQuestionAnswer(string userId, DateTime deletedDate)
        {
            try
            {

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand(Constants.SP_DELETEUSERQUESTIONANSWER, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("user_id", userId));
                        cmd.Parameters.Add(new MySqlParameter("deleted_Date", deletedDate));
                        cmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public List<CategoryModel> GetQuestionCategory(string userId)
        {
            try
            {
                DataSet dsQuestions = new DataSet();
                List<CategoryModel> lstCategory = new List<CategoryModel>();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETQUESTIONCATEGORY, con);
                    adapter.Fill(dsQuestions);
                }


                foreach (DataRow item in dsQuestions.Tables[0].Rows)
                {
                    CategoryModel objCate = new CategoryModel();
                    objCate.categoryid = Convert.ToInt32(item["categoryid"]);
                    objCate.categoryname = item[objRes.GetColumnNameForCurrentCulture("categoryname")].ToString();
                    objCate.isactive = Convert.ToBoolean(item["isactive"]);

                    lstCategory.Add(objCate);
                }

                return lstCategory;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public List<QuestionModel> GetQuestions(string userId)
        {
            try
            {
                DataSet dsQuestions = new DataSet();
                List<QuestionModel> lstQuestions = new List<QuestionModel>();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETQUESTION, con);
                    adapter.Fill(dsQuestions);
                }


                foreach (DataRow item in dsQuestions.Tables[0].Rows)
                {
                    QuestionModel objQues = new QuestionModel();
                    objQues.question_id = Convert.ToInt32(item["question_id"]);
                    objQues.question_text = item[objRes.GetColumnNameForCurrentCulture("question_text")].ToString();
                    objQues.isEnabled = Convert.ToBoolean(item["isEnabled"]);
                    objQues.categoryid = Convert.ToInt32(item["categoryid"]);

                    lstQuestions.Add(objQues);
                }

                return lstQuestions;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public List<QuestionAnswerModel> GetQuestionsForUserToAnswer(string userId, int questionCnt)
        {
            try
            {
                DataSet dsQuestions = new DataSet();
                List<QuestionAnswerModel> lstQuestions = new List<QuestionAnswerModel>();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETQUESTIONFORUSERTOANSWER, con);

                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("questionCnt", questionCnt));
                    

                    adapter.Fill(dsQuestions);
                }

                foreach (DataRow item in dsQuestions.Tables[0].Rows)
                {
                    QuestionAnswerModel objQues = new QuestionAnswerModel();
                    objQues.question_id = Convert.ToInt32(item["question_id"]);
                    objQues.question_text = item[objRes.GetColumnNameForCurrentCulture(("question_text"))].ToString();
                    objQues.answer = item["answer"].ToString();

                    lstQuestions.Add(objQues);
                }

                return lstQuestions;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public List<QuestionModel> GetUserRegisteredQuestions(string userId)
        {
            try
            {
                DataSet dsLdap = new DataSet();

                using (MySqlConnection con = MySqlConnector.OpenConnection())
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = new MySqlCommand(Constants.SP_GETUSERREGISTEREDQUESTIONS, con);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.Add(new MySqlParameter("user_id", userId));

                    adapter.Fill(dsLdap);
                }

                List<QuestionModel> lstQuestion = (from rw in dsLdap.Tables[0].AsEnumerable()
                                                     select new QuestionModel()
                                                         {
                                                             question_id = Convert.ToInt32(rw["question_id"]),
                                                             categoryid = Convert.ToInt32(rw["categoryid"]),
                                                         }).ToList();


                return lstQuestion;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

    }
}
