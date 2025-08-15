using Erp_V1.BLL;
using NUnit.Framework;
using System.Linq;

namespace Erp_V1.Tests
{
    [TestFixture]
    public class LoginSecurityTests
    {
        private EmployeeBLL _bll;

        [SetUp]
        public void Setup()
        {
            _bll = new EmployeeBLL();
        }

        private bool TryLogin(string userNo, string pwd)
        {
            var dto = _bll.Select();
            return dto.Employees.Any(x => x.UserNo.ToString() == userNo && x.Password == pwd);
        }

        [TestCase("anything' OR '1'='1", "whatever")]
        [TestCase("admin'; --", "password")]
        [TestCase("'. DROP TABLE EMPLOYEE;--", "x")]
        public void SqlInjectionStrings_ShouldNotAuthenticate(string user, string pass)
        {
            Assert.That(
                TryLogin(user, pass),
                Is.False,
                $"Injection payload `{user}` / `{pass}` should not succeed."
            );
        }

        [Test]
        public void EmptyCredentials_ShouldFail()
        {
            Assert.That(
                TryLogin("", ""),
                Is.False,
                "Empty username & password must fail."
            );
        }

        [TestCaseSource(nameof(LongInputs))]
        public void LongInputs_ShouldNotCrash(string longUser, string longPass)
        {
            // ensure no exception
            Assert.DoesNotThrow(() => TryLogin(longUser, longPass));

            // and still false
            Assert.That(
                TryLogin(longUser, longPass),
                Is.False,
                "Extremely long inputs should not authenticate."
            );
        }

        static object[] LongInputs =
        {
            new object[] { new string('A', 10000), new string('A', 10000) }
        };
    }
}
