using Orders.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Core.ViewModel
{
    public class LoginResponseViewModel
    {
        public UserViewModel userVm { get; set; }
        public AccessTockenViewModel accessTockenVm { get; set; }

    }
}
