using FastMember;
using Model.Models;
using Service.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public bool Create(Token token) => _tokenRepository.Create(token);

        public IEnumerable<Token> GetAll() => _tokenRepository.GetAllEnumerable();

        public Token GetById(int tokenId) => _tokenRepository.GetById(tokenId);

        public Token GetBySymbol(string tokenSymbol) => _tokenRepository.GetBySymbol(tokenSymbol);

        public DataTable GetTokenPieChartData()
        {
            return _tokenRepository.GetAllDataTable();
        }

        public DataTable GetTokenGridData()
        {
            var tokens = _tokenRepository.GetAllEnumerable();
            var sumTotalSupply = tokens.Sum(token => token.TotalSupply);


            var gridTokens = tokens.Select(token => new TokenGridData()
            {
                Id = token.Id,
                Name = token.Name,
                Price = token.Price,
                Symbol = token.Symbol,
                ContractAddress = token.ContractAddress,
                TotalHolders = token.TotalHolders,
                TotalSupply = token.TotalSupply,
                PercentTotalSupply = (float)token.TotalSupply / sumTotalSupply * 100,
            }).ToList();

            DataTable tokeDt = new DataTable();
            using (var reader = ObjectReader.Create(gridTokens))
            {
                tokeDt.Load(reader);
            }

            return tokeDt;
        }

        public bool Update(Token token) => _tokenRepository.Update(token);
    }
}
