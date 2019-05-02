using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    public class QuestionLikes
    {
        public int Questionid { get; set; }
        public int Userid { get; set; }

        public User User { get; set; }
        public Question Question { get; set; }
    }
}
