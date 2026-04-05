namespace PracticaAPI.Middlewares
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ResponseTimeMiddleware(RequestDelegate next)
        { 
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tiempo = new System.Diagnostics.Stopwatch();
            tiempo.Start();

            await _next(context);

            tiempo.Stop();

            var tiempoEnMilisegundos = tiempo.ElapsedMilliseconds;

            //Agrega el tiempo de respuesta en el encabezado
            //context.Response.Headers.Append("X-Response-Time-Ms", tiempoEnMilisegundos.ToString());

            Console.WriteLine($"[Timer] la ruta {context.Request.Path} tardo {tiempoEnMilisegundos} ms");
        }
    }
    public static class ResponseTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTime(this IApplicationBuilder app)
            => app.UseMiddleware<ResponseTimeMiddleware>();
    }



}
