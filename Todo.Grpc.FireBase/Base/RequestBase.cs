using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Todo.Grpc.FireBase.Enums;
using Todo.Grpc.FireBase.Extensions;
using Todo.Grpc.FireBase.Interfaces;

// ReSharper disable StringLiteralTypo

namespace Todo.Grpc.FireBase.Base
{
    public abstract class RequestBase<T> : IFireRequest<T>
        where T : IFireResponse
    {
        private readonly string _url;

        protected RequestBase(EApiRoute route)
        {
            var routeName = route.GetDisplayAttributes()?.Name;
            const string? apiKey = "AIzaSyDld9K_RaezUr9JDGftvA50JzBnAUIlL-Q";

            _url = $"https://identitytoolkit.googleapis.com/v1/{routeName}?key={apiKey}";
        }

        public virtual async Task<FireResult<T>> Dispatch()
        {
            return await FireDispatcher.Dispatch(_url, this);
        }

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}