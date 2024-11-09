namespace MediatrFluentValidation.Util
{
    public sealed class Resultado<T>
    {
        public T? Valor { get; }
        public Erro? Erro { get; }
        public bool Sucesso { get; }
        public bool Falha => !Sucesso;

        private Resultado(T valor)
        {
            Valor = valor ?? throw new ArgumentNullException(nameof(valor), "O valor não pode ser nulo.");
            Sucesso = true;
            Erro = null;
        }

        private Resultado(Erro erro)
        {
            Erro = erro ?? throw new ArgumentNullException(nameof(erro), "O erro não pode ser nulo.");
            Sucesso = false;
        }
        public static Resultado<T> ComSucesso(T valor) => new(valor);

        public static Resultado<T> ComFalha(Erro erro) => new(erro);

        public TResult AoMatch<TResult>(Func<T, TResult> aoSucesso, Func<Erro, TResult> aoErro)
        {
            return Sucesso ? aoSucesso(Valor!) : aoErro(Erro!);
        }
    }
    public sealed record Erro(int Codigo, string Descricao)
    {
        public static Erro ProdutoNaoEncontrado => new(100, "Produto não encontrado");

        public static Erro RequisicaoInvalidaProduto => new(101, "Requisição inválida para o produto");
    }
}
