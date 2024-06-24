using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtHandler.Model
{
    public class QuestionAnswerModel
    {
        public string userid { get; set; }
        public int question_id { get; set; }
        public string question_text { get; set; }
        public string answer { get; set; }
        public bool isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime deletedDate { get; set; }
    }
}
