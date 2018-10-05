using api_aski.DB;
using api_aski.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace aski_api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ApiController
    {
        private Context _dbContext = Context.getInstance();


        [HttpGet]
        [Route("api/users/")]
        public IHttpActionResult GetAll()
        {

            var users = _dbContext.Users
                .ToList();

            return Ok(users);
        }

        [HttpGet]
        [Route("api/users/specialistsusers/{disciplineId}")]
        public IHttpActionResult UsersSpecialistsByDisciplineId(string disciplineId)
        {

            var disciplines = _dbContext.Disciplines
                .Include("UsersOwners")
                .FirstOrDefault(d => d.Id.Equals(disciplineId));


            return Ok(disciplines.UsersOwners);

        }

        [HttpGet]
        [Route("api/users/userbyemail/{email}")]
        public IHttpActionResult UsersByEmail(string email)
        {

            var user = _dbContext.Users
                .FirstOrDefault(u => u.Email.Equals(email));

            return Ok(user);
        }

        [HttpGet]
        [Route("api/users/byid/{id}")]
        public IHttpActionResult UserById(string id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id.Equals(id));

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("api/users/create")]
        public HttpResponseMessage CreateUser([FromBody] User user)
        {

            if (_dbContext.Users.FirstOrDefault(u => u.Email.Equals(user.Email, StringComparison.CurrentCultureIgnoreCase)) != null)
                return Request.CreateResponse(HttpStatusCode.Conflict);

            user.Id = Guid.NewGuid().ToString();

            var hasDificultyIn = new List<Discipline>();
            var hasDomainIn = new List<Discipline>();

            var newUser = new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                WantBeHelped = user.WantBeHelped,
                WantToHelp = user.WantToHelp,
            };

            _dbContext.Users.Add(newUser);

            _dbContext.SaveChanges();

            foreach (var item in user.HasDificultyIn)
            {
                var discipline = _dbContext.Disciplines.Include("DependentUsers").FirstOrDefault(d => d.Id.Equals(item.Id));
                discipline.DependentUsers.Add(newUser);
                _dbContext.Disciplines.Attach(discipline);
                _dbContext.Entry(discipline).State = System.Data.Entity.EntityState.Modified;
                hasDificultyIn.Add(discipline);
                _dbContext.SaveChanges();
            }

            foreach (var item in user.HasDomainIn)
            {
                var discipline = _dbContext.Disciplines.Include("UsersOwners").FirstOrDefault(d => d.Id.Equals(item.Id));
                discipline.UsersOwners.Add(newUser);
                _dbContext.Disciplines.Attach(discipline);
                _dbContext.Entry(discipline).State = System.Data.Entity.EntityState.Modified;
                hasDomainIn.Add(discipline);
                _dbContext.SaveChanges();
            }


            return Request.CreateResponse(HttpStatusCode.Created, newUser);
        }


    }
}
