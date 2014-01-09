using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication1
{
    public partial class Olamování : Form
    {
        public Olamování()
        {
            InitializeComponent();
        }

        public Color NahodnaBarva()
        {
            return Color.FromArgb(trackBar2.Value, rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }

        public void ZmenaBarvy(Color novaBarva, object sender)
        {
            pero.Color = novaBarva;

            if (sender == null || ! (sender is NumericUpDown))
            {
                barvaR.Value = pero.Color.R; barvaG.Value = pero.Color.G; barvaB.Value = pero.Color.B;
            }

            label8.BackColor = novaBarva;
        }

        int minuleX, minuleY;
        Graphics g;
        public Random rnd;
        Pen pero;
        Bitmap bmp;

        private void Form1_Load(object sender, EventArgs e)
        {
            rnd = new Random();

            bmp = new Bitmap(1024, 768);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            pictureBox1.BackColor = Color.White;

            Color prvni = NahodnaBarva();
            pero = new Pen(prvni, trackBar1.Value);
            ZmenaBarvy(prvni, sender);
            Collection<LineCap> koncePer = new Collection<LineCap>() { LineCap.AnchorMask, LineCap.ArrowAnchor, LineCap.Custom, LineCap.DiamondAnchor, LineCap.Flat, LineCap.NoAnchor, LineCap.Round, LineCap.RoundAnchor, LineCap.Square, LineCap.SquareAnchor, LineCap.Triangle};
            // ?
            foreach (LineCap l in koncePer)
            {
                comboBox1.Items.Add(l);
                comboBox2.Items.Add(l);
            }
            comboBox1.SelectedItem = LineCap.Triangle;
            comboBox2.SelectedItem = LineCap.Square;

            label6.Text = trackBar1.Value.ToString();
            label7.Text = trackBar2.Value.ToString();

            pero.StartCap = (LineCap) comboBox1.SelectedItem;
            pero.EndCap = (LineCap) comboBox2.SelectedItem;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            pero.Width = trackBar1.Value;
            label6.Text = trackBar1.Value.ToString();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (checkBox1.Checked)
                    ZmenaBarvy(NahodnaBarva(), sender);

                g.DrawLine(pero, minuleX, minuleY, e.X, e.Y);
                pictureBox1.Refresh();
            }
            minuleX = e.X;
            minuleY = e.Y;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            if (checkBox1.Checked)
                ZmenaBarvy(NahodnaBarva(), sender);

            g.DrawLine(pero, e.X, e.Y, Math.Abs(e.X - 1), e.Y);
            pictureBox1.Refresh();

            minuleX = e.X;
            minuleY = e.Y;
        }

        private void resetovatPlátnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bmp = null;
            bmp = new Bitmap(1024, 768);
            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
            pictureBox1.BackColor = Color.White;

        }

        private void uložitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bmp.Save(saveFileDialog1.FileName);
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            ZmenaBarvy(Color.FromArgb(trackBar2.Value, ((Bitmap)pictureBox2.Image).GetPixel(e.X, e.Y)), sender);
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            ZmenaBarvy(Color.FromArgb(trackBar2.Value, pero.Color), sender);
            label7.Text = trackBar2.Value.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pero.StartCap = (LineCap)comboBox1.SelectedItem;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pero.EndCap = (LineCap)comboBox2.SelectedItem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ZmenaBarvy(Color.FromArgb(trackBar2.Value, (int)barvaR.Value, (int)barvaG.Value, (int)barvaB.Value), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ZmenaBarvy(NahodnaBarva(), sender);
        }

        private void barvaR_ValueChanged(object sender, EventArgs e)
        {

        }

        private void barvaG_ValueChanged(object sender, EventArgs e)
        {

        }

        private void barvaB_ValueChanged(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = false;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            MyDialog.Color = pero.Color;

            // Update the text box color if the user clicks OK  
            if (MyDialog.ShowDialog() == DialogResult.OK)
                ZmenaBarvy(MyDialog.Color, sender);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ZmenaBarvy(Color.FromArgb(trackBar2.Value, (int)barvaR.Value, (int)barvaG.Value, (int)barvaB.Value), sender);      
        }
    }
}