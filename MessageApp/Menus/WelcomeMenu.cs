using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	public class WelcomeMenu : BaseMenu
	{
		public static string screenName = "<<<<<<<<<<<<<<<<<<<<<<<<WELCOME>>>>>>>>>>>>>>>>>>>>>>>>>>>";
		public static List<string> commands = new List<string> { "1", "2", "3" };
		public static List<BaseScreen> commandsActions = new List<BaseScreen>() { new LoginScreen(), new SignUpScreen(), new ExitScreen() };

		public WelcomeMenu() : base(screenName, commands, commandsActions)
		{
			PrintCommands();

			CheckCommand(Console.ReadLine());
		}
	}
}
