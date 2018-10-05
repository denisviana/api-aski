using api_aski.DB;
using api_aski.Model;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api_aski.Controllers
{
    [Route("api/[controller]")]
    public class DisciplinesController : ApiController
    {
        private Context _dbContext = Context.getInstance();


        //GET all disciplines
        [HttpGet]
        [Route("api/disciplines/")]
        public IHttpActionResult GetDisciplines()
        {
            var disciplines = _dbContext.Disciplines
                .ToList();

            return Ok(disciplines);
        }



        //GET discipline by id
        [HttpGet]
        [Route("api/disciplines/{id}")]
        public IHttpActionResult GetDisciplineById(string id)
        {

            var discipline = _dbContext.Disciplines
                .FirstOrDefault(d => d.Id.Equals(id));

            if (discipline == null)
            {
                return NotFound();
            }

            return Ok(discipline);
        }


        [HttpPost]
        [Route("api/disciplines/create")]
        public HttpResponseMessage Create([FromBody] Discipline discipline)
        {


            if (_dbContext.Disciplines.FirstOrDefault(d => d.Name.Equals(discipline.Name, StringComparison.CurrentCultureIgnoreCase)) != null)
                return Request.CreateResponse(HttpStatusCode.Conflict);

            discipline.Id = Guid.NewGuid().ToString();
            _dbContext.Disciplines.Add(discipline);
            _dbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created,discipline);
        }


        //PUT update a discipline
        [HttpPut]
        [Route("api/disciplines/update/{id}")]
        public IHttpActionResult UpdateDiscipline(string id, [FromBody]Discipline discipline)
        {

            var item = _dbContext.Disciplines.FirstOrDefault(d => d.Id.Equals(id));

            if (item == null)
            {
               return NotFound();
            }

            item.Id = discipline.Id;
            item.Name = discipline.Name;
            item.UsersOwners = discipline.UsersOwners;
            item.DependentUsers = discipline.DependentUsers;

            _dbContext.Disciplines.Attach(item);
            _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();

           return Ok();
        }


        //DELETE discipline
        [HttpDelete]
        [Route("api/disciplines/delete/{id}")]
        public IHttpActionResult DeleteDiscipline(string id){

            var item = _dbContext.Disciplines.FirstOrDefault(d => d.Id.Equals(id));

            if (item == null)
            {
                return NotFound();
            }

            _dbContext.Disciplines.Remove(item);
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}