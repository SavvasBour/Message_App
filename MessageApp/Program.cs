using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
    class Program
    {
        static void Main(string[] args)
        {
			using (var userRepo = new UserRepository())
			{
				if (userRepo.GetByUsername("admin") == null)
					userRepo.Add(new User() { Username = "admin", Password = "admin", CreatedBy = "admin", RoleValue = User.Roles.Admin });
			}

			new WelcomeMenu();
		}

    }
}
