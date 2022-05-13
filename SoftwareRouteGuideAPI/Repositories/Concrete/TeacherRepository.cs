using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Teacher;
using SoftwareRouteGuideAPI.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Repositories.Concrete
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IConfiguration _configuration;
        private string sqlConnectionAddress;
        public TeacherRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection");
        }
        public List<ApplicationDetails> AllApplications()
        {
            List<ApplicationDetails> applicationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT users.Name,users.Surname,teacherapplication.userID,teacherapplication.Message,users.Email FROM teacherapplication INNER JOIN users ON teacherapplication.userID = users.userID "; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    applicationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        applicationList.Add(new ApplicationDetails
                        {
                           userID = Convert.ToInt32(dataReader["userID"]),
                           Name = dataReader["Name"].ToString(),
                           Surname = dataReader["Surname"].ToString(),
                           Email = dataReader["Email"].ToString(),
                           Message = dataReader["Message"].ToString()
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return applicationList;
        }

        public List<Teachers> allTeachers()
        {
            List<Teachers> teacherList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM users WHERE userType = @userType"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@userType", "Teacher"); //Parametreleri eşliyoruz
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    teacherList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        teacherList.Add(new Teachers
                        {
                            userID = Convert.ToInt32(dataReader["userID"]),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString(),
                            Telephone = dataReader["Telephone"].ToString()
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return teacherList;
        }

        public List<Teachers> GetTeacherByID(int id)
        {
            List<Teachers> teacherList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM users WHERE userID = @userID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@userID", id); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    teacherList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        teacherList.Add(new Teachers
                        {
                            userID = Convert.ToInt32(dataReader["userID"]),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString(),
                            Telephone = dataReader["Telephone"].ToString()
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return teacherList;
        }

        public bool TeacherApplication(TeacherApplicationDTO teacher)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = $"INSERT INTO teacherapplication(userID,Message) VALUES (@userID,@Message)"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {

                    sqlCommand.Parameters.AddWithValue("@userID", teacher.userID); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@Message", teacher.Message);

                    var executeCheck = sqlCommand.ExecuteNonQuery();

                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (executeCheck == 1) //Kayıt işlemi başarılıysa
                        return true;
                }
            }
            return false;
        }

        public bool ApproveApplication(int id)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = $"UPDATE users SET userType = @userType WHERE userID = @userID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {

                    sqlCommand.Parameters.AddWithValue("@userID", id); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@userType", "Teacher");

                    var executeCheck = sqlCommand.ExecuteNonQuery();

                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (executeCheck == 1)
                    {
                        var deleteApp = DeleteApplication(id);
                        if(deleteApp)
                            return true;
                    }
                }
            }
            return false;
        }

        public bool RejectApplication(int id)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = $"UPDATE users SET userType = @userType WHERE userID = @userID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {

                    sqlCommand.Parameters.AddWithValue("@userID", id); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@userType", "User");

                    var executeCheck = sqlCommand.ExecuteNonQuery();

                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (executeCheck == 1)
                    {
                        var deleteApp = DeleteApplication(id);
                        if (deleteApp)
                            return true;
                    }
                }
            }
            return false;
        }

        public bool DeleteApplication(int id)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"DELETE FROM teacherapplication WHERE userID = @userID;"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@userID", id); //Parametreleri eşliyoruz
                    var result = sqlCommand.ExecuteNonQuery();
                    
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                    
                    if (result == 1) 
                        return true;
                }
            }
            return false;
        }

    }
}
