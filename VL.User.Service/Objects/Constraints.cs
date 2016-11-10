using VL.Common.Protocol.IService;

namespace VL.User.Objects
{
    public class Constraints
    {
        public static ModuleReportHelper ReportHelper { set; get; } = new ModuleReportHelper(nameof(VL.User));
    }
}
