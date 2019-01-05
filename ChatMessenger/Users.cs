using System;
using System.Collections.Generic;

namespace ChatMessenger
{
    public enum UserType
    {
        User,
        ViewAdmin,
        ViewEditAdmin,
        ViewEditDeleteAdmin,
        SuperAdmin
    }

    public delegate void Application();

    public abstract class Users
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
        public UserType _TypeOfUser { get; set; }

        public Dictionary<string, Application> application = new Dictionary<string, Application>();

        public virtual void PublicMenuMethod()
        {
            Console.WriteLine("To choose a user to send message press {a}");
        }
    }



    public class User : Users
    {
        public User(string NewUsernamame, int newId)
        {
            _Id = newId;
            _Name = NewUsernamame;
            _TypeOfUser = UserType.User;
            application.Add("a", ApplicationsMenus.MessageMenuMethod);
        }
    }



    public class ViewAdmin : User
    {
        public ViewAdmin(string NewUsernamame, int newId) : base(NewUsernamame, newId)
        {
            _TypeOfUser = UserType.ViewAdmin;
            application.Add("b", MainApplication.ViewMessageMethod);
        }

        public override void PublicMenuMethod()
        {
            Console.WriteLine("To choose a user to send message press {a}");
            Console.WriteLine("To View the transacted data between the users press {b}");
        }
    }



    public class ViewEditAdmin : ViewAdmin
    {
        public ViewEditAdmin(string NewUsernamame, int newId) : base(NewUsernamame, newId)
        {
            _TypeOfUser = UserType.ViewEditAdmin;
            application.Add("c", MainApplication.EditMessageMethod);
        }

        public override void PublicMenuMethod()
        {
            Console.WriteLine("To Choose a user to send message press {a}");
            Console.WriteLine("To View the transacted data between the users press {b}");
            Console.WriteLine("To View and Edit the transacted data between the users press {c}");
        }
    }



    public class ViewEditDeleteAdmin : ViewEditAdmin
    {

        public ViewEditDeleteAdmin(string NewUsernamame, int newId) : base(NewUsernamame, newId)
        {
            _TypeOfUser = UserType.ViewEditDeleteAdmin;
            application.Add("d", MainApplication.DeleteMessageMethod);
        }

        public override void PublicMenuMethod()
        {
            Console.WriteLine("To choose a user to send message press {a}");
            Console.WriteLine("To View the transacted data between the users press {b}");
            Console.WriteLine("To View and Edit the transacted data between the users press {c}");
            Console.WriteLine("To View, Edit and Delete the transacted data between the users press {d}");
        }
    }



    public class SuperAdmin : ViewEditAdmin
    {

        public SuperAdmin(string NewUsernamame, int newId) : base(NewUsernamame, newId)
        {
            _TypeOfUser = UserType.SuperAdmin;
            application.Add("e", MainApplication.CreateUserMethod);
            application.Add("f", MainApplication.ViewUserMethod);
            application.Add("g", MainApplication.DeleteUserMethod);
            application.Add("h", MainApplication.UpdatePasswordMethod);
            application.Add("i", MainApplication.UpdateRoleMethod);
        }

        public override void PublicMenuMethod()
        {
            Console.WriteLine("To choose a user to send message press {a}");
            Console.WriteLine("To View the transacted data between the users press {b}");
            Console.WriteLine("To View and Edit the transacted data between the users press {c}");
            Console.WriteLine("To View, Edit and Delete the transacted data between the users press {d}");
            Console.WriteLine("To create a user press {e}");
            Console.WriteLine("To view the users press {f}");
            Console.WriteLine("To delete a user press {g}");
            Console.WriteLine("To update a user password press {h}");
            Console.WriteLine("To update a user role press {i}");
        }
    }


}
