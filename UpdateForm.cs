using System;
using System.Deployment.Application;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace CWIcon
{
    public partial class UpdateForm : Form
    {
        private EventHandler onCancelEvent;
        public EventHandler OnCancelEvent { get => onCancelEvent; set => onCancelEvent = value; }

        public string VersionLabel
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
            }
        }

        public UpdateForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;

            int x = Screen.GetWorkingArea(this).Right - Size.Width;
            int y = Screen.GetWorkingArea(this).Bottom - Size.Height;

            this.Location = new Point(x, y);

            lbMessage.Text = "CW Icon" + VersionLabel;

        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            btUpdate.Enabled = false;
            // update cucc
            InstallUpdateSyncWithInfo();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InstallUpdateSyncWithInfo()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed == true)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;
                tbLongMessage.Text += Properties.Resources.msgUpdateStated + Environment.NewLine;

                try
                {
                    info = ad.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    tbLongMessage.Text += Properties.Resources.msgUpdateServerInaccessable + Environment.NewLine;
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    tbLongMessage.Text += Properties.Resources.msgUpdatePackageCorrupt + Environment.NewLine;
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    tbLongMessage.Text += Properties.Resources.msgUpdateInvalidApplicationDeployment + Environment.NewLine;
                    return;
                }

                if (info.UpdateAvailable == true)
                {
                    Boolean doUpdate = true;
                    /* kept for for silent update:
                     * 
                    if (info.IsUpdateRequired == false)
                    {
                        // optional update
                        DialogResult dr = MessageBox.Show("An update is available. Would you like to update the application now?", "Update Available", MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show("This application has detected a mandatory update from your current " +
                            "version to version " + info.MinimumRequiredVersion.ToString() +
                            ". The application will now install the update and restart.",
                            "Update Available", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    } */

                    if (doUpdate == true)
                    {
                        try
                        {
                            ad.Update();
                            tbLongMessage.Text += Properties.Resources.msgUpdateSuccessRestart + Environment.NewLine;   
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show("Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + dde);
                            return;
                        }
                    }
                }
                else
                {
                    tbLongMessage.Text += Properties.Resources.msgUpdateNoUpdate + Environment.NewLine;
                }
            } 
            else
            {
                tbLongMessage.Text += Properties.Resources.msgUpdateLocalDeployment + Environment.NewLine;
            }
        }


    }
}
