using Model.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Service.Repository
{
    public class TokenRepository : ITokenRepository
    {
        public bool Create(Token token)
        {
            string query = "INSERT INTO token VALUES(@Symbol, @Name, @TotalSupply, @ContractAddress, @TotalHolders, @Price)";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Symbol", token.Symbol);
                    cmd.Parameters.AddWithValue("@Name", token.Name);
                    cmd.Parameters.AddWithValue("@TotalSupply", token.TotalSupply);
                    cmd.Parameters.AddWithValue("@ContractAddress", token.ContractAddress);
                    cmd.Parameters.AddWithValue("@TotalHolders", token.TotalHolders);
                    cmd.Parameters.AddWithValue("@Price", token.Price);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return true;
        }

        public DataTable GetAllDataTable()
        {
            DataTable tokenDt = new DataTable();

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM token";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                   
                        sda.Fill(tokenDt);
                }
            }

            return tokenDt;
        }

        public IEnumerable<Token> GetAllEnumerable()
        {

            List<Token> tokenList = new List<Token>();

            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT * FROM token";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                {
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        tokenList = dt.AsEnumerable().Select(r => new Token()
                        {
                            Id = (int)r["id"],
                            Name = (string)r["name"],
                            Price = (decimal)r["price"],
                            Symbol = (string)r["symbol"],
                            ContractAddress = (string)r["contract_address"],
                            TotalHolders = (int)r["total_holders"],
                            TotalSupply = (long)r["total_supply"],

                        }).ToList();
                    }
                }
            }

            return tokenList;
        }

        public Token GetById(int tokenId)
        {

            Token token = new Token();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT id, name, price,symbol,  contract_address, total_holders, total_supply FROM token where id=@Id";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                   
                    cmd.Parameters.AddWithValue("@Id", tokenId);

                    cmd.Connection = con;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            token = new Token(){
                               Id = reader.GetInt32(0),
                               Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                               Symbol = reader.GetString(3),
                               ContractAddress = reader.GetString(4),
                               TotalHolders = reader.GetInt32(5), 
                               TotalSupply = reader.GetInt64(6),
                           };
                        }
                    }
                    else
                    {
                        throw new System.Exception("Invalid token id");
                    }
                    con.Close();
                }
             
            }

            return token;
        }

        public Token GetBySymbol(string tokenSymbol)
        {
            Token token = new Token();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            string query = "SELECT id, name, price,symbol,  contract_address, total_holders, total_supply FROM token where symbol=@symbol";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Parameters.AddWithValue("@symbol", tokenSymbol);

                    cmd.Connection = con;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            token = new Token()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                Symbol = reader.GetString(3),
                                ContractAddress = reader.GetString(4),
                                TotalHolders = reader.GetInt32(5),
                                TotalSupply = reader.GetInt64(6),
                            };
                        }
                    }
                    else
                    {
                        throw new System.Exception("Invalid token id");
                    }
                    con.Close();
                }

            }

            return token;
        }

        public bool Update(Token token)
        {
            string query = "UPDATE token SET symbol=@Symbol, name=@Name, total_supply=@TotalSupply, contract_address=@ContractAddress, total_holders=@TotalHolders, price=@Price WHERE id=@Id";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@Symbol", token.Symbol);
                    cmd.Parameters.AddWithValue("@Name", token.Name);
                    cmd.Parameters.AddWithValue("@TotalSupply", token.TotalSupply);
                    cmd.Parameters.AddWithValue("@ContractAddress", token.ContractAddress);
                    cmd.Parameters.AddWithValue("@TotalHolders", token.TotalHolders);
                    cmd.Parameters.AddWithValue("@Price", token.Price);
                    cmd.Parameters.AddWithValue("@Id", token.Id);

                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return true;
        }
    }
}
