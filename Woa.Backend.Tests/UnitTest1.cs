using System;
using Woa.Infrastructure;
using Xunit;

namespace Woa.Backend.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Hasher()
        {
            var pwd = "any-string";
            var salt = Salt.Create();
            var hash = Hash.Create(pwd, salt);
            var result = Hash.Validate(pwd, salt, hash);
            Assert.True(result);
        }
    }
}
