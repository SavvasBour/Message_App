using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	class CreateUser : BaseScreen
	{
		public CreateUser() : base()
		{
			ScreenName = "CREATE A USER";
		}
		public override void Execute()
		{
			base.Execute();

			User user;
			do
			{
				Console.WriteLine("Insert the new user's Username:");
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
			Console.WriteLine("Insert the new user's Password:");
			string password = Console.ReadLine();
			user.Password = password;
			user.CreatedBy = Session.Username;
			PrintSeparator();
			Console.WriteLine("======================= Users Roles ======================");
			string[] allRoles = Enum.GetNames(typeof(User.Roles)).Where(r => r != User.Roles.Admin.ToString()).ToArray();

			for (int i = 0; i < allRoles.Length; i++)
				Console.WriteLine(allRoles[i]);
			PrintSeparator();
			string selectedRole;
			do
			{
				Console.WriteLine("Type the role you want to assign to the user from the roles above.");

				selectedRole = Console.ReadLine();
			}
			while (!allRoles.Contains(selectedRole));

			
			using (var userRepo = new UserRepository())
			{
				user.Role = selectedRole;
				userRepo.Add(user);
			}
				

			

			PrintSuccess($"The user {user.Username} was created successfully");
			Console.ReadLine();

			new MainMenu();
		}
	}
}

