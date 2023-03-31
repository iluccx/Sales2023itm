using Sales.WEB.Repositories;

namespace Web.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> Get<T>(string url);

        Task<HttpResponseWrapper<object>> Get(string url);

        Task<HttpResponseWrapper<object>> Post<T>(string url, T model); //post que no devuelva nada <object>

        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T model); // caso contrario, devuelve el body

        Task<HttpResponseWrapper<object>> Delete(string url); // Porque el API no devuelve nada

        Task<HttpResponseWrapper<object>> Put<T>(string url, T model); // Put sin respuesta

        Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T model); // Put sobrecargado con respuesta
    }
}
