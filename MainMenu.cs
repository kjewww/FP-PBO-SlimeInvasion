using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class MainMenu : Form
    {
        private Button playButton;
        private Button exitButton;

        public MainMenu()
        {
            InitializeComponent();
            InitlializeControls();
        }
        private void InitializeComponent()
        {
            this.Text = "Main Menu";
            this.Size = new Size(800,600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // play button
            playButton = new Button();
            playButton.Text = "Play Game";
            playButton.Size = new Size(200, 50);
            playButton.Location = new Point(this.ClientSize.Width/2 - 100, this.ClientSize.Height/2 - 50);
            playButton.Click += PlayButton_Click;
        }

        private void InitlializeControls()
        {
            this.Controls.Add(playButton);
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            LevelForm levelForm = new LevelForm();
            levelForm.ShowDialog();
            this.Close();
        }
    }
}
