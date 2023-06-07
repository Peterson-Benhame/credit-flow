#Documentação do Projeto CreditFlow
#Introdução
O projeto CreditFlow é uma aplicação web desenvolvida em ASP.NET Core que fornece uma API para cálculo de crédito. O sistema permite que o usuário escolha entre diferentes tipos de crédito, cada um com sua própria taxa de juros, e calcula o valor total do crédito com juros.

#Estrutura do Projeto
O projeto é organizado em camadas, incluindo a camada de modelo (Model), a camada de controle (Controller) e a camada de serviço (Service). A camada de modelo contém as classes de domínio que representam os conceitos centrais do sistema, como Credito, CreditoDireto, CreditoConsignado, etc. A camada de controle contém os controladores que manipulam as solicitações HTTP e retornam respostas HTTP. A camada de serviço contém a lógica de negócios e a lógica de acesso ao banco de dados.

#Modelos
O modelo Credito é a classe base para todos os tipos de crédito. Ele contém propriedades comuns a todos os créditos, como ValorCredito, QuantidadeParcelas e DataPrimeiroVencimento. Cada tipo de crédito é representado por uma classe que herda de Credito e define sua própria taxa de juros. Por exemplo, a classe CreditoDireto define uma taxa de juros de 2%.

#Controladores
O controlador CreditController manipula as solicitações HTTP para calcular o crédito. Ele usa a classe CreditoFactory para criar uma instância do tipo de crédito correto com base no tipo de crédito especificado na solicitação. Em seguida, ele chama o método Calcular do objeto de crédito para calcular o valor total do crédito com juros.

#Serviços
O serviço DatabaseService é responsável por interagir com o banco de dados. Ele usa a biblioteca MySqlConnector para conectar-se a um banco de dados MySQL e executar consultas SQL.

#Banco de Dados
O sistema usa um banco de dados MySQL para armazenar os dados dos clientes e dos créditos. As tabelas do banco de dados incluem:

clientes: Armazena os dados dos clientes, como nome e CPF.
CREATE TABLE Clientes (
    CPF VARCHAR(11) PRIMARY KEY,
    Nome VARCHAR(100),
    UF CHAR(2),
    Celular VARCHAR(15)
);

financiamentos: Armazena os dados dos financiamentos, como o CPF do cliente, o valor do crédito e a quantidade de parcelas.
CREATE TABLE Financiamentos (
    Id INT PRIMARY KEY,
    CPF VARCHAR(11),
    TipoFinanciamento VARCHAR(50),
    ValorTotal DECIMAL(18, 2),
    UltimoVencimento DATE,
    FOREIGN KEY (CPF) REFERENCES Clientes(CPF)
);

parcelas: Armazena os dados das parcelas de cada financiamento, como a data de vencimento e a data de pagamento.
CREATE TABLE Parcelas (
    IdFinanciamento INT,
    NumeroParcela INT,
    ValorParcela DECIMAL(18, 2),
    DataVencimento DATE,
    DataPagamento DATE,
    FOREIGN KEY (IdFinanciamento) REFERENCES Financiamentos(Id)
);

Aqui estão alguns exemplos de inserções de dados para teste:
INSERT INTO Clientes (Nome, CPF, UF, Celular) VALUES
('João Silva', '12345678901', 'SP', '(11) 98765-4321'),
('Maria Santos', '23456789012', 'RJ', '(21) 87654-3210'),
('Ana Pereira', '34567890123', 'MG', '(31) 76543-2109'),
('Carlos Oliveira', '45678901234', 'RS', '(51) 65432-1098'),
('Paula Costa', '56789012345', 'BA', '(71) 54321-0987');

INSERT INTO Financiamentos (Id, CPF, TipoFinanciamento, ValorTotal, UltimoVencimento) VALUES
(1, '12345678901', 'Crédito Direto', 50000.00, '2023-07-01'),
(2, '23456789012', 'Crédito Consignado', 30000.00, '2023-07-15'),
(3, '34567890123', 'Crédito Pessoa Jurídica', 200000.00, '2023-07-30'),
(4, '45678901234', 'Crédito Pessoa Física', 70000.00, '2023-08-14'),
(5, '56789012345', 'Crédito Imobiliário', 300000.00, '2023-08-29');

INSERT INTO Parcelas (IdFinanciamento, NumeroParcela, ValorParcela, DataVencimento, DataPagamento) VALUES
(1, 1, 10000.00, '2023-07-01', '2023-07-01'),
(1, 2, 10000.00, '2023-08-01', '2023-08-01'),
(2, 1, 15000.00, '2023-07-15', '2023-07-15'),
(2, 2, 15000.00, '2023-08-15', NULL),
(3, 1, 40000.00, '2023-07-30', '2023-07-30'),
(3, 2, 40000.00, '2023-08-30', NULL),
(3, 3, 40000.00, '2023-09-30', NULL),
(3, 4, 40000.00, '2023-10-30', NULL),
(3, 5, 40000.00, '2023-11-30', NULL),
(4, 1, 35000.00, '2023-08-14', '2023-08-13')

