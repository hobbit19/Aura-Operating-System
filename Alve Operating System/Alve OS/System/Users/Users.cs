﻿/*
* PROJECT:          Alve Operating System Development
* CONTENT:          User class
* PROGRAMMERS:      Alexy DA CRUZ <dacruzalexy@gmail.com>
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Alve_OS.System.Translation;
using Alve_OS.System.Security;

namespace Alve_OS.System.Users
{
    class Users
    {
        public void Login()
        {
            switch (Kernel.langSelected)
            {
                case "fr_FR":
                    Console.Write("Utilisateur > ");
                    var user = Console.ReadLine();
                    Console.WriteLine();

                    if (File.Exists(@"0:\System\Users\" + user + ".usr"))
                    {
                        Console.Write("Mot de passe > ");
                        var psw = Console.ReadLine();
                        string md5psw = MD5.hash(psw);
                        Console.WriteLine();

                        string UserFile = File.ReadAllText(@"0:\System\Users\" + user + ".usr");

                        Char delimiter = '|';
                        string[] UserFileContent = UserFile.Split(delimiter);

                        if (md5psw == UserFileContent[0])
                        {
                            //LOGGED
                            Kernel.userLogged = user;
                            UserLevel.LevelReader(UserFileContent[1]);
                            if(user == "root")
                            {
                                Kernel.userLevelLogged = UserLevel.Administrator();
                            }
                            Console.Clear();
                            WelcomeMessage.Display();
                            Text.Display("logged", user);
                            Console.WriteLine("");
                            Kernel.Logged = true;
                        }
                    }
                    else
                    {
                        Text.Display("unknownuser");
                    }

                    break;

                case "en_US":
                    Console.Write("Login > ");
                    var user2 = Console.ReadLine();
                    Console.WriteLine();

                    if (File.Exists(@"0:\System\Users\" + user2 + ".usr"))
                    {
                        Console.Write("Mot de passe > ");
                        var psw = Console.ReadLine();
                        string md5psw = MD5.hash(psw);
                        Console.WriteLine();

                        string UserFile = File.ReadAllText(@"0:\System\Users\" + user2 + ".usr");

                        Char delimiter = '|';
                        string[] UserFileContent = UserFile.Split(delimiter);

                        if (md5psw == UserFileContent[0])
                        {
                            //LOGGED
                            Kernel.userLogged = user2;
                            UserLevel.LevelReader(UserFileContent[1]);
                            if (user2 == "root")
                            {
                                Kernel.userLevelLogged = UserLevel.Administrator();
                            }
                            Console.Clear();
                            WelcomeMessage.Display();
                            Text.Display("logged", user2);
                            Console.WriteLine("");
                            Kernel.Logged = true;
                        }
                    }
                    else
                    {
                        Text.Display("unknownuser");
                    }

                    break;


            }
        }

        /// <summary>
        /// Méthode pour créer un utilisateur.
        /// </summary>
        /// <param name="username">Nom du nouvel utilisateur</param>
        /// <param name="userlevel">Niveau de l'utilisateur (Admin, Standard)</param>
        public void Create(string username)
        {
            try
            {
                Console.WriteLine();
                Text.Display("chooseyourusername");
                Console.WriteLine();
                Text.Display("user");
                username = Console.ReadLine();

                if (File.Exists(@"0:\System\Users\" + username + ".usr"))
                {
                    Text.Display("alreadyuser");
                    Create(username);
                }
                else
                {
                    if ((username.Length >= 4) && (username.Length <= 20))
                    {
                        Console.WriteLine();
                        Text.Display("passuser", username);

                        psw:

                        Console.WriteLine();
                        Text.Display("passwd");
                        Console.WriteLine();

                        Console.ForegroundColor = ConsoleColor.Black;
                        var clearpassword = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        if ((clearpassword.Length >= 6) && (clearpassword.Length <= 40))
                        {
                            string password = MD5.hash(clearpassword);
                            Console.ForegroundColor = ConsoleColor.White;

                            Console.WriteLine();

                            File.Create(@"0:\System\Users\" + username + ".usr");
                            Directory.CreateDirectory(@"0:\Users\" + username);

                            if (File.Exists(@"0:\System\Users\" + username + ".usr"))
                            {

                                Console.WriteLine();
                                Text.Display("whattypeuser");
                                groups:
                                Text.Display("groupsavailable");
                                Console.WriteLine();

                                var userlevel = Console.ReadLine();

                                if (userlevel == UserLevel.Administrator())
                                {
                                    File.WriteAllText(@"0:\System\Users\" + username + ".usr", password + "|admin");
                                }
                                else if (userlevel == UserLevel.StandardUser())
                                {
                                    File.WriteAllText(@"0:\System\Users\" + username + ".usr", password + "|standard");
                                }
                                else
                                {
                                    Console.WriteLine();
                                    goto groups;
                                }
                            }
                            else
                            {
                                Text.Display("passwd");
                            }
                        }
                        else
                        {
                            Text.Display("pswcharmin");
                            goto psw;
                        }
                    }
                    else
                    {
                        Text.Display("charmin");
                    }
                }
            }
            catch
            {
                Text.Display("errorwhileusercreating");
            }          
        }
    }
}