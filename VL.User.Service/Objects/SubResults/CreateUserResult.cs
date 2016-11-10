using System.Runtime.Serialization;

namespace VL.User.Objects.SubResults
{
    [DataContract]
    public enum CreateUserResult
    {
        [EnumMember]
        Success,
        [EnumMember]
        DbOperationFailed,
        [EnumMember]
        UserNameExist,
        [EnumMember]
        MobileExist,
        [EnumMember]
        EmailExist,
        [EnumMember]
        IdExist,
    }
}
