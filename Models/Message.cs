using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Message : BaseModel
    {
        public int SenderId
		{
			get
			{
				return Sender != null ? Sender.Id : 0;
			}
			set
			{
				if (Sender == null)
					Sender = new User();

				Sender.Id = value;
			}
		}
        public int ReceiverId
		{
			get
			{
				return Receiver != null ? Receiver.Id : 0;
			}
			set
			{
				if (Receiver == null)
					Receiver = new User();

				Receiver.Id = value;
			}
		}
        public string Subject { get; set; }
        public string Text { get; set; }

		#region helper properties
		public User Sender { get; set; }
		public User Receiver { get; set; }
		#endregion
	}
}
