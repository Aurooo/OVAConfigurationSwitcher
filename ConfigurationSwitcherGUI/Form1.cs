using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OVAConfigSwitcher.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigurationSwitcherGUI
{
    public partial class Form1 : Form
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private ConfigSwitcher configSwitcher;
        public Form1(IOptions<AppSettings> appsettings, ILogger<Form1> logger)
        {
            _appSettings = appsettings.Value;
            _logger = logger;
            configSwitcher = InitializeConfigSwitcher();
            InitializeComponent();
        }

        private ConfigSwitcher InitializeConfigSwitcher()
        {
            throw new NotImplementedException();
        }
    }
}
