using api_aski.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace api_aski.DB
{
    public class Context : DbContext
    {

        private static Context instance;

        public Context() : base("AskiDB")
        {
        }

        public static Context getInstance()
        {
            if (instance == null)
                instance = new Context();

            return instance;
        }
       

        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
    }
}
