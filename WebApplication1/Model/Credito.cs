using System.Drawing;

namespace CreditFlow.Model
{
    public class Credito
    {
        public decimal ValorCredito { get; set; }
        public int QuantidadeParcelas { get; set; }
        public string? TipoCredito { get; set; }
        public DateTime DataPrimeiroVencimento { get; set; }

        public virtual decimal Taxa { get; }

        public decimal CalcularValorTotalComJuros()
        {
            return ValorCredito * (1 + Taxa);
        }

        public decimal CalcularValorJuros()
        {
            return CalcularValorTotalComJuros() - ValorCredito;
        }
        public virtual bool Validar()
        {
            // O valor máximo a ser liberado para qualquer tipo de empréstimo é de R$ 1.000.000,00
            if (ValorCredito > 1000000.00m)
            {
                return false;
            }

            // A quantidade mínima de parcelas é de 5x e máxima de 72x
            if (QuantidadeParcelas < 5 || QuantidadeParcelas > 72)
            {
                return false;
            }

            // A data do primeiro vencimento sempre será no mínimo 15 dias e no máximo 40 dias a partir da data atual
            var diasParaPrimeiroVencimento = (DataPrimeiroVencimento - DateTime.Today).TotalDays;
            if (diasParaPrimeiroVencimento < 15 || diasParaPrimeiroVencimento > 40)
            {
                return false;
            }

            return true;
        }
    }
}
