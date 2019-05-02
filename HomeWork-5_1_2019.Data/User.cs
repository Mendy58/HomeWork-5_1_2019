using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    public class User
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string PasswordHash { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }

        public List<QuestionLikes> QuestionLikes { get; set; }
        public List<AnswerLikes> AnswerLikes { get; set; }
    }
}
