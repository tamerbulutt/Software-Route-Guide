using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Model.Education;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Repositories.Concrete
{
    public class ComplaintRepository : IComplaintRepository
    {
        IConfiguration _configuration;
        private string sqlConnectionAddress;

        public ComplaintRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection");
        }

        public bool Add(Complaint entity)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"INSERT INTO complaints(Fullname,Email,Telephone,Message,date,userID) 
                                                VALUES (@Fullname,@Email,@Telephone,@Message,@date,@userID)"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@Fullname", entity.Fullname);
                    sqlCommand.Parameters.AddWithValue("@Email", entity.Email);
                    sqlCommand.Parameters.AddWithValue("@Telephone", entity.Telephone);
                    sqlCommand.Parameters.AddWithValue("@Message", entity.Message);
                    sqlCommand.Parameters.AddWithValue("@date", DateTime.Now);
                    sqlCommand.Parameters.AddWithValue("@userID", entity.userID);

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
                string query = @"DELETE FROM complaints WHERE complaintsID = @complaintsID;"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@complaintsID", id); //Parametreleri eşliyoruz
                    var result = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                    if (result == 1) return true;
                }
            }
            return false;
        }

        public List<Complaint> GetAll()
        {
            List<Complaint> complaintList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM complaints"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    complaintList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        complaintList.Add(new Complaint
                        {
                            complaintID = (int)dataReader["complaintsID"],
                            Fullname = dataReader["Fullname"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            Telephone = dataReader["Telephone"].ToString(),
                            Message = dataReader["Message"].ToString(),
                            date = (DateTime)dataReader["date"],
                            userID = (int)dataReader["userID"]
                        });
                    }
                    
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return complaintList;
        }

        public List<Complaint> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(Complaint entity)
        {
            throw new System.NotImplementedException();
        }
    }
}