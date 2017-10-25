using System;
using System.Text;
using Org.XmlUnit.Builder;
using Org.XmlUnit.Diff;

namespace XmlUnitDemo
{
    public class XmlUnitNoDifferenceFoundException : Exception
    {
        private readonly Diff _diff;

        public XmlUnitNoDifferenceFoundException(Diff diff)
        {
            _diff = diff;
        }

        public override string Message
        {
            get
            {
                var sb = new StringBuilder(_diff.ToString());
                sb.AppendLine();
                sb.AppendLine("Full XML:");
                sb.AppendLine(_diff.ControlSource.ToString());

                return sb.ToString();
            }
        }
    }
}