using System;
using System.Linq;
using System.Security.Principal;
using System.Web.Security;
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
            return result.Code == Common.Constraints.CProtocol.CReport.CSuccess;
        }
        public bool IsInUser(string user)
        {
            return false;
        }
    }
}