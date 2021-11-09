﻿using ConfigurationSwitcherGUI.Presenter;
using ConfigurationSwitcherGUI.Views;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OVAConfigSwitcher.Business.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigurationSwitcherGUI.Views
{
    public partial class ConfigurationSwitcherView : Form, IConfigurationSwitcherView
    {
        public IConfigurationSwitcherPresenter presenter;
        public ConfigurationSwitcherView(IOptions<AppSettings> appsettings, ILogger<ConfigurationSwitcherPresenter> logger)
        {
            presenter = new ConfigurationSwitcherPresenter(this, appsettings, logger);
            InitializeComponent();
        }

        public IEnumerable<string> Environments
        {
            set
            {
                cbEnvironments.DataSource = value.ToList();
            }
        }

        public IEnumerable<string> Configurations
        {
            set
            {
                cbConfigurations.DataSource = value.ToList();
            }
        }

        private void cbEnvironments_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configurations = presenter.GetConfigurations((sender as ComboBox).SelectedItem.ToString());
        }

        private void ConfigurationSwitcherView_Load(object sender, EventArgs e)
        {
            presenter.LoadView();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            presenter.ApplyConfiguration(cbEnvironments.Text, cbConfigurations.Text);
        }
    }
}
