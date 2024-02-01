using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
	public class MyContext : DbContext
	{
		
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=demobai7;Integrated Security=True;Trust Server Certificate=True");
		}
		public MyContext()
		{
		}

		public MyContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<DongVat> DongVats { get; set;}
	}
}
