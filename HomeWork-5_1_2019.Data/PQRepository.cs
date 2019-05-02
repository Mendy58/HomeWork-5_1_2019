using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    class PQRepository
    {
        private string _connectionstring;

        public PQRepository(string _ConnnectionString)
        {
            _connectionstring = _ConnnectionString;
        }
        
        public IEnumerable<Question> GetUserQuestion(int id)
        {
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                return PQ.Questions.Where(q => q.Userid == id).ToList();
            }
        }
        public IEnumerable<Question> GetQuestions()
        {
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                return PQ.Questions.OrderByDescending(q => q.DatePosted).ToList();
            }
        }
        public Question GetQuestionWithAnswers(int id)
        {
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                Question q = PQ.Questions.Include(Q => Q.Answers).FirstOrDefault(Q => Q.Id==id);
                q.Answers = new List<Answer>();
                return q;
            }
        }
        public IEnumerable<Question> GetQuestionsByTag(int tagid)
        {
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                return PQ.QuestionsTags.Where(q => q.TagId == tagid).Select(q => q.Question).ToList();
            }
        }
        public void AddQuestion(Question q)
        {
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                PQ.Questions.Add(q);
                PQ.SaveChanges();
            }
        }
        public void AddAnswer(Answer a)
        {
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                PQ.Answers.Add(a);
                PQ.SaveChanges();
            }
        }
        public void AddUser(User user)
        {
            string PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            user.PasswordHash = PasswordHash;
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                PQ.User.Add(user);
                PQ.SaveChanges();
            }
        }
        public User AuthUser(string email, string Password)
        {
            string PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password);
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                return PQ.User.FirstOrDefault(U => U.email.ToLower() == email.ToLower() && U.PasswordHash == PasswordHash);
            }
        }
    }
}
