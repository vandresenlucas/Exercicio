namespace Questao5.Infrastructure.Providers
{
    public static class LocalizationConfiguration
    {
        public static IApplicationBuilder ConfigureLocalization(this IApplicationBuilder app)
        {
            var supportedCultures = new[] { "en-US", "pt-BR" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture("pt-BR")
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}
