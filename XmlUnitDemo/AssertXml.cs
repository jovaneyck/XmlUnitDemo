using Org.XmlUnit.Builder;
using Xunit;

namespace XmlUnitDemo
{
    public static class AssertXml
    {
        public static void AreDifferent(DiffBuilder builder)
        {
            var diff = builder.Build();
            if (!diff.HasDifferences())
            {
                throw new XmlUnitNoDifferenceFoundException(diff);
            }
        }

        public static void AreIdentical(DiffBuilder builder)
        {
            var myDiff =
                builder
                    .Build();

            if (myDiff.HasDifferences())
            {
                throw new XmlUnitDifferenceFoundException(myDiff);
            }
        }
    }
}