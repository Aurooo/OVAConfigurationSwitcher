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
using OVAConfigSwitcher.Business.Contracts.Models;

namespace ConfigurationSwitcherGUI.View
{
    public partial class ConfigurationSwitcherForm : Form, IConfigurationSwitcherView
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private ConfigSwitcher configSwitcher;
        public ConfigurationSwitcherForm(IOptions<AppSettings> appsettings, ILogger<ConfigurationSwitcherForm> logger)
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
