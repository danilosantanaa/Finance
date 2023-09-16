namespace FinanceWasm.Models {
    public class Estado {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Cidade> Cidades { get; set; }
    }
}
