using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	public class MainMenu : BaseMenu
	{
		public static string screenName = "<<<<<<<<<<<<<<<<<<<<<<< MAIN MENU >>>>>>>>>>>>>>>>>>>>>>>>";
		public static List<string> commands = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
		public static List<BaseScreen> commandsActions = new List<BaseScreen>() { new SendMessageScreen(), new ReadMessageScreen(),
			new ReadlAllMessagesScreen(), new EditAnyMessage(), new DeleteAnyMessage(),new CreateUser(), new EditUserRole(),new DeleteUser(), new LogoutScreen(), new ExitScreen() };

		public MainMenu() : base(screenName, commands, commandsActions)
		{
			if (Session.UserRole != Models.User.Roles.Admin && Session.UserRole != Models.User.Roles.SuperUser1
				&& Session.UserRole != Models.User.Roles.SuperUser2 && Session.UserRole != Models.User.Roles.SuperUser3)
			{
				int indx = Commands.IndexOf("3");
				Commands.RemoveAt(indx);
				CommandsActions.RemoveAt(indx);
			}
			if (Session.UserRole != Models.User.Roles.Admin && Session.UserRole != Models.User.Roles.SuperUser2 && Session.UserRole != Models.User.Roles.SuperUser3)
			{
				int indx = Commands.IndexOf("4");
				Commands.RemoveAt(indx);
				CommandsActions.RemoveAt(indx);
			}
			if (Session.UserRole != Models.User.Roles.Admin && Session.UserRole != Models.User.Roles.SuperUser3)
			{
				int indx = Commands.IndexOf("5");
				Commands.RemoveAt(indx);
				CommandsActions.RemoveAt(indx);
			}
			if (Session.UserRole != Models.User.Roles.Admin)
			{
				int indx = Commands.IndexOf("6");
				Commands.RemoveAt(indx);
				CommandsActions.RemoveAt(indx);

				indx = Commands.IndexOf("7");
				Commands.RemoveAt(indx);
				CommandsActions.RemoveAt(indx);

				indx = Commands.IndexOf("8");
				Commands.RemoveAt(indx);
				CommandsActions.RemoveAt(indx);
			}

			PrintCommands();

			CheckCommand(Console.ReadLine());
		}
	}
}
