using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
//using MySql.Data;
//using MySql.Data.Types;
//using MySql.Data.MySqlClient;
using TH.DACClass;
using System.Data.SqlClient;

/// <summary>
/// HH DAC Layer
/// </summary>
/// 

namespace MVCSlider.DAC
{
    public class MSSQL_DAC
    {

        public string c_ConnectionString = "";
        public const string c_Error_Default = "";

        private string strError_Message;

        public MSSQL_DAC()
        {
            c_ConnectionString = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }



        public MSSQL_DAC(string pConnectionstring)
        {
            c_ConnectionString = pConnectionstring;
        }

        public string Error_Message
        {
            get
            {
                return strError_Message;
            }
            set
            {
                strError_Message = value;
            }
        }

        public static int SaveVisitor()
        {
           
            SqlConnection con = new SqlConnection(Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));
            SqlCommand command = new SqlCommand("[SetVisitor]", con);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Visitingtime", DateTime.Now);
            command.Parameters.Add("@TotalRowsCount", SqlDbType.Int);
            command.Parameters["@TotalRowsCount"].Direction = ParameterDirection.Output;
            command.Connection = con;
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            int TotalRowsCount = Convert.ToInt32(command.Parameters["@TotalRowsCount"].Value);
            
            return TotalRowsCount;
            
        }

        public string Get_ErrorString(string vError)
        {
            return c_Error_Default + vError.Replace("'", "`").Replace("(", "[").Replace(")", "]").Replace('\r', '-').Replace('\n', '-');
        }

