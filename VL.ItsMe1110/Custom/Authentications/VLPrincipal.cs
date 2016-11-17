using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;
using VL.Common.Constraints.Protocol;
using VL.ItsMe1110.SubjectUserService;

namespace VL.ItsMe1110.Custom.Authentications
{
    public class VLPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public TUser User { get; private set; }

        public VLPrincipal(FormsAuthenticationTicket ticket, TUser user)
        {
            if (ticket==null)
                throw new ArgumentNullException(nameof(ticket));
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            Identity = new FormsIdentity(ticket);
            User = user;
        }

        public bool IsInRole(string role)
        {
            var client = new SubjectUserServiceClient();
            var result = client.CheckUserInRole(User, role.Split(',').ToList());
            return result.Code == CProtocol.CReport.CSuccess;
        }
        public bool IsInUser(string user)
        {
            var users = user.Split(',');
            return users.Contains(User.UserName);
        }
    }
}