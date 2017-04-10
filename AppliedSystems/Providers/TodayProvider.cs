using System;
using AppliedSystems.Interfaces;

namespace AppliedSystems.Providers
{
    public class TodayProvider : IProvide<DateTime>
    {
        public DateTime Get()
        {
            return DateTime.Today;
        }
    }
}
