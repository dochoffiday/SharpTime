using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class SharpTimeTests
    {
        [Fact]
        public void ShouldUseGiveTime()
        {
            var dateTimeUtcToUse = new DateTime(2020, 12, 25, 9, 33, 28);

            Assert.NotEqual(dateTimeUtcToUse, SharpTime.UtcNow);

            using (SharpTime.UseSpecificDateTimeUtc(dateTimeUtcToUse))
            {
                Assert.Equal(dateTimeUtcToUse, SharpTime.UtcNow);
            }

            Assert.NotEqual(dateTimeUtcToUse, SharpTime.UtcNow);
        }

        [Fact]
        public void ShouldThrowError()
        {
            using (SharpTime.UseSpecificDateTimeUtc(new DateTime(2020, 12, 25)))
            {
                Assert.Throws<InvalidOperationException>(() =>
                {
                    SharpTime.UseSpecificDateTimeUtc(new DateTime(2020, 01, 01));
                });
            }
        }

        [Fact]
        public void ShouldThreadCorrectly()
        {
            var dateTimeToUse = new DateTime(2020, 12, 25);

            using (SharpTime.UseSpecificDateTimeUtc(dateTimeToUse))
            {
                Assert.Equal(dateTimeToUse, SharpTime.UtcNow);

                var threadedDateTime = Task.Run(() =>
                {
                    return SharpTime.UtcNow;
                }).Result;

                Assert.NotEqual(dateTimeToUse, threadedDateTime);
            }
        }
    }
}