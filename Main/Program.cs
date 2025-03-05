// Nicole Birch 03/03/2024 - to create a basic login program
using Figgle;

namespace Main
{
	internal class Program
	{
		static void Main()
		{
			int menuOption = 0;

			// assign numbers for menu options
			int menuPage = 0;
			int registerPage = 1;
			int loginPage = 2;
			int viewAccountsPage = 3;
			int exit = 4;

			string username;
			string password;
			bool logInStatus = false; 

			string fileName = "..//..//..//accounts.txt"; //import file

			while (menuOption != exit)
			{
				// main menu
				if (menuOption == menuPage)
				{
					ConfigConsole(ConsoleColor.Green);
					Console.WriteLine(FiggleFonts.Isometric3.Render("Welcome!\n"));
					Menu.ShowList("Register", "Log In", "View Accounts", "Exit");
					menuOption = Menu.ListOptions(1, 4);
				}

				// registration
				if (menuOption == registerPage)
				{
					ConsoleColor colour = ConsoleColor.Cyan; // set universal background for register page

					ConfigConsole(colour);
					Menu.Title("Register");

					while (true) // loop to make sure all conditions are met
					{
						Console.Write("Username: "); // prompt username
						username = Console.ReadLine();

						if (!Register.UsernameCheck(fileName, username))
						{
							do
							{
								Console.Write("Password: ");
								password = Console.ReadLine();
							} while (!Register.PasswordCheck(password));

							// append details to file
							string registerDetails = $"{username},{password}";
							Register.Registered(fileName, registerDetails);
							menuOption = viewAccountsPage;
							logInStatus = true;
							Thread.Sleep(2000);

							break; // break loop if all conditions met
						}
						else
						{
							ConfigConsole(colour);
							Console.WriteLine($"{username} has already been registered.\n\nPlease register a new username, or select login.");
							menuOption = ReturnToMainMenu();
							break;
						}
					}
				}

				//log in
				else if (menuOption == loginPage)
				{
					ConsoleColor colour = ConsoleColor.Gray; // set universal background colour for login screen

					ConfigConsole(colour);
					Menu.Title("Log In");

					Console.Write("Username: ");
					username = Console.ReadLine();
					if (Register.UsernameCheck(fileName, username)) // check if username is in file
					{
						bool validate = false;
						while (!validate)
						{
							Console.Write("Password: ");
							password = Console.ReadLine();
							string loginDetails = $"{username},{password}";
							if (Register.LoginValidator(fileName, loginDetails)) // check whether username and password are in file
							{
								menuOption = viewAccountsPage;
								logInStatus = true;
								Console.WriteLine("Log in successful.");
								Thread.Sleep(1000);
								validate = true;
							}
							else
							{
								Console.WriteLine("Incorrect Password");
							}
						}
					}
					else // if username is not found in file
					{
						ConfigConsole(colour);
						Console.WriteLine($"{username} is not registered.");
						menuOption = ReturnToMainMenu();
					}

				}

				//view accounts
				if (menuOption == viewAccountsPage)
				{
					while (!logInStatus) // error handling if user tries to view accounts before login in
					{
						Console.Clear();
						Console.WriteLine("Please log in to view accounts.\n");
						menuOption = ReturnToMainMenu();
						break;
					}

					while (logInStatus)
					{
						ConsoleColor colour = ConsoleColor.DarkYellow; // universal colour for HomePage

						ConfigConsole(colour);
						Menu.Title("Home");
						Menu.ShowList("View Number of Accounts", "View Account Names", "Log Out");
						int homeSelection = Menu.ListOptions(1, 3);

						// if view account number selected
						if (homeSelection == 1)
						{
							ConfigConsole(colour);
							int number = NumAccounts(fileName);
							Console.WriteLine($"Number of Accounts: {number}\n");

							Menu.ShowList("View Account Names", "Log Out");
							int select = Menu.ListOptions(1, 2);
							homeSelection = (select == 1) ? 2 : 3;
						}

						// if view account names selected
						if (homeSelection == 2)
						{
							ConfigConsole(colour); 
							Console.WriteLine(FiggleFonts.Speed.Render("Account Names"));

							int number = NumAccounts(fileName);
							Console.WriteLine($"Number of Accounts: {number}\n");

							ViewAccounts(fileName);
							Console.WriteLine("\n");
							Menu.ShowList("Log Out");
							Menu.ListOptions(1, 1);
							homeSelection = 3;
						}

						// if log out
						if (homeSelection == 3) // log out
						{
							menuOption = menuPage;
							Console.Clear();
							Console.WriteLine(FiggleFonts.CyberLarge.Render("Logged Out."));
							Thread.Sleep(2500);
							logInStatus = false;
							break;
						}				
                    }
				}
			}

			// if exit selected from menu
			Console.Clear();
			Console.WriteLine(FiggleFonts.Isometric3.Render("Goodbye."));
			Thread.Sleep(2500);
		}

		static void ConfigConsole(ConsoleColor backgroundColour)
		{
			Console.BackgroundColor = backgroundColour;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.Clear();
		}

		static void ViewAccounts(string file)
		{
			try
			{
				using (StreamReader sr = new StreamReader(file))
				{
					string line = "";
					int count = 1;
					while ((line = sr.ReadLine()) != null)
					{
						int commaIndex = line.IndexOf(',');
						if (commaIndex != -1)
						{
							string listOfUsernames = line.Substring(0, commaIndex);
							Console.WriteLine($"[{count}] {listOfUsernames}");
							count++;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Environment.Exit(1);
			}
		}

		static int NumAccounts(string file)
		{
			try
			{
				using (StreamReader sr = new StreamReader(file))
				{
					int count = 0;
					string line = "";

					while ((line = sr.ReadLine()) != null)
					{
						int lineEnd = line.IndexOf("\n");
						count++;
					}
					return count;
				}
			}
			catch(Exception ex)
			{
                Console.WriteLine(ex.Message);
				return -1;
            }
		}

		static int ReturnToMainMenu()
		{
			Menu.ShowList("Register", "Log In", "Back to Main Menu");
			int opt = Menu.ListOptions(1, 3);
			return opt == 1 ? 1 : (opt == 2 ? 2 : 0);
		}
	}
}