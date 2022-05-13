using System.Collections.Generic;
using SoftwareRouteGuideAPI.Model.Identity;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Repositories.Abstract;
using System;
using SoftwareRouteGuideAPI.Model.DTOs;

namespace SoftwareRouteGuideAPI.Repositories.Concrete
{
    public class UserRepository:IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration){
            _configuration = configuration;
        }
         
        public bool Add(User user)
        {
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz

            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"INSERT INTO users(name,surname,email,Telephone,password,registerDate,userRoleID) VALUES (@name,@surname,@email,@Telephone,@password,@registerDate,@userRoleID)"; //SQL SORGUSU

                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@name", user.firstName); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@surname", user.lastName);
                    sqlCommand.Parameters.AddWithValue("@email", user.email);
                    sqlCommand.Parameters.AddWithValue("@Telephone", user.Telephone);
                    sqlCommand.Parameters.AddWithValue("@password", user.password);
                    sqlCommand.Parameters.AddWithValue("@registerDate", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@userRoleID", 0);

                    var executeCheck = sqlCommand.ExecuteNonQuery();

                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (executeCheck == 1) //Kayıt işlemi başarılıysa
                        return true;
                }
            }
            return false;
        }

        public bool Update(User entity){
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"UPDATE users SET name = @name, surname = @surname, email = @email, password = @password, userRoleID = @userRoleID WHERE userID = @user_id"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@user_id", entity.user_id); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@name", entity.firstName);
                    sqlCommand.Parameters.AddWithValue("@surname", entity.lastName);
                    sqlCommand.Parameters.AddWithValue("@email", entity.email);
                    sqlCommand.Parameters.AddWithValue("@password", entity.password);
                    sqlCommand.Parameters.AddWithValue("@userRoleID", entity.userRoleId);
                    var insertResult = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                    if (insertResult == 1)
                        return true;
                }
            }
            return false;
        }

        public bool Delete(int id){
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"DELETE FROM users WHERE userID = @user_id;"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@user_id", id); //Parametreleri eşliyoruz
                    var insertResult = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                    if (insertResult == 1)
                        return true;
                }
            }
            return false;
        }

        public List<User> GetAll(){
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz
            List<User> userList;

            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM users"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    userList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        userList.Add(new User
                        {
                            user_id = (int)dataReader["userID"],
                            firstName = dataReader["name"].ToString(),
                            lastName = dataReader["surname"].ToString(),
                            email = dataReader["email"].ToString(),
                            password = dataReader["password"].ToString(),
                            registerDate = (DateTime)dataReader["registerDate"],
                            userRoleId = (int)dataReader["userRoleID"]
                        });
                    }
                    
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return userList;
        }

        public List<User> GetById(int id){
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz
            List<User> userList;
            
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM users WHERE userID = @user_id"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@user_id", id); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    userList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        userList.Add(new User{
                            user_id = (int)dataReader["userID"],
                            firstName = dataReader["name"].ToString(),
                            lastName = dataReader["surname"].ToString(),
                            email = dataReader["email"].ToString(),
                            password = dataReader["password"].ToString(),
                            registerDate = (DateTime)dataReader["registerDate"],
                            userRoleId = (int)dataReader["userRoleID"]
                        });
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return userList;
        }

        public bool UserLogin(User user)
        {
            string connectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz

            using (MySqlConnection mySqlConnection = new MySqlConnection(connectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM users WHERE Email = @Email AND Password = @Password"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@Email", user.email); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@Password", user.password); //Parametreleri eşliyoruz
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım

                    while (dataReader.Read()) //Reader her veri okuduğunda;
                    {
                        if (user.email == dataReader["email"].ToString())
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

        public User getByEmail(string email)
        {
            string sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection"); //Bağlantı için mysql adresini çekiyoruz
            User user;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM users WHERE email = @email"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@email", email); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    user = new();
                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        user.user_id = (int)dataReader["userID"];
                        user.firstName = dataReader["name"].ToString();
                        user.lastName = dataReader["surname"].ToString();
                        user.email = dataReader["email"].ToString();
                        user.password = dataReader["password"].ToString();
                        user.token = dataReader["token"].ToString();
                        user.registerDate = (DateTime)dataReader["registerDate"];
                        user.userRoleId = (int)dataReader["userRoleID"];
                        user.userType = dataReader["userType"].ToString();
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            if(user.user_id > 0) return user;
            else return null;
        }
    }
}