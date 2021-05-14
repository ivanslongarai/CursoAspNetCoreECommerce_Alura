using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CursoAspNetCoreECommerce
{
    public interface IBooksReport
    {
        Task Print(HttpContext context);
    }
}