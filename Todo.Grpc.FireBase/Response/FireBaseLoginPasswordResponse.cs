using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Todo.Grpc.FireBase.Base;
using Todo.Grpc.FireBase.Interfaces;

namespace Todo.Grpc.FireBase.Response
{
    [DataContract]
    public sealed class FireBaseLoginPasswordResponse : ResponseBase
    {
        /// <summary>
        /// The uid of the authenticated user.
        /// </summary>
        [DataMember, NotNull, JsonProperty("localId")]
        public string? LocalId { get; set; }

        /// <summary>
        /// The email for the authenticated user.
        /// </summary>
        [DataMember, NotNull, JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember, NotNull, JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// A Firebase Auth ID token for the authenticated user
        /// </summary>
        [DataMember, NotNull, JsonProperty("idToken")]
        public string? IdToken { get; set; }

        /// <summary>
        /// Whether the email is for an existing account.
        /// </summary>
        [DataMember, NotNull, JsonProperty("registered")]
        public bool? Registered { get; set; }

        /// <summary>
        /// A Firebase Auth refresh token for the authenticated user.
        /// </summary>
        [DataMember, NotNull, JsonProperty("refreshToken")]
        public string? RefreshToken { get; set; }

        /// <summary>
        /// The number of seconds in which the ID token expires.
        /// </summary>
        [DataMember, NotNull, JsonProperty("expiresIn")]
        public string? ExpiresIn { get; set; }
    }
}