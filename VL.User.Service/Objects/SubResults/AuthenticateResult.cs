using System.Runtime.Serialization;

namespace VL.User.Objects.SubResults
{
    [DataContract]
    public enum AuthenticateResult
    {
        [EnumMember]
        Success,
        [EnumMember]
        UserNameUnexist,
        [EnumMember]
        PasswordError
    }
}
