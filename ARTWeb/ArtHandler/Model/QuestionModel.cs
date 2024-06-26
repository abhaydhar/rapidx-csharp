using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Model
{
    public class QuestionModel
    {
        public int question_id { get; set; }
        public string question_text { get; set; }
        public bool isEnabled { get; set; }
        public int categoryid { get; set; }
        public bool isSelected { get; set; }
    }
    public class CategoryModel
    {
        public int categoryid { get; set; }
        public string categoryname { get; set; }
        public bool isactive { get; set; }
    }
    public class QuestionsCategoryModel
    {
        public string categoryname { get; set; }
        public List<QuestionModel> lstQuestions { get; set; }
    }
}
