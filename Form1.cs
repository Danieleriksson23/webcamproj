using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Media;
using System.Threading;
using System.IO;



namespace webcamproj
{
    
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();

            var fileSystemWatcher = new FileSystemWatcher();

            fileSystemWatcher.Changed += FileSystemWatcher_changed;

            fileSystemWatcher.Path = @"C:\Users\daniel.eriksson23\Google Drive";

            fileSystemWatcher.EnableRaisingEvents = true;
            


        }
        private void FileSystemWatcher_changed(object sender, FileSystemEventArgs e)
        {
            label1.Invoke(new Action(() => label1.Text = "random123"));
            this.Invoke((MethodInvoker)delegate ()
            {

                CaptureDevice = new VideoCaptureDevice(InfoCollection[comboBox1.SelectedIndex].MonikerString);
            });
            // skapar en funktion för att kunna starta kameran och sedan visa vad kameran ser
            
            CaptureDevice.NewFrame += CaptureDevice_newframe;

            CaptureDevice.Start();;

        }


        FilterInfoCollection InfoCollection;
        VideoCaptureDevice CaptureDevice;
        CameraControlProperty cameracontrol;
      
        


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptureDevice = new VideoCaptureDevice(InfoCollection[comboBox1.SelectedIndex].MonikerString);
            // skapar en funktion för att kunna starta kameran och sedan visa vad kameran ser
            CaptureDevice.NewFrame += CaptureDevice_newframe;
            CaptureDevice.Start();
            
        }
        private void CaptureDevice_newframe(object sender, NewFrameEventArgs eventArgs)
        {
            // visar vad kameran ser i pictureboxen
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //när applikationen stängs så avaktiveras capturedevice(kameran) 
            if (CaptureDevice.IsRunning == true)
                CaptureDevice.Stop();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

          

            InfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //skriv alla tillgängliga kameror i en combobox
            foreach (FilterInfo Device in InfoCollection)
                comboBox1.Items.Add(Device.Name);
            comboBox1.SelectedIndex = 0;
            CaptureDevice = new VideoCaptureDevice();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
