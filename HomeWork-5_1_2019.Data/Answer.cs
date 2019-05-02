using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    public class Answer
    {
        public int id { get; set; }
        public int Personid { get; set; }
        public string answer { get; set; }
        public List<AnswerLikes> AnswerLikes { get; set; }
    }
}
