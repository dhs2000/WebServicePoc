using System.Threading.Tasks;

namespace ApplicationServices.Common
{
    public interface IRequestHandler<TResult, in TQuery> where TQuery : IQuery
    {
        Task<TResult> Handle(TQuery request);
    }
}
