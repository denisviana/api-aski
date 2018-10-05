using api_aski.DB;
using api_aski.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_aski.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : ApiController
    {

        private Context _dbContext = Context.getInstance();


        [HttpPost]
        [Route("api/questions")]
        public IHttpActionResult NewQuestion(Question question)
        {            

            question.Id = Guid.NewGuid().ToString();
            _dbContext.Questions.Add(question);
            _dbContext.SaveChanges();

            var whoAsks = _dbContext.Users.FirstOrDefault(u => u.Id.Equals(question.WhoAsks.Id));
            whoAsks.IAsked.Add(question);
            _dbContext.Users.Attach(whoAsks);
            _dbContext.Entry(whoAsks).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            var whoResponds = _dbContext.Users.FirstOrDefault(u => u.Id.Equals(question.WhoResponds.Id));
            whoResponds.IAsked.Add(question);
            _dbContext.Users.Attach(whoResponds);
            _dbContext.Entry(whoResponds).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(question);
        }

        [HttpGet]
        [Route("api/questions")]
        public IHttpActionResult GetAll()
        {

            var questions = _dbContext.Questions
                .Include("WhoAsks")
                .Include("WhoResponds")
                .ToList();           

            return Ok(questions);
        }


        [HttpDelete]
        [Route("api/questions/{id}")]
        public IHttpActionResult Delete(string id)
        {

            var item = _dbContext.Questions.FirstOrDefault(d => d.Id.Equals(id));

            if (item == null)
            {
                return NotFound();
            }

            _dbContext.Questions.Remove(item);
            _dbContext.SaveChanges();

            return Ok();

        }

        [HttpPut]
        [Route("api/questions/{id}")]
        public IHttpActionResult Update(Question quetion, string id)
        {

            var item = _dbContext.Questions.FirstOrDefault(d => d.Id.Equals(id));

            if (item == null)
            {
                return NotFound();
            }

            item.Id = quetion.Id;
            item.Content = quetion.Content;
            item.WhoAsks = quetion.WhoAsks;
            item.WhoResponds = quetion.WhoResponds;

            _dbContext.Questions.Attach(item);
            _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok();

        }

    }
}
