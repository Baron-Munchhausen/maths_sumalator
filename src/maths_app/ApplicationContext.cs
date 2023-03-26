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
		public DbSet<User_statistic> User_statistics { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User_statistic>((us =>
			{
				us.HasNoKey();
				us.ToView("User_statistics");
			}));
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite("Data Source=math.db");
		}
	}
}
