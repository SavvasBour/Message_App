using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Models;
namespace MessageApp
{
    public class LoginScreen : BaseScreen
    {
        public LoginScreen() : base()
        {
			ScreenName = "LOGIN";
        }

        public override void Execute()
        {
			base.Execute();
						
			Console.WriteLine("Insert Username:");
			string username = Console.ReadLine();
			PrintSeparator();
			Console.WriteLine("Insert Password:");
			string password = Console.ReadLine();

			if (Session.LoginUser(username, password))
			{
				PrintSuccess($"User {Session.Username} was successfully logged in!!!");
				Console.ReadLine();

				new MainMenu();
			}
			else
			{
				PrintError("Login failed, username or password is incorrect.");

				Console.ReadKey();
				new WelcomeMenu();
			}
		}
    }
}
