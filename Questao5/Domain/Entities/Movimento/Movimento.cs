using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities.Movimento
{
    public class Movimento
    {
        [Column("idmovimento")]
        public string IdMovimento { get; set; }

        [Column("idcontacorrente")]
        public string IdContaCorrente { get; set; }

        [Column("datamovimento")]
        public DateTime DataMovimento { get; set; }

        [Column("tipomovimento")]
        public string TipoMovimento { get; set; }

        [Column("valor")]
        public decimal Valor { get; set; }

        public Movimento(string idMovimento, 
            string idContaCorrente, 
            DateTime dataMovimento,
            string tipoMovimento, 
            decimal valor)
        {
            IdMovimento = idMovimento;
            IdContaCorrente = idContaCorrente;
            DataMovimento = dataMovimento;
            TipoMovimento = tipoMovimento;
            Valor = valor;
        }
    }
}
