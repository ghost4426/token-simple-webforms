using Model.Models;
using Service.Services;
using System;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Etherscan
{
    public partial class _Default : Page
    {

        private ITokenService _tokenService;
        private Random rnd = new Random();

        public _Default()
        {
        }

        public _Default(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                refeshChartAndGrid();
            }
        }


        private void Bindchart()
        {
            try
            {
                DataTable ChartData = _tokenService.GetTokenPieChartData();

                //storing total rows count to loop on each Record  
                string[] XPointMember = new string[ChartData.Rows.Count];
                int[] YPointMember = new int[ChartData.Rows.Count];

                for (int count = 0; count < ChartData.Rows.Count; count++)
                {
                    //storing Values for X axis  
                    XPointMember[count] = ChartData.Rows[count]["symbol"].ToString();
                    //storing values for Y Axis  
                    YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["total_supply"]);

                }
                //binding chart control  
                TokenStatisChart.Series[0].Points.DataBindXY(XPointMember, YPointMember);

                //Setting  of line  
                TokenStatisChart.Series[0]["PieLabelStyle"] = "Outside";
                TokenStatisChart.Series[0].BorderWidth = 1;
                TokenStatisChart.Series[0].BorderDashStyle = ChartDashStyle.Solid;
                TokenStatisChart.Series[0].BorderColor = Color.Black;
                //setting Chart type   
                TokenStatisChart.Series[0].ChartType = SeriesChartType.Doughnut;


                foreach (Series charts in TokenStatisChart.Series)
                {
                    foreach (DataPoint point in charts.Points)
                    {
                        // Create color base on Name because token lenght is not limit
                        string color = "000000";
                        using (MD5 md5Hash = MD5.Create())
                        {
                            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(point.AxisLabel));
                            color = BitConverter.ToString(data).Replace("-", string.Empty).Substring(0, 6);
                        }
                        point.Color = ColorTranslator.FromHtml($"#{color}");
                        point.Label = point.AxisLabel;
                        point.ToolTip = $"{point.YValues[0]}";
                    }
                }
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
            }
        }

        private void BindGrid()
        {
            try
            {
                DataTable ChartData = _tokenService.GetTokenGridData();
                TokenGrid.DataSource = ChartData;
                TokenGrid.DataBind();
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
            }
        }

        protected void Edit(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
                var tokeId = int.Parse(row.Cells[0].Text.Trim());
                var token = _tokenService.GetById(tokeId);
                lblId.Text = token.Id.ToString();
                lblPrice.Text = token.Price.ToString();
                txtName.Text = token.Name;
                txtSymbol.Text = token.Symbol;
                txtContractAddress.Text = token.ContractAddress;
                txtTotalSupply.Text = token.TotalSupply.ToString();
                txtTotalHolders.Text = token.TotalHolders.ToString();

                refeshChartAndGrid();
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
            }
        }

        protected void Reset(object sender, EventArgs e)
        {
            try
            {
                lblId.Text = "";
                txtName.Text = "";
                txtSymbol.Text = "";
                txtContractAddress.Text = "";
                txtTotalSupply.Text = "";
                txtTotalHolders.Text = "";

                refeshChartAndGrid();
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
            }
        }

        protected void Save(object sender, EventArgs e)
        {
            try
            {
                var token = new Token()
                {
                    Name = txtName.Text.Trim(),
                    Price = decimal.Parse(lblPrice.Text),
                    Symbol = txtSymbol.Text.Trim(),
                    ContractAddress = txtContractAddress.Text.Trim(),
                    TotalHolders = Int32.Parse(txtTotalHolders.Text.Trim()),
                    TotalSupply = Int64.Parse(txtTotalSupply.Text.Trim()),
                };

                if (string.IsNullOrEmpty(lblId.Text.Trim()))
                {
                    token.Id = 0;
                    _tokenService.Create(token);

                }
                else
                {
                    token.Id = int.Parse(lblId.Text.Trim());
                    _tokenService.Update(token);
                }
                Reset(null, null);
                refeshChartAndGrid();
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
            }

        }

        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                TokenGrid.PageIndex = e.NewPageIndex;
                refeshChartAndGrid();
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
            }
        }

        protected void onExport(object sender, EventArgs e)
        {
            try
            {
                ExportGridToCSV();
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
            }
        }

        private void ExportGridToCSV()
        {

            BindGrid();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Tokens.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            TokenGrid.AllowPaging = false;
            TokenGrid.DataBind();
            StringBuilder columnbind = new StringBuilder();
            for (int k = 0; k < TokenGrid.Columns.Count; k++)
            {
                columnbind.Append(TokenGrid.Columns[k].HeaderText + ',');
            }
            columnbind.Append("\r\n");
            for (int i = 0; i < TokenGrid.Rows.Count; i++)
            {
                for (int k = 0; k < TokenGrid.Columns.Count; k++)
                {
                    if (TokenGrid.Columns[k].HeaderText == "Symbol")
                    {
                        var a = (HtmlAnchor)TokenGrid.Rows[i].FindControl("lnkDet");
                        columnbind.Append(a.InnerText + ',');
                    }
                    else
                    {
                        columnbind.Append(TokenGrid.Rows[i].Cells[k].Text + ',');
                    }
                }
                columnbind.Append("\r\n");
            }
            Response.Output.Write(columnbind.ToString());
            Response.Flush();
            Response.End();
        }

        private void refeshChartAndGrid()
        {
            try
            {
                Bindchart();
                BindGrid();
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
            }
        }
    }
}
