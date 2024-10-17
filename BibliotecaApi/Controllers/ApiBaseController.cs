using Aplicacao.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public abstract class ApiBaseController : ControllerBase
    {
        protected ActionResult HandleResponse( RespostaOperacao result, ActionResult sucessResult )
        {
            if( result.Succedeed )
                return sucessResult;

            return result.Status switch
            {
                CodigoErro.BadRequest => BadRequest( result.MensagemErro ),
                CodigoErro.NaoLocalizado => NotFound( result.MensagemErro ),
                CodigoErro.ErroInternoServidor => StatusCode( 500, result.MensagemErro ),
                _ => StatusCode( 500, result.MensagemErro )
            };
        }
    }
}
