using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	public class ExitScreen : BaseScreen
	{
		public ExitScreen()
		{
			ScreenName = "EXIT";
		}

		public override void Execute()
		{
			Console.Clear();
			Console.WriteLine("Good bye!");

			Console.ReadLine();

			Environment.Exit(0);
		}
	}
}
