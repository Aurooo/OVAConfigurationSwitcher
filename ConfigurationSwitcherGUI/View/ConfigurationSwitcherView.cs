using ConfigurationSwitcherGUI.Models;
using ConfigurationSwitcherGUI.Presenter;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OVAConfigSwitcher.Business.Contracts.Models;
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

namespace ConfigurationSwitcherGUI.View
{
    public partial class ConfigurationSwitcherView : Form, IConfigurationSwitcherView
    {
        private IConfigurationSwitcherPresenter presenter;
        public ConfigurationSwitcherView(IOptions<AppSettings> appsettings, ILogger<ConfigurationSwitcherPresenter> logger)
        {
            presenter = new ConfigurationSwitcherPresenter(this, appsettings, logger);
            InitializeComponent();
            btnApply.Enabled = false;
        }

        public void ShowView()
        {
            this.Show();
        }
        public void ShowError(string ErrorMessage)
        {
            lblError.Text = ErrorMessage;
        }
        public void Populate(IEnumerable<EnvironmentDirectory> environments)
        {
            twConfigurations.AfterSelect += TwConfigurations_AfterSelect;
            twConfigurations.LabelEdit = false;

            foreach (var environment in environments.ToList())
            {
                TreeNode node = new TreeNode(environment.Name);

                foreach (var file in environment.Files.ToList())
                {
                    node.Nodes.Add(new TreeNode(file.AgencyFileName));
                }

                twConfigurations.Nodes.Add(node);
            }
        }

        private void TwConfigurations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tree = sender as TreeView;

            if (tree.SelectedNode.Text.ToLower().Contains(".xml"))
            {
                tbFilePath.Text = Path.Combine("..", tree.SelectedNode.Parent.Text, tree.SelectedNode.Text);
                btnApply.Enabled = true;
            }

            lblError.Text = "";
                
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            bool applied = presenter.Apply(twConfigurations.SelectedNode.Parent.Text, twConfigurations.SelectedNode.Text);

            if (applied)
            {
                lblError.Text = "Configuration applied";
                btnApply.Enabled = false;
            }
            else
            {
                lblError.Text = "Could not apply configuration";
            }
        }
    }
}
