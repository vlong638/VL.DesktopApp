﻿using System.Collections.Generic;
using VL.Common.Core.DAS;
using VL.Common.Core.Protocol;
using VL.Common.Logger;

namespace VL.User.Service.Utilities
{
    public class ServiceContextOfUser : ServiceContext
    {
        public static ModuleReportHelper ReportHelper { set; get; } = new ModuleReportHelper(nameof(VL.User));

        public ServiceContextOfUser() : base()
        {
        }
        public ServiceContextOfUser(DbConfigEntity databaseConfig, ProtocolConfig protocolConfig, ILogger serviceLogger) : base(databaseConfig, protocolConfig, serviceLogger)
        {
        }

        public override string GetUnitName()
        {
            return nameof(VL.User);
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
