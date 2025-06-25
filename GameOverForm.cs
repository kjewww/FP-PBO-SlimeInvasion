using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShooterGame2D
{
    public class GameOverForm : Form
    {
        public GameOverForm(int score, int seconds)
        {
            this.Text = "Game Over";
            this.Size = new System.Drawing.Size(400, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            var labelScore = new Label
            {
                Text = $"Score: {score}",
                Font = new System.Drawing.Font("Arial", 18),
                AutoSize = true,
                Location = new System.Drawing.Point(30, 30)
            };
            this.Controls.Add(labelScore);

            var labelTime = new Label
            {
                Text = $"Time: {TimeSpan.FromSeconds(seconds):hh\\:mm\\:ss}",
                Font = new System.Drawing.Font("Arial", 18),
                AutoSize = true,
                Location = new System.Drawing.Point(30, 70)
            };
            this.Controls.Add(labelTime);

            var btnRestart = new Button
            {
                Text = "Restart",
                Size = new System.Drawing.Size(100, 40),
                Location = new System.Drawing.Point(30, 130)
            };
            btnRestart.Click += (s, e) => { this.DialogResult = DialogResult.Retry; this.Close(); };
            this.Controls.Add(btnRestart);

            var btnExit = new Button
            {
                Text = "Exit",
                Size = new System.Drawing.Size(100, 40),
                Location = new System.Drawing.Point(150, 130)
            };
            btnExit.Click += (s, e) => { this.DialogResult = DialogResult.Abort; this.Close(); };
            this.Controls.Add(btnExit);
        }
    }
}
