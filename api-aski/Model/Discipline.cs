using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace api_aski.Model
{
    [Table("Discipline")]
    public class Discipline
    {

        public Discipline()
        {
            this.UsersOwners = new List<User>();
            this.DependentUsers = new List<User>();
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public Course Course { get; set; }
        public ICollection<User>  UsersOwners { get; set; }
        public ICollection<User> DependentUsers { get; set; }
    }
}