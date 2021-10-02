using System.ComponentModel.DataAnnotations;

namespace Todo.Grpc.FireBase.Enums
{
    public enum EStatusCode
    {
        [Display(Name = "INVALID_PASSWORD")]
        InvalidPassword,
        
        /// <summary>
        /// The user's credential is no longer valid. The user must sign in again.
        /// </summary>
        [Display(Name = "TOKEN_EXPIRED")]
        TokenExpired
    }
}