namespace Todo.Grpc.FireBase.Interfaces
{
    public interface IFireResponse
    {
        /// <summary>
        /// Serialize JSON to POST body
        /// </summary>
        /// <returns></returns>
        string Serialize();
    }
}