using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace maths_app
{
	class ApplicationContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<User_answer> User_answers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=math.db");
		}
	}
}
