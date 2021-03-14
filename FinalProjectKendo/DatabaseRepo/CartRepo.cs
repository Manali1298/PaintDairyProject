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
    public interface ICartRepo
    {
        int addCart(int photo_id, int id, int quantity);
        List<CartData> ListCart(int u_id);
    }
    public class CartRepo:ICartRepo
    {

        public int addCart(int photo_id, int id, int quantity)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            string query = "INSERT INTO \"mg_cartData\" (u_id,a_id,cart_itemqua) VALUES(@u_id,@a_id,@cart_itemqua)";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_id", id);
            cmd.Parameters.AddWithValue("@a_id", photo_id);
            cmd.Parameters.AddWithValue("@cart_itemqua", quantity);
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
        public int IncreeaseCartItem(int u_id)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            string query = "UPDATE \"mg_cartData\" SET cart_itemqua=cart_itemqua+1 WHERE u_id=@u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_id", u_id);
            //cmd.Parameters.AddWithValue("@a_id", a_id);
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

        public int DecreesCartItem(int u_id, int photo_id)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            string query = "UPDATE \"mg_cartData\" SET cart_itemqua=cart_itemqua-1 WHERE a_id=@a_id AND u_id=@u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@a_id", photo_id);
            cmd.Parameters.AddWithValue("@u_id", u_id);
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
        public int totalPrise(int u_id, int photo_id)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            AlbumDatabase mod = new AlbumDatabase();
            string query = "select sum(mg_albumstore.a_price*mg_cartData.cart_itemqua) as total_prise from mg_albumstore left outer join mg_cartData on mg_albumstore.a_id=mg_cartData.a_id where u_id=@u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_id", u_id);
            cmd.Parameters.AddWithValue("@a_id", photo_id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        mod.a_price = Convert.ToInt32(dr["total_prise"]);
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
            return mod.a_price;
        }
        public int updateprise(int photo_id, int u_id)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            string query = "UPDATE \"mg_cartData\" SET cart_itemqua=cart_itemqua+1 WHERE a_id=@a_id AND u_id=@u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@a_id", photo_id);
            cmd.Parameters.AddWithValue("@u_id", u_id);
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
        public AlbumDatabase detail(int photo_id, int u_id)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            AlbumDatabase mod = new AlbumDatabase();
            string query = "select * from \"mg_cartData\" where a_id=@a_id and u_id=@u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@a_id", photo_id);
            cmd.Parameters.AddWithValue("@u_id", u_id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        mod.cart_itemqua = Convert.ToInt32(dr["cart_itemqua"]);
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

        public List<CartData> ListCart(int u_id)
        {
            //Add Database connections string Data 

            List<CartData> list = new List<CartData>();
            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
         
            string query = "select mg_albumstore.a_id,mg_albumstore.al_img,mg_albumstore.a_title,mg_albumstore.a_price,mg_albumstore.a_id,ob.cart_itemqua from mg_albumstore left Outer join  \"mg_cartData\" ob on mg_albumstore.a_id = ob.a_id where u_id =(select u_id from mg_datauser where u_id=@u_id)";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@a_id", photo_id);
            cmd.Parameters.AddWithValue("@u_id", u_id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        //mod.cart_itemqua = Convert.ToInt32(dr["cart_itemqua"]);
                        CartData model = new CartData();
                        model.a_id = Convert.ToInt32(dr["a_id"]);
                        model.al_img = dr["al_img"].ToString();
                        model.a_title = dr["a_title"].ToString();
                        model.a_price= Convert.ToInt32(dr["a_price"]);
                        model.cart_itemqua= Convert.ToInt32(dr["cart_itemqua"]);
                        list.Add(model);
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
            return list;
        }
        public int removeItem(int id, int photo_id)
        {
            //Add Database connections string Data 

            List<CartData> list = new List<CartData>();
            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            int i = 0;
            string query = "DELETE FROM \"mg_cartData\" WHERE a_id = @a_id AND u_id=@u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@a_id", photo_id);
            cmd.Parameters.AddWithValue("@u_id", id);
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
        public int getCartId(int id)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            CartData mod = new CartData();
            string query = "SELECT cart_id FROM public.mg_cart WHERE u_id=@u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_id", id);
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        mod.cart_id = Convert.ToInt32(dr["cart_id"]);
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
            return mod.cart_id;
        }
        public int CheckOut(CheckOutData model)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            string query = "INSERT INTO public.mg_checkoutdata(firstnm, lastnm, address, state, postalcode, mobile, email, shipdate, totalprice, u_id, order_num) VALUES(@firstnm,@lastnm,@address,@state,@postalcode,@mobile,@email,@shipdate,@totalprice, @u_id,@order_num)";
            connection.Open();
            DateTime date = Convert.ToDateTime(model.shipdate);
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@firstnm", model.firstnm);
            cmd.Parameters.AddWithValue("@lastnm", model.lastnm);
            cmd.Parameters.AddWithValue("@address", model.address);
            cmd.Parameters.AddWithValue("@state", model.state);
            cmd.Parameters.AddWithValue("@postalcode", model.postalcode);
            cmd.Parameters.AddWithValue("@mobile", model.mobile);
            cmd.Parameters.AddWithValue("@email", model.email);
            cmd.Parameters.AddWithValue("@shipdate", date);
            cmd.Parameters.AddWithValue("@totalprice", model.totalprice);
            cmd.Parameters.AddWithValue("@u_id", model.u_id);
            //cmd.Parameters.AddWithValue("@cart_id", model.cart_id);
            cmd.Parameters.AddWithValue("@order_num", model.order_num);
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
        public CheckOutData OrderDetails(int u_id)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            string query = "SELECT order_num,totalprice FROM public.mg_checkoutdata WHERE u_id=@u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_id", u_id);
            CheckOutData model = new CheckOutData();
            try
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.order_num = Convert.ToInt32(dr["order_num"]);
                        model.totalprice = Convert.ToInt32(dr["totalprice"]);
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

        public int DeleteAllCartItem(int u_id)
        {
            //Add Database connections string Data 

            String connections = ("Server=;Port=;Database=;User Id=;Password=");
            NpgsqlConnection connection = new NpgsqlConnection();
            connection.ConnectionString = connections;
            int i = 0;
            string query = "DELETE FROM \"mg_cartData\" WHERE u_id = @u_id";
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@u_id", u_id);
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



    }
}