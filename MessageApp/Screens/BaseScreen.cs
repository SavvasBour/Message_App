using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
    public class BaseScreen : BaseFunctions
    {		
		public string ScreenName { get; set; }

        public virtual void Execute()
        {
            Console.Clear();
            PrintSeparator();
            Console.WriteLine(ScreenName);            
            PrintSeparator();
        }
	}
}
