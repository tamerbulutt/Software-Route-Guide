using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Model.Exam;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Repositories.Concrete
{
    public class AnswerRepository : IAnswerRepository
    {
        IConfiguration _configuration;
        private string sqlConnectionAddress;

        public AnswerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection");
        }

        public bool addAnswers(List<Answer> answers)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress))
            {
                string query = @"INSERT INTO answers(answer,isCorrect,questionID) 
                                            VALUES (@answer,@isCorrect,@questionID)";
                mySqlConnection.Open();
                int executeCheck = 0;
                foreach (var answer in answers){
                    using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@answer", answer.answer);
                        sqlCommand.Parameters.AddWithValue("@isCorrect", answer.isCorrect);
                        sqlCommand.Parameters.AddWithValue("@questionID", answer.questionID);

                        executeCheck = sqlCommand.ExecuteNonQuery();
                    }
                }
                mySqlConnection.Close();

                if (executeCheck == 1)
                    return true;
            }
            return false;
        }

        public List<Answer> getAnswers(int questionID)
        {
            List<Answer> answerList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM answers WHERE questionID = @questionID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@questionID", questionID); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    answerList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        answerList.Add(new Answer{
                            answerID = (int)dataReader["answerID"],
                            answer = dataReader["answer"].ToString(),
                            isCorrect = (bool)dataReader["isCorrect"],
                            questionID = (int)dataReader["questionID"]
                        });
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return answerList;
        }

        public bool updateAnswers(List<Answer> answers)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress))
            {
                string query = @"UPDATE answers SET answer = @answer, isCorrect = @isCorrect, 
                                        questionID = @questionID WHERE answerID = @answerID";
                mySqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection))
                {
                    int executeCheck = 0;
                    foreach (var answer in answers)
                    {
                        sqlCommand.Parameters.AddWithValue("@answerID", answer.answerID);
                        sqlCommand.Parameters.AddWithValue("@answer", answer.answer);
                        sqlCommand.Parameters.AddWithValue("@isCorrect", answer.isCorrect);
                        sqlCommand.Parameters.AddWithValue("@questionID", answer.questionID);

                        executeCheck = sqlCommand.ExecuteNonQuery();
                    }
                    mySqlConnection.Close();

                    if (executeCheck == 1)
                        return true;
                }
            }
            return false;
        }
    }
}