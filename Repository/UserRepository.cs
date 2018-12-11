using Dapper;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Repository
{
    public class UserRepository : BaseRepository, IRepository<User>
    {
        public UserRepository() : base()
        {

        }
        public UserRepository(IDbConnection con) : base(con)
        {

        }
        public void Add(User model)
        {
            try
            {
                Dictionary<string, object> dictValues = GetValuesDictionary(model, false);

                DynamicParameters parms = new DynamicParameters(dictValues);

                StringBuilder query = new StringBuilder("INSERT INTO dbo.[User] ");
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
                string query = "DELETE dbo.[User] WHERE Id = @Id";

                _con.Execute(query, new { Id = id });                
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<User> GetAll()
        {
            string query = "SELECT * FROM dbo.[User] ORDER BY Username ASC";
            
            return _con.Query<User>(query.ToString()).ToList();
        }

        public User GetById(int id)
        {
            try
            {
                string query = "SELECT * FROM dbo.[User] where Id = @Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                return _con.Query<User>(query, parameters).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetByUsername(string username)
        {
            try
            {
                string query = "SELECT * FROM dbo.[User] where Username = @Username";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Username", username);
                return _con.Query<User>(query, parameters).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Dictionary<string, object> GetValuesDictionary(User model, bool isForUpdate)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>
            {
                { "@Username", model.Username },
                { "@Password", model.Password },
                { "@Role", model.Role }
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

        public void Update(User model)
        {
            try
            {
                Dictionary<string, object> dictValues = GetValuesDictionary(model, true);

                DynamicParameters parms = new DynamicParameters(dictValues);
                parms.Add("@Id", model.Id);

                string query = String.Format($"UPDATE dbo.[User] SET {GetUpdateQuery(dictValues.Keys)} WHERE Id = @Id");

                _con.Execute(query, parms);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
