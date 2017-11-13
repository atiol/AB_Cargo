using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AB_CargoServices.Models
{
    public class Subscribers
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name should be maximumly 50 characters long")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class SubscribersDBHandler
    {
        private OracleConnection conn;
        public void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["AB_CargoDB"].ConnectionString;
            conn = new OracleConnection(constring);
        }

        public List<Subscribers> GetSubscribers()
        {
            Connection();
            List<Subscribers> MySubscribers = new List<Subscribers>();
            string query = "SELECT NAME, EMAIL FROM SUBSCRIBERS";
            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.CommandType = CommandType.Text;

            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                oda.Fill(dt);
                conn.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    MySubscribers.Add(
                        new Subscribers
                        {
                            Name = Convert.ToString(dr["NAME"]),
                            Email = Convert.ToString(dr["EMAIL"])
                        }
                    );
                }

                return MySubscribers;
            }
            catch
            {
                return MySubscribers = null;
            }
            finally
            {
                conn.Close();
            }
        }

        // Add new subscribers
        public bool CreateSubscriber(Subscribers subscriber)
        {
            Connection();
            string sql = "INSERT INTO SUBSCRIBERS(NAME, EMAIL) VALUES('" + subscriber.Name + "', '" + subscriber.Email + "')";
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        //Delete subscribers

        public bool DeleteSubscriber(string email)
        {
            Connection();
            string query = "DELETE FROM ATIOL.SUBSCRIBERS WHERE EMAIL = '"+email+"'";
            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.CommandType = CommandType.Text;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}