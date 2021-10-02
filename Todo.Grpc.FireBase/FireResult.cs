using Todo.Grpc.FireBase.Interfaces;
using Todo.Grpc.FireBase.Response;

namespace Todo.Grpc.FireBase
{
    public class FireResult<T>
        where T : IFireResponse
    {
        private readonly T? _message;
        private readonly FireBaseError? _error;

        private FireResult(T? message, FireBaseError? error)
        {
            _message = message;
            _error = error;
        }
        
        public bool IsError => _error != null;

        public FireBaseError? Error()
        {
            return _error;
        }

        public T? Message()
        {
            return _message;
        }

        public static FireResult<T> Success(T? message)
        {
            return new FireResult<T>(message, null);
        }

        public static FireResult<T> Fail(FireBaseError? error)
        {
            return new FireResult<T>(default, error);
        }
    }
}