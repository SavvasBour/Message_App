using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	class DeleteUser : BaseScreen
	{
		private List<User> _users { get; set; }

		public DeleteUser() : base()
		{
			ScreenName = "DELETE ANY USER";
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
					Console.WriteLine("Choose a User to be deleted.");
					string input = Console.ReadLine();
					Int32.TryParse(input, out userNo);
				}
				while (userNo < 0 || userNo > _users.Count);

				if (userNo != 0)
				{
					var selectedUser = _users[userNo - 1];

					using (var messageRepo = new MessageRepository())
						messageRepo.DeleteBySenderOrReceiverId(selectedUser.Id);

					using (var userRepo = new UserRepository())
						userRepo.Delete(selectedUser.Id);

					int indx = _users.FindIndex(m => m.Id == selectedUser.Id);
					_users.RemoveAt(indx);

					PrintSuccess("The user was succesfully deleted!");

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
