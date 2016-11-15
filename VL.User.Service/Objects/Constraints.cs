using VL.Common.Protocol;

namespace VL.User.Objects
{
    public class Constraints
    {
        public static ModuleReportHelper ReportHelper { set; get; } = new ModuleReportHelper(nameof(VL.User));
    }
}
