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
    // Tracking info page model
    public class CargoViewModel
    {
        public float Weight { get; set; }
        public float Volume { get; set; }
        [Display(Name = "Sender's National ID or Drivers Licence")]
        public long SenderID { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public CargoViewModel(float weight, float volume, long senderid, string description)
        {
            Volume = volume;
            Weight = weight;
            SenderID = senderid;
            Description = description;
        }
    }

    public class Cities
    {
        public string CityName { get; set; }
    }

    public class CargoDbHandler
    {
        // Database connection setup
        private OracleConnection conn;
        public void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["AB_CargoDB"].ConnectionString;
            conn = new OracleConnection(constring);
        }

        // retrieve cities list
        public List<Cities> GetCities()
        {
            Connection();
            List<Cities> myCities = new List<Cities>();

            string queryString = "SELECT * FROM GET_CITIES";
            OracleCommand cmd = new OracleCommand(queryString, conn);
            cmd.CommandType = CommandType.Text;

            DataTable dt = new DataTable();
            OracleDataAdapter oda = new OracleDataAdapter(cmd);

            try
            {
                conn.Open();
                oda.Fill(dt);
                conn.Close();

                foreach(DataRow dr in dt.Rows)
                {
                    myCities.Add(new Cities { CityName = dr["CITY_NAME"].ToString()});
                }

                return myCities;
            }
            catch
            {
                return myCities = null;
            }
            finally
            {
                conn.Close();
            }
        }

        //Add new Cargo
        public string AddNewCargo(CargoViewModel cargo)
        {
            Connection();
            OracleCommand cmd = new OracleCommand("ADD_CARGO", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("WEIGHT_", OracleDbType.Decimal).Value = cargo.Weight;
            cmd.Parameters.Add("VOLUME_", OracleDbType.Decimal).Value = cargo.Volume;
            cmd.Parameters.Add("SENDERID_", OracleDbType.Long).Value = cargo.SenderID;
            cmd.Parameters.Add("DESCRIPTION_", OracleDbType.Varchar2).Value = cargo.Description;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return "success";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
    }
    
    public class CargoDetailsViewModel
    {
        [Display(Name = "First Name")]
        public string S_FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string S_LastName { get; set; }
        [Display(Name = "First Name")]
        public string R_FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string R_LastName { get; set; }
        [Display(Name = "Cargo Status")]
        public string CargoStatus { get; set; }
    }

    public class CargoDetailsDBHandler
    {
        private OracleConnection conn;
        public void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["AB_CargoDB"].ConnectionString;
            conn = new OracleConnection(constring);
        }

        // GET : Get cargo tracking details
        public List<CargoDetailsViewModel> GetTrackingInfo(Int64 tracking_number)
        {
            Connection();
            List<CargoDetailsViewModel> trackingInfo = new List<CargoDetailsViewModel>();
            string queryStr = 
                "SELECT S.FIRST_NAME AS SENDER_FN, S.LAST_NAME AS SENDER_LN, R.FIRST_NAME RECEIVER_FN, R.LAST_NAME AS RECEIVER_LN, CS.MESSAGE AS CARGO_STATUS "+
                "FROM SENDER S, RECEIVER R, CARGO C, CARGO_STATUS CS, C.SENT_DATE, C.DESCRIPTION " + 
                "WHERE C.TRACKING_NO = "+tracking_number+" AND C.SENDER_ID = S.S_ID AND R.SENDER_ID = S.S_ID AND C.STATUS_ID = CS.STATUS_ID";

            OracleCommand cmd = new OracleCommand(queryStr, conn);
            cmd.CommandType = CommandType.Text;

            //cmd.Parameters.Add("TRACKINGID_", OracleDbType.Int64).Value = tracking_number;

            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                oda.Fill(dt);
                conn.Close();

                if(dt.Rows.Count > 0 )
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        trackingInfo.Add(
                                new CargoDetailsViewModel
                                {
                                    S_FirstName = dr["SENDER_FN"].ToString(),
                                    S_LastName = dr["SENDER_LN"].ToString(),
                                    R_FirstName = dr["RECEIVER_FN"].ToString(),
                                    R_LastName = dr["RECEIVER_LN"].ToString(),
                                    CargoStatus = dr["CARGO_STATUS"].ToString()
                                }
                            );
                    }

                    return trackingInfo;
                }
                else
                {
                    return trackingInfo = null;
                }
            }
            catch
            {
                return trackingInfo = null;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}