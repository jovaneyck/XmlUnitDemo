using System.Linq;
using System.Text;
using Org.XmlUnit.Diff;
using Xunit.Sdk;

namespace XmlUnitDemo
{
    public class XmlUnitDifferenceFoundException : XunitException
    {   
        private readonly Diff _diff;

        public XmlUnitDifferenceFoundException(Diff diff)
        {
            _diff = diff;
        }

        public override string Message
        {
            get
            {
                var sb = new StringBuilder(_diff.ToString());
                sb.AppendLine();
                sb.AppendLine(string.Join("\n", _diff.Differences.Select(d => $"Difference: {d.Result} - {d.Comparison.ToString()}")));
                sb.AppendLine("Full XML:");
                sb.AppendLine("EXPECTED: ");
                sb.Append(_diff.ControlSource);
                sb.AppendLine();
                sb.AppendLine("ACTUAL: ");
                sb.Append(_diff.TestSource);

                return sb.ToString();
            }
        }
    }
}