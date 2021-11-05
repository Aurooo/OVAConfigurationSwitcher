using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OVAConfigSwitcher.Business;
using OVAConfigSwitcher.Business.Contracts.Models;
using RegistryReader;

namespace ConfigurationSwitcherGUI.View
{
    public partial class ConfigurationSwitcherForm : Form
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly ConfigSwitcher configSwitcher;

        public ConfigurationSwitcherForm(IOptions<AppSettings> appsettings, ILogger<ConfigurationSwitcherForm> logger)
        {
            _appSettings = appsettings.Value;
            _logger = logger;
            configSwitcher = InitializeConfigSwitcher();
            InitializeComponent();
        }

        private ConfigSwitcher InitializeConfigSwitcher()
        {
            var currentConfig = new RegistryStream().Read(_appSettings.RegistryKey)
                .Where(element => element.Name == "ConfigFilePath")
                .Select(element => element).FirstOrDefault().Value;

            return new ConfigSwitcher(_appSettings.RootDirectory, currentConfig);
        }
        private void InitializeTree()
        {
            twConfigurations.AfterSelect += new TreeViewEventHandler(twConfigurations_AfterSelect);

            twConfigurations.LabelEdit = false;

            var environments = configSwitcher.GetEnvironments();
            foreach (var environment in environments)
            {
                TreeNode node = new TreeNode(environment.EnvironmentName);

                foreach (var file in configSwitcher.GetAgencyConfigurationFiles(environment.EnvironmentName))
                {
                    node.Nodes.Add(new TreeNode(file.AgencyFileName));
                }

                twConfigurations.Nodes.Add(node);
            }
        }



        #region events
        private void ConfigurationSwitcherForm_Load(object sender, EventArgs e)
        {
            InitializeTree();
            lblRoot.Text = _appSettings.RootDirectory;
            lblError.MouseHover += LblError_MouseHover;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            var fullPath = Path.Combine(_appSettings.RootDirectory, tbFilePath.Text.Replace("..\\", ""));
            try
            {
                configSwitcher.ApplyConfigurationFile(new AgencyConfigurationFile(
                    fullPath));
                lblError.Text = "Applied";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }

        }

        private void twConfigurations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tree = sender as TreeView;
            if (tree.SelectedNode.Text.ToLower().Contains(".xml"))
                tbFilePath.Text = Path.Combine("..", tree.SelectedNode.Parent.Text, tree.SelectedNode.Text);
        }

        private void LblError_MouseHover(object sender, EventArgs e)
        {
            tpError.SetToolTip(lblError, lblError.Text);
        }
        #endregion

    }
}
