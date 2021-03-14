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
    public interface IAlbumDataRepo
    {
        int AddAlbum(AlbumData model);
        int DeleteAlbum(int a_id);
         int editAlbum(AlbumData model);
         List<AlbumData> GetAllAlbum();
        AlbumData GetAlbumDetails(int aid);
        List<AlbumData> AllAlbum();

        AlbumData AlbumDetail(int album_id);
        
        int totalPrise(string user_email);
        int UpdatesellQuantity(int quantity, int album_id);

        List<CheckOutData> checkoutbarchart();
        List<CartData> sellalbumcount();
        
    }
 
    public class AlbumDataRepo : IAlbumDataRepo
    {
        ApplicationDbContext db;
        public AlbumDataRepo()
        {
            db = new ApplicationDbContext();
        }
        NpgsqlCommand cmd = null;

        
        public int AddAlbum(AlbumData model)
        {
            int i = 0;
            ///Add Database connections string Data 
            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();

            
            string query = "INSERT INTO public.mg_albumstore(al_img, a_artist, a_title,quantity,a_price)VALUES(@al_img,@a_artist, @a_title,@quantity,@a_price)";

            connection.ConnectionString = connections;
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.Add(new NpgsqlParameter("@al_img", model.al_img));
            cmd.Parameters.Add(new NpgsqlParameter("@a_artist", model.a_artist));
            cmd.Parameters.Add(new NpgsqlParameter("@a_title", model.a_title));
            cmd.Parameters.Add(new NpgsqlParameter("@quantity", model.quantity));
            cmd.Parameters.Add(new NpgsqlParameter("@a_price", model.a_price));
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return i;
        }
       
        public List<AlbumData> GetAllAlbum()
        {
            var user = db.AlbumDatas.ToList();
            return user;

        }
       

        public int DeleteAlbum(int a_id)
        {

            int i = 0;
            ///Add Database connections string Data 

            string query = "DELETE FROM public.mg_albumstore WHERE a_id=@a_id";
            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            connection.Open();
            //try
            //{
                cmd = new NpgsqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@a_id", a_id);


                i = cmd.ExecuteNonQuery();
            if(i >= 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

           
        
    }

        public int editAlbum(AlbumData model)
        {
            int i = 0;
            ///Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
           
            string query = "UPDATE public.mg_albumstore SET a_title=@a_title,a_price=@a_price,a_artist=@a_artist,quantity=@quantity WHERE a_id=@a_id";
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@a_title", model.a_title);
            cmd.Parameters.AddWithValue("@a_price", model.a_price);
            cmd.Parameters.AddWithValue("@a_id", model.a_id);
            cmd.Parameters.AddWithValue("@quantity", model.quantity);
            //cmd.Parameters.AddWithValue("@user_albumid", model.user_albumid);
            cmd.Parameters.AddWithValue("@a_artist", model.a_artist);
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return i;

        }
        //By Entity Framework(ApplicationDBContext)
        public AlbumData GetAlbumDetails(int nid)
        {
            AlbumData news = db.AlbumDatas.Where(x => x.a_id == nid).FirstOrDefault();
            return news;
        }
        //By ADO.Net
        public AlbumData AlbumDetailAdmin(int album_id)
        {
            //int i = 0;
            ///Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;



            AlbumData model = new AlbumData();
            string query = String.Format("Select * FROM mg_albumstore WHERE a_id={0}", album_id);
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.a_id = Convert.ToInt32(dr["a_id"]);
                        model.a_title = dr["a_title"].ToString();
                        model.al_img = dr["al_img"].ToString();
                        model.a_price = Convert.ToInt32(dr["a_price"]);
                        model.a_artist = dr["a_artist"].ToString();
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
            return model;
        }
        public AlbumData AlbumDetail(int album_id)
        {
            ///Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            AlbumData model = new AlbumData();
            string query = String.Format("Select * FROM mg_albumstore WHERE a_id={0}", album_id);
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.a_id = Convert.ToInt32(dr["a_id"]);
                        model.a_title = dr["a_title"].ToString();
                        model.al_img = dr["al_img"].ToString();
                        model.a_price = Convert.ToInt32(dr["a_price"]);
                        model.a_artist = dr["a_artist"].ToString();
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
            return model;
        }
        public List<AlbumData> AllAlbum()
        {
            //int i = 0;
            ///Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;


            List<AlbumData> photo = new List<AlbumData>();
            string query = "SELECT * FROM public.mg_albumstore";
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        AlbumData mod = new AlbumData();
                        mod.a_id = Convert.ToInt32(dr["a_id"]);
                        mod.al_img = dr["al_img"].ToString();
                        mod.a_title = dr["a_title"].ToString();
                        mod.a_price = Convert.ToInt32(dr["a_price"]);
                        mod.a_artist = dr["a_artist"].ToString();

                        mod.quantity = Convert.ToInt32(dr["quantity"]);
                        //mod.sellqua = Convert.ToInt32(dr["sellqua"]);
                        photo.Add(mod);
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
            return photo;
        }
        public int UpdatesellQuantity(int quantity, int album_id)
        {
            ///Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            string query = "UPDATE public.mg_albumstore SET sellqua=sellqua+@sellqua WHERE a_id=@a_id";
            connection.Open();
            cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@a_id", album_id);
            cmd.Parameters.AddWithValue("@sellqua", quantity);
            int i = 0;
            try
            {
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return i;
        }

        public int totalPrise(string u_email)
        {
            ///Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            int total_prise = 0;
            string query = "select sum(mg_albumstore.a_price * ab.cart_itemqua) as total_price from mg_albumstore left outer join \"mg_cartData\" ab on mg_albumstore.a_id = ab.a_id where u_id =(SELECT u_id FROM public.mg_datauser WHERE u_email=@u_email)";
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
                        total_prise = Convert.ToInt32(dr["total_price"]);
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
            return total_prise;
        }


       
        public List<CheckOutData> checkoutbarchart()
        {
            ///Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            List<CheckOutData> model = new List<CheckOutData>() ;
            connection.Open();
            string query = "select shipdate,order_num,totalprice from mg_checkoutdata";
            cmd = new NpgsqlCommand(query, connection);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CheckOutData mod = new CheckOutData();
                        mod.shipdate =dr["shipdate"].ToString();
                        //  model.order_num = Convert.ToInt32(dr["order_num"]);
                        mod.totalprice = Convert.ToInt32(dr["totalprice"]);
                        model.Add(mod);
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
            return model;
        }
       public  List<CartData> sellalbumcount()
        {
            ///Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            string[] colors = { "Yellow", "RED", "PINK", "BLUE", "GRAY", "Black", "White" };
            string query= "SELECT al.a_title, avg(ca.cart_itemqua) AS NumberOfOrders FROM \"mg_cartData\" ca LEFT JOIN mg_albumstore al  ON ca.a_id = al.a_id GROUP BY a_title";
           // string query = "select ca.a_id,ca.cart_itemqua,al.al_img,al.a_title from \"mg_cartData\" ca left outer join mg_albumstore al on ca.a_id=al.a_id;";
            //  select cart_itemqua, al.al_img from "mg_cartData" left outer join mg_albumstore al on "mg_cartData".a_id = al.a_id;
            List<CartData> mod = new List<CartData>();
            connection.Open();
       
            cmd = new NpgsqlCommand(query, connection);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    int i = 0;
                    while (dr.Read() && i < colors.Length)
                    {
                        CartData model = new CartData();
                        //model.al_img =dr["al_img"].ToString();
                       // model.u_id = Convert.ToInt32(dr["u_id"]);
                        model.a_title = dr["a_title"].ToString();
                        model.cart_itemqua = Convert.ToInt32(dr["NumberOfOrders"]);
                        model.Color = colors[i];
                        mod.Add(model);
                        i++;
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
            return mod;

        }


    }

}

