using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	class DeleteAnyMessage : BaseScreen
	{
		private List<Message> _messages { get; set; }

		public DeleteAnyMessage() : base()
		{
			ScreenName = "DELETE ANY MESSAGE";
		}

		public override void Execute()
		{
			using (var messageRepo = new MessageRepository())
			{
				messageRepo.ReadLevel = MessageRepository.MessageReadLevel.WithSenderReceiver;

				_messages = messageRepo.GetAll();
			}
			int subjectNo = -1;
			do
			{
				base.Execute();

				PrintInfoMessage($"Total Messages: ({_messages.Count})");

				PrintSubjects();
				PrintSeparator();

				do
				{
					Console.WriteLine("Choose a subject number to delete the relevant message.");
					string input = Console.ReadLine();
					Int32.TryParse(input, out subjectNo);
				}
				while (subjectNo < 0 || subjectNo > _messages.Count);

				if (subjectNo != 0)
				{
					Console.Clear();
					PrintSeparator();
					Console.WriteLine("==================== MESSAGE TEXT ========================");
					PrintSeparator();
					var selectedMessage = _messages[subjectNo - 1];
					WriteLineWordWrap(selectedMessage.Text);
					PrintSeparator();
					string yesNoInput;
					do
					{
						PrintInfoMessage("Do you want to delete this message? (Y / N)");
						yesNoInput = Console.ReadLine();
					}
					while (yesNoInput != "Y" && yesNoInput != "N");

					if (yesNoInput == "Y")
					{
						using (var messageRepo = new MessageRepository())
							messageRepo.Delete(selectedMessage.Id);

						int indx = _messages.FindIndex(m => m.Id == selectedMessage.Id);
						_messages.RemoveAt(indx);

						PrintSuccess("The message was deleted successfully!");

						Console.ReadLine();
					}
				}
			}
			while (subjectNo != 0);

			new MainMenu();
		}

		private void PrintSubjects()
		{
			Console.WriteLine("0: Back to main menu");
			for (int i = 0; i < _messages.Count; i++)
			{
				var message = _messages[i];
				Console.WriteLine($"{i + 1}: {message.Subject}, {{ Sender: {message.Sender.Username} , Receiver: {message.Receiver.Username} }}");
			}
		}
	}
}
