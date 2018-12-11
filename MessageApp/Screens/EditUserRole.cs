using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	class EditUserRole : BaseScreen
	{
		private List<User> _users { get; set; }

		public EditUserRole() : base()
		{
			ScreenName = "EDIT ANY USER'S ROLE";
		}

		public override void Execute()
		{
			using (var userRepo = new UserRepository())
			{
				_users = userRepo.GetAll();
			}
			_users = _users.FindAll(u => u.RoleValue != User.Roles.Admin);

			int userNo = -1;
			do
			{
				base.Execute();

				PrintInfoMessage($"Total Users: ({_users.Count})");

				PrintUsers();
				PrintSeparator();

				do
				{
					Console.WriteLine("Choose a User to change his role.");
					string input = Console.ReadLine();
					Int32.TryParse(input, out userNo);
				}
				while (userNo < 0 || userNo > _users.Count);

				if (userNo != 0)
				{
					var selectedUser = _users[userNo - 1];

					string[] allRoles = Enum.GetNames(typeof(User.Roles)).Where(r => r != User.Roles.Admin.ToString()).ToArray();

					for (int i = 0; i < allRoles.Length; i++)
						Console.WriteLine(allRoles[i]);

					string selectedRole;
					do
					{
						Console.WriteLine("Type the role you want to assign to the user from the roles above.");

						selectedRole = Console.ReadLine();
					}
					while (!allRoles.Contains(selectedRole));

					selectedUser.Role = selectedRole;
					selectedUser.ModifiedBy = Session.Username;

					using (var userRepo = new UserRepository())
						userRepo.Update(selectedUser);

					PrintSuccess($"{selectedUser.Username}'s role was update successfully!");

					Console.ReadLine();
				}
			}
			while (userNo != 0);

			new MainMenu();
		}
		private void PrintUsers()
		{
			Console.WriteLine("0: Back to main menu");
			for (int i = 0; i < _users.Count; i++)
			{
				var user = _users[i];
				Console.WriteLine($"{i + 1}: {user.Username}, {{ Role: {user.Role} }}");
			}
		}
	}
}
