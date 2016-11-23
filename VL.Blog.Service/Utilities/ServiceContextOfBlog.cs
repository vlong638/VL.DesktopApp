using System.Collections.Generic;
using VL.Common.Core.DAS;
using VL.Common.Core.Protocol;
using VL.Common.Logger;
using VL.User.Service.Utilities;

namespace VL.Blog.Service.Utilities
{
    public class ServiceContextOfBlog : ServiceContext
    {
        public static ModuleReportHelper ReportHelper { set; get; } = new ModuleReportHelper(nameof(VL.Blog));

        public ServiceContextOfBlog() : base()
        {
        }
        public ServiceContextOfBlog(DbConfigEntity databaseConfig, ProtocolConfig protocolConfig, ILogger serviceLogger) : base(databaseConfig, protocolConfig, serviceLogger)
        {
        }

        public override string GetUnitName()
        {
            return nameof(VL.Blog);
        }

        protected override DbConfigEntity GetDefaultDatabaseConfig()
        {
            return new DbConfigOfBlog("DbConnections.config");
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
