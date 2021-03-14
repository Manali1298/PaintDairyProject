using FinalProjectKendo.DataContext;
using FinalProjectKendo.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//.............................................
//Add Database connections string Data 
//.............................................

namespace FinalProjectKendo.DatabaseRepo
{
    public interface IUserProfileRepo
    {
        int RegisterUser(UserData model);
        bool signIn(string u_email, string u_password);
        List<UserData> GetAllUser();
        int GetUserId(string u_email);


    }
        public class UserDataRepo: IUserProfileRepo
    {
        ApplicationDbContext db;
        NpgsqlCommand cmd = null;
        public UserDataRepo()
        {
            db = new ApplicationDbContext();
        }

        //code for Registration
        public int RegisterUser(UserData model)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            connection.Open();
            string query = "INSERT INTO public.mg_datauser(u_name, u_email, u_password) VALUES(@u_name,@u_email,@u_password)";
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_name", model.u_name);
            cmd.Parameters.AddWithValue("@u_email", model.u_email);
            cmd.Parameters.AddWithValue("@u_password", model.u_password);
            int i = 0;
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return i;
        }
        //code for login
        public bool signIn(string u_email, string u_password)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
        
            string query = "SELECT u_email,u_password FROM public.mg_datauser where u_email=@u_email AND u_password=@u_password";
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_email", u_email);
            cmd.Parameters.AddWithValue("@u_password", u_password);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return false;
        }
        //code for getalluser
        public List<UserData> GetAllUser()
        {
            var user = db.UserInfos.ToList();
            return user;

        }
        //public int GetUserId(string email)
        //{
        //    var data = db.UserInfos.Where(x => x.email == email).FirstOrDefault();
        //    if (data != null)
        //    {
        //        return data.uid;
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}
        public UserData GetUserDetails(int nid)
        {
            UserData news = db.UserInfos.Where(x => x.u_id == nid).FirstOrDefault();
            return news;
        }
        public int GetUserId(string u_email)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
          
            int id = 0;
            string query = "SELECT  u_id FROM public.mg_datauser WHERE u_email=@u_email";
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_email", u_email);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        id = Convert.ToInt32(dr["u_id"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return id;
        }

    }
}
