using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTypes;
using System.Configuration;

namespace DatabaseManagement
{
    public class SqlServerDAL : IDisposable
    {
        private SqlConnection _conn;
        public SqlServerDAL()
        {
            try
            {
                string config = ConfigurationManager.AppSettings["connectionString"];
                _conn = new SqlConnection(config);
                _conn.Open();
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public DbDataReader GetData(string sql)
        {
            try
            {
                SqlCommand command = new SqlCommand(sql, _conn);
                return command.ExecuteReader();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<User> GetAllUsersDetailsFromDB()
        {
            List<User> Users = new List<User>();
            DbDataReader reader = GetData("select * from Users");
            while (reader.Read())
            {
                User u = new User();
                u.ID = GetValue<string>(reader, "UserID");
                u.Name = GetValue<string>(reader, "UserName");
                u.Password = GetValue<string>(reader, "PasswordString");
                u.LastConnectionDate = GetValue<string>(reader, "LastConnectionDate");

                Users.Add(u);
            }
            reader.Close();
            return Users;
        }
        public IEnumerable<TextMessage> GetTextMessagesFromDB()
        {
            List<TextMessage> Messages = new List<TextMessage>();
            DbDataReader reader = GetData("select * from Messages");
            while (reader.Read())
            {
                TextMessage t = new TextMessage();
                t.Message = GetValue<string>(reader, "MessageText");
                t.TimeOfSending = GetValue<DateTime>(reader, "SentDate");
                t.SenderId = GetValue<string>(reader, "UserId");
                Messages.Add(t);
            }
            reader.Close();
            return Messages;
        }
        public int UpdateData(string sql)
        {
            SqlCommand command = new SqlCommand(sql, _conn);
            return command.ExecuteNonQuery();
        }
        public T GetValue<T>(DbDataReader reader, string columnName)
        {
            object value = reader[columnName];
            if (value is DBNull)
            {
                return default(T);
            }
            if (value is T)
            {
                return (T)value;
            }
            throw new ArgumentException("The value is not of type " + typeof(T).Name, "T");
        }

        public void Dispose()
        {
            _conn.Dispose();
        }


    }
}
