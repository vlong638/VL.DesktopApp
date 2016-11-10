using System.Collections.Generic;
using VL.Common.DAS.Utilities;
using VL.Common.Logger.Objects;
using VL.Common.Logger.Utilities;
using VL.Common.Protocol;
using VL.Common.Protocol.IService;
using VL.User.Service.Configs;

namespace VL.User.Service.Utilities
{
    public class ServiceContextOfUser : ServiceContext
    {
        public ServiceContextOfUser() : base()
        {
        }
        public ServiceContextOfUser(DbConfigEntity databaseConfig, ProtocolConfig protocolConfig, ILogger serviceLogger) : base(databaseConfig, protocolConfig, serviceLogger)
        {
        }

        public override string GetUnitName()
        {
            return nameof(VL.User.Service);
        }

        protected override DbConfigEntity GetDefaultDatabaseConfig()
        {
            return new DbConfigOfUser("DbConnections.config");
        }
        protected override ProtocolConfig GetDefaultProtocolConfig()
        {
            return new ProtocolConfig("ProtocolConfig.config");
        }
        protected override ILogger GetDefaultServiceLogger()
        {
            return LoggerProvider.GetLog4netLogger("ServiceLog");
        }

        protected override List<DependencyResult> InitOthers()
        {
            return new List<DependencyResult>();
        }
    }
}
