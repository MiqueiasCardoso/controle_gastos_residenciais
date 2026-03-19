using Dapper;
using System.Data;

namespace ControleGastos.Api.Db
{
    public class DbInicializer
    {
        private readonly IDbConnection _connection;

        public DbInicializer(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Initialize()
        {
            _connection.Open();

            _connection.Execute("PRAGMA foreign_keys = ON;");

            var sql = @"

            CREATE TABLE IF NOT EXISTS Pessoas (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Nome TEXT NOT NULL CHECK(length(Nome) <= 200),
                Idade INTEGER NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Categorias (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Descricao TEXT NOT NULL CHECK(length(Descricao) <= 400),
                Finalidade INTEGER NOT NULL
            );

            CREATE TABLE IF NOT EXISTS Transacoes (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Descricao TEXT CHECK(length(Descricao) <= 400),
                Valor REAL NOT NULL,
                Tipo INTEGER NOT NULL,
                CategoriaId INTEGER NOT NULL,
                PessoaId INTEGER NOT NULL,
                FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id),
                FOREIGN KEY (PessoaId) REFERENCES Pessoas(Id)
            );
            ";

            _connection.Execute(sql);
        }
    }
}