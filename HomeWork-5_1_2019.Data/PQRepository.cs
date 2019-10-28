using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    public class PQRepository
    {
        private string _connectionstring;

        public PQRepository(string _ConnnectionString)
        {
            _connectionstring = _ConnnectionString;
        }
        
        public IEnumerable<Question> GetUserQuestions(int id)
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
                return PQ.QuestionsTags.Where(q => q.TagId == tagid).Select(q => q.Question).Include(a => a.Answers).ToList();
            }
        }
        public void AddQuestion(Question q, IEnumerable<string> tags)
        {
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                PQ.Questions.Add(q);
                foreach (string tag in tags)
                {
                    Tag t = GetTag(tag);
                    int tagId;
                    if (t == null)
                    {
                        tagId = AddTag(tag);
                    }
                    else
                    {
                        tagId = t.Id;
                    }
                    PQ.QuestionsTags.Add(new QuestionsTags
                    {
                        QuestionId = q.Id,
                        TagId = tagId
                    });
                }
                PQ.SaveChanges();
            }
        }
        private Tag GetTag(string name)
        {
            using (var ctx = new PeopleQuestionsContext(_connectionstring))
            {
                return ctx.Tags.FirstOrDefault(t => t.Name == name);
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
        public int GetUseridByEmail(string email)
        {
            using (PeopleQuestionsContext PQ = new PeopleQuestionsContext(_connectionstring))
            {
                return PQ.User.FirstOrDefault(u => u.email == email).id;
            }
        }
        public void AddUser(User user)
        {
            string HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash, SaltRevision.Revision2A);
            user.PasswordHash = HashedPassword;
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
                User s = PQ.User.FirstOrDefault(U => U.email.ToLower() == email.ToLower());
                if(BCrypt.Net.BCrypt.Verify(Password, s.PasswordHash))
                {
                    return s;
                }
                else
                {
                    return null;
                }
            }
        }
        private int AddTag(string name)
        {
            using (var ctx = new PeopleQuestionsContext(_connectionstring))
            {
                var tag = new Tag { Name = name };
                ctx.Tags.Add(tag);
                ctx.SaveChanges();
                return tag.Id;
            }
        }
    }
}
