using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bebach.Reports
{
    public partial class Izvjesca : System.Web.UI.Page
    {
        DateTime odd, dod;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
            }

            if (txtOd.Text == string.Empty)
            { odd = DateTime.Now.AddYears(-1); }
            else
            {
                odd = Convert.ToDateTime(txtOd.Text);
            }
            if (txtDo.Text == string.Empty)
            {
                dod = DateTime.Now.AddYears(1);
            }
            else
            {
                dod = Convert.ToDateTime(txtDo.Text);
            }
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            int beba = 1;
            switch (ddlVrsta.SelectedIndex)
            {
                case 1:
                    PokaziReport(beba, odd, dod, "spPreglediPoDatumu", Server.MapPath(@"~\Reports\rptPregled.rdlc"));
                    break;
                case 2:
                    PokaziReport(beba, odd, dod, "spAktivnostiPoDatumu", Server.MapPath(@"~\Reports\rptAktivnost.rdlc"));

                    break;


                default:
                    break;
            }

        }

        private void PokaziReport(int bebaID, DateTime odd, DateTime dod, string proc, string putanja)
        {


            ReportViewer1.LocalReport.DataSources.Clear();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ErrorLog"].ConnectionString;
            SqlCommand cmd = new SqlCommand("dbo." + proc, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OD", odd);
            cmd.Parameters.AddWithValue("@DO", dod);
            cmd.Parameters.AddWithValue("@BebaID", bebaID);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();

            adpt.Fill(dataSet, "Bebe");
            ReportDataSource repDs = new ReportDataSource("DataSet1", dataSet.Tables[0]);
            if (proc.Contains("spAktivnostiPoDatumu"))
            {
                repDs = new ReportDataSource("dsAkt", dataSet.Tables[0]);
            }
            else
            {
                repDs = new ReportDataSource("dsPre", dataSet.Tables[0]);
            }


            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = putanja;

            // set param values, not really doing anything except showing up in the report,
            //ovo će trebati za prikaz reporta da se vide datumi razdublja, još treba na rep. vieweru ShowParameterPrompts="true"
            string paraOd = odd.ToShortDateString();
            string paraDo = dod.ToShortDateString();
           
            ReportParameter[] param = new ReportParameter[3];
            param[0] = new ReportParameter("paraOd", paraOd, false);
            param[1] = new ReportParameter("paraDo", paraDo, false);
            param[2] = new ReportParameter("BebaID", bebaID.ToString(), false);
            this.ReportViewer1.LocalReport.SetParameters(param);

            ReportViewer1.LocalReport.DataSources.Add(repDs);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}