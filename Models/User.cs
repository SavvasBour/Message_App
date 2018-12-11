
namespace Models
{
    public class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role
        {
            get
            {
                switch (RoleValue)
                {
                    case Roles.Admin:
                        return "Admin";
                    case Roles.SuperUser1:
                        return "SuperUser1";
                    case Roles.SuperUser2:
                        return "SuperUser2";
                    case Roles.SuperUser3:
                        return "SuperUser3";
                    default:
                        return "User";
                }
            }
            set
            {
                switch (value)
                {
                    case "Admin":
                        RoleValue = Roles.Admin;
                        break;
                    case "SuperUser1":
                        RoleValue = Roles.SuperUser1;
                        break;
                    case "SuperUser2":
                        RoleValue = Roles.SuperUser2;
                        break;
                    case "SuperUser3":
                        RoleValue = Roles.SuperUser3;
                        break;
                    default:
                        RoleValue = Roles.User;
                        break;
                }
            }
        }

        public Roles RoleValue { get; set; } = Roles.User;
        
        public enum Roles
        {
            Admin,
            User,
            SuperUser1,
            SuperUser2,
            SuperUser3
        }
    }
}
