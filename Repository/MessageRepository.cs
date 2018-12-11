using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MessageRepository : BaseRepository, IRepository<Message>
    {
		#region ReadLevel
		public enum MessageReadLevel
		{
			Normal,
			WithSenderReceiver
		}

		public MessageReadLevel ReadLevel { get; set; } = MessageReadLevel.Normal;
		#endregion

		public MessageRepository() : base()
        {

        }
        public MessageRepository(IDbConnection con) : base(con)
        {

        }
        public void Add(Message model)
        {
            try
            {
                Dictionary<string, object> dictValues = GetValuesDictionary(model, false);

                DynamicParameters parms = new DynamicParameters(dictValues);

                StringBuilder query = new StringBuilder("INSERT INTO dbo.[Message] ");
                query.Append(GetInsertQuery(dictValues.Keys));

                _con.Execute(query.ToString(), parms);
            }
            catch
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                string query = "DELETE dbo.[Message] WHERE Id = @Id";

                _con.Execute(query, new { Id = id });
            }
            catch (Exception)
            {
                throw;
            }
        }

		public void DeleteBySenderOrReceiverId(int id)
		{
			try
			{
				string query = "DELETE dbo.Message WHERE SenderId = @UserId OR ReceiverId = @UserId";

				_con.Execute(query, new { UserId = id });
			}
			catch (Exception)
			{
				throw;
			}
		}

		public List<Message> GetAll()
		{
			StringBuilder query = new StringBuilder("SELECT * FROM dbo.[Message] M");

			if (ReadLevel == MessageReadLevel.WithSenderReceiver)
			{
				query.Append(" JOIN dbo.[User] S ON M.SenderId = S.Id");
				query.Append(" JOIN dbo.[User] R ON M.ReceiverId = R.Id");
			}

			query.Append(" ORDER BY M.Id ASC");

			if (ReadLevel == MessageReadLevel.WithSenderReceiver)
				return _con.Query<Message, User, User, Message>(query.ToString(), (m, s, r) =>
				{
					m.Sender = s;
					m.Receiver = r;

					return m;
				}).ToList();
			else
				return _con.Query<Message>(query.ToString()).ToList();
		}

		public Message GetById(int id)
        {
            try
            {
                string query = "SELECT * FROM dbo.[Message] where Id = @Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                return _con.Query<Message>(query, parameters).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

		public List<Message> GetByReceiverId(int receiverId)
		{
			try
			{
				string query = "SELECT * FROM dbo.[Message] where ReceiverId = @ReceiverId";
				DynamicParameters parameters = new DynamicParameters();
				parameters.Add("@ReceiverId", receiverId);

				return _con.Query<Message>(query, parameters).ToList();
			}
			catch (Exception)
			{
				throw;
			}
		}

		public Dictionary<string, object> GetValuesDictionary(Message model, bool isForUpdate)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "@ReceiverId", model.ReceiverId },
                { "@SenderId", model.SenderId },
                { "@Subject", model.Subject },
                { "@Text", model.Text }
            };

            if (isForUpdate)
            {
                dict.Add("@ModifiedBy", model.ModifiedBy);
                dict.Add("@LastEditDate", DateTime.UtcNow);
            }
            else
            {
                dict.Add("@CreatedBy", model.CreatedBy);
                dict.Add("@CreationDate", DateTime.UtcNow);
            }

            return dict;
        }

        public void Update(Message model)
        {
            try
            {
                Dictionary<string, object> dictValues = GetValuesDictionary(model, true);

                DynamicParameters parms = new DynamicParameters(dictValues);
                parms.Add("@Id", model.Id);

                string query = String.Format($"UPDATE dbo.[Message] SET {GetUpdateQuery(dictValues.Keys)} WHERE Id = @Id");

                _con.Execute(query, parms);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
