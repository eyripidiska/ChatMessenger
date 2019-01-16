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
        public int id { get; set; }

        public string username { get; set; }

        public string role { get; set; }

        public UserType type { get; set; }

        public bool deleted { get; set; }

        

        public Dictionary<string, Application> application = new Dictionary<string, Application>();

        public virtual void PublicMenu()
        {
            Console.WriteLine("MAIN MENU - Please choose an option.");
            Console.WriteLine("====================================");
            Console.WriteLine("x. Log Off");
            Console.WriteLine("a. Chat");
        }
    }



    public class User : Users
    {
        public User()
        {

        }

        public User(string NewUsernamame, int newId)
        {
            LoginScreen ls = new LoginScreen();
            id = newId;
            username = NewUsernamame;
            deleted = false;
            type = UserType.User;
            application.Add("x", ls.LoginMethod);
            application.Add("a", ApplicationsMenus.ChatMenu);
        }
    }



    public class ViewAdmin : User
    {
        public ViewAdmin(string NewUsernamame, int newId) : base(NewUsernamame, newId)
        {
            type = UserType.ViewAdmin;
            application.Add("b", MainApplication.ViewAllMessage);
        }

        public override void PublicMenu()
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
            type = UserType.ViewEditAdmin;
            application.Add("c", MainApplication.EditMessage);
        }

        public override void PublicMenu()
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
            type = UserType.ViewEditDeleteAdmin;
            application.Add("d", MainApplication.DeleteMessage);
        }

        public override void PublicMenu()
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
            type = UserType.SuperAdmin;
            application.Add("e", ApplicationsMenus.UserMenu);
        }

        public override void PublicMenu()
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