#Querys
A consulta abaixo está selecionando os clientes de São Paulo que pagaram mais de 60% de suas parcelas
SELECT c.Nome, c.CPF, c.UF, c.Celular
    FROM Clientes c
JOIN Financiamentos f ON f.CPF = c.CPF
JOIN Parcelas p ON p.IdFinanciamento = f.Id
    WHERE c.UF = 'SP'
GROUP BY c.Nome, c.CPF, c.UF, c.Celular
HAVING SUM(CASE WHEN p.DataPagamento IS NOT NULL THEN 1 ELSE 0 END) / COUNT(*) > 0.6

*** Passo a Passo ***
Selecionando as colunas Nome, CPF, UF e Celular da tabela Clientes.

Juntando a tabela Clientes com as tabelas Financiamentos e Parcelas com base na correspondência do CPF e do IdFinanciamento, respectivamente.

Filtrando os resultados para incluir apenas os clientes que estão localizados no estado de São Paulo (UF = 'SP').

Agrupando os resultados por Nome, CPF, UF e Celular. Isso significa que cada linha de resultado representará um cliente único.

A cláusula HAVING está sendo usada para filtrar os resultados do agrupamento. A expressão dentro do SUM está contando o número de parcelas que foram pagas (DataPagamento IS NOT NULL). Essa soma é então dividida pelo número total de parcelas (COUNT(*)). O resultado é a proporção de parcelas pagas. A consulta está filtrando para incluir apenas os clientes que pagaram mais de 60% de suas parcelas.

Já esta consulta está selecionando os 4 primeiros clientes que têm pelo menos uma parcela não paga que vence em mais de 5 dias
SELECT c.Nome, c.CPF, c.UF, c.Celular
    FROM clientes c
JOIN financiamentos f ON f.CPF = c.CPF
JOIN parcelas p ON p.IdFinanciamento = f.Id
    WHERE p.DataPagamento IS NULL AND p.DataVencimento > DATE_ADD(CURDATE(), INTERVAL 5 DAY)
GROUP BY c.Nome, c.CPF
LIMIT 4

*** Passo a Passo ***
Selecionando as colunas Nome, CPF, UF e Celular da tabela Clientes.

Juntando a tabela Clientes com as tabelas Financiamentos e Parcelas com base na correspondência do CPF e do IdFinanciamento, respectivamente.

Filtrando os resultados para incluir apenas as parcelas que ainda não foram pagas (DataPagamento IS NULL) e cuja data de vencimento é superior a 5 dias a partir da data atual (DataVencimento > DATE_ADD(CURDATE(), INTERVAL 5 DAY)).

Agrupando os resultados por Nome e CPF. Isso significa que cada linha de resultado representará um cliente único.

Limitando os resultados a apenas 4 clientes.

graph TB
  U["Usuário"] -- "Requisições" --> LB["Balanceador de Carga"]
  LB -- "Rota para" --> US["Serviço de Usuários"]
  LB -- "Rota para" --> PS["Serviço de Produtos"]
  LB -- "Rota para" --> OS["Serviço de Pedidos"]
  LB -- "Rota para" --> PYS["Serviço de Pagamento"]
  LB -- "Rota para" --> SS["Serviço de Envio"]
  US -- "Gerencia" --> UDB["Banco de Dados de Usuários"]
  PS -- "Gerencia" --> PDB["Banco de Dados de Produtos"]
  OS -- "Gerencia" --> ODB["Banco de Dados de Pedidos"]
  PYS -- "Gerencia" --> PYDB["Banco de Dados de Pagamentos"]
  SS -- "Gerencia" --> SDB["Banco de Dados de Envios"]
  linkStyle 0 stroke:#2ecd71,stroke-width:2px;
  linkStyle 1 stroke:#2ecd71,stroke-width:2px;
  linkStyle 2 stroke:#2ecd71,stroke-width:2px;
  linkStyle 3 stroke:#2ecd71,stroke-width:2px;
  linkStyle 4 stroke:#2ecd71,stroke-width:2px;
  linkStyle 5 stroke:#2ecd71,stroke-width:2px;
  linkStyle 6 stroke:#2ecd71,stroke-width:2px;
  linkStyle 7 stroke:#2ecd71,stroke-width:2px;
  linkStyle 8 stroke:#2ecd71,stroke-width:2px;
  linkStyle 9 stroke:#2ecd71,stroke-width:2px;
  linkStyle 10 stroke:#2ecd71,stroke-width:2px;
