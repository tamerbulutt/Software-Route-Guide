using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Model.Identity;
using System;
using System.Collections.Generic;

namespace SoftwareRouteGuideAPI.Helpers.DBO
{
    public class DBO
    {
        private readonly IConfiguration _configuration;
        public DBO(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool EmailCheck(string email)
        {
            string connectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz

            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT Email FROM users WHERE Email = @Email"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@Email", email); //Parametreleri eşliyoruz
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım

                    while (dataReader.Read()) //Reader her veri okuduğunda;
                    {
                        if(email == dataReader["email"].ToString()){ //...okunulan veriyi parametre ile karşılaştırıyoruz ve eşleşirse;
                            dataReader.Close(); // Veri okumayı bitiriyoruz
                            mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                            return true; // True döndürüyoruz
                        }
                    }
                    // Hiçbir değer eşleşmezse;
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                    return false; // False döndürüyoruz
                }
            }
        }

        public bool SaveUserToken(string email, string token)
        {
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"UPDATE users SET token = @token WHERE Email = @Email"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@token", token); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@Email", email);

                    var insertResult = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (insertResult == 1)
                        return true;
                }
            }
            return false;
        }

        public bool SaveAdminToken(string email, string token)
        {
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"UPDATE admin SET token = @token WHERE Email = @Email"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@token", token); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@Email", email);

                    var insertResult = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (insertResult == 1)
                        return true;
                }
            }
            return false;
        }
        public bool CheckUserToken(string email, string token)
        {
            string connectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz

            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM users WHERE Email = @Email AND token = @token"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@Email", email); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@token", token); //Parametreleri eşliyoruz
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım

                    while (dataReader.Read()) //Reader her veri okuduğunda;
                    {
                        if (email == dataReader["email"].ToString())
                        { 
                            dataReader.Close(); // Veri okumayı bitiriyoruz
                            mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                            return true; // True döndürüyoruz
                        }
                    }
                    // Hiçbir değer eşleşmezse;
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    return false; // False döndürüyoruz
                }
            }
        }
    }
}
