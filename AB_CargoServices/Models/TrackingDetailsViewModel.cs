using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace AB_CargoServices.Models
{
    public class TrackingDetailsViewModel
    {
        public string SF_Name { get; set; }
        public string SL_Name { get; set; }
        public string RF_Name { get; set; }
        public string RL_Name { get; set; }
        public string CargoStatus { get; set; }
        public DateTime Sent { get; set; }
        public string Description { get; set; }

    }

    public class TrackerDb
    {
        // Database connection setup
        private OracleConnection conn;
        public void Connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["AB_CargoDB"].ConnectionString;
            conn = new OracleConnection(constring);
        }

        

        public TrackingDetailsViewModel TrackingInfo(decimal trackingId)
        {
            Connection();
            TrackingDetailsViewModel TrackInfo = new TrackingDetailsViewModel();
            string queryStr =
                "SELECT S.FIRST_NAME, S.LAST_NAME, R.FIRST_NAME, R.LAST_NAME, CS.MESSAGE, C.SENT_DATE, C.DESCRIPTION " +
                "FROM SENDER S, RECEIVER R, CARGO C, CARGO_STATUS CS " +
                "WHERE C.TRACKING_NO = " + trackingId + " AND C.SENDER_ID = S.S_ID AND R.SENDER_ID = S.S_ID AND C.STATUS_ID = CS.STATUS_ID";
            
            OracleCommand cmd = new OracleCommand(queryStr, conn);
            cmd.CommandType = CommandType.Text;

            //OracleCommand cmd = new OracleCommand("GET_TRACK_INFO", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("TRACKINGID_", OracleDbType.Int64, ParameterDirection.Input).Value = trackingId;
            //cmd.Parameters.Add("SENDER_FN", OracleDbType.Varchar2, ParameterDirection.Output);
            //cmd.Parameters.Add("SENDER_LN", OracleDbType.Varchar2, ParameterDirection.Output);
            //cmd.Parameters.Add("RECEIVER_FN", OracleDbType.Varchar2, ParameterDirection.Output);
            //cmd.Parameters.Add("RECEIVER_LN", OracleDbType.Varchar2, ParameterDirection.Output);
            //cmd.Parameters.Add("CARGO_STATUS", OracleDbType.Varchar2, ParameterDirection.Output);
            //cmd.Parameters.Add("SENT_", OracleDbType.Date, ParameterDirection.Output);
            //cmd.Parameters.Add("DESCRIPTION_", OracleDbType.Varchar2, ParameterDirection.Output);

            OracleDataAdapter oda = new OracleDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                //cmd.ExecuteNonQuery();
                oda.Fill(dt);
                conn.Close();

                //TrackInfo.SF_Name = cmd.Parameters["SENDER_FN"].Value.ToString();
                //TrackInfo.SL_Name = cmd.Parameters["SENDER_LN"].Value.ToString();
                //TrackInfo.RF_Name = cmd.Parameters["RECEIVER_FN"].Value.ToString();
                //TrackInfo.RL_Name = cmd.Parameters["RECEIVER_LN"].Value.ToString();
                //TrackInfo.CargoStatus = cmd.Parameters["CARGO_STATUS"].Value.ToString();
                //TrackInfo.Sent = Convert.ToDateTime(cmd.Parameters["SENT_"].Value);
                //TrackInfo.Description = cmd.Parameters["DESCRIPTION_"].Value.ToString();



                if (dt.Rows.Count >= 1)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TrackInfo.SF_Name = dr[0].ToString();
                        TrackInfo.SL_Name = dr[1].ToString();
                        TrackInfo.RF_Name = dr[2].ToString();
                        TrackInfo.RL_Name = dr[3].ToString();
                        TrackInfo.CargoStatus = dr[4].ToString();
                        TrackInfo.Sent = Convert.ToDateTime(dr[5]);
                        TrackInfo.Description = dr[6].ToString();
                    }
                    return TrackInfo;
                }
                else
                {
                    return TrackInfo = null;
                }
                
            }
            catch
            {
                return TrackInfo = null;
            }
            finally
            {
               if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}