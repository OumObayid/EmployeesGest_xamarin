using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeGest.MasterPage
{
    public class MasterPageFlyoutMenuItem
    {
        public MasterPageFlyoutMenuItem()
        {
            TargetType = typeof(MasterPageFlyoutMenuItem);
        }
        public int Id { get; set; }

        public string Icon { get; set; }

        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}