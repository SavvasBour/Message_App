using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	public class SignUpScreen : BaseScreen
	{
		public SignUpScreen() : base()
		{
			ScreenName = "SIGN UP";
		}
		public override void Execute()
		{
			base.Execute();

			User user;
			do
			{
				Console.WriteLine("Username:");
				string username = Console.ReadLine();
				
				using (var userRepo = new UserRepository())
				{
					user = userRepo.GetByUsername(username);
				}

				if (user != null)
				{
					PrintError("Username is already in use!");
				}
				else
					user = new User() { Username = username };
			}
			while (user.Id != 0);

			PrintSeparator();
			Console.WriteLine("Password:");
			string password = Console.ReadLine();
			user.Password = password;
			user.CreatedBy = user.Username;
			using (var userRepo = new UserRepository())
				userRepo.Add(user);

			Session.LoginUser(user.Username, user.Password);

			PrintSuccess($"The user {user.Username} was created successfully");
			Console.ReadLine();

			new MainMenu();			
		}
	}
}
