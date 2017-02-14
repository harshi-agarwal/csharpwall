using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using theWall.Models;
using Microsoft.Extensions.Options;
namespace theWall.Factory
{
    public class CommentFactory : IFactory<Comment>
    {
        private readonly IOptions<MySqlOptions> mysqlConfig;
    
    public CommentFactory(IOptions<MySqlOptions> conf) {
            mysqlConfig = conf;
    }
    
        internal IDbConnection Connection
        {
            get {
                return new MySqlConnection(mysqlConfig.Value.ConnectionString);
            }
        }
        public void Add(Comment item,int user_id,int msg_id)
        {
            using (IDbConnection dbConnection = Connection) {
                
                string query =  $"INSERT INTO comments (user_id,message_id,context,created_at,updated_at) VALUES ('{user_id}','{msg_id}',@context,NOW(), NOW())";
                dbConnection.Open();
                dbConnection.Execute(query, item);
            }
        }
        public IEnumerable<Comment> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string query="SELECT * FROM comments JOIN users on  comments.user_id WHERE users.id=comments.user_id;";
                dbConnection.Open();
                var mycomments = dbConnection.Query<Comment,User,Comment>(query, (comment, user) => { comment.user = user; return comment; });

                return mycomments ;
            }
        }
         
    }
}