using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
       
        
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            playButton.Enabled = false;
            playButton.Visible = false;
            exitButton.Visible = false;
            exitButton.Enabled = false;
            shadowLabel.Visible = false;
            shadowLabel.Enabled = false;
            creditLabel.Visible = false;
            creditLabel.Enabled = false;


            GameScreen gs = new GameScreen();

            this.Controls.Add(gs);
        }

        private void exitButtion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
