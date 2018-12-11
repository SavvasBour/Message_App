using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	public static class Session
	{
		public static User User { get; private set; }
		public static string Username
		{
			get { return User?.Username; }
		}
		public static int? UserId
		{
			get { return User?.Id; }
		}
		public static User.Roles? UserRole
		{
			get
			{
				return User?.RoleValue;
			}
		}
		public static bool IsAuthenticated
		{
			get
			{
				return User != null && User.Id != 0;
			}
		}
		public static bool LoginUser(string username, string password)
		{
			if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
				return false;
			using(var userRepo = new UserRepository())
			{
				User user = userRepo.GetByUsername(username);
				if (user != null && user.Password == password) //LOGIN SUCCESS
					User = user;

				return IsAuthenticated;
			}
		}

		public static void LogoutUser()
		{
			User = null; 
			new WelcomeMenu();
		}
	}
}
