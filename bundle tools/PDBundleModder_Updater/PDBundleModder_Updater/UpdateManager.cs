using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters;
using System.ComponentModel;
using System.IO;
using Ionic.Zip;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace PDBundleModder_Updater
{
    public class LogEntry
    {
        private String message;
        private Color color;

        public LogEntry()
        {
            this.message = "";
            this.color = Color.Black;
        }

        public LogEntry(String message)
        {
            this.message = message;
            this.color = Color.Black;
        }

        public LogEntry(String message, Color color)
        {
            this.message = message;
            this.color = color;
        }

        public Color getColor()
        {
            return color;
        }

        public String getMessage()
        {
            return message;
        }

        public override string ToString()
        {
            return message.ToString();
        }
    }
    
    public enum UpdateStatus
    {
        None,
        InProgress,
        Complete,
        Error
    }
    
    public class UpdateManager
    {
        private string version;
		#if LINUX
		private static int downloadid = 16387; // Expected ID of the Bundle Modder
		#else
        private static int downloadid = 197; // Expected ID of the Bundle Modder
		#endif
        private string infourl = @"http://manager.lastbullet.net/GetSingleDownload/" + downloadid + ".json";
        private string[] ignorefiles = { "exceptions.log" };
        private dynamic responce;
        private Queue<LogEntry> log = new Queue<LogEntry>();
        private float downloadprogess = 0.0f;
        private float extractprogess = 0.0f;
        public UpdateStatus updatestate = UpdateStatus.None;
        private int downloadprogresscheck = 0;

        public UpdateManager()
        {
            performCleanup();
        }

        public UpdateManager(string version)
        {
            this.version = version.Replace(',', '.'); //Version cannot contain ',' so we replace it with '.'

            performCleanup();
        }

        public LogEntry[] getLog()
        {
            return log.ToArray();
        }

        public int getTotalProgress()
        {
            return (int)((downloadprogess + extractprogess) / 2.0f * 100.0f);
        }

        private bool fetchData()
        {
            if (String.IsNullOrWhiteSpace(infourl))
                return false;

            try
            {
                WebClient client = new WebClient();
                string reply = client.DownloadString(infourl);

                responce = JsonConvert.DeserializeObject<dynamic>(reply, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
                });

                Console.WriteLine(reply);
            }
            catch(Exception)
            {
                return false;
            }
            
            return true;
        }

        public bool checkUpdate()
        {
            if (this.fetchData())
            {
                if (responce != null && responce[downloadid.ToString()] != null && responce[downloadid.ToString()]["version"] != null && responce[downloadid.ToString()]["download"] != null)
                {
                    double passedVersion;
                    double updateVersion;

                    if (Double.TryParse(this.version, NumberStyles.Number, CultureInfo.InvariantCulture, out passedVersion) && Double.TryParse(getUpdateVersion(), NumberStyles.Number, CultureInfo.InvariantCulture, out updateVersion))
                    {
                        if (updateVersion > passedVersion)
                            return true;
                    }
                    else
                    {
                        if (!this.version.Equals(getUpdateVersion()))
                            return true;
                    }
                }
            }

            return false;
        }

        public bool retrieveUpdate()
        {
            updatestate = UpdateStatus.InProgress;
            try
            {
                if (!String.IsNullOrWhiteSpace(getDownloadURL()))
                {
                    this.log.Enqueue(new LogEntry("Starting download... "));

                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                    // Starts the download
                    client.DownloadFileAsync(new Uri(getDownloadURL()), "update.zip");

                }

            }
            catch(Exception exc)
            {
                log.Enqueue(new LogEntry(exc.Message, Color.Red));
                updatestate = UpdateStatus.Error;
                return false;
            }
            return false;
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes;

            downloadprogresscheck++;

            if (downloadprogresscheck % 10 == 0)
            {
                log.Enqueue(new LogEntry("Download " + Math.Truncate(percentage * 100) + "%"));
                this.downloadprogess = (float)(Math.Truncate(percentage * 100) / 100.0f);
            }
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.log.Enqueue(new LogEntry("Download complete."));
            this.log.Enqueue(new LogEntry());
            this.downloadprogess = 1.0f;

            if (extractUpdate())
                updatestate = UpdateStatus.Complete;
            else
                updatestate = UpdateStatus.Error;
        }

        /*
         * Please note that update.zip must be of following sample format:
         *      zipfile.zip\PDBundleModPatcher.exe
         *      zipfile.zip\PDBundleModPatcher.pdb
         *      zipfile.zip\SoundBankParser.dll
         *      zipfile.zip\SoundBankParser.pdb
         *      zipfile.zip\DieselBundle.dll
         *      zipfile.zip\DieselBundle.pdb
         *      zipfile.zip\Microsoft.WindowsAPICodePack.dll
         *      zipfile.zip\Microsoft.WindowsAPICodePack.Shell.dll
         *      zipfile.zip\Hash64.dll
         *      zipfile.zip\Ionic.Zip.Reduced.dll
         *      zipfile.zip\Newtonsoft.Json.dll
         */
        private bool extractUpdate()
        {
            if (File.Exists("update.zip"))
            {
                log.Enqueue(new LogEntry("Unpacking update..."));

                try
                {
                    ZipFile update_file = new ZipFile("update.zip");
                    log.Enqueue(new LogEntry(update_file.Count + " files detected."));

                    for(int i = 0; i < update_file.Count; i++)
                    {
                        float percentage = ((float)i / (float)update_file.Count);

                        ZipEntry fileentry = update_file[i];
                        log.Enqueue(new LogEntry("[" + Math.Truncate(percentage * 100) + "%] Updating: " + fileentry.FileName));
                        this.extractprogess = (float)(Math.Truncate(percentage * 100) / 100.0f);

                        if (ignorefiles.Contains(fileentry.FileName))
                            continue;

                        try
                        {
                            if (File.Exists(fileentry.FileName))
                                File.Move(fileentry.FileName, fileentry.FileName + ".updateremove");
                            fileentry.Extract();
                        }
                        catch (IOException e)
                        {
                            log.Enqueue(new LogEntry(fileentry.FileName + " is inaccessible.", Color.Red));

                            MessageBox.Show(fileentry.FileName + " is inaccessible.\r\nPlease close Bundle Modder.\r\n\r\n" + e.Message);
                            i--;
                            continue;
                        }
                    }

                    update_file.Dispose();
                    File.Delete("update.zip");
                }
                catch(Exception exc)
                {
                    log.Enqueue(new LogEntry(exc.Message, Color.Red));

                    return false;
                }

                log.Enqueue(new LogEntry("All files updated."));
                log.Enqueue(new LogEntry());
                log.Enqueue(new LogEntry("You can now close the updater and restart Bundle Modder."));
                this.extractprogess = 1.0f;
                
                return true;
            }
            else
            {
                this.log.Enqueue(new LogEntry("Update file not found.", Color.Red));
                return false;
            }
        }

        private void performCleanup()
        {
            string[] messyfiles = Directory.GetFiles(".", "*.updateremove", SearchOption.AllDirectories);

            foreach (string file in messyfiles)
            {
                try
                {
                    File.Delete(file);
                }
                catch(Exception exc)
                {
                    log.Enqueue(new LogEntry("Unable to delete file - " + file, Color.Red));
                }
            }

            if (File.Exists("update.zip"))
                File.Delete("update.zip");
        }

        public string getUpdateVersion()
        {
            if (responce != null && responce[downloadid.ToString()] != null && responce[downloadid.ToString()]["version"] != null)
            {
                return responce[downloadid.ToString()]["version"];
            }

            return "#JSON Version Error#";
        }

        public string getUpdateChanges()
        {
            if (responce != null && responce[downloadid.ToString()] != null && responce[downloadid.ToString()]["changelog"] != null)
            {
                return responce[downloadid.ToString()]["changelog"];
            }

            return "No documented changes.";
        }

        private string getDownloadURL()
        {

            if (responce != null && responce[downloadid.ToString()] != null && responce[downloadid.ToString()]["download"] != null && !String.IsNullOrWhiteSpace((string)responce[downloadid.ToString()]["download"]))
            {
                return @"http://forums.lastbullet.net/mydownloads/downloads/" + responce[downloadid.ToString()]["download"];
            }

            return "";
        }
    }
}
