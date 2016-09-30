using System.Threading.Tasks;

namespace ApplicationServices.Common
{
    public interface IRequestHandler<in TQuery, TResult> where TQuery : IQuery
    {
        Task<TResult> Handle(TQuery request);
    }
}
