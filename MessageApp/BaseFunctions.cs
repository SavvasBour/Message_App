using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MessageApp
{
	public class BaseFunctions
	{
		public void PrintError(string error)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.WriteLine(error);
			Console.BackgroundColor = ConsoleColor.Black;
		}

		public void PrintSuccess(string message)
		{
			Console.BackgroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine(message);
			Console.BackgroundColor = ConsoleColor.Black;
		}

		public void PrintInfoMessage(string message)
		{
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine(message);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
		}

		public void PrintSeparator()
		{
			Console.WriteLine(new String('=', 58));
		}
		
		public static void WriteLineWordWrap(string paragraph) //wraps the text in relation of the size of the console window
		{
			paragraph = new Regex(@" {2,}").Replace(paragraph.Trim(), @" ");
			var left = Console.CursorLeft; var top = Console.CursorTop; var lines = new List<string>();
			for (var i = 0; paragraph.Length > 0; i++)
			{
				lines.Add(paragraph.Substring(0, Math.Min(Console.WindowWidth, paragraph.Length)));
				var length = lines[i].LastIndexOf(" ", StringComparison.Ordinal);
				if (length > 0) lines[i] = lines[i].Remove(length);
				paragraph = paragraph.Substring(Math.Min(lines[i].Length + 1, paragraph.Length));
				Console.SetCursorPosition(left, top + i); Console.WriteLine(lines[i]);
			}
		}
	}
}
