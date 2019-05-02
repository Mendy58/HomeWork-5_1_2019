using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    public class AnswerLikes
    {
        public int Answerid { get; set; }
        public int Userid { get; set; }

        public User User { get; set; }
        public Answer Answer { get; set; }
    }
}
