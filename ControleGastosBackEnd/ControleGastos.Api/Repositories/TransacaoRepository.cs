using ControleGastos.Core.CoreViewModels;
using ControleGastos.Core.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace ControleGastos.Api.Repositories
{
    public class TransacaoRepository
    {
        private readonly IDbConnection _connection;

        public TransacaoRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<List<Transacao>> Get()
        {
            var transacoes = await _connection.GetAllAsync<Transacao>();
            return transacoes.ToList();
        }
        public async Task<Transacao> GetById(int id)
        {
            return await _connection.GetAsync<Transacao>(id);
        }
        public async Task Insert(Transacao transacao)
        {
            await _connection.InsertAsync(transacao);
        }

        public async Task<int> DeleteTransacoesByPessoaId(int pessoaId)
        {
            var sql = @"
               Delete From Transacoes
               WHERE PessoaId = @pessoaId
            ";
            return await _connection.ExecuteAsync(sql, new { pessoaId = pessoaId });
        }

        // Reports
        public async Task<List<ResumoFinanceiroPessoaViewModel>> GetResumoFinanceiroByPessoa()
        {
            var sql = @"SELECT
                          A.Id PessoaId,
                          A.Nome,
                          SUM(
                            CASE
                              WHEN B.Tipo = 2 THEN B.Valor
                              ELSE 0.0
                            END
                          ) AS Receitas,
                          SUM(
                            CASE
                              WHEN B.Tipo = 1 THEN B.Valor
                              ELSE 0.0
                            END
                          ) AS Despesas,
                          SUM(
                            CASE
                              WHEN B.Tipo = 2 THEN B.Valor
                              ELSE 0.0
                            END
                          ) - SUM(
                            CASE
                              WHEN B.Tipo = 1 THEN B.Valor
                              ELSE 0.0
                            END
                          ) AS Saldo
                        FROM Pessoas A
                          LEFT JOIN Transacoes B 
                          ON A.ID = B.PessoaId
                        GROUP BY
                          A.Id,
                          A.Nome";
            return (await _connection.QueryAsync<ResumoFinanceiroPessoaViewModel>(sql)).ToList();
        }

        public async Task<List<ResumoFinanceiroCategoriaViewModel>> GetResumoFinanceiroByCategoria()
        {
            var sql = @"SELECT
                      A.ID CategoriaId,
                      A.Descricao,
                      SUM(
                        CASE
                          WHEN B.Tipo = 2 THEN B.Valor
                          ELSE 0.0
                        END
                      ) AS Receitas,
                      SUM(
                        CASE
                          WHEN B.Tipo = 1 THEN B.Valor
                          ELSE 0.0
                        END
                      ) AS Despesas,
                      SUM(
                        CASE
                          WHEN B.Tipo = 2 THEN B.Valor
                          ELSE 0.0
                        END
                      ) - SUM(
                        CASE
                          WHEN B.Tipo = 1 THEN B.Valor
                          ELSE 0.0
                        END
                      ) Saldo
                    FROM Categorias A
                      LEFT JOIN Transacoes B 
                      ON A.ID = B.CategoriaId
                    GROUP BY
                      A.Id,
                      A.Descricao  
                        ";
            return (await _connection.QueryAsync<ResumoFinanceiroCategoriaViewModel>(sql)).ToList();
        }
    }
}
