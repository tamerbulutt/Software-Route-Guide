using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using SoftwareRouteGuideAPI.Model.DTOs;
using SoftwareRouteGuideAPI.Model.Education;
using SoftwareRouteGuideAPI.Repositories.Abstract;

namespace SoftwareRouteGuideAPI.Repositories.Concrete
{
    public class EducationRepository : IEducationRepository
    {
        private readonly IConfiguration _configuration;
        private string sqlConnectionAddress;
        public EducationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlConnectionAddress = _configuration.GetConnectionString("DatabaseConnection");
        }

        public bool Add(Education entity)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {

                string completedString = "";
                string completedStringValue = "";

                if (!string.IsNullOrEmpty(entity.Photo1))
                {
                    completedString += ",Photo1";
                    completedStringValue += ",@Photo1";
                }

                if (!string.IsNullOrEmpty(entity.Photo2))
                {
                    completedString += ",Photo2";
                    completedStringValue += ",@Photo2";

                }

                string query = $"INSERT INTO educations(Title,Type,Paragraph1,Paragraph2,Paragraph3,Subtitle1,Paragraph4,Paragraph5,Paragraph6,Paragraph7,Paragraph8,Subtitle2,Paragraph9,Paragraph10,userID{completedString}) VALUES (@Title,@Type,@Paragraph1,@Paragraph2,@Paragraph3,@Subtitle1,@Paragraph4,@Paragraph5,@Paragraph6,@Paragraph7,@Paragraph8,@Subtitle2,@Paragraph9,@Paragraph10,@userID{completedStringValue})"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {

                    sqlCommand.Parameters.AddWithValue("@Title", entity.Title); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@Type", entity.Type);

                    if (!string.IsNullOrEmpty(entity.Photo1))
                        sqlCommand.Parameters.AddWithValue("@Photo1", entity.Photo1);

                    sqlCommand.Parameters.AddWithValue("@Paragraph1", entity.Paragraph1);
                    sqlCommand.Parameters.AddWithValue("@Paragraph2", entity.Paragraph2);
                    sqlCommand.Parameters.AddWithValue("@Paragraph3", entity.Paragraph3);
                    sqlCommand.Parameters.AddWithValue("@Subtitle1", entity.Subtitle1);
                    sqlCommand.Parameters.AddWithValue("@Paragraph4", entity.Paragraph4);
                    sqlCommand.Parameters.AddWithValue("@Paragraph5", entity.Paragraph5);

                    if (!string.IsNullOrEmpty(entity.Photo2))
                        sqlCommand.Parameters.AddWithValue("@Photo2", entity.Photo2);

                    sqlCommand.Parameters.AddWithValue("@Paragraph6", entity.Paragraph6);
                    sqlCommand.Parameters.AddWithValue("@Paragraph7", entity.Paragraph7);
                    sqlCommand.Parameters.AddWithValue("@Paragraph8", entity.Paragraph8);
                    sqlCommand.Parameters.AddWithValue("@Subtitle2", entity.Subtitle2);
                    sqlCommand.Parameters.AddWithValue("@Paragraph9", entity.Paragraph9);
                    sqlCommand.Parameters.AddWithValue("@Paragraph10", entity.Paragraph10);
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
                string query = @"DELETE FROM educations WHERE educationID = @educationID;"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@educationID", id); //Parametreleri eşliyoruz
                    var result = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                    if (result == 1) return true;
                }
            }
            return false;
        }

        public bool Update(Education entity)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"UPDATE educations SET Title = @Title, Type = @Type, Photo1 = @Photo1, Paragraph1 = @Paragraph1, Paragraph2 = @Paragraph2,
                                Paragraph3 = @Paragraph3, Subtitle1 = @Subtitle1, Paragraph4 = @Paragraph4, Paragraph5 = @Paragraph5, Photo2 = @Photo2,
                                 Paragraph6 = @Paragraph6, Paragraph7 = @Paragraph7, Paragraph8 = @Paragraph8, Subtitle2 = @Subtitle2, Paragraph9 = @Paragraph9, Paragraph10 = @Paragraph10
                                WHERE educationID = @educationID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@Title", entity.Title); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@Type", entity.Type);
                    sqlCommand.Parameters.AddWithValue("@Photo1", entity.Photo1);
                    sqlCommand.Parameters.AddWithValue("@Paragraph1", entity.Paragraph1);
                    sqlCommand.Parameters.AddWithValue("@Paragraph2", entity.Paragraph2);
                    sqlCommand.Parameters.AddWithValue("@Paragraph3", entity.Paragraph3);
                    sqlCommand.Parameters.AddWithValue("@Subtitle1", entity.Subtitle1);
                    sqlCommand.Parameters.AddWithValue("@Paragraph4", entity.Paragraph4);
                    sqlCommand.Parameters.AddWithValue("@Paragraph5", entity.Paragraph5);
                    sqlCommand.Parameters.AddWithValue("@Photo2", entity.Photo2);
                    sqlCommand.Parameters.AddWithValue("@Paragraph6", entity.Paragraph6);
                    sqlCommand.Parameters.AddWithValue("@Paragraph7", entity.Paragraph7);
                    sqlCommand.Parameters.AddWithValue("@Paragraph8", entity.Paragraph8);
                    sqlCommand.Parameters.AddWithValue("@Subtitle2", entity.Subtitle2);
                    sqlCommand.Parameters.AddWithValue("@Paragraph9", entity.Paragraph9);
                    sqlCommand.Parameters.AddWithValue("@Paragraph10", entity.Paragraph10);
                    sqlCommand.Parameters.AddWithValue("@educationID", entity.educationID);

                    var insertResult = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                    if (insertResult == 1)
                        return true;
                }
            }
            return false;
        }

        public List<Education> GetAll()
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM educations INNER JOIN users ON educations.userID = users.userID WHERE status = @status"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@status", "Active"); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"]),
                            Title = dataReader["Title"].ToString(),
                            Date = (DateTime)dataReader["Date"],
                            Type = dataReader["Type"].ToString(),
                            Photo1 = dataReader["Photo1"].ToString(),
                            Paragraph1 = dataReader["Paragraph1"].ToString(),
                            Paragraph2 = dataReader["Paragraph2"].ToString(),
                            Paragraph3 = dataReader["Paragraph3"].ToString(),
                            Subtitle1 = dataReader["Subtitle1"].ToString(),
                            Paragraph4 = dataReader["Paragraph4"].ToString(),
                            Paragraph5 = dataReader["Paragraph5"].ToString(),
                            Photo2 = dataReader["Photo2"].ToString(),
                            Paragraph6 = dataReader["Paragraph6"].ToString(),
                            Paragraph7 = dataReader["Paragraph7"].ToString(),
                            Paragraph8 = dataReader["Paragraph8"].ToString(),
                            Subtitle2 = dataReader["Subtitle2"].ToString(),
                            Paragraph9 = dataReader["Paragraph9"].ToString(),
                            Paragraph10 = dataReader["Paragraph10"].ToString(),
                            userID = Convert.ToInt32(dataReader["userID"]),
                            status = dataReader["status"].ToString(),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString()
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return educationList;
        }

        public List<Education> GetById(int id)
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM educations INNER JOIN users ON educations.userID = users.userID WHERE educationID = @educationID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@educationID", id); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"]),
                            Title = dataReader["Title"].ToString(),
                            Date = (DateTime)dataReader["Date"],
                            Type = dataReader["Type"].ToString(),
                            Photo1 = dataReader["Photo1"].ToString(),
                            Paragraph1 = dataReader["Paragraph1"].ToString(),
                            Paragraph2 = dataReader["Paragraph2"].ToString(),
                            Paragraph3 = dataReader["Paragraph3"].ToString(),
                            Subtitle1 = dataReader["Subtitle1"].ToString(),
                            Paragraph4 = dataReader["Paragraph4"].ToString(),
                            Paragraph5 = dataReader["Paragraph5"].ToString(),
                            Photo2 = dataReader["Photo2"].ToString(),
                            Paragraph6 = dataReader["Paragraph6"].ToString(),
                            Paragraph7 = dataReader["Paragraph7"].ToString(),
                            Paragraph8 = dataReader["Paragraph8"].ToString(),
                            Subtitle2 = dataReader["Subtitle2"].ToString(),
                            Paragraph9 = dataReader["Paragraph9"].ToString(),
                            Paragraph10 = dataReader["Paragraph10"].ToString(),
                            userID = Convert.ToInt32(dataReader["userID"]),
                            status = dataReader["status"].ToString(),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString()
                        });
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return educationList;
        }

        public List<Education> GetByTeacherId(int id)
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM educations INNER JOIN users ON educations.userID = users.userID WHERE users.userID = @userID AND educations.status = 'Active' "; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@userID", id); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"]),
                            Title = dataReader["Title"].ToString(),
                            Date = (DateTime)dataReader["Date"],
                            Type = dataReader["Type"].ToString(),
                            Photo1 = dataReader["Photo1"].ToString(),
                            Paragraph1 = dataReader["Paragraph1"].ToString(),
                            Paragraph2 = dataReader["Paragraph2"].ToString(),
                            Paragraph3 = dataReader["Paragraph3"].ToString(),
                            Subtitle1 = dataReader["Subtitle1"].ToString(),
                            Paragraph4 = dataReader["Paragraph4"].ToString(),
                            Paragraph5 = dataReader["Paragraph5"].ToString(),
                            Photo2 = dataReader["Photo2"].ToString(),
                            Paragraph6 = dataReader["Paragraph6"].ToString(),
                            Paragraph7 = dataReader["Paragraph7"].ToString(),
                            Paragraph8 = dataReader["Paragraph8"].ToString(),
                            Subtitle2 = dataReader["Subtitle2"].ToString(),
                            Paragraph9 = dataReader["Paragraph9"].ToString(),
                            Paragraph10 = dataReader["Paragraph10"].ToString(),
                            userID = Convert.ToInt32(dataReader["userID"]),
                            status = dataReader["status"].ToString(),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString()
                        });
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return educationList;
        }

        public bool Suggest(Education entity)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string completedString = "";
                string completedStringValue = "";

                if (!string.IsNullOrEmpty(entity.Photo1))
                {
                    completedString += ",Photo1";
                    completedStringValue += ",@Photo1";
                }

                if (!string.IsNullOrEmpty(entity.Photo2))
                {
                    completedString += ",Photo2";
                    completedStringValue += ",@Photo2";

                }
                
                string query = $"INSERT INTO educations(Title,Type,Paragraph1,Paragraph2,Paragraph3,Subtitle1,Paragraph4,Paragraph5,Paragraph6,Paragraph7,Paragraph8,Subtitle2,Paragraph9,Paragraph10,userID,status{completedString}) VALUES (@Title,@Type,@Paragraph1,@Paragraph2,@Paragraph3,@Subtitle1,@Paragraph4,@Paragraph5,@Paragraph6,@Paragraph7,@Paragraph8,@Subtitle2,@Paragraph9,@Paragraph10,@userID,@status{completedStringValue})"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {

                    sqlCommand.Parameters.AddWithValue("@Title", entity.Title); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@Type", entity.Type);

                    if (!string.IsNullOrEmpty(entity.Photo1))
                        sqlCommand.Parameters.AddWithValue("@Photo1", entity.Photo1);

                    sqlCommand.Parameters.AddWithValue("@Paragraph1", entity.Paragraph1);
                    sqlCommand.Parameters.AddWithValue("@Paragraph2", entity.Paragraph2);
                    sqlCommand.Parameters.AddWithValue("@Paragraph3", entity.Paragraph3);
                    sqlCommand.Parameters.AddWithValue("@Subtitle1", entity.Subtitle1);
                    sqlCommand.Parameters.AddWithValue("@Paragraph4", entity.Paragraph4);
                    sqlCommand.Parameters.AddWithValue("@Paragraph5", entity.Paragraph5);

                    if (!string.IsNullOrEmpty(entity.Photo2))
                        sqlCommand.Parameters.AddWithValue("@Photo2", entity.Photo2);

                    sqlCommand.Parameters.AddWithValue("@Paragraph6", entity.Paragraph6);
                    sqlCommand.Parameters.AddWithValue("@Paragraph7", entity.Paragraph7);
                    sqlCommand.Parameters.AddWithValue("@Paragraph8", entity.Paragraph8);
                    sqlCommand.Parameters.AddWithValue("@Subtitle2", entity.Subtitle2);
                    sqlCommand.Parameters.AddWithValue("@Paragraph9", entity.Paragraph9);
                    sqlCommand.Parameters.AddWithValue("@Paragraph10", entity.Paragraph10);
                    sqlCommand.Parameters.AddWithValue("@userID", entity.userID);
                    sqlCommand.Parameters.AddWithValue("@status", "Proposal");

                    var executeCheck = sqlCommand.ExecuteNonQuery();

                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (executeCheck == 1) //Kayıt işlemi başarılıysa
                        return true;
                }
            }
            return false;
        }

        public List<Education> AllSuggests()
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM educations INNER JOIN users ON educations.userID = users.userID WHERE status = @status"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@status", "Proposal"); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"]),
                            Title = dataReader["Title"].ToString(),
                            Date = (DateTime)dataReader["Date"],
                            Type = dataReader["Type"].ToString(),
                            Photo1 = dataReader["Photo1"].ToString(),
                            Paragraph1 = dataReader["Paragraph1"].ToString(),
                            Paragraph2 = dataReader["Paragraph2"].ToString(),
                            Paragraph3 = dataReader["Paragraph3"].ToString(),
                            Subtitle1 = dataReader["Subtitle1"].ToString(),
                            Paragraph4 = dataReader["Paragraph4"].ToString(),
                            Paragraph5 = dataReader["Paragraph5"].ToString(),
                            Photo2 = dataReader["Photo2"].ToString(),
                            Paragraph6 = dataReader["Paragraph6"].ToString(),
                            Paragraph7 = dataReader["Paragraph7"].ToString(),
                            Paragraph8 = dataReader["Paragraph8"].ToString(),
                            Subtitle2 = dataReader["Subtitle2"].ToString(),
                            Paragraph9 = dataReader["Paragraph9"].ToString(),
                            Paragraph10 = dataReader["Paragraph10"].ToString(),
                            userID = Convert.ToInt32(dataReader["userID"]),
                            status = dataReader["status"].ToString(),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString()
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return educationList;
        }

        public bool ChangeSuggestStatus(SuggestStatusDto suggestStatus)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"UPDATE educations SET status = @status WHERE educationID = @educationID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@status", suggestStatus.status); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@educationID", suggestStatus.educationID);

                    var insertResult = sqlCommand.ExecuteNonQuery();
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz

                    if (insertResult == 1)
                        return true;
                }
            }

            return false;
        }

        public List<Education> GetLast2Education()
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM educations INNER JOIN users ON educations.userID = users.userID WHERE status = @status ORDER BY Date DESC LIMIT 2;"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@status", "Active"); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"]),
                            Title = dataReader["Title"].ToString(),
                            Type = dataReader["Type"].ToString(),
                            Photo1 = dataReader["Photo1"].ToString(),
                            Subtitle1 = dataReader["Subtitle1"].ToString(),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString(),
                            Date = (DateTime)dataReader["Date"]
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return educationList;
        }

        public List<Education> GetFirst6Education()
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM educations INNER JOIN users ON educations.userID = users.userID WHERE status = @status ORDER BY Date ASC LIMIT 6;"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@status", "Active"); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"]),
                            Date = (DateTime)dataReader["Date"],
                            Title = dataReader["Title"].ToString(),
                            Type = dataReader["Type"].ToString(),
                            Photo1 = dataReader["Photo1"].ToString(),
                            Subtitle1 = dataReader["Subtitle1"].ToString(),
                            Name = dataReader["Name"].ToString(),
                            Surname = dataReader["Surname"].ToString()
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return educationList;
        }

        public List<Education> UserEducations(int id)
        {
            List<Education> educationList;
            List<Education> educationList2;

            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM usereducations WHERE userID = @userID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@userID", id); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"]),
                            Date = (DateTime)dataReader["Date"]
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }

                string queryCompletedString = "";

                if (educationList.Count <= 0)
                    return educationList;

                if (educationList.Count > 0)
                {
                    for (var i = 1; i < educationList.Count; i++)
                    {
                        queryCompletedString += "OR educationID = @educationID" + i + " ";
                    }
                }

                string query2 = @"SELECT * FROM educations WHERE educationID = @educationID " + queryCompletedString; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query2, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    
                    sqlCommand.Parameters.AddWithValue("@educationID", educationList[0].educationID); //Parametreleri eşliyoruz
                    for (var count = 1; count < educationList.Count; count++)
                    {
                        sqlCommand.Parameters.AddWithValue("@educationID" + count, educationList[count].educationID); //Parametreleri eşliyoruz
                    }
                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList2 = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList2.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"]),
                            Title = dataReader["Title"].ToString(),
                            Subtitle1 = dataReader["Subtitle1"].ToString(),
                            Photo1 = dataReader["Photo1"].ToString(),
                            Date = (DateTime)dataReader["Date"]
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }

            return educationList2;
        }

        public int GetUserEducationsCount(int id)
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM usereducations WHERE userID = @userID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@userID", id); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"])
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return educationList.Count;
        }

        public int GetUserEducationsMonthlyCount(int id)
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM usereducations WHERE Date >= NOW() - INTERVAL 1 month AND userID = @userID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@userID", id); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            educationID = Convert.ToInt32(dataReader["educationID"])
                        });
                    }

                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            return educationList.Count;
        }

        public bool GetExamStatus(int userId, int educationId)
        {
            List<Education> educationList;
            using (MySqlConnection mySqlConnection = new MySqlConnection(sqlConnectionAddress)) //MySql bağlantısını sağlıyoruz
            {
                string query = @"SELECT * FROM usereducations WHERE userID = @userID AND educationID = @educationID"; //SQL SORGUSU
                mySqlConnection.Open(); //Bağlantıyı aktif ediyoruz

                using (MySqlCommand sqlCommand = new MySqlCommand(query, mySqlConnection)) //Sorgumuzu, bağlantı adresimize yolluyoruz
                {
                    sqlCommand.Parameters.AddWithValue("@userID", userId); //Parametreleri eşliyoruz
                    sqlCommand.Parameters.AddWithValue("@educationID", educationId); //Parametreleri eşliyoruz

                    var dataReader = sqlCommand.ExecuteReader(); //Veriyi okumamıza yarayan kısım
                    educationList = new(); //Cevap için oluşturulan list

                    while (dataReader.Read()) //Reader her veri okuduğunda userList adlı listeye ekleme yapıyoruz
                    {
                        educationList.Add(new Education
                        {
                            userID = Convert.ToInt32(dataReader["userID"])
                        });
                    }
                    dataReader.Close(); // Veri okumayı bitiriyoruz
                    mySqlConnection.Close(); // MySql Bağlantısını sonlandırıyoruz
                }
            }
            if (educationList.Count > 0)
                return true;
            
            return false;
        }

    }
}