using CreditFlow.Model;
using MySqlConnector;

namespace CreditFlow.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Cliente> GetClientesSPComParcelasPagas()
        {
            var clientes = new List<Cliente>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                            SELECT c.Nome, c.CPF, c.UF, c.Celular
                                FROM Clientes c
                            JOIN Financiamentos f ON f.CPF = c.CPF
                            JOIN Parcelas p ON p.IdFinanciamento = f.Id
                                WHERE c.UF = 'SP'
                            GROUP BY c.Nome, c.CPF, c.UF, c.Celular
                            HAVING SUM(CASE WHEN p.DataPagamento IS NOT NULL THEN 1 ELSE 0 END) / COUNT(*) > 0.6";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente
                            {
                                Nome = reader.GetString("Nome"),
                                CPF = reader.GetString("CPF"),
                                UF = reader.GetString("UF"),
                                Celular = reader.GetString("Celular")
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }


        public List<Cliente> GetClientesComParcelasSemAtraso()
        {
            var clientes = new List<Cliente>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                            SELECT c.Nome, c.CPF, c.UF, c.Celular
                                FROM clientes c
                            JOIN financiamentos f ON f.CPF = c.CPF
                            JOIN parcelas p ON p.IdFinanciamento = f.Id
                                WHERE p.DataPagamento IS NULL AND p.DataVencimento > DATE_ADD(CURDATE(), INTERVAL 5 DAY)
                            GROUP BY c.Nome, c.CPF
                            LIMIT 4";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente
                            {
                                Nome = reader.GetString("Nome"),
                                CPF = reader.GetString("CPF"),
                                UF = reader.GetString("UF"),
                                Celular = reader.GetString("Celular")
                            };

                            clientes.Add(cliente);
                        }
                    }
                }
            }

            return clientes;
        }
    }
}
