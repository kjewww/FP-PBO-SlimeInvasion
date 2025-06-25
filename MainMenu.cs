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
        private Label titleLabel;

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

            // judul game
            titleLabel = new Label
            {
                Text = "Slime Invasion!",
                Font = new Font("Pixelify Sans", 48, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(30, 100),
            };
            this.Controls.Add(titleLabel);

            // play button
            playButton = new Button
            {
                Text = "Start",
                Size = new Size(200, 50),
                Location = new Point(this.ClientSize.Width/2 - 100, 250)
            };
            playButton.Click += PlayButton_Click;
            this.Controls.Add(playButton);
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
