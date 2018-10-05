using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api_aski.Model
{
    public class Question
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public int Points { get; set; }
        public Discipline Discipline { get; set; }
        public User WhoAsks { get; set; }
        public User WhoResponds { get; set; }
    }
}