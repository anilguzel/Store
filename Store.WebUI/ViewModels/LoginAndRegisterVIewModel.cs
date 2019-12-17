using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.WebUI.ViewModels
{
    public class LoginAndRegisterViewModel
    {
        public RegisterViewModel Register { get; set; } = new RegisterViewModel();
        public LoginViewModel Login { get; set; } = new LoginViewModel();
    }
}
