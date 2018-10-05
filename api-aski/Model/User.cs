using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_aski.Model
{
    [Table("User")]
    public class User
    {

        public User()
        {
            this.HasDomainIn = new List<Discipline>();
            this.HasDificultyIn = new List<Discipline>();
        }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool WantBeHelped { get; set; }
        public bool WantToHelp { get; set; }
        public ICollection<Discipline> HasDomainIn { get; set; }
        public ICollection<Discipline> HasDificultyIn { get; set; }
        public ICollection<Question> IAsked { get; set; }
        public ICollection<Question> AskedMe { get; set; }

    }
}