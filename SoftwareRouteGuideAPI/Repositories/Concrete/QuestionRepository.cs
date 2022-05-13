using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Model.Exam;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Repositories.Concrete
{
    public class QuestionRepository : IQuestionRepository
    {
        IConfiguration _configuration;
        private string sqlConnectionAddress;
        public QuestionRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection");
        }

        public long AddQuestion(Question question)
        {
            long lastId = 0;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress))
            {
                string query = @"INSERT INTO questions(question,educationID) 
                                            VALUES (@question,@educationID)";
                mySqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection))
                {
                    int executeCheck = 0;

                    sqlCommand.Parameters.AddWithValue("@question", question.question);
                    sqlCommand.Parameters.AddWithValue("@educationID", question.educationID);

                    executeCheck = sqlCommand.ExecuteNonQuery();
                    lastId = sqlCommand.LastInsertedId;

                    mySqlConnection.Close();
                }
            }
            return lastId;
        }

        public int CheckAnswers(List<int> ids)
        {
            int totalScore = 0;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress))
            {
                string query = @"SELECT * FROM answers WHERE answerID = @answerID";
                mySqlConnection.Open();

                foreach (var id in ids)
                {
                    using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@answerID", id);
                        var dataReader = sqlCommand.ExecuteReader();
                        while (dataReader.Read())
                        {
                            if (Convert.ToBoolean(dataReader["isCorrect"]))
                                totalScore += 25;
                        }
                        dataReader.Close();
                    }
                }
                mySqlConnection.Close();
            }
            return totalScore;
        }

        public bool FinishExam(CalculateModel model, int score)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress))
            {
                string query = @"INSERT INTO usereducations(userID,educationID,Date,examScore) 
                                            VALUES (@userID,@educationID,@Date,@examScore)";
                mySqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@examScore", score);
                    sqlCommand.Parameters.AddWithValue("@educationID", model.educationID);
                    sqlCommand.Parameters.AddWithValue("@userID", model.userID);
                    sqlCommand.Parameters.AddWithValue("@Date", DateTime.Now);

                    var insertResult = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close();
                    if (insertResult == 1)
                        return true;
                }
            }
            return false;
        }

        public List<Question> getQuestions(int educationID)
        {
            List<Question> questionList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM questions WHERE educationID = @educationID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@educationID", educationID); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    questionList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        questionList.Add(new Question
                        {
                            questionID = (int)dataReader["questionID"],
                            question = dataReader["question"].ToString(),
                            educationID = (int)dataReader["educationID"]
                        });
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return questionList;
        }

        public bool UpdateQuestions(List<Question> questions)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress))
            {
                string query = @"UPDATE questions SET question = @question, educationID = @educationID 
                                                        WHERE questionID = @questionID";
                mySqlConnection.Open();

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection))
                {
                    int executeCheck = 0;
                    foreach (var question in questions)
                    {
                        sqlCommand.Parameters.AddWithValue("@questionID", question.questionID);
                        sqlCommand.Parameters.AddWithValue("@question", question.question);
                        sqlCommand.Parameters.AddWithValue("@educationID", question.educationID);

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