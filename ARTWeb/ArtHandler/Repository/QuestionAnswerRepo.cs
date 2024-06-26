using ArtHandler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Repository
{
    public class QuestionAnswerRepo
    {
        public List<QuestionsCategoryModel> GetQuestions(string userId)
        {
            try
            {
                List<QuestionModel> lstQuestions = new List<QuestionModel>();
                List<QuestionModel> lstRegisteredQuestions = new List<QuestionModel>();
                List<CategoryModel> lstCategory = new List<CategoryModel>();
                List<QuestionsCategoryModel> lstQuestionModel = new List<QuestionsCategoryModel>();

                DAL_QuestionAnswer objDALQuestionAns = new DAL_QuestionAnswer();
                lstCategory = objDALQuestionAns.GetQuestionCategory(userId);

                lstQuestions = objDALQuestionAns.GetQuestions(userId: userId);
                lstRegisteredQuestions = objDALQuestionAns.GetUserRegisteredQuestions(userId: userId);

                foreach (var CategoryModel in lstCategory)
                {
                    QuestionsCategoryModel objQuestionModel = new QuestionsCategoryModel();
                    List<QuestionModel> lstFiltered = lstQuestions.Where(a => a.categoryid == CategoryModel.categoryid).ToList();
                    objQuestionModel.categoryname = CategoryModel.categoryname;

                    foreach (var item in lstFiltered)
                    {
                        var registerQuestion = lstRegisteredQuestions.Where(b => b.question_id == item.question_id && b.categoryid == item.categoryid).ToList();
                        if (registerQuestion.Count > 0)
                            item.isSelected = true;
                        else
                            item.isSelected = false;
                    }

                    objQuestionModel.lstQuestions = lstFiltered;
                    lstQuestionModel.Add(objQuestionModel);
                }

                return lstQuestionModel;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }

        public bool InsertQuestionAnswer(string userId, List<QuestionAnswerModel> lstQuestionAns)
        {
            try
            {
                if (lstQuestionAns != null)
                {
                    DAL_QuestionAnswer objDALQuestionAns = new DAL_QuestionAnswer();

                    bool isDeleted = objDALQuestionAns.DeleteQuestionAnswer(userId, DateTime.Now);

                    foreach (var item in lstQuestionAns)
                    {
                        objDALQuestionAns.InsertQuestionAnswer(userId, item.question_id, item.answer, item.isDeleted, item.createdDate, item.deletedDate);
                    }

                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }
        public List<QuestionAnswerModel> GetQuestionsForUserToAnswer(string userId, int questionCnt)
        {
            try
            {
                DAL_QuestionAnswer objDALQuestionAns = new DAL_QuestionAnswer();

                return objDALQuestionAns.GetQuestionsForUserToAnswer(userId, questionCnt);
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return null;
            }
        }
        public bool CheckUserQuestionAnswer(string userId, List<QuestionAnswerModel> lstQuestionAnswer)
        {
            try
            {
                DAL_QuestionAnswer objDALQuestionAns = new DAL_QuestionAnswer();
                bool isUserAnsValid = true;
                foreach (var item in lstQuestionAnswer)
                {
                    bool result = objDALQuestionAns.CheckUserQuestionAnswer(userId, item.question_id, item.answer);

                    if (!result)
                    {
                        isUserAnsValid = false;
                        break;
                    }
                }

                return isUserAnsValid;
            }
            catch (Exception ex)
            {
                Log.LogException(new CustomException(userId.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name));
                return false;
            }
        }

        public bool CheckUserRegistered(string userId)
        {
            try
            {
                DAL_QuestionAnswer objDALQuestionAns = new DAL_QuestionAnswer();

                List<QuestionAnswerModel> lstQueAns = objDALQuestionAns.GetQuestionsForUserToAnswer(userId, questionCnt: 1); // check atleast 1 question is registered

                if (lstQueAns.Count > 0)
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
    }
}
