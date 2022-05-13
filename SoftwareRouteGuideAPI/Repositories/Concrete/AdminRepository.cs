using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Model.Admin;
using SoftwareRouteGuideAPI.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareRouteGuideAPI.Repositories.Concrete
{
    public class AdminRepository : IAdminRepository
    {

        private readonly IConfiguration _configuration;

        public AdminRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool AdminLogin(Admin admin)
        {
            string connectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz

            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM admin WHERE Email = @Email AND Password = @Password"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@Email", admin.Email); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@Password", admin.Password); //Parametreleri eşliyoruz
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım

                    while (dataReader.Read()) //Reader her veri okuduğunda;
                    {
                        if (admin.Email == dataReader["email"].ToString())
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

        public Admin getByEmail(string email)
        {
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz
            Admin admin = new();
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM admin WHERE email = @email"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@email", email); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    
                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        admin.adminID = (int)dataReader["adminID"];
                        admin.Name = dataReader["Name"].ToString();
                        admin.Surname = dataReader["Surname"].ToString();
                        admin.Email = dataReader["Email"].ToString();
                        admin.token = dataReader["token"].ToString();
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            if (admin.adminID > 0) return admin;
            else return null;
        }
    }
}
