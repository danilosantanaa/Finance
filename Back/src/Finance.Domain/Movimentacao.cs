namespace Finance.Domain;

public class Movimentacao
{
    public int Id { get; set; }
    public Recibo Recibo { get; set; }
    public DateTime DataMovimentacao { get; set; } = DateTime.Now;
}