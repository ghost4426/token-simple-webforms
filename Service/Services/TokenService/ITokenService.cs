using Model.Models;
using System.Collections.Generic;
using System.Data;

namespace Service.Services
{
   public interface ITokenService
    {
        IEnumerable<Token> GetAll();

        bool Update(Token token);

        DataTable GetTokenPieChartData();

        DataTable GetTokenGridData();

        Token GetById(int tokenId);

        Token GetBySymbol(string tokenSymbol);

        bool Create(Token token);
    }
}
