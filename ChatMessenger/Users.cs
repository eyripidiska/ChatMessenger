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
            Console.WriteLine("MAIN MENU - Please choose an option.");
            Console.WriteLine("====================================");
            Console.WriteLine("x. Log Off");
            Console.WriteLine("a. Chat");
        }
    }



    public class User : Users
    {
        public User(string NewUsernamame, int newId)
        {
            _Id = newId;
            _Name = NewUsernamame;
            _TypeOfUser = UserType.User;
            application.Add("x", LoginScreen.LoginMethod);
            application.Add("a", ApplicationsMenus.ChatMenuMethod);
        }
    }



    public class ViewAdmin : User
    {
        public ViewAdmin(string NewUsernamame, int newId) : base(NewUsernamame, newId)
        {
            _TypeOfUser = UserType.ViewAdmin;
            application.Add("b", MainApplication.ViewAllMessageMethod);
        }

        public override void PublicMenuMethod()
        {
            Console.WriteLine("MAIN MENU - Please choose an option.");
            Console.WriteLine("====================================");
            Console.WriteLine("x. Log Off");
            Console.WriteLine("a. Chat");
            Console.WriteLine("b. View Messages");
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
            Console.WriteLine("MAIN MENU - Please choose an option.");
            Console.WriteLine("====================================");
            Console.WriteLine("x. Log Off");
            Console.WriteLine("a. Chat");
            Console.WriteLine("b. View Messages");
            Console.WriteLine("c. Edit Messages");
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
            Console.WriteLine("MAIN MENU - Please choose an option.");
            Console.WriteLine("====================================");
            Console.WriteLine("x. Log Off");
            Console.WriteLine("a. Chat");
            Console.WriteLine("b. View Messages");
            Console.WriteLine("c. Edit Messages");
            Console.WriteLine("d. Delete Messages");
        }
    }



    public class SuperAdmin : ViewEditDeleteAdmin
    {

        public SuperAdmin(string NewUsernamame, int newId) : base(NewUsernamame, newId)
        {
            _TypeOfUser = UserType.SuperAdmin;
            application.Add("e", ApplicationsMenus.UserMenuMethod);
        }

        public override void PublicMenuMethod()
        {
            Console.WriteLine("MAIN MENU - Please choose an option.");
            Console.WriteLine("====================================");
            Console.WriteLine("x. Log Off");
            Console.WriteLine("a. Chat");
            Console.WriteLine("b. View Messages");
            Console.WriteLine("c. Edit Messages");
            Console.WriteLine("d. Delete Messages");
            Console.WriteLine("e. Edit Users");
        }
    }


}
