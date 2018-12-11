using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	public class BaseMenu : BaseFunctions
	{
		#region base properties
		public string MenuName { get; set; }
		public List<string> Commands { get; set; }
		public List<BaseScreen> CommandsActions { get; set; }
		#endregion

		public BaseMenu(string menuName, List<string> commands, List<BaseScreen> commandsActions)
		{
			MenuName = menuName;
			Commands = new List<string>();
			commands.ForEach(c => Commands.Add(c));
			CommandsActions = new List<BaseScreen>();
			commandsActions.ForEach(ca => CommandsActions.Add(ca));
		}

		public virtual void PrintCommands(bool clearConsole = true)
		{
			if (clearConsole)
			{
				Console.Clear();
				PrintSeparator();
				Console.WriteLine(MenuName);
			}
			PrintSeparator();
			PrintInfoMessage("Choose one of the options below:");
			for (int i = 0; i < Commands.Count; i++)
				Console.WriteLine($"{Commands[i]}: {CommandsActions[i].ScreenName}.");
			PrintSeparator();
			CheckCommand(Console.ReadLine());
		}

		protected void CheckCommand(string input)
		{
			int indx = Commands.IndexOf(input);
			if (indx >= 0) // The command is correct
				CommandsActions[indx].Execute();
			else
			{
				Console.Clear();
				PrintSeparator();
				PrintError("Wrong input!!! Select one of the following options!!!");
				PrintCommands(false);
			}

		}
	}
}
