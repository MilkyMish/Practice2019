using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entities;
using DataBusinessLayer;

namespace Tanks
{
    public partial class MainForm : Form
    {
        private readonly DataBL data = new DataBL();
        Graphics g;
        Bitmap bitmap;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Configuration startConf = data.Start();
        
            PictureBox pictureBox1 = new PictureBox();
            CreateMapBackground(startConf);
        }

        public void CreateMapBackground(Configuration configuration)
        {
            int Width = configuration.MapWidth;
            int Heigth = configuration.MapHeight;

            Map.Size = new Size(Width, Heigth);
            this.Controls.Add(Map);

            Bitmap background = new Bitmap(Width, Heigth);
            Graphics flagGraphics = Graphics.FromImage(background);

            flagGraphics.FillRectangle(Brushes.Black, 0, 0, Width, Heigth);
               
            Map.Image = background;

            //bitmap = new Bitmap(83, 83);
            //g = Graphics.FromImage(bitmap);
            //g.FillRectangle(new SolidBrush(Color.White), 0, 0, 83, 83);
            //timer.Enabled = true;
            //Bitmap sprite = new Bitmap("Tanks/img/tanks.png");
            //imageList.Images.Add(sprite);
            //sprite.

           
        }

       
        private void timer_Tick(object sender, EventArgs e)
        {
            
            //for (int i = 0; i < 6; i++)
            //{
            //    imageList.Draw(g, new Point(83, 83*(i+1)), 0);
            //}
            //if (timer.Interval <= 10) timer.Interval = 600;
            //Map.Image = bitmap;
        }
    }
}
