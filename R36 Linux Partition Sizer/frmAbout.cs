using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace R36_Linux_Partition_Sizer
{
    partial class frmAbout : Form
    {
        public frmAbout()
        {
            // Just the basic About Dialog Form template stuff which gets the
            // application (assembly) information and puts it on the form/labels
            // This way we don't have to update the labels manually and thus won't be able
            // to accidentally forget to update them during possible future updates
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            lblApplication.Text = AssemblyProduct;
            lblVersion.Text = String.Format("Version {0}", AssemblyVersion);
            lblCopyright.Text = AssemblyCopyright;
        }

        #region Assembly Attribute Accessors
        // These are just basic functions which are included in the About Dialog Form Templated included
        // in Visual Studio (2022). I initially planned to 'just smack a template about into the application'
        // but later on decided that I did wanted it to be more custom to include the links and additional info.
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

         public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        #endregion

        #region "Default Form Functions"

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region "All the Clickable Links"

        private void LlblWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This will open a (new) browser window to the website of the XNL R36 Linux Partition Sizer:\nhttps://www.teamxnl.com/R36-Linux-Partition-Sizer", "https://www.teamxnl.com/R36-Linux-Partition-Sizer");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This will open a (new) browser window to my Main Channel On YouTube:\nhttps://www.youtube.com/@XNLFutureTechnologies", "https://www.youtube.com/@XNLFutureTechnologies");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This will open a (new) browser window to my Second Channel On YouTube:\nhttps://www.youtube.com/@XNLFutureTechnologies2", "https://www.youtube.com/@XNLFutureTechnologies2");
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This will open a (new) browser window to my Patreon Page:\nhttps://www.patreon.com/xnlfuturetechnologies", "https://www.patreon.com/xnlfuturetechnologies");
        
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This will open a (new) browser window to the XNL R36S & R36H ArkOS Central, a 'central hub' I've created with a collection of information in regards to the R36S/H and a collection of links to my releases for the R36 units:\nhttps://www.teamxnl.com/r36s-r36h-arkos-central/", "https://www.teamxnl.com/r36s-r36h-arkos-central/");
        
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This is will open a (new) browser window to my (very detailed) tutorial on how to install or Update ArkOS for the R36S and R36H, which (near the end of the tutorial) also includes a section about this program:\nhttps://www.teamxnl.com/installing-or-updating-arkos-r36s-r36h/", "https://www.teamxnl.com/installing-or-updating-arkos-r36s-r36h/");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This is will open a (new) browser window to the Official ArkOS page, wiki and downloads on GitHub:\nhttps://github.com/christianhaitian/arkos/wiki", "https://github.com/christianhaitian/arkos/wiki");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This is will open a (new) browser window to the Community Maintained version of ArkOS for the R36S/R36H, maintained by AeolusUX:\nhttps://github.com/AeolusUX/ArkOS-R3XS", "https://github.com/AeolusUX/ArkOS-R3XS");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This is will open a (new) browser window to my ArkOS Installation/Update tutorial but it will point directly to the DTB/'Display' Files section:\nhttps://www.teamxnl.com/installing-or-updating-arkos-r36s-r36h/#Step24dtbs", "https://www.teamxnl.com/installing-or-updating-arkos-r36s-r36h/#Step24dtbs");
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenExternalLinkWithConfirm("This is will open a (new) browser window to an excelent source of information when it comes to spotting fake/clone R36S units:\nhttps://handhelds.miraheze.org/wiki/R36S_Clones", "https://handhelds.miraheze.org/wiki/R36S_Clones");
        }

        private void OpenExternalLinkWithConfirm(string MessageText, string WebsiteURL)
        {
            // Show a messagebox which displays the text set at the MessageText variable )
            if (MessageBox.Show($"{MessageText}\n\nDo you want to continue to visit this website?", "XNL R36 Linux Partition Sizer", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // If the user allowed to open the link to my Main YouTube Channel, THEN we'll open a browser to it.
                Process.Start(new ProcessStartInfo(WebsiteURL) { UseShellExecute = true });
            }
        }

        #endregion


    }
}
