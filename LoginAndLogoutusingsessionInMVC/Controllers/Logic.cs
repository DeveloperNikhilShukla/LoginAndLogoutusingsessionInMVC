using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace OnlineMedicalShopingApplication.Controllers
{
    public class Logic
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);


        public void OpenCoonection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

        }
        public void CloseCoonection()
        {
            con.Close();
        }
        public int InsertUpdateCommand(string Query)
        {
            int i = 0;
            try
            {
                OpenCoonection();
                SqlCommand cmd = new SqlCommand(Query, con);
                i = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                i = 0;
            }
            return i;

        }

        public DataSet GetRecordWithDataset(string query)
        {
            DataSet dt = new DataSet();
            try
            {

                OpenCoonection();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
            return dt;
        }


        public DataSet ExcecuteProcedureWithDataset(string ProcedureName, object[] ParameterList)
        {
            string s = "";
            string query = "";
            int i = 0;
            try
            {

                if (ParameterList != null)
                {
                    if (ParameterList.Count() > 0)
                    {
                        foreach (var item in ParameterList)
                        {
                            Type data = item.GetType();
                            if (i == 0)
                            {
                                s = data.FullName == "System.String" || data.FullName == "System.DateTime" ? "'" + item.ToString() + "'" : item.ToString();
                                i = i + 1;
                            }
                            else
                            {
                                s = s + "," + (data.FullName == "System.String" || data.FullName == "System.DateTime" ? "'" + item.ToString() + "'" : item.ToString());
                                i = i + 1;
                            }

                        }
                    }
                }
                if (ParameterList != null)
                {
                    if (ParameterList.Count() > 0)
                    {
                        query = "exec " + ProcedureName + " " + s;
                    }
                    else
                    {
                        query = "exec " + ProcedureName;
                    }
                }
                else
                {
                    query = "exec " + ProcedureName;
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }


            return GetRecordWithDataset(query);
        }



     


     
    }
    }
