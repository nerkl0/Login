namespace Main
{
	internal class Register
	{
		public static bool UsernameCheck(string file, string userInput)
		{
			try
			{
				using (StreamReader sr = new StreamReader(file))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						string[] splitString = line.Split(',');
						if (splitString[0] == userInput)
						{
							return true;
						}
					}
				}
				return false;
			}
			catch (Exception ex) // exception if error occurs while opening file
			{
				Console.WriteLine(ex.Message);
				Environment.Exit(1);
				return false;
			}
		}

		public static bool PasswordCheck(string input)
		{
			const int PasswordMinimum = 10;

			while (true)
			{
				bool hasUpper = false;
				bool hasLower = false;
				bool hasPunctuation = false;

				if (input.Length >= PasswordMinimum) // if password meets minimum requirements, check other requirements
				{
					for (int i = 0; i < input.Length; i++)
					{
						if (char.IsPunctuation(input[i]))
						{
							hasPunctuation = true;
						}
						else if (char.IsUpper(input[i]))
						{
							hasUpper = true;
						}
						else if (char.IsLower(input[i]))
						{
							hasLower = true;
						}

						if (hasUpper && hasLower && hasPunctuation) // exit loop as soon as all conditions are met
						{
							return true;
						}
					}
				}
                Console.WriteLine($"Password must be {PasswordMinimum} characters and include:\n" +
								"\t\t* at least 1 lowercase character\n" +
								"\t\t* at least 1 uppercase character\n" +
								"\t\t* at least 1 special character.\n");
				return false;
			}
		}

		public static void Registered(string file, string userInput)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(file, true))
				{
					sw.WriteLine(userInput);
					Console.WriteLine("Registration successful");
				}
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}
		}

		public static bool LoginValidator(string file, string x)
		{
			try
			{
				using (StreamReader sr = new StreamReader(file))
				{
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						if (line == x)
						{
							return true;
						}
					}
					return false;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Environment.Exit(1);
				return false;
			}
		}
	}
}

