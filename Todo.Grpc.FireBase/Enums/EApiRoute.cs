using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Todo.Grpc.FireBase.Enums
{
    public enum EApiRoute
    {
        [Display(Name = "accounts:signInWithPassword")]
        SignInWithPassword
    }
}