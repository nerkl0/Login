using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Figgle;

namespace Main
{
	internal class Menu
	{
		public static void Title(string word)
		{
			Console.WriteLine(
				FiggleFonts.DRPepper.Render(word));
		}

		public static void ShowList(params string[] menuList)
		{
			Console.WriteLine("Menu");
			for (int i = 0; i < menuList.Length; i++)
			{
				Console.WriteLine($"[{i + 1}] {menuList[i]}");
            }
		}

		public static int ListOptions(int lowest, int highest)
		{
			int optionSelect;

			while (true)
			{
				Console.Write("Please select an option: ");
				int.TryParse(Console.ReadLine(), out optionSelect);

				if (optionSelect >= lowest && optionSelect <= highest)
				{
					break;
				}
				else
				{
					Console.WriteLine("Please enter a valid integer.");
				}	
			}
			return optionSelect;
		}
	}
}
