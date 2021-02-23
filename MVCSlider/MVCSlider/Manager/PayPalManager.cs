using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCSlider.DAC;
using System.Data;
using MVCSlider.Models;
using MVCSlider.App_Code;

namespace MVCSlider.Manager
{
    public class PayPalManager
    {


        #region DAC

        public int CreatePayPalInvoice(PayPalInvoiceModel payPalInvoiceModel)
        {

            var clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("InvoiceGUID",payPalInvoiceModel.InvoiceGUID),
                    new KeyValuePair<string,object>("QuestionID",payPalInvoiceModel.QuestionID),
                    new KeyValuePair<string,object>("PaymentID",payPalInvoiceModel.PaymentID),
                    new KeyValuePair<string,object>("RequestXML",payPalInvoiceModel.RequestXML),
                    };

            return clsDAC.Execute_Procedure("sp_SavePayPalInvoice", lstData);
        }

        public int LogException(string InvoiceGUID, string ExceptionXML)
        {

            var clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("InvoiceGUID",InvoiceGUID),
                    new KeyValuePair<string,object>("QuestionID",ExceptionXML)
                    };

            return clsDAC.Execute_Procedure("sp_SavePayPalExecutePaymentException", lstData);
        }

        public PayPalInvoiceModel GetPayPalInvoiceByGUID(string invoiceGUID)
        {
            DataTable dt_1 = null;
            MSSQL_DAC clsDAC = new MSSQL_DAC();
            PayPalInvoiceModel clsSubjectRet = null;

            dt_1 = clsDAC.Read_DT_Procedure("sp_GetPayPalInvoiceByGUID", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("InvoiceGUID", invoiceGUID) });

            if (dt_1 != null || dt_1.Rows.Count > 0)
            {
                DataRow dr_1 = dt_1.Rows[0];

                clsSubjectRet = new PayPalInvoiceModel()
                {
                    InvoiceGUID = Convert.ToString(dr_1["InvoiceGUID"]),
                    PayPalInvoiceID = (dr_1["PayPalInvoiceID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["PayPalInvoiceID"])),
                    QuestionID = (dr_1["QuestionID"] == DBNull.Value ? 0 : Convert.ToInt32(dr_1["QuestionID"])),
                    RequestXML = Convert.ToString(dr_1["RequestXML"]),
                    PaymentID = Convert.ToString(dr_1["PaymentID"]),
                    ResponseXML = Convert.ToString(dr_1["ResponseXML"]),
                    State = Convert.ToString(dr_1["State"]),
                };
            }
            return clsSubjectRet;
        }

        public int UpdatePayPalInvoice(PayPalInvoiceModel payPalInvoiceModel)
        {

            var clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("InvoiceGUID",payPalInvoiceModel.InvoiceGUID),
                    new KeyValuePair<string,object>("State",payPalInvoiceModel.State),
                    new KeyValuePair<string,object>("ResponseXML",payPalInvoiceModel.ResponseXML),
                    };

            return clsDAC.Execute_Procedure("sp_UpdatePayPalInvoice", lstData);
        }
        public int DeletePayPalInvoice(string TID)
        {

            var clsDAC = new MSSQL_DAC();

            List<KeyValuePair<string, object>> lstData = new List<KeyValuePair<string, object>>()
                    {
                    new KeyValuePair<string,object>("InvoiceGUID",TID)                  
                    };

            return clsDAC.Execute_Procedure("sp_DeletePayPalInvoice", lstData);
        }
        #endregion DAC
    }
}