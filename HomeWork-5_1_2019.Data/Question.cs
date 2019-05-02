using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Userid { get; set; }
        public DateTime DatePosted { get; set; }
        public List<QuestionsTags> QuestionsTags { get; set; }
        public List<QuestionLikes> QuestionLikes { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
