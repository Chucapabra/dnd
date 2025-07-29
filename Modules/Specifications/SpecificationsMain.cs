using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNDHelper.Modules.Specifications
{
	internal class SpecificationsMain
	{

	}

    internal class Characteristics
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    internal class TestGrid
    {
        public IEnumerable<Characteristics> Characteristics { get; set; }
    }
}
