using Service.Services;
using System;
using System.Timers;

namespace Etherscan
{
    public partial class Detail : System.Web.UI.Page
    {


        private ITokenService _tokenService;

        public Detail()
        {
        }

        public Detail(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                string tokenId = Request.QueryString["id"];
                fetchTokenDetail(tokenId);
            }
            else
            {
                Response.Redirect("~/");
            }
        }

        void fetchTokenDetail(string tokenSymbol)
        {
            var token = _tokenService.GetBySymbol(tokenSymbol);
            lblcontractAddress.Text = token.ContractAddress;
            lblName.Text = token.Name;
            lblPrice.Text = token.Price.ToString();
            lblToltalHolders.Text = token.TotalHolders.ToString();
            lblToltalSupply.Text = token.TotalSupply.ToString();
            lblToltalSupply.Text = token.TotalSupply.ToString();
            lblToltalSymbol.Text = token.Symbol;
        }
    }
}