// =========================================================================================================================
// XNL Future Technologies R36 Linux Partition Sizer
// =========================================================================================================================
// Purpose: This program is basically an (EXTREMELY xD) over-engineered tool to simply modify the file expandtoexfat.sh
// and change a simple numeric value in it and then saving it again with UTF-8 format. Nothing which could not easily be
// done with Notepad++, I just wanted to make a small program for this to make it easier for my community to set a custom
// Linux/ArkOS partition size.
//
// At the same time I wanted to take this opportunity to make a small C# program with LOTS of comments in it so beginners
// might learn something from it on how I did things and why I did certain things.
//
// For those who might be wondering why I'm using .NET Framework 4.7.2, firstly because it does the job which
// it needs to do well enough ;), and secondly because I'm trying to keep my applications as compatible as much
// with other operating systems, try to minimize runtime requirements AND because I want my applications to
// be able to run on Microsoft Surface RT tablets which have Windows RT 10 (ARM32) running on them when possible ;)
//
//---------------------------------------------------------------------------------------------------------
// Website:   https://www.teamxnl.com/R36-Linux-Partition-Sizer
// YouTube:   https://www.youtube.com/XNLFutureTechnologies
// YouTube 2: https://www.youtube.com/XNLFutureTechnologies2
// License:   Source code is only intended for educational purposes and to give people
//            the ability to 'take a look' what this application is doing. It CAN be used
//            to learn from and use snippets to make (a different) program from them for yourself and
//            also for public release, but it's not allowed to modify this program and then re-release
//            it as an alternative version (more explained on why below)
//            for the full license please visit the website of this project/program (listed above)
//---------------------------------------------------------------------------------------------------------
//
// Reason why I don't allow alternative versions of this program in the license:
// Because like stated at the top already: It's just an over-engineered tool which basically (in the end)
// does nothing more than just changing one numeric value in a text file (.sh actually).
// And I don't see any need why this needs to be forked and re-published/released dozens of times when
// this program just does what it needs to do.
//
// Q: So I can't adapt it so that it also works for my device (which uses different files and such)?
// A: NO, because that would defeat the whole purpose of my application which makes it super clear that
//    it's intended for the R36S and R36H (and most likely also usable for the RG351MP). If there would
//    be dozens of variations of my program with just minor changes in the code and some slight graphical
//    adjustments it would only clutter the internet with dozens of variations and cause confusion, or it
//    will result in lots of repositories which are all at a different version, alternative version etc
//    without being transparent about WHAT has changed or not compared to the original
//    (which happens more than enough already these days in my opinion).
//
// Q: But what if I COMPLETELY re-do the UI, make sure there is no confusion with this version etc?
// A: IF it is indeed for a completely different device which would otherwise not be supported by my
//    version (also not when holding shift while starting the application), AND you are going to re-do
//    the application UI, make your own icons/images, remove my logo/branding from it etc, THEN it
//    won't be a problem. That would be an exception to the license limitation(s) for the source code.
//    HOWEVER, you are then still required to include credits in your source-code, your version has
//    has to be SHARE-A-LIKE (and thus also be published WITH open-source), and the compiled version
//    will need to include credits to this version and a link to the website of this version (the teamxnl.com page)
//    in for example the about screen (for example something like: Based on the XNL R36 Linux Partition Sizer).
//
//    But in my honest opinion? Just don't clutter the internet with yet another version of this program and
//    instead just make your own version which is specifically designed for your device(s) inspired on/by this one :)
//
// I know, usually I'm less "troublesome" with my releases, and allowing re-releases, but this tool would just cause
// cluttering and confussion if it gets re-released a dozen times with the same layout, function etc.
// Hope you'll understand and respect this, thank you :)
// =========================================================================================================================
using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace R36_Linux_Partition_Sizer
{
    public partial class frmMain : Form
    {
        #region "Global Declarations"

        long MaxDriveSizeLimit = 0;                 // Global Variable which will be used to calculate and set the maximum allowed size (via the slider) for the Linux Partition
        long DetectedDriveSize = 0;                 // Global variable which will be used to store the detected total Drive size
        long CurLinuxArkOSPartSize = 0;             // Global variable which will be used to store the minimal Linux/ArkOS partition size (retrieved from the initial boot files and set by the ArkOS Developer)
        long BootDriveSize = 0;                     // Global variable which will be used to store the boot partition size
        long ReservedRomPartition = 5368709120;     // Mandatory minimal size (5GB/GiB in Base-2! 1GB = 1024MB) for the ROMS Partition (to prevent possible issues with temp files, other apps/games etc)
        long BaseCalc = 1024;                       // The Application is by default configured to use Base-2 (1KB = 1024bytes, 1GB = 1024MB etc), but this can be changed with the radio buttons by the user
        string FoundBootDriveLttr = "";             // Global variable which will be used to store the detected Boot Partition it's drive letter
        int TuxClicks = 0;                          // Used to count the amount of clicks on the Tux-SD-Card within the time limit set by the timer to enable 'pro-mode' (a "hidden" feature")
        bool UsedBackupScript = false;              // Used to set if we've used the initial value(s) of a detected present backup script or not (used when for example a user decided he/she wanted to edit the boot script again after saving)
        bool BootedInIgnoreCompatibility = false;   // Used to register if users have booted the application while holding down shift to ignore the R36 Only limitations, and thus taking the risk themselves that they might mess up their Linux Configuration
        int DetectedPartitionCount = 0;             // Used to register the amount of found partitions on the same (physical) drive where we found the partition labeled BOOT

        #endregion

        #region "Main Forum Functions"

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("XNL R36 Linux Partition Sizer is already running, you can only run one instance of this program at the same time", "Program Already Active", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
                return;
            }

            // A small feature for people who do still want to use this application but NOT for the R36S/R36H, however this is 100%
            // at their own responsibility. Because I have not (and WILL NOT) test the script edits for other devices, and can't (and won't) confirm
            // it will work for other boot-scripts.
            if (Control.ModifierKeys == Keys.Shift)
            {
                BootedInIgnoreCompatibility = true;
                imgWarning.Visible = true;
                tmrBlinkWarning.Start();
                MessageBox.Show("You've started the XNL R36 Linux Partition Sizer in 'Ignore R36 Detection mode' by holding down shift during the application start!\n\nThis means that the XNL R36 Linux Partition Sizer will not particularly check for matching files which belong to the R36 image. However this program still needs certain files to be (almost) the same to be able to function properly! So even while you told it to ignore the R36 checks, it is still possible that the program does not work for your device and/or it's boot files!\n\nPROCEED AT YOUR OWN RISK!", "XNL R36 Linux Partition Sizer - Security Override!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // In this variable we'll count how many "Correct R36 boot drives" we've found. Simply because I'm going to reject
            // modifying the bootfiles if the user has more than one SD-Card inserted in his/her computer.
            int CorrectBootDrivesFound = 0;

            // A value we MIGHT need later on IF it turns out that the user is re-editing an boot file which he/she had already edited previously
            // but returned to for another round of editing ;)
            long CurrentSetUserSize = 0;

            // First We'll loop through all USB drives which report as READY!
            // This needs to be done because (if everything is correct!) your SD-Card should have two partitions ("drives")
            // which will report as ready/usable, and one which isn't because Windows can't recognize/read the Linux partition of ArkOS
            foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Removable && d.IsReady))
            {

                // If we've found the drive labeled BOOT, then we'll investigate further
                // to see IF this could be the correct card/usb drive we need to work with.
                // YES, I will refuse to work with other 'boot drives' if the partition name/label doesn't match
                // simply to prevent possible issues with other devices and such. I want this tool to be as simple as
                // possible for all 'levels of users' with minimal risk.
                if (drive.VolumeLabel == "BOOT")
                {
                    // Might be a bit confusing for some, but here name is actually the drive letter, not the 'label'
                    string DriveLtr = drive.Name;

                    // Remember the Boot Partition size (because we'll need to subtract it from the maximum size later!)                    
                    BootDriveSize = drive.TotalSize;

                    // First we'll check if the dtb file we should expect for the R36S/R36H is present or not
                    if (File.Exists($"{DriveLtr}rk3326-rg351mp-linux.dtb") || BootedInIgnoreCompatibility == true)
                    {
                        // Then we also want to make sure that both the files firstboot.sh and ext**** are present
                        // if this is the case, then this means that this card has not been inserted into the R36S/R36H yet
                        // and that we can change the Linux partition size
                        if (File.Exists($"{DriveLtr}firstboot.sh") && File.Exists($"{DriveLtr}expandtoexfat.sh"))
                        {
                            // We'll remember the drive letter of the (correct) bootdrive we've detected
                            FoundBootDriveLttr = DriveLtr;

                            // Show the (boot) drive letter we're working on in the User Interface
                            lblDriveLttr.Text = FoundBootDriveLttr;

                            // Detect if the SD-Card already contains a backup of the original boot files, this would indicate that the
                            // user had previously edited the files already but for example decided to make some additional adjustments.
                            // this is for example needed to prevent that the minimal size won't be locked to their manually set size
                            if (File.Exists($"{DriveLtr}expandtoexfat.xbck"))
                            {
                                // Get the currently configured Linux Partition Size (from the backupped Initial Boot Scripts of ArkOS)
                                CurLinuxArkOSPartSize = GetCurrentArkOSPartSize(FoundBootDriveLttr + "expandtoexfat.xbck");
                                // Register that we are working with a value from the backup script instead of the 'main script'
                                // otherwise safety checks during saving will fail!
                                UsedBackupScript = true;

                                // We'll ALSO load the already modified file but this is so that we can 'reload' the slider position to the
                                // position the user had previously set it to.
                                CurrentSetUserSize = GetCurrentArkOSPartSize(FoundBootDriveLttr + "expandtoexfat.sh");
                            }
                            else
                            {
                                // Get the currently configured Linux Partition Size (from the ORIGINAL Initial Boot Scripts of ArkOS)
                                CurLinuxArkOSPartSize = GetCurrentArkOSPartSize(FoundBootDriveLttr + "expandtoexfat.sh");
                            }

                            // If we where able to detect the 'set partition size' from the expandtoexfat script, then we'll resume with the rest of the program
                            if (CurLinuxArkOSPartSize > 0)
                            {
                                // We'll register that we've found a matching boot drive that is (well should be) ready to modify
                                CorrectBootDrivesFound++;

                                // We'll remember the (TOTAL) size of the SD-Card (which includes all three partitions)
                                DetectedDriveSize = GetDiskSizeInGB(DriveLtr);

                                // If the detected partition count for the drive that seemed to match is not exactly 3 partitions
                                // (BOOT, Linux Partition, and ROM's Partition), then we'll bail out (ALSO if the user ignored the R36 exclusive checks!
                                // because this application is specifically designed to work with (calculate for) three partitions!)
                                if (DetectedPartitionCount != 3){
                                    MessageBox.Show("Sorry, can't continue!\n\nXNL R36 Linux Partition Sizer expects three partitions on the SD-Card which for example also has the partition labeled BOOT.\n\nThis is because this program expects a boot partition, a Linux/ArkOS partition (ext4) and a ROM's partition.\n\nThe XNL R36 Linux Partition Sizer will now exit.", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Application.Exit();
                                    return;
                                }

                                // Show an illustration with an estimate of the actual SD-Card size label from the Manufacturer
                                RenderSDCardIllustration(EstimateSDCardSize(DetectedDriveSize));
                            }
                            else
                            {
                                // The error when parsing ('converting') the detected string can't be converted properly to a numeric value
                                if (CurLinuxArkOSPartSize == -2)
                                {
                                    // Show a message box which will explain that we could 
                                    MessageBox.Show("Sorry, can't continue!\n\nThere was an error while trying to parse the required Linux partition size from the expandtoexfat script.\n\nThis could either mean that your script is not compatible with this program (anymore), that you are trying to modify a bootdrive which this program isn't intended for, or that for example the ArkOS Developer(s) have changed the file so drastically that The XNL R36 Linux Partition Sizer now needs an update to be compatible (again).\n\nThe XNL R36 Linux Partition Sizer will now exit.\n", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Application.Exit();
                                    return;

                                }
                                // The error when when the required startMarker or endMarker could not be found in the expandtoexfat script (most likely indicating that the developer has changed the script significantly)
                                if (CurLinuxArkOSPartSize == -3)
                                {
                                    // Show a message box which will explain that we could not detect the required data in the expandtoextfat script
                                    MessageBox.Show("Sorry, can't continue!\n\nCould not detect the required data in the expandtoexfat script.\n\nThis could most likely mean that the ArkOS developer has changed the script and/or it's internal commands in such a way that the XNL R36 Linux Partition Sizer first requires an update before it is compatible again.\n\nIt COULD however also mean that you are trying to modify a boot drive which this application is not compatible with.\n\nThe XNL R36 Linux Partition Sizer will now exit.\n", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Application.Exit();
                                    return;
                                }

                                // Show a message box which will explain that we encountered an unknown error while trying to process the expandtoexfat script
                                MessageBox.Show("Sorry, can't continue!\n\nAn unknown exception occured while trying to process the expandtoexfat script from the SD-Card.\n\nPlease remove the SD-Card, reinsert it and try again. If the problem persists, then you could also try to re-flash the ArkOS image to your SD-Card.\n\nThe XNL R36 Linux Partition Sizer will now exit.\n", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                                return;
                            }
                        }
                        else
                        {
                            // If we DID found the expected dtb file but we did NOT find the firstboot files, then this means
                            // that this card has already been setup by the ArkOS and thus the partitioning already took place
                            // In this instance we will just outright refuse to modify the partitions, because we don't want to be
                            // the possible cause for data and/or savegame loss etc.
                            MessageBox.Show("Sorry, can't continue!\n\nWhile the correct drive and boot file(s) where detected for the R36S/R36H, it seems like it that this SD-Card has already been through the initial boot process!\n\nThe XNL R36 Linux Partition Sizer needs to be used DIRECTLY after you've flashed the image to the SD-Card, and BEFORE you insert it into the R36S/R36H for the first time.\n\nThe XNL R36 Linux Partition Sizer will now exit.\n", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                            return;
                        }

                    }
                }
            }
            if (CorrectBootDrivesFound < 1)
            {
                MessageBox.Show("Could not detect a (suitable) R36S/R36H SD-Card and/or boot partition!\n\nTo use the XNL R36 Linux Partition Sizer you will need to run this program DIRECTLY after flashing the ArkOS image to the SD-Card and BEFORE inserting (and booting) it into the R36S/R36H for the first time.\nNote: If you are experiencing detection issues, you might try to remove the SD-Card from the computer, re-insert it and then try to run the XNL R36 Linux Partition Sizer again.\n\nIF you are trying to use this program for another device than the R36, then you can TRY to force it to continue anyway by holding shift while starting the program.\n\nThe XNL R36 Linux Partition Sizer will now exit.\n", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            else if (CorrectBootDrivesFound > 1)
            {
                MessageBox.Show("More than one suitable R36S/R36H modifiable SD-Card detected!\n\nThe XNL R36 Linux Partition Sizer will only allow one R36S/R36H modifiable SD-Card to be inserted in your computer/card-reader at the same time to prevent possible issues or incorrect editing of boot scripts!\n\nPlease make sure that you only have one R36S/R36H SD-Card inserted and then restart the XNL R36 Linux Partition Sizer.\n\nThe XNL R36 Linux Partition Sizer will now exit.\n", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
            else
            {
                // Making sure that the SD-Card (it's total size) is at least 32GB to ensure we do have enough 'headroom'
                // to alter the size and still (at least) have 5GB for roms (because we won't allow to turn the entire card into Linux with my tool)
                if (DetectedDriveSize < 30089095168)
                {
                    // Yup, I DO realize that we are actually checking for 30GB here and not exactly 32GB, however this is to account for
                    // allocation tables, 10-base vs 2-base etc. And considering that the COMMON SD-Card sizes are:
                    // 2GB, 4GB, 8GB, 16GB, 32GB, 64GB, 128GB, 256GB, 512GB, 1TB etc etc, this method should not cause problems for 99.5% of the users
                    // And yes, I could have stayed closer to the 32GB by using 29 for the check for example, but just "playing it safe" here.
                    MessageBox.Show("Sorry, but The XNL R36 Linux Partition Sizer needs at least a 32GB SD-Card to have a decent (and usable) effect!\n\nIt is however recommended for more serious use of these options that you would use a 64GB or even a 128GB SD-Card.\n\nThe XNL R36 Linux Partition Sizer will now exit.\n", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }
                else
                {
                    UpdateUI();

                    // If we detected that we are using data from a backup script (indicating another round of editing)
                    if (UsedBackupScript)
                    {
                        // And the "re-loaded" user set value is loaded correctly AND the loaded original ArkOS partition size (set by the image Developer) are correct
                        if (CurrentSetUserSize >= 0 && CurLinuxArkOSPartSize > 0)
                        {
                            // then we'll going to make sure that the slider (re-)loads at the correct position again.
                            long SliderValue = (CurrentSetUserSize - CurLinuxArkOSPartSize) / 1000000;
                            tbSizeAdjuster.Value = (int)SliderValue;
                        }
                        else
                        {
                            // If we detected a possible issue with the back up, then we won't allow re-editing of the file(s) either anymore!
                            MessageBox.Show("Sorry, but it seems like it that something is going wrong!\n\nThe XNL R36 Linux Partition Sizer isn't able to properly (re-)load of your previous edit of this boot drive!\n\nYou could manually try to revert the back up by removing expandtoexfat.sh and renaming expandtoexfat.xbck to expandtoexfat.sh after that, but it might also be required to re-flash the ArkOS image to your SD-Card.\n\nThe XNL R36 Linux Partition Sizer will now exit.\n", "XNL R36 Linux Partition Resizer - Can't Continue", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Application.Exit();
                        }
                    }
                }
            }
        }
        #endregion

        #region "XNL Functions"

        private void UpdateUI()
        {
            // So that we can change the unit labels depending on the set base calculation mode
            string MBLabel = "MiB";
            string GBLabel = "GiB";

            // If the program has been set to base-10, well also use the 'unit label'
            if (BaseCalc == 1000)
            {
                MBLabel = "MB";
                GBLabel = "GB";
            }

            // This function is called to update the User Interface after the use has either moved the slider,
            // or when he/she has changed the 'calculation method' (base-10 vs base-2)
            MaxDriveSizeLimit = DetectedDriveSize - BootDriveSize - ReservedRomPartition;
            double SliderMax = (long)(MaxDriveSizeLimit - CurLinuxArkOSPartSize) / 1000000;
            tbSizeAdjuster.Maximum = (int)SliderMax;

            // CURRENTLY not even necessary, but here we'll check if the boot drive should
            // be labeled with MB or GB. (Although I don't suspect it will EVER needs to be GB, but just future proofing it)
            double BootPartSize = BootDriveSize / (BaseCalc * BaseCalc);
            if (BootPartSize >= BaseCalc)
            {
                lblBootPartSize.Text = ((double)BootPartSize / BaseCalc).ToString("F0") + GBLabel;
            }
            else
            {
                lblBootPartSize.Text = BootPartSize.ToString("F0") + MBLabel;
            }

            // Show the current Minimal Linux/ArkOS partition size (and represent it on screen using the set calculation method base-10 or base-2)
            lblMinimalLinuxPartitionSize.Text = ((double)CurLinuxArkOSPartSize / (BaseCalc * BaseCalc * BaseCalc)).ToString("F2") + GBLabel;

            // If the user choose to use base 10 configuration then we'll add a plus-minus in front of the detected size to indicate that it's
            // not fully accurate due to the calculation methods used (conversions from long to integers etc)
            String PrefixDetectedSize = "";
            if (BaseCalc == 1000) { PrefixDetectedSize = "±"; }

            // Show the detected (usable) card size in either base-10 or base-2 depending on the selected setting
            lblCardSize.Text = PrefixDetectedSize + ((double)DetectedDriveSize / (BaseCalc * BaseCalc * BaseCalc)).ToString("F2") + GBLabel;

            long AddedSize = (long)tbSizeAdjuster.Value * 1000000;

            // Then we'll add the 'new MB's' as bytes to the CurLinPartSize so we can display it and maybe use it for processing the file later
            long CurLinPartSize = CurLinuxArkOSPartSize + AddedSize;

            // Show the current bytes in the textbox (if it's not hidden)
            txtNewSize.Text = CurLinPartSize.ToString();

            // Show the current (selected) Linux/ArkOS partition size (again using either base-10 or base-2)
            lblLinuxPartSize.Text = ((double)CurLinPartSize / (BaseCalc * BaseCalc * BaseCalc)).ToString("F2") + GBLabel;


            // Next we'll calculate the new ROM's partition size with the following steps:
            // - First we'll take the full detected size of the drive and 'scale it down' to GB's (using the selected base method)
            // - Then we'll substract the current value from the slider from this full SD-Card size (because the value of the slider basically indicated the desired size of the Linux Partition)
            // - And then we also substract the size of the already pre-defined bootdrive (it's just about 110MB or so (at the moment of writing this), but still, just to keep it a bit more accurate)
            double RomsPartSize = ((double)DetectedDriveSize / (BaseCalc * BaseCalc * BaseCalc)) - ((double)CurLinPartSize / (BaseCalc * BaseCalc * BaseCalc)) - ((double)BootDriveSize / (BaseCalc * BaseCalc * BaseCalc));

            // And then we'll update the label underneath the "ROM's SD-Card" with the currently selected size of the ROM's Partition
            lblRomsPartSize.Text = RomsPartSize.ToString("F2") + GBLabel;
        }

        static long GetCurrentArkOSPartSize(string filePath)
        {
            // This function is used to read the minimal set Linux/ArkOS partition size from the file
            // expandtoexfat.sh on the BOOT Partition. This file is used during the inital boot of ArkOS
            // after flashing the ArkOS Image to the SD-Card. The size read from this file is set by the developer
            // of the ArkOS image that has been used to flash this SD-Card, and thus we will not allow the users of
            // XNL R36 Linux Partition Sizer to make the Linux/ArkOS partition smaller than the size set by the ArkOS/Image
            // developer in that file.

            // We'll use a 'try-catch' block, so we can easily catch any (unforeseen) errors while retrieving the pre-set partition size
            // from the script. Usually I would use more specific error handling, but considering everything needs to match and work
            // and otherwise I WANT to let this Application abort, we can just simply (and lazy) use a try-catch ;)
            try
            {
                // We'll read the file (which has been passed as parameter when we called this function) into a string/"memory"
                string fileContent = File.ReadAllText(filePath);

                // The start marker of the section inbetween we want to find the pre-set Linux Partition size (in bytes)
                string startMarker = "newExtSizePct=$(printf %.2f \"$((10**4 * ";

                // The end marker/position of the section inbetween we want to find the pre-set Linux Partition size (in bytes)
                string endMarker = "/$maxSize))e-4\")";

                // Find the positions of markers in the string
                int startIndex = fileContent.IndexOf(startMarker);
                int endIndex = fileContent.IndexOf(endMarker, startIndex);

                // If BOTH have returned a valid position (meaning both 'markers' have been found) AND the EndMarker
                // was found in the string/file at a later position than the start marker, THEN we will continue to
                // read the 
                if ((startIndex != -1 && endIndex != -1) && endIndex > startIndex)
                {
                    // Here will add the length of the startMarker string to the startIndex position where this string was found
                    // this basically means that if we from here on use startIndex, that it won't start at the beginning of the
                    // 'search string'/startMarker, but that it will start at the end of that startMarker. Because we only want
                    // the number value between the startMarker and the endMarker, we do not also want to include the entire
                    // "startMarker".
                    startIndex += startMarker.Length;

                    // Now we'll subtract a substring from the mainstring (which contains the entire file), where we will
                    // 'start extracting' at the (now adjusted) startIndex, and to know the length of the string between the
                    // start and endMarker we'll simply subtract the startIndex from the endIndex, and then you basically have
                    // the amount of characters between these two markers :)   lastly we'll add .Trim() to the 'command' which
                    // will basically ensure that there are no trailing or leading whitespaces (spaces, tabs, newlines etc) in the substring.
                    string numberString = fileContent.Substring(startIndex, endIndex - startIndex).Trim();


                    // Next we'll try to convert the found substring into an actual numeric value (a long)
                    // and if this fails we'll just return -1, which we are using as error code in this Application
                    if (long.TryParse(numberString, out long extractedValue))
                    {
                        return extractedValue;  // If it succeeded we return the found value
                    }
                    else
                    {
                        return -2;              // If 'conversion' failed, we'll return a 'custom error code' (-2)
                    }
                }
                else
                {
                    // If we could not detect the correct startMarker or endMarker, then we'll return a 'custom error code' (-3) so that
                    // we can inform the user that we suspect that this is either not a valid ArkOS initial boot file, or that the file for
                    // example might have been updated/changed and that this Application is no longer compatible with this file.
                    return -3;
                }
            }
            catch
            {
                // If ANY unexpected error occurred, we'll just return our default error code (-1)
                return -1;
            }
        }

        private string EstimateSDCardSize(long DetectedSizeInBytes)
        {
            // This is a small and simple 'helper function' which quickly returns the estimated SD-Card size
            // in human readable format (32GB for example) when the function is passed the detected size in bytes.
            // Reason why I'm using quite a 'large range' to detect/guess the SD-Card size, is because I want to
            // account for SD-Cards which report a different size than expected (even when taking base-10 / base-2 into account).

            // NOTE: We DO NOT have to change the values here based on when the user selects base-10 or base-2, because these
            // calculations are done only once during the start of the application and for this 'estimation' raw-byte values are used

            // This one basically reports that it's (most likely!) an 32GB SD-Card when the passed SizeInBytes is larger or
            // equal to 30000000000 bytes (a "tad" under 32GB) and smaller than 60000000000 bytes (nearing 64GB) 
            if (DetectedDriveSize >= 30000000000 && DetectedDriveSize < 60000000000)
            {
                return "32GB";
            }

            // And the same basically happens for each following block with increasing steps
            // Won't comment on all of them though, that's just not necessary here ;)
            if (DetectedDriveSize >= 60000000000 && DetectedDriveSize < 120000000000)
            {
                return "64GB";
            }
            if (DetectedDriveSize >= 120000000000 && DetectedDriveSize < 240000000000)
            {
                return "128GB";
            }
            if (DetectedDriveSize >= 240000000000 && DetectedDriveSize < 500000000000)
            {
                return "256GB";
            }
            if (DetectedDriveSize >= 500000000000 && DetectedDriveSize < 1000000000000)
            {
                return "512GB";
            }
            if (DetectedDriveSize >= 1000000000000 && DetectedDriveSize < 1400000000000)
            {
                return "1TB";
            }
            if (DetectedDriveSize >= 1400000000000 && DetectedDriveSize < 1900000000000)
            {
                return "1.5TB";
            }
            if (DetectedDriveSize >= 1900000000000 && DetectedDriveSize < 2300000000000)
            {
                return "2TB";
            }

            // If none of the above matched, then we'll just return >2TB (meaning bigger than 2TB)
            // so this is currently/basically just the 'hardcap limit' of this application.
            // Which is in my opinion MORE than enough for the consoles for which this application is intended for ;) 
            return ">2TB";
        }

        private void RenderSDCardIllustration(string SDCardSize)
        {
            // A Simple function which draws the (estimated) size of the detected SD-Card
            // on the SD-Card image in the top-left of the application

            // Load the "blank" SD-Card from the application resources
            Image SDCardIllustration = Properties.Resources.SDCardIllu;

            // Create a new bitmap in memory (to draw on and from the image we've just 'loaded')
            Bitmap bitmap = new Bitmap(SDCardIllustration);

            // Using basically means that everything we do to manipulate the variable GraphicsDraw
            // will happen between this 'using block', once we leave this block the resources are
            // also released again and can for example be cleaned up by the garbage collector.
            using (Graphics GraphicsDraw = Graphics.FromImage(bitmap))
            {
                // Enabling Anti-aliasing for smoother text drawing
                GraphicsDraw.SmoothingMode = SmoothingMode.HighQuality;

                // Create an object called DrawingFont, which we setup as a new font
                Font DrawingFont = new Font("Arial", 23, FontStyle.Bold);

                // Create an object called DrawingBrushColor which we will use to give our 'brush'
                // which we will use to draw the text on the SD-card a custom color from RGB (Red, Green and Blue)
                // The range for RGB here is: 0 to 255
                Color DrawingBrushColor = Color.FromArgb(234, 212, 241);

                // Anothe using blick, but now we create and use an object called DrawingBrush
                using (Brush DrawingBrush = new SolidBrush(DrawingBrushColor))
                {
                    // Set the position of the 'brush' where it needs to start drawing
                    PointF position = new PointF(13, 29);
                    
                    // Drawing the String onto the GraphicsDraw object, where SDCardsize is the parameter we've passed into this
                    // function, the DrawingFont is the font we've setup above, just like the DrawingBrush and
                    // the position where we want to start drawing the string (of text)
                    GraphicsDraw.DrawString(SDCardSize, DrawingFont, DrawingBrush, position);
                }
            }

            // Lastly we'll update the image on the Form itself with the 'newly drawn' image.
            imgSDIllustration.Image = bitmap;
        }

         long GetDiskSizeInGB(string driveLetter)
         {
             //=========================================================================================================================
             // As those more proficient with WMI might have noticed here: I absolutely HATE working with it, always
             // struggle with that stuff and won't deny that either xD. This most likely (well IS) a mess, but hey, it works and
             // does what it needs to do 😂. Whatever you do, don't use this part as a 'perfect example' because it's nowhere
             // near anything like that haha. Every time I did got it working as I wanted and then switched to another SD-Card, or
             // card reader something else "broke" again. And yeah, that most definitely has to do with me and my everlasting struggle
             // with WMI😂, but: I. DO. NOT. CARE. It works now🤷🏽‍
             // And YES, I have tried combining the queries, but I just s*ck at WMI queries 😂 and I kept breaking it
             //=========================================================================================================================
         
             // If the drive letter for example is being passed as E:\ then we'll strip the \
             // NOTE: The double \ is because it's an escape character in strings, so we remove it to just get the drive letter (e.g. "E").
             driveLetter = driveLetter.Replace("\\", "");
         
             try
             {
                 // Preparing a query which we are going to use with WMI to find the logical disk of which the detected 'boot drive' is part of
                 // The drive letter we are using for this has been passed as parameter when calling this function.
                 string query = $"SELECT * FROM Win32_LogicalDisk WHERE DeviceID = '{driveLetter}'";
         
                 // 'using' is used here to manage the searcher object and ensure it's properly disposed of when done.
                 using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
                 {
                     // Loop through all found logical disks (there should basically only be 1 matching disk).
                     foreach (ManagementObject disk in searcher.Get())
                     {
                         // Get the DeviceID for the logical disk
                         string deviceId = disk["DeviceID"].ToString();
         
                         // Query to find the partition associated with the logical disk. Were we're using the logical disk's DeviceID to find its associated partition
                         string partitionQuery = $"ASSOCIATORS OF {{Win32_LogicalDisk.DeviceID='{deviceId}'}} WHERE AssocClass=Win32_LogicalDiskToPartition";
         
                         // Another 'using' statement to manage the partition searcher and dispose it properly when done
                         using (ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(partitionQuery))
                         {
                             // Loop through found partitions
                             foreach (ManagementObject partition in partitionSearcher.Get())
                             {
                                 // Get the partition's DeviceID
                                 string partitionId = partition["DeviceID"]?.ToString();
         
                                 // If partitionId is null or empty, we skip it and move to the next partition.
                                 if (string.IsNullOrEmpty(partitionId)) continue;
         
                                 // Query to find the physical disk associated with the partition. This is to link the partition back to its corresponding physical disk.
                                 string diskQuery = $"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partitionId}'}} WHERE AssocClass=Win32_DiskDriveToDiskPartition";
         
                                 // Another 'using' to manage the physical disk searcher.
                                 using (ManagementObjectSearcher diskSearcher = new ManagementObjectSearcher(diskQuery))
                                 {
                                     // Loop through all found physical disks (again: should just be one).
                                     foreach (ManagementObject physicalDisk in diskSearcher.Get())
                                     {
                                         // Reset the partition counter
                                         DetectedPartitionCount = 0;
                                         
                                         // YUP! ANOTHER WMI Query 😂
                                         string partitionCountQuery = $"SELECT * FROM Win32_DiskPartition WHERE DiskIndex = {physicalDisk["Index"]}";
                                         using (ManagementObjectSearcher partitionSearcher2 = new ManagementObjectSearcher(partitionCountQuery))
                                         {
                                            // Storing the amount of partitions of this physical drive in a global variable,
                                            // because we will ONLY proceed if the SD-Card has exactly three partitions (boot, a Linux partition and roms)
                                            DetectedPartitionCount = partitionSearcher2.Get().Count;
                                         }
                                         // Check if the physical disk has the "Size" property.
                                         // This "Size" represents the total size of the disk in bytes.
                                         if (physicalDisk["Size"] != null)
                                         {
                                             // Convert the size (which is a ulong) to a long for the return value.
                                             ulong totalSize = (ulong)physicalDisk["Size"];
                                             return (long)totalSize; // Return the total size of the disk in bytes.
                                         }
                                     }
                                 }
                             }
                         }
                     }
                 }
             }
             catch
             {
                 // If any exception occurs during the execution of the queries, return -1 to indicate an error
                 return -1;
             }
         
             // Return -1 if no matching disk has been found (meaning the drive letter was not valid)
             return -1;
         }

        public static bool ModifyBootScript(string filePath, string newSizeValue)
        {
            // These are (just like the function where we fetch the current partition size) the markers
            // were inbetween we'll have to set the new partition size value
            string startMarker = "newExtSizePct=$(printf %.2f \"$((10**4 * ";
            string endMarker = "/$maxSize))e-4\")";

            // This is a new inserted section of code which will basically clean up the back-up we've created
            // of the initial bootscript file. This backup would only be needed if for some reason the overwritten file
            // doesn't work (anymore). BUT if this line of code inside the original is able to remove the back up, then
            // this also means that the overwritten file was able to run (and thus we don't need the backup either anymore)
            string insertText = "# Clean up the expandtoexfat back-up from XNL R36 Linux Partition Sizer\n" +
                                "sudo rm -v /boot/expandtoexfat.xbck\n";

            string fileContent = "";
            // Read the entire original file to memory
            try
            {
                fileContent = File.ReadAllText(filePath);
            }
            catch
            {
                MessageBox.Show("Error while trying to read the data from the original boot script!\n\nThe XNL R36 Linux Partition Sizer was unable to apply your changes to the boot script!", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            // First we'll find the two 'markers' where we inbetween want to set the new Linux Partition size
            int startIndex = fileContent.IndexOf(startMarker);
            int endIndex = fileContent.IndexOf(endMarker, startIndex);

            // If we have found a valid location for that marker then we'll resume editing the file
            if ((startIndex != -1 && endIndex != -1) && endIndex > startIndex)
            {
                // Here we basically find the initially set Linux Partition size
                // this code is a bit more elaborately explained in the function GetCurrentArkOSPartSize
                startIndex += startMarker.Length;
                string originalNumber = fileContent.Substring(startIndex, endIndex - startIndex).Trim();

                // Then we basically just do a simple string replacement
                fileContent = fileContent.Replace(originalNumber, newSizeValue);

                // We'll check if the loaded file already contains our 'backup cleanup line' or not (and thus means that this file has been edited previously)
                int DetectRemoveBackUpLine = fileContent.IndexOf("sudo rm -v /boot/expandtoexfat.xbck");
                // And if we did NOT find the 'backup removal line', only THEN we'll add it to the script (at least if we can find the following string that is)
                if (DetectRemoveBackUpLine == -1)
                {
                    // Here we'll look for a particular string, and if that line of code is found then we'll going to inject
                    // another line of code (and a comment) above that line
                    int insertIndex = fileContent.IndexOf("if [ $exitcode -eq 0 ]; then");
                    if (insertIndex != -1)
                    {
                        // Here we'll try to find the area where we can safely insert our additional clean up code while at the same time making sure that it is actually being able to run.
                        // // We can't append it all the way at the bottom of the original script, because in the final few lines the script actually removes itself, and then calls
                        // // the function reboot. So the removal of our backup would never be reached if we would just appended it to the end of the file.
                        fileContent = fileContent.Insert(insertIndex, insertText);
                    }
                    else
                    {
                        // If we didn't found that line of code we'll warn the user that he/she needs to clean-up the backup we've made him/herself after
                        // the R36S/H has properly (and fully) booted. It does however not matter if the user would just leave that backup on the bootdrive
                        // because it won't affect the system in anyway if they did.
                        MessageBox.Show("Sorry, XNL R36 Linux Partition Sizer was unable to add the automatic clean-up lines to remove the expandtoexfat.xbck after the device has successfully booted into Linux/ArkOS.\n\nThis means that you will have to remove the expandtoexfat.xbck file yourself after your device has fully completed the partitioning.\n", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                try
                {
                    // Write the file back to the boot drive/partition in UTF8 without using Byte Order Mark (BOM) to be as much as possible Unix/Linux/Bash compatible
                    File.WriteAllText(filePath, fileContent, new System.Text.UTF8Encoding(false));
                }
                catch (Exception ex)
                {
                    // We'll display an error (messagebox) uf there was an error while writing the file back to the SD-Card 
                    MessageBox.Show($"Sorry, There was an unknown error while trying to write the file back to your SD-Card:\n{ex.Message.ToString()}\n", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                return true;
            }
            else
            {

                return false;
            }
        }

        #endregion

        #region "User Application Controls"

        private void tbSizeAdjuster_ValueChanged(object sender, EventArgs e)
        {
            // So that we can change the unit labels depending on the set base calculation mode
            string GBLabel = "GiB";

            // If the program has been set to base-10, well also use the 'unit label'
            if (BaseCalc == 1000)
            {
                GBLabel = "GB";
            }

            // Here we'll convert the value/ticks of the slider to 1MB per tick (in base-10 to bytes)
            long AddedSize = (long)tbSizeAdjuster.Value * 1000000;

            // Then we'll add the 'new MB's' as bytes to the CurLinPartSize so we can display it and maybe use it for processing the file later
            long CurLinPartSize = CurLinuxArkOSPartSize + AddedSize;

            // Show the current bytes in the textbox (if it's not hidden)
            txtNewSize.Text = CurLinPartSize.ToString();

            // Show the current (selected) Linux/ArkOS partition size (again using either base-10 or base-2)
            lblLinuxPartSize.Text = ((double)CurLinPartSize / (BaseCalc * BaseCalc * BaseCalc)).ToString("F2") + GBLabel;

            // Next we'll calculate the new ROM's partition size with the following steps:
            // - First we'll take the full detected size of the drive and 'scale it down' to GB's (using the selected base method)
            // - Then we'll substract the current value from the slider from this full SD-Card size (because the value of the slider basically indicated the desired size of the Linux Partition)
            // - And then we also substract the size of the already pre-defined bootdrive (it's just about 110MB or so (at the moment of writing this), but still, just to keep it a bit more accurate)
            double RomsPartSize = ((double)DetectedDriveSize / (BaseCalc * BaseCalc * BaseCalc)) - ((double)CurLinPartSize / (BaseCalc * BaseCalc * BaseCalc)) - ((double)BootDriveSize / (BaseCalc * BaseCalc * BaseCalc));

            // And then we'll update the label underneath the "ROM's SD-Card" with the currently selected size of the ROM's Partition
            lblRomsPartSize.Text = RomsPartSize.ToString("F2") + GBLabel;
        }

        private void rbBase10_CheckedChanged(object sender, EventArgs e)
        {
            // This is the code which switches the used calculation method
            // between base-10 (1GB = 1000MB) and base-2 (1GB = 1024MB)
            // Manufacuters of storage devices almost always use base-10 to advertise the size of their device,
            // while most operating systems actually use base-2 to show the drive size and free space for example.
            if (rbBase10.Checked == true)
            {
                BaseCalc = 1000;
            }
            else
            {
                BaseCalc = 1024;
            }

            // The function which updates the User Interface after the calculation method has been changed by the user
            UpdateUI();
        }

        private void LlblAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAbout ShowAbout = new frmAbout();
            ShowAbout.Owner = this;
            ShowAbout.ShowDialog();
        }

        private void LlblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmHelpAndInfo ShowHelp = new frmHelpAndInfo();
            ShowHelp.Owner = this;
            ShowHelp.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Just a simple hard exit out of the XNL R36 Linux Partition Sizer
            Application.Exit();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // NOTE: I'm doing SOME basic checks to ensure that the drive is still there, hasn't been swapped with another one etc,
            // but I will not go to 'large extends' into doing so. If the user on purpose decided to circumvent the 'failsafe/idiot-proofing'
            // then it is just his/her own fault if stuff goes wrong ;) (Let's be honest: If you on purpose remove or swap the SD-Card
            // after the application has started and done it's safety checks, then you VERY WELL know what you are trying to do!)


            // Ask the user if he/she is happy with the currently set configuration or not and if he/she wants to proceed to write the modifications to the initial bootfiles or ArkOS
            if (MessageBox.Show($"You are about to configure the initial boot scripts of your ArkOS installation for the following sizes:\nBoot Partition: {lblBootPartSize.Text} (Fixed size)\nLinux/ArkOS Partition: {lblLinuxPartSize.Text}\nROM's Partition: {lblRomsPartSize.Text}\n\nAre you sure that this is the desired configuration?\n\nNOTE:\nModifying the initial boot scripts is of course at your own risk!\n\nA backup of the original boot script will be created at: {FoundBootDriveLttr}expandtoexfat.xbck", "XNL R36 Linux Partition Sizer", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                // And if the user replied/clicked no, then we'll simply bail out and won't resume modifying any files
                return;
            }

            // first I'm checking if all the required files (to Match the R36S/R36H) are still present on the disk
            if ((File.Exists($"{FoundBootDriveLttr}rk3326-rg351mp-linux.dtb") || BootedInIgnoreCompatibility == true) && File.Exists($"{FoundBootDriveLttr}firstboot.sh") && File.Exists($"{FoundBootDriveLttr}expandtoexfat.sh"))
            {
                // Then I will once more load the partition size set by the developer of the ArkOS image which was used to flash the SD-Card
                long CurLinuxArkOSPartSizeCheck = GetCurrentArkOSPartSize(FoundBootDriveLttr + "expandtoexfat.sh");

                // I will check if the SD-Card still has the same size as when the application was started
                long DetectedDriveSizeCheck = GetDiskSizeInGB(FoundBootDriveLttr);

                // When it turns out that physical the SD-Card size "magically changed" ;)
                if (DetectedDriveSizeCheck != DetectedDriveSize)
                {
                    MessageBox.Show("The SD-Card size does not seem to match with the size of the SD-Card which was detected upon starting the XNL R36 Linux Partition Sizer!\n\nI don't know what you are trying to do, but this program is ONLY intended to be used with ArkOS for use on the R36S/R36H for a reason.\n\nIf you just wanted to do another SD-Card for a different R36S/R36H then please just restart the XNL R36 Linux Partition Sizer to start over with a fresh set of data.\n\nSave opperation canceled.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // When it turns out that data in the initial bootfile "magically changed" compared to the data we've detected during the start of our application 
                // BUT, if the application has been started while we loaded the data (partially) from a backup already created by this program, then we'll ignore this check
                if (CurLinuxArkOSPartSizeCheck != CurLinuxArkOSPartSize && UsedBackupScript == false)
                {
                    MessageBox.Show("The data in the initial boot scripts for ArkOS doen't seem to match with the data which was detected upon starting the XNL R36 Linux Partition Sizer!\n\nI don't know what you are trying to do, but this program is ONLY intended to be used with ArkOS for use on the R36S/R36H for a reason.\n\nIf you just wanted to do another SD-Card for a different R36S/R36H then please just restart the XNL R36 Linux Partition Sizer to start over with a fresh set of data.\n\nSave opperation canceled.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // If we haven't created a backup of the original boot script yet, we will do so now, otherwise we will just leave it as is
                // because we don't want multiple overwrites if a user for example keeps changing his/her mind with the partition size he/she wants to allocate
                if (!File.Exists($"{FoundBootDriveLttr}expandtoexfat.xbck"))
                {
                    try
                    {
                        // We'll TRY to make a back up of the original script, and if that fails we will bail out.
                        System.IO.File.Copy($"{FoundBootDriveLttr}expandtoexfat.sh", $"{FoundBootDriveLttr}expandtoexfat.xbck");
                    }
                    catch
                    {
                        // If we are not able to create a backup of the initial bootscript for some reason then we'll abort the save operation
                        MessageBox.Show($"Could not create a backup of the original initial bootscript ({FoundBootDriveLttr}expandtoexfat.sh).\n\nSave opperation canceled.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Try to save the modified boot script
                if (ModifyBootScript($"{FoundBootDriveLttr}expandtoexfat.sh", $"{txtNewSize.Text}") == true)
                {
                    try
                    {
                        // We'll store the size with which we've increased the Linux partition to a file on the boot drive,
                        // this can then be used by my other programs running on the R36 like the XNL Package Manager for example to determine IF
                        // it would be 'safe' to install very large packages without 'stealing' reserved storage space from future updates of ArkOS
                        long AddedSize = (long)tbSizeAdjuster.Value * 1000000;
                        File.WriteAllText($"{FoundBootDriveLttr}.xnlft-linpatext", $"{AddedSize}\n", new System.Text.UTF8Encoding(false));
                    }
                    catch
                    {
                        // Nope, It's not a "big-disaster" if writing to this file fails, we'll also use other methods to ensure that there is
                        // enough storage on the device when installing (insanely) large packages using the XNL Package Manager, but if this function
                        // did saved properly it will also show it in the XNL System Information for the user, and other packages will be a bit easier
                        // to install (but nothing the user will notice)
                    }

                    MessageBox.Show("The Initial Boot Files for your ArkOS installation have successfully been updated.\n\nYou can now remove the SD-Card from your computer, insert it into your R36S/R36H and ArkOS will do the rest for you (re-sizing the partitions) upon it's first boot.\n\nThe XNL R36 Linux Partition Sizer will now exit.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Euhm... yeah.... ;)
                    Application.Exit();
                }
                else
                {
                    // Upon failure when trying to save we'll give an error message
                    MessageBox.Show("An unknown error occured while trying to update the inital partition creation bootscript!\n\nPlease proceed with caution or manually restore the created backup expandtoexfat.xbck by renaming it back to expandtoexfat.sh\n\nIt might also be needed to just re-flash the entire ArkOS image to your SD-Card (worse-case-scenario though).", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // If we don't detect the boot files anymore (could be SD-Card fail, removed SD-Card, swapped SD-card etc), we will give an error
                MessageBox.Show("For some reason the expected bootfiles for the R36S/R36H are no longer being detected as present on the SD-Card!\n\nI don't know what you are trying to do, but this program is ONLY intended to be used with ArkOS for use on the R36S/R36H for a reason.\n\nIf you just wanted to do another SD-Card for a different R36S/R36H then please just restart the XNL R36 Linux Partition Sizer to start over with a fresh set of data.\n\nSave opperation canceled.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
        }

        #endregion

        #region "User information buttons/clickables aka 'Interactive Help'"

        private void lblCardSize_Click(object sender, EventArgs e)
        {
            // Showing a messagebox with information about the detected card size and why it might appear smaller than what is printed on the SD-Card/Packaging
            MessageBox.Show("This is the detected total size of the ArkOS SD-Card you've flashed the image to.\n\nNOTE: It is normal if the drive shows less space than the card is advertised with (for example showing 31GB for a 32GB Card).\n\nThis simply put has to do with filesystem overhead and other technical things ;) (or the calculation method you've selected bellow).\n\nIf you noticed that the sizes don't add up perfectly (especially when using base-10), then please read the help to understand why.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblNewSize_Click(object sender, EventArgs e)
        {
            // Messagebox explaining what the textbox is for when users have enabled 'pro-mode'
            MessageBox.Show("You can copy-paste this the value from this textbox directly into your expandtoexfat.sh file to edit it yourself.\n\nNOTE: Do make sure that you know what you are doing and that you are using the correct file format and editor!.\n\nIt is HIGHLY recommended to first read the help section about doing this yourself.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblDriveLttr_Click(object sender, EventArgs e)
        {
            // Showing a messagebox explaining that this is the detected drive letter and asking if they would like to open this drive in File Explorer
            if (MessageBox.Show("This is the detected drive letter of your ArkOS Boot Partition.\n\nWould you like to open this drive in Windows File Explorer to view the contents?", "XNL R36 Linux Partition Sizer", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Process.Start("explorer.exe", $"{lblDriveLttr.Text}");
            }
        }

        private void imgSDIllustration_Click(object sender, EventArgs e)
        {
            // Showing a messagebox explaining that the top left SD-Card is just an estimated 'guess' on the SD-Card size
            MessageBox.Show("This is just an illustration, the size shown is an calculated estimation based on the detected (usable) size of your SD-Card.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lblSliderTitle_Click(object sender, EventArgs e)
        {
            // Showing a messagebox with a small 'help message' on how to use the slider
            MessageBox.Show("With this slider you can choose how much storage space you wish to assign to Linux/ArkOS and how much you want to keep/assign for ROM's, games, savegames, media etc.\n\nMoving the slider to the left will assign more space to Linux/ArkOS, moving the slider to the right will assign more space to the roms partition (EASYROMS).", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void imgBootPartition_Click(object sender, EventArgs e)
        {
            // Show a messagebox explaning what the boot partition is and that the user can't change the size of it
            MessageBox.Show("This SD-Card represents the BOOT Partition of your SD-Card, this partition basically contains the (like the name suggests) boot files needed to boot Linux/ArkOS.\n\nThis is also the partition on which your dtb files (and \"display drivers\" simply put) are located.\n\nNOTE: You can't change the size of this boot partition, and neither should you use it for anything else.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void imgLinuxPartition_Click(object sender, EventArgs e)
        {
            // If the user clicks on Tux while control AND shift are being held down
            if (isCtrlPressed && isShiftPressed)
            {
                // If it's less than 10 times a user has clicked on tux within the set 'time limit' Then...
                if (TuxClicks < 10)
                {
                    // add +1 to the times the user has clicked tux
                    TuxClicks++;
                    // Play a default system beep to let the user hear it has been registred
                    System.Media.SystemSounds.Beep.Play();
                    // Reset the timer so we have another 1.2 seconds till the next required click
                    tmrProMode.Stop();
                    tmrProMode.Start();

                    // 10 or more clicks? Then we'll enable pro mode
                    if (TuxClicks >= 10)
                    {
                        // Timer can be disabled now
                        tmrProMode.Stop();
                        // Setting the value so high that this block won't activate a second time at all anymore
                        TuxClicks = 100;
                        // Show the message that pro-mode has been enabled
                        MessageBox.Show("'Pro-mode' Enabled!\n\nYou can now copy the bytes from the text field that appeared into the expandtoexfat.sh file to edit it yourself.\n\nWARNING: Do make sure you know what you are doing! and PLEASE make sure to read the help about editing that file if you want to make sure you won't mess it up!", "XNL R36 Linux Partition Sizer - Hidden Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Showing the 'pro-mode' textbox and label (in a groupbox)
                        gbProMode.Visible = true;
                    }
                }
                else
                {
                    // If the amount of clicks on Tux are already registred to be more than 10 (100 due to our 'lock' above) then we'll show a message that Pro-mode is already active
                    MessageBox.Show("'Pro-mode' has already been enabled.\n\nYou can now copy the bytes from the text field that appeared into the expandtoexfat.sh file to edit it yourself.\n\nWARNING: Do make sure you know what you are doing! and PLEASE make sure to read the help about editing that file if you want to make sure you won't mess it up!", "XNL R36 Linux Partition Sizer - Hidden Feature", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // And if Tux is being clicked without control and shift are being held down, then we'll show a messagebox explaning what the "Tux Partition/SD-Card represents
                MessageBox.Show("This SD-Card represents the selected size of the Linux/ArkOS Partition of your SD-Card after you save the changes with the XNL R36 Linux Partition Sizer.\n\nThe minimum allowed size for this partition has been set by the developer of the ArkOS image you've used to flash this SD-Card.\n\nTIP: Click on help at the top right if you want some general tips/recommendations about the size options for your Linux partition.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void imgRomsPartition_Click(object sender, EventArgs e)
        {
            // Show a messagebox explaining what the "Games SD-Card Illustration" means
            MessageBox.Show("This SD-Card represents the selected size of the ROM's partition (often labeled EASYROMS) after you save the changes with the XNL R36 Linux Partition Sizer.\n\nWhen you increase the size of the Linux/ArkOS partition (to for example make/reserve more space for additional software, mods, drivers etc), you will basically 'sacrifice' the extra amount you are assigning to Linux/ArkOS from the ROM's partition.\n\nThe XNL Linux Partition Sizer has a hardcoded limit which prevents you shrinking the ROM's partition below 5GB. This to prevent possible compatibility issues with other applications, games, scripts etc.\n\nNOTE: This limit is set with base-2 (1GB = 1024MB).", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void lblMinimalLinuxPartitionSize_Click(object sender, EventArgs e)
        {
            // Show a messagebox explaning what the minimal Linux Partition size means and where it comes from
            MessageBox.Show("This is the minimal size the Linux/ArkOS partition needs to be.\n\nThis size has been set by the developer of the ArkOS Image you've used to flash your SD-Card with.\n\nYou are not be able make the Linux partition smaller than this value.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Show a messagebox explaining the basic difference between base-10 and base-2
            MessageBox.Show("There are basically two ways to calculate storage size: base-10 and base-2.\n\nStorage manufacturers use base-10 when telling you that you for example bought a 128GB SD-Card.\n\nOperating systems like Windows and Linux actually use base-2 to calculate storage sizes.\n\nBase-10 (Decimal System):\nIn base-10 1GB = 1000MB\n\nBase-2 (Binary System):\nIn base-2 1GB = 1024MB\nNOTE: The official label for base-2 GB's is actually GiB.\n\nWhich is also the reason why you would for example only see/'get' about 119.2GB in Windows for an SD-Card labeled as 128GB.", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void imgWarning_Click(object sender, EventArgs e)
        {
            // Showing a warning message that the user is using the program in an override mode to ignore the check for the R36/RG351MP Boot file!
            MessageBox.Show("The XNL R36 Linux Partition Sizer has been started in a mode where it's told to ignore the checks for R36 speciffic boot files!\n\nIf you are able to see this message then it does mean that the program did still detected the firstboot.sh and expandtoexfat.sh scripts AND that your bootdrive is indeed also labeled BOOT, but this does NOT guarantee that the XNL R36 Linux Partition Sizer will work as expected for your device!\n\nUSE WITH CAUTION, PROCEED AT YOUR OWN RISK!", "XNL R36 Linux Partition Sizer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        #endregion

        #region "User Clickable Links / LinkLabels"

        private void LlblYouTube1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show a messagebox telling that this will open a link to my YouTube (Main Channel) and then asking confirmation IF the user wants to open the link or not (I don't like to just instantly open links to pages for other people!)
            OpenExternalLinkWithConfirm("This will open a (new) browser window to my main YouTube Channel:\nhttps://www.youtube.com/@XNLFutureTechnologies\n\nDo you want to continue to visit my Main YouTube Channel?", "https://www.youtube.com/@XNLFutureTechnologies");
        }

        private void LlblYouTube2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (LlblYouTube2.Text == "PIGEONS ARE AWESOME!!")
            {
                // Part of the easter egg when people press and hold CTRL+SHIFT
                OpenExternalLinkWithConfirm("I KNOW RIGHT!? :D\n\nWe have a taken in a non-releasable rescue pigeon, and we're having an amazing time with her and her adventures.\nLots of my electronics and 3D projects are also for her or inspired by her btw :)\n\nWould you like to visit Sophie's own YouTube Channel?\n\nhttps://www.youtube.com/@SophieThePigeon", "https://www.youtube.com/@SophieThePigeon");
            }
            else
            {
                // Show a messagebox telling that this will open a link to my YouTube (Secondary channel) and then asking confirmation IF the user wants to open the link or not (I don't like to just instantly open links to pages for other people!)
                OpenExternalLinkWithConfirm("This will open a (new) browser window to my Secondary YouTube Channel:\nhttps://www.youtube.com/@XNLFutureTechnologies2\n\nDo you want to continue to visit my Secondary YouTube Channel?", "https://www.youtube.com/@XNLFutureTechnologies2");
            }
        }

        private void LlblWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show a messagebox telling that this will open a link to the XNL R36 Linux Partition Sizer Website and then asking confirmation IF the user wants to open the link or not (I don't like to just instantly open links to pages for other people!)
            OpenExternalLinkWithConfirm("This will open a (new) browser window to the XNL R36 Linux Partition Sizer Website:\nhttps://www.teamxnl.com/R36-Linux-Partition-Sizer\n\nDo you want to continue to visit the XNL R36 Linux Partition Sizer Website?", "https://www.teamxnl.com/R36-Linux-Partition-Sizer");
        }

        private void imgXNLFtLogo_Click(object sender, EventArgs e)
        {
            // Show a messagebox telling that this will open a link to my extended tutorial in regards to installing/updating ArkOS on the R36S/H  and then asking confirmation IF the user wants to open the link or not (I don't like to just instantly open links to pages for other people!)
            OpenExternalLinkWithConfirm("This will open a (new) browser window to my (very detailed) tutorial on how to install or Update ArkOS for the R36S and R36H, which (near the end of the tutorial) also includes a section about this program:\nhttps://www.teamxnl.com/installing-or-updating-arkos-r36s-r36h/\n\nDo you want to continue to visit my R36S/R36H ArkOS Installation or Update tutorial?", "https://www.teamxnl.com/installing-or-updating-arkos-r36s-r36h/");
        }

        private void LlblR36Central_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Show a messagebox telling that this will open a link to the XNL R36S & R36H ArkOS Central Page and then asking confirmation IF the user wants to open the link or not (I don't like to just instantly open links to pages for other people!)
            OpenExternalLinkWithConfirm("This will open a (new) browser window to my 'R36S & R36H ArkOS Central Page', this page basically contains all kinds of resources, information, links (both my own and external) and a collection of releases I've made for the R36S and R36H:\nhttps://www.teamxnl.com/r36s-r36h-arkos-central/\n\nDo you want to continue to visit the XNL R36S & R36H ArkOS Central Page?", "https://www.teamxnl.com/r36s-r36h-arkos-central/");
        }

        private void OpenExternalLinkWithConfirm(string MessageText, string WebsiteURL)
        {
            // This is a simple 'helper function' which basically prevents that I have to copy-paste the messagebox dialog asking the user if they
            // want to open a browser to the selected link everytime.

            // Show a messagebox which displays the text set at the MessageText variable )
            if (MessageBox.Show($"{MessageText}", "XNL R36 Linux Partition Sizer", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // If the user allowed to open the link to my Main YouTube Channel, THEN we'll open a browser to it.
                Process.Start(new ProcessStartInfo(WebsiteURL) { UseShellExecute = true });
            }
        }

        #endregion

        #region "'Hidden Feature' to enable 'pro-mode' and 'Ignore R36 Limit Check Warning Timer'"

        // Variable used by the timer bellow which will make a warning icon blink...
        bool blink = false;
        private void tmrBlinkWarning_Tick(object sender, EventArgs e)
        {
            // This Timer is used to make a warning blink on the form if the user has started the application while
            // holding shift, which means it will ignore searching for the exact dtb file used for the R36S/R36H/RG351MP
            
            // This is simply telling blink = not blink. So if it's true it will turn false and vice versa
            blink = !blink;

            if (blink)
            {
                // If blink == true, then we'll show the warning icon in the image box
                imgWarning.Image = Properties.Resources.WarningIcon;
            }
            else
            {
                // If blink == false, then we'll clear the image box
                imgWarning.Image = null;
            }

            // The reason why we're not just changing the visibility of the image box is that this would make
            // it very annoying for the user to click on if it keeps going invisible ;)
            // this way the image box still remains clickable but it will just be empty.
        }

        private void tmrProMode_Tick(object sender, EventArgs e)
        {
            // After the timer ended it will reset the amount of TuxClicks
            tmrProMode.Enabled = false;
            TuxClicks = 0;
        }

        #endregion

        #region "PigeonEgg"
        // Just a small Easter Egg as a tribute to our lovely rescue (office) pigeon, Sophie :)
        private bool isCtrlPressed = false;
        private bool isShiftPressed = false;

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            // If the control key is being held down we'll register this in a global variable
            if (e.Control)
                isCtrlPressed = true;

            // Same for the shift key
            if (e.Shift)
                isShiftPressed = true;

            // If both keys (CTRL+Shift) are being held down, then we'll show a custom message (the "easter egg")
            if (isCtrlPressed && isShiftPressed)
            {
                LlblYouTube2.Text = "PIGEONS ARE AWESOME!!";
            }
        }

        private void frmMain_KeyUp(object sender, KeyEventArgs e)
        {
            // If the control key is being released then set the global variable for it to false
            if (e.KeyCode == Keys.ControlKey)
                isCtrlPressed = false;

            // If the shift key is being released then set the global variable for it to false
            if (e.KeyCode == Keys.ShiftKey)
                isShiftPressed = false;

            // Check if the Easter egg has to remain visible
            CheckPigeonEgg();
        }

        private void frmMain_Activated(object sender, EventArgs e)
        {
            // If the form lost focus and then regains it we'll (de)activate the easter egg again
            isCtrlPressed = false;
            isShiftPressed = false;

            // Check if the Easter egg has to remain visible
            CheckPigeonEgg();
        }

        private void CheckPigeonEgg()
        {
            // If one of the keys is no longer being held down, then we'll revert back to the normal link
            if (!isCtrlPressed || !isShiftPressed)
            {
                LlblYouTube2.Text = "XNL Future Technologies YouTube (Secondary)";
            }
        }

        #endregion
    
    }
}
