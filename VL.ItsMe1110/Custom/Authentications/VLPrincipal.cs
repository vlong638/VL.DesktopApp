using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Security;
using VL.Common.Object.Protocol;
using VL.Common.Object.VL.User;

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
            List<ERole> roles = new List<ERole>();
            foreach (var r in role.Split(','))
            {
                ERole eRole;
                Enum.TryParse(role, out eRole);
                roles.Add(eRole);
            }
            var client = new ObjectUserService.ObjectUserServiceClient();
            var result = client.CheckUserInRole(User, roles);
            return result.Code == CProtocol.CReport.CSuccess;
        }
        public bool IsInUser(List<string> users)
        {
            return users.Contains(User.UserName);
        }
    }
}