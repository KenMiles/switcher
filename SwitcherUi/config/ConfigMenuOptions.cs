using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwitcherUi.config
{
    public delegate Form CreateConfigForm(IConfiguration config);
    public class ConfigMenuOptions
    {
        public string MenuText { get; set; }
        public CreateConfigForm CreateConfigForm { get; set; }
    }
}
