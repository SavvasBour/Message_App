using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	class LogoutScreen: BaseScreen
	{
		public LogoutScreen()
		{
			ScreenName = "LOGOUT";
		}

		public override void Execute()
		{
			Console.Clear();
			PrintSuccess("You logged out succesfully!");

			Console.ReadLine();

			Session.LogoutUser();
		}
	}
}
