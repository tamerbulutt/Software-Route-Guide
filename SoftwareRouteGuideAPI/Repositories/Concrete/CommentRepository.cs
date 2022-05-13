using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Model.Education;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Repositories.Concrete
{
    public class CommentRepository : ICommentRepository
    {
        IConfiguration _configuration;
        private string sqlConnectionAddress;

        public CommentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection");
        }

        public bool Add(Comment entity)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"INSERT INTO comments(content,fullname,date,educationID) 
                                                VALUES (@content,@fullname,@date,@educationID)"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@fullname", entity.fullname); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@content", entity.content);
                    sqlCommand.Parameters.AddWithValue("@date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@educationID", entity.educationID);

                    var executeCheck = sqlCommand.ExecuteNonQuery();

                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (executeCheck == 1) //Kayıt işlemi başarılıysa
                        return true;
                }
            }
            return false;
        }

        public bool Delete(int id)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"DELETE FROM comments WHERE commentID = @commentID;"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@commentID", id); //Parametreleri eşliyoruz
                    var result = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                    if (result == 1) return true;
                }
            }
            return false;
        }

        public List<Comment> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public List<Comment> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Comment> getCommentsByEducationId(int educationID)
        {
            List<Comment> commentList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM comments WHERE educationID = @educationID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@educationID", educationID); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    commentList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        commentList.Add(new Comment{
                            commentID = (int)dataReader["commentID"],
                            fullname = dataReader["fullname"].ToString(),
                            content = dataReader["content"].ToString(),
                            date = (DateTime)dataReader["date"],
                            educationID = (int)dataReader["educationID"]
                            
                        });
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return commentList;
        }

        public bool Update(Comment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}