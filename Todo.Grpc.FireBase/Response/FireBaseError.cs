using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Todo.Grpc.FireBase.Base;

namespace Todo.Grpc.FireBase.Response
{
    [DataContract]
    public sealed class ErrorList
    {
        [DataMember, NotNull, JsonProperty("message")]
        public string? Message { get; set; }

        [DataMember, NotNull, JsonProperty("domain")]
        public string? Domain { get; set; }

        [DataMember, NotNull, JsonProperty("reason")]
        public string? Reason { get; set; }
    }

    [DataContract]
    public sealed class ErrorInfo
    {
        [DataMember, JsonProperty("code")]
        public int Code { get; set; }

        [DataMember, NotNull, JsonProperty("message")]
        public string? Message { get; set; }

        [DataMember, NotNull, JsonProperty("errors")]
        public List<ErrorList>? Errors { get; set; }
    }

    [DataContract]
    public sealed class FireBaseError : ResponseBase
    {
        [DataMember, NotNull, JsonProperty("error")]
        public ErrorInfo? Error { get; set; }
    }
}

