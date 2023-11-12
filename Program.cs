 using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;
using static System.Threading.Thread;

// ReSharper disable PossibleNullReferenceException

namespace CommandPrompt1;

internal static class Program
{
    private static readonly Dictionary<string, string[]> UserData = new();
    private static readonly Dictionary<string, string[]> AdminData = new();

    private static bool _isAdmin;
    private static string _command;
    private static string _user;

    private static void DataHandler() // key: username, value[0]: password
    {
        AdminData.Add("otdAzimi",
            new[] { "itspassw", "A-0001", "a...@gmail.com", "(012) 345-6789" });
        AdminData.Add("mmd", new[] { "011", "A-0002", "_", "_" });
        AdminData.Add("sanaz", new[] { "1010", "A-0003", "_", "_" });
        UserData.Add("madklok", new[] { "2_11", "U-0001", "MadKloK12@gmail.com", "(211) 211-2011" });
        UserData.Add("esteghlal", new[] { "kise", "U-0002", "BlahBlahBlah@gmail.com", "in ghesmat surakh ast" });
    }

    private static void EditProfile(int n)
    {
        var belongsTo = _isAdmin ? AdminData : UserData;
        string ne; // newElement
        switch (n)
        {
            case 3:
                do
                {
                    Write("Insert phone number in this form: 0123456789 \n" +
                          "Phone number: "); ne = ReadLine();
                    var isNumeric = int.TryParse(ne, out _);
                    if (ne.Length != 10 || !isNumeric) 
                        WriteLine("\nInvalid phone number!\n");
                    else break;
                    
                } while (true);
                
                var pn = $"({ne[0]}{ne[1]}{ne[2]}) {ne[3]}{ne[4]}{ne[5]}-{ne[6]}{ne[7]}{ne[8]}{ne[9]}";
                belongsTo[_user][n] = pn;
                WriteLine("\nProcessing...");
                Sleep(2000);
                WriteLine("\nInformation successfully changed!\n");
                break;
            
            case 2:
                Write("Email address: ");
                ne = ReadLine(); 
                belongsTo[_user][n] = ne;
                WriteLine("\nProcessing...");
                Sleep(2000);
                WriteLine("\nInformation successfully changed!\n");
                break;
            
            case 0:
                do
                {
                    Write("Password: "); ne = ReadLine();
                    if (ne.Length > 3) break;
                    WriteLine("\nInvalid password!\n");
                } while (true);
                belongsTo[_user][n] = ne;
                WriteLine("\nProcessing...");
                Sleep(2000);
                WriteLine("\nInformation successfully changed!\n");
                break;
        }
    }
    
    private static void Profile()
    {
        var idk = _isAdmin ? AdminData : UserData;

        WriteLine("\nGetting data. Wait a moment...");
        Sleep(2000);
        WriteLine($"\nProfile information of User: {idk[_user][1]}");
        WriteLine($"    - Username: {_user}\n" +
                  $"    - Email address: {idk[_user][2]}\n" +
                  $"    - Phone number: {idk[_user][3]}\n");
    }

    private static void MainTaskHandler1()
    {
        do
        {
            Write("\nLogin to enter the menu. " +
                  "(use 'login' or 'signup' to continue and 'exit' to close the program)" +
                  "\ncommand: ");
            _command = ReadLine().ToLower().Replace(" ", "");

            switch (_command)
            {
                case "login":
                    Login();
                    break;
                case "signup":
                    SignUp();
                    break;
                case "exit":
                    Exit();
                    break;
            }
        } while (_command != "exit" && _command != "login" && _command != "signup");
    }

    private static void LogoutHandler()
    {
        MainTaskHandler1();
    }

    private static void SignUp()
    {
        string user, passw;
        do
        {
            Write("\nInsert username: ");
            user = ReadLine();
            Write("Insert Password: ");
            passw = ReadLine();

            if (AdminData.ContainsKey(user) || UserData.ContainsKey(user))
                WriteLine("\nThis username is already selected");

            else if (user.Length <= 3 || passw.Length <= 3)
                WriteLine("\nUsername or password is not allowed");
        } while (AdminData.ContainsKey(user) || UserData.ContainsKey(user)
                                             || user.Length <= 3 || passw.Length <= 3);

        Write("\nWait a moment...\n");

        UserData.Add(user, new[] { passw, $"U-{$"{UserData.Count + 1}".PadLeft(4, '0')}", "_", "_" });

        Sleep(3000);
        WriteLine("your account successfully created!\n" +
                  "\nHere's a hearty welcome, big and warm enough to encompass you! " +
                  "\nTo say we are thrilled to see you is an understatement.\n");
        _user = user;
    }

