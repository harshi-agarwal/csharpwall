using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using theWall.Models;
using Microsoft.Extensions.Options;
namespace theWall.Factory
{
    public class MessageFactory : IFactory<Message>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;
    
    public MessageFactory(IOptions<MySqlOptions> conf) {
            mysqlConfig = conf;
    }
    
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }
        public void Add(Message item,int user_id)
        {
            using (IDbConnection dbConnection = Connection) {
                
                string query =  $"INSERT INTO messages (msg,user_id,created_at,updated_at) VALUES (@msg,'{user_id}',NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Message> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query="SELECT * FROM messages JOIN users ON messages.user_id WHERE users.id = messages.user_id;";
                dbConnection.Open();
                var mymessages = dbConnection.Query<Message,User,Message>(query, (message, user) => { message.user = user; return message; });

                return mymessages ;
            }
        }
         
    }
}