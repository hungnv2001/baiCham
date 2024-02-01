using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData
{
	public class DongVat
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public double Weight {  get; set; }
		public double Height {  get; set; }
		public string Type { get; set; }
		public bool Gender { get; set; }

		public bool Status { get; set; }
	}
}