    private static void AddUser()
    {
        string user, passw;
        do
        {
            Write("\nInsert username: ");
            user = ReadLine();
            Write("Insert password: ");
            passw = ReadLine();

            if (user.Length > 3 && passw.Length > 3 &&
                !AdminData.ContainsKey(user) && !UserData.ContainsKey(user)) break;

            WriteLine("Processing...");
            Sleep(2000);
            Write("\nPaye ye nafar raft ro sim. Dobare emthan knid.\n");
        } while (true);

        UserData.Add(user, new[] { passw, $"U-{$"{UserData.Count + 1}".PadLeft(4, '0')}", "_", "_" });
        Write("\nProcessing...  20%");
        Sleep(2500);
        Write("\nProcessing...  60%");
        Sleep(1000);
        Write("\nProcessing...  70%");
        Sleep(1700);
        WriteLine("\nProcessing...  100%");
        WriteLine("\nuser-adding successfully completed!\n");
    }

    private static void Exit()
    {
        WriteLine("\nHave a good time ;)" +
                  "\n______________________" +
                  "\nPress any key to close");
        ReadKey();
        Environment.Exit(-1);
    }

    private static void Login()
    {
        string user;
        do
        {
            Write("\nUsername: ");
            user = ReadLine();
            Write("Password: ");
            var passw = ReadLine();

            if (AdminData.ContainsKey(user) || UserData.ContainsKey(user))
                if (AdminData.ContainsKey(user))
                {
                    _isAdmin = AdminData[user][0] == passw;
                    if (_isAdmin)
                        break;
                }
                else if (UserData.ContainsKey(user))
                {
                    if (UserData[user][0] == passw)
                        break;
                }

            WriteLine("\nInvalid information!");
        } while (true);

        WriteLine("Logging in. Wait a moment...");
        Sleep(2000);
        WriteLine("\nYour logged in!\n");
        _user = user;
    }

    private static void DataAccess()
    {
        WriteLine("\nGetting data. Wait a moment...");
        Sleep(2000);

        Write("\nAdmins: \n");
        foreach (var str in AdminData.Keys.Select(i => $"{i} : {AdminData[i][0]}"))
        {
            WriteLine($"\t{str}");
        }

        Write("\nMembers: \n");
        foreach (var str in UserData.Keys.Select(i => $"{i} : {UserData[i][0]}"))
        {
            WriteLine($"\t{str}");
        }

        WriteLine("");
    }

    private static void Main()
    {
        DataHandler();

        WriteLine("MadKloKs OS [Version 1.1.5]\n" +
                  "(c) MadKloK. All rights reserved.");

        MainTaskHandler1();

        Write("#use 'help' to see the command list.\ncommand: ");
        _command = ReadLine().ToLower().Replace(" ", "");

        while (_command != "exit")
        {
            switch (_command)
            {
                case "editprofile":
                    Write("\nEmail address - Phone number - Password\n" + 
                          "\nSelect index of the item you want to change: ");
                    var index = Convert.ToInt64(ReadLine());
                    index = index == 3 ? 0: index + 1;
                    WriteLine();
                    EditProfile((int)index);
                    break;
                    
                case "profinfo":
                    Profile();
                    break;

                case "signup":
                    WriteLine("\nYou have to logout first!\n");
                    break;

                case "signin":
                    WriteLine("\nYou have to logout first!\n");
                    break;

                case "logout":
                    WriteLine("\nYour logged out!");
                    LogoutHandler();
                    break;

                case "howisitgoing?":
                    WriteLine("\n:||||\n");
                    break;

                case "adduser":
                    if (_isAdmin)
                        AddUser();
                    else
                        WriteLine("\nAccess Denied!\n");
                    break;

                case "help":
                    WriteLine("\nCommand List: " + 
                              "\n\tuserdata            Shows the User-list(admins only)" +
                              "\n\tadduser             Makes a new account(admins only)" +
                              "\n\tprofinfo            Shows your account information" +
                              "\n\teditprofile         edits profile information" +
                              "\n\thow is it going?    Poker-face" +
                              "\n\tlogout              Exits from this account" +
                              "\n\texit                To end the program\n");
                    break;

                case "userdata":
                    if (_isAdmin)
                        DataAccess();
                    else
                        WriteLine("\nAccess Denied!\n");
                    break;

                default:
                {
                    WriteLine($"\n'{_command}' is not recognized as an internal or external command\n");
                    break;
                }
            }

            Write("command: ");
            _command = ReadLine().ToLower().Replace(" ", "");
        }

        Exit();
    }
}