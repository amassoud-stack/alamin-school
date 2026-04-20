using Tahfeez.Api.Helpers;

namespace Tahfeez.Api.Extentions
{
    public static class DiContainerExtentions
    {
        public static void AddDiContainer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<TokenHelper>();
        }
    }
}
