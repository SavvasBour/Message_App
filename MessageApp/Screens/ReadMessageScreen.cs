using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	public class ReadMessageScreen : BaseScreen
	{
		private List<Message> _myMessages { get; set; }

		public ReadMessageScreen() : base()
		{
			ScreenName = "READ MY MESSAGES";
		}

		public override void Execute()
		{
			using (var messageRepo = new MessageRepository())
			{
				_myMessages = messageRepo.GetByReceiverId(Session.UserId.Value);
			}
			int subjectNo = -1;
			do
			{
				base.Execute();

				PrintInfoMessage($"Total Messages: ({_myMessages.Count})");

				PrintSubjects();


				do
				{
					Console.WriteLine("Choose a subject number to read the relevant message.");
					string input = Console.ReadLine();
					Int32.TryParse(input, out subjectNo);
				}
				while (subjectNo < 0 || subjectNo > _myMessages.Count);

				if (subjectNo != 0)
				{
					Console.Clear();
					PrintSeparator();
					Console.WriteLine("==================== MESSAGE TEXT ========================");
					PrintSeparator();
					WriteLineWordWrap(_myMessages[subjectNo - 1].Text);
					PrintSeparator();
					Console.ReadLine();
				}
			}
			while (subjectNo != 0);

			new MainMenu();
		}

		private void PrintSubjects()
		{
			Console.WriteLine("0: Back to main menu");
			for (int i = 0; i < _myMessages.Count; i++)
				Console.WriteLine($"{i + 1}: {_myMessages[i].Subject}");
		}
	}
}
