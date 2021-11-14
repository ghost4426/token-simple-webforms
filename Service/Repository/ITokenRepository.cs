using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Model.Models;

namespace Service.Repository
{
    public interface ITokenRepository
    {
        IEnumerable<Token> GetAllEnumerable();

        DataTable GetAllDataTable();

        Token GetById(int tokenId);

        Token GetBySymbol(string tokenSymbol);

        bool Update(Token token);

        bool Create(Token token);

    }
}
