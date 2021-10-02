using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Todo.Grpc.FireBase.Base;
using Todo.Grpc.FireBase.Enums;
using Todo.Grpc.FireBase.Interfaces;
using Todo.Grpc.FireBase.Response;

namespace Todo.Grpc.FireBase.Request
{
    /// <summary>
    /// https://firebase.google.com/docs/reference/rest/auth#section-sign-in-email-password
    /// </summary>
    [DataContract]
    public sealed class FireBaseLoginPasswordRequest : RequestBase<FireBaseLoginPasswordResponse>
    {
        /// <summary>
        /// The email the user is signing in with.
        /// </summary>
        [DataMember, JsonProperty("email")]
        public string Email { get; }

        /// <summary>
        /// The password for the account.
        /// </summary>
        [DataMember, JsonProperty("password")]
        public string Password { get; }

        /// <summary>
        /// Whether or not to return an ID and refresh token. Should always be true.
        /// </summary>
        [DataMember, JsonProperty("returnSecureToken")]
        public bool ReturnSecureToken { get; }

        public FireBaseLoginPasswordRequest(string email, string password)
            : base(EApiRoute.SignInWithPassword)
        {
            Email = email;
            Password = password;
            ReturnSecureToken = true;
        }
    }
}