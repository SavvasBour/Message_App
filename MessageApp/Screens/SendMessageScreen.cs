using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageApp
{
	public class SendMessageScreen : BaseScreen
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public SendMessageScreen() : base()
		{
			ScreenName = "SEND MESSAGE";
		}

		public override void Execute()
		{
			base.Execute();

			
			User receiver = null;
			do
			{
				Console.WriteLine("Type the username of the receiver.");

				string receiverUsername = Console.ReadLine();
				if (receiverUsername != Session.Username)
				{
					using (var userRepo = new UserRepository())
					{
						receiver = userRepo.GetByUsername(receiverUsername);
					}

					if (receiver == null)
						PrintError($"The user: {receiverUsername} was not found! Please try again.");
				}
				else
					PrintError("Haha nice try! You can't send a message to yourself silly!");
			}
			while (receiver == null);

			string subject;
			do
			{
				Console.WriteLine("Type a non empty subject. Subject must not be longer than 50 characters.");
				subject = Console.ReadLine();
			}
			while (String.IsNullOrWhiteSpace(subject) || subject.Length > 50);

			string text;
			do
			{
				Console.WriteLine("Type a non empty message. Message text must not be longer than 250 characters.");
				text = Console.ReadLine();
			}
			while (String.IsNullOrWhiteSpace(text) || text.Length > 250);

			var message = new Message() { SenderId = Session.UserId.Value, ReceiverId = receiver.Id, Subject = subject, Text = text, CreatedBy = Session.Username };

			using (var messageRepo = new MessageRepository())
				messageRepo.Add(message);

			logger.Info($"{{ Date of Submission: {DateTime.Now.ToString("dd/MM/yyyy")}, Sender: {Session.Username}, Receiver: {receiver.Username}, Subject: {message.Subject}, Text: {message.Text} }}");

			PrintSuccess($"Message to user {receiver.Username} was sent successfully!");
			Console.ReadLine();

			new MainMenu();			
		}
	}
}
