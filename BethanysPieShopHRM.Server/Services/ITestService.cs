using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Services
{

    public interface ITestService
    {
        string GetTime();
    }

    public class TestService : ITestService
    {
        public string GetTime()
        {
            return DateTime.UtcNow.ToString();
        }
    }
}
