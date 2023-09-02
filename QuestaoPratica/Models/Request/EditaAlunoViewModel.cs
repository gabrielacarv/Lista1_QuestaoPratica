namespace QuestaoPratica.Models.Request
{
    public class EditaAlunoViewModel
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Ra { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
    }
}
