using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Questao5.Application;
using Questao5.Domain.Language;

namespace Questao5.Infrastructure.Providers.Customizations
{
    public static class FluentValidationErrorCustomization
    {
        public static void ErrorCustomization(this ApiBehaviorOptions options)
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var localizer = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizer<ManagerResources>>();

                var erro = context.ModelState.Values.FirstOrDefault().Errors.Select(e => e.ErrorMessage).FirstOrDefault();

                var mensagemTraduzida = localizer[erro];

                return new BadRequestObjectResult(new Result(false, tipoErro: erro, mensagem: mensagemTraduzida));
            };
        }
    }
}
