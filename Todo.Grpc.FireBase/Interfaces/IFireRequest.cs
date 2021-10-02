using System.Threading.Tasks;

namespace Todo.Grpc.FireBase.Interfaces
{
    public interface IFireRequest<T>
        where T : IFireResponse
    {
        /// <summary>
        /// Dispatch to REST API
        /// </summary>
        Task<FireResult<T>> Dispatch();
        
        /// <summary>
        /// Serialize JSON to POST body
        /// </summary>
        /// <returns></returns>
        string Serialize();
    }
}