        //---Fill DataRow By SQL Query
        public DataRow Read_Row_Query(string vQuery)
        {
            DataRow dr_1 = null;
            try
            {
                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;
                dr_1 = objC_DACClass.Get_DR_Qry(vQuery);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {

            }
            return dr_1;

        }

        //---Fill DataSet By SQL Query
        public DataSet Read_DS_Query(string vQuery)
        {
            DataSet SDataSet = new DataSet();
            try
            {
                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;
                SDataSet = objC_DACClass.Get_DS_Qry(vQuery);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {
            }
            return SDataSet;
        }

        //---Fill DataTable By SQL Query
        public DataTable Read_DT_Query(string vQuery)
        {
            DataTable SDataTable = new DataTable();
            try
            {
                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;
                SDataTable = objC_DACClass.Get_DT_Qry(vQuery);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {
            }
            return SDataTable;
        }

        //---Fill DataTable By Stored Procedure
        public DataTable Read_DT_Procedure(string vSPName, SqlCommand SCmd)
        {
            DataTable dt_1 = new DataTable();

            try
            {
                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;
                dt_1 = objC_DACClass.Get_DT_SP(vSPName, SCmd);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {
            }
            return dt_1;
        }

        //---Fill DataTable By Stored Procedure New
        public DataTable Read_DT_Procedure(string vSPName, List<KeyValuePair<string, object>> pParams = null)
        {
            DataTable dt_1 = new DataTable();

            try
            {
                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;

                SqlCommand SCmd = new SqlCommand();
                SCmd.CommandType = CommandType.StoredProcedure;
                SCmd.CommandText = vSPName;
                if (pParams != null)
                {
                    foreach (var parameter in pParams)
                    {
                        SCmd.Parameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
                    }
                }

                dt_1 = objC_DACClass.Get_DT_SP(vSPName, SCmd);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {
            }
            return dt_1;
        }

        //---Fill DataSet By Stored Procedure
        public DataSet Read_DS_Procedure(string vSPName, SqlCommand SCmd)
        {
            DataSet ds_1 = new DataSet();

            try
            {

                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;
                ds_1 = objC_DACClass.Get_DS_SP(vSPName, SCmd);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {
            }
            return ds_1;
        }

        //---Fill DataSet By Stored Procedure New
        public DataSet Read_DS_Procedure(string vSPName, List<KeyValuePair<string, object>> pParams = null)
        {
            DataSet ds_1 = new DataSet();

            try
            {

                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;
                SqlCommand SCmd = new SqlCommand();
                SCmd.CommandType = CommandType.StoredProcedure;
                SCmd.CommandText = vSPName;
                if (pParams != null)
                {
                    foreach (var parameter in pParams)
                    {
                        SCmd.Parameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
                    }
                }
                ds_1 = objC_DACClass.Get_DS_SP(vSPName, SCmd);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {
            }
            return ds_1;
        }

        //---Execute Parametered Stored Procedure 
        public int Execute_Procedure(string vSPName, SqlCommand SCmd)
        {
            int strRecAffected = 0;

            try
            {
                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;
                strRecAffected = objC_DACClass.Exec_Int_SP(vSPName, SCmd);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {
            }

            return strRecAffected;
        }

        //---Execute Parametered Stored Procedure new
        public int Execute_Procedure(string vSPName, List<KeyValuePair<string, object>> pParams = null)
        {
            int strRecAffected = 0;

            try
            {
                C_DACClass objC_DACClass = new C_DACClass();
                objC_DACClass.Connection_String = c_ConnectionString;
                SqlCommand SCmd = new SqlCommand();
                SCmd.CommandType = CommandType.StoredProcedure;
                SCmd.CommandText = vSPName;
                if (pParams != null)
                {
                    foreach (var parameter in pParams)
                    {
                        SCmd.Parameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
                    }
                }
                strRecAffected = objC_DACClass.Exec_Int_SP(vSPName, SCmd);
                strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            }
            catch (Exception ex)
            {
                strError_Message = Get_ErrorString(ex.Message);
            }
            finally
            {
            }

            return strRecAffected;
        }

        ////---Execute Parametered Stored Procedure new
        //public int Execute_Procedure(string vSPName, List<KeyValuePair<string, object>> pParams = null)
        //{
        //    int strRecAffected = 0;


        //    try
        //    {
        //        C_DACClass objC_DACClass = new C_DACClass();
        //        objC_DACClass.Connection_String = c_ConnectionString;
        //        SqlCommand SCmd = new SqlCommand();
        //        SCmd.CommandType = CommandType.StoredProcedure;
        //        SCmd.CommandText = vSPName;
        //        if (pParams != null)
        //        {
        //            foreach (var parameter in pParams)
        //            {
        //                SCmd.Parameters.Add(new SqlParameter("@" + parameter.Key, parameter.Value));
        //            }
        //        }
        //        strRecAffected = objC_DACClass.Exec_Int_SP(vSPName, SCmd);
        //        strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        strError_Message = Get_ErrorString(ex.Message);
        //    }
        //    finally
        //    {
        //    }

        //    return strRecAffected;
        //}

        //---Execute Parametered Stored Procedure with in BLL New
        public int Execute_Procedure_Continue(string vSPName, SqlCommand SCmd, SqlConnection sqlCon, SqlTransaction Tran)
        {
            int strRecAffected = 0;
            C_DACClass objC_DACClass = new C_DACClass();
            objC_DACClass.Connection_String = c_ConnectionString;
            strRecAffected = objC_DACClass.Exec_Int_Multi(vSPName, SCmd, sqlCon, Tran);
            strError_Message = Get_ErrorString(objC_DACClass.Error_Message);
            return strRecAffected;
        }

    }

    //public class MYSQL_DAC
    //{
    //    public string cn_ConnectionString { get; set; }

    //    public MYSQL_DAC()
    //    {
    //        cn_ConnectionString = Convert.ToString(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"]);
    //    }

    //    public DataSet getDataByQuery(string pSqlQuery)
    //    {
    //        DataSet ds_Ret = new DataSet();
    //        MySqlConnection clsmySqlConnection = new MySqlConnection(cn_ConnectionString);
    //        MySqlTransaction clsTran = null;
    //        try
    //        {
    //            clsmySqlConnection.Open();
    //            if (clsmySqlConnection.State == ConnectionState.Open)
    //            {
    //                clsTran = clsmySqlConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
    //                MySqlCommand clsCmd = new MySqlCommand(pSqlQuery, clsmySqlConnection, clsTran);
    //                MySqlDataAdapter clsDAdapter = new MySqlDataAdapter(clsCmd);
    //                clsDAdapter.Fill(ds_Ret);
    //                clsTran.Commit();
    //                clsmySqlConnection.Close();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            if (clsTran != null)
    //            {
    //                clsTran.Rollback();
    //            }
    //            if (clsmySqlConnection.State != ConnectionState.Closed)
    //            {
    //                clsmySqlConnection.Close();
    //            }

    //            throw ex;
    //        }

    //        return ds_Ret;
    //    }


    //}
}