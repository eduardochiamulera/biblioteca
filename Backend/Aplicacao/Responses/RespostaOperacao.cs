using Core.Enumeradores;

namespace Aplicacao.Responses
{
    public class RespostaOperacao
    {
        public CodigoErro Status { get; set; } = CodigoErro.None;
        public string MensagemErro { get; set; }
        public bool Succedeed { get; set; } = true;

        public static RespostaOperacao Sucesso => new();
        public static RespostaOperacao Falha( CodigoErro errorCode, string message = null ) => new RespostaOperacao()
        {
            Status = errorCode,
            MensagemErro = message,
            Succedeed = false,
        };

    }
}
