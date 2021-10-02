using Newtonsoft.Json;
using Todo.Grpc.FireBase.Interfaces;

namespace Todo.Grpc.FireBase.Base
{
    public abstract class ResponseBase : IFireResponse
    {
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}