using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class LevelForm : Form
    {
        private Button pauseButton;
        private Button restartButton;
        private Button exitButton;

        private int ScoreCounter = 0;
        private Label Score;
        private Label HealthBar;
        private Label TimeLabel;
        private System.Windows.Forms.Timer TimerText = new System.Windows.Forms.Timer();
        private int elapsedSeconds = 0;

        public Player player;
        public List<Bullet> Bullets = new List<Bullet>();

        public List<Slime> slimes = new List<Slime>();

        private System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();
        HashSet<Keys> keysPressed = new HashSet<Keys>();

        public LevelForm()
        {
            InitializeComponent();
            EnemySpawner();

        }

        private void InitializeComponent()
        {
            this.Text = "Level Form";
            this.Size = new Size(1920, 1080);
            this.BackColor = Color.DarkGray;
            this.StartPosition = FormStartPosition.CenterScreen;

            player = new Player(new PointF(this.ClientSize.Width/2, this.ClientSize.Height/2));

            // score label
            Score = new Label
            {
                Text = "Score: 0",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Arial", 16),
                ForeColor = Color.Black
            };
            this.Controls.Add(Score);

            // health bar
            HealthBar = new Label
            {
                Text = "Health: " + player.Health,
                Location = new Point(10, 40),
                AutoSize = true,
                Font = new Font("Arial", 16),
                ForeColor = Color.Black
            };
            this.Controls.Add(HealthBar);

            // time label
            TimeLabel = new Label
            {
                Text = "00:00:00",
                Location = new Point(10, 70),
                AutoSize = true,
                Font = new Font("Arial", 16),
                ForeColor = Color.Black
            };
            this.Controls.Add(TimeLabel);
            TimerText.Interval = 1000;
            TimerText.Tick += TimeText;
            TimerText.Start();

            // restart button
            restartButton = new Button
            {
                Text = "R",
                Size = new Size(50, 50),
                Location = new Point(this.ClientSize.Width - 120, 10),
            };
            restartButton.Click += RestartButton_Click;
            this.Controls.Add(restartButton);

            // exit button
            exitButton = new Button
            {
                Text = "X",
                Size = new Size(50, 50),
                Location = new Point(ClientSize.Width - 60, 10),
            };
            exitButton.Click += ExitButton_Click;
            this.Controls.Add(exitButton);

            // move controls
            this.KeyPreview = true;
            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;
            this.Paint += DrawGame;

            // timer
            this.DoubleBuffered = true;
            Timer.Interval = 20;
            Timer.Tick += GameLoop;
            Timer.Start();

            // on click mouse
            this.MouseDown += OnMouseClick;

        }

        private void TimeText(object sender,  EventArgs e)
        {
            elapsedSeconds++;
            TimeLabel.Text = TimeSpan.FromSeconds(elapsedSeconds).ToString(@"hh\:mm\:ss");
        }

        public void OnKeyDown(object sender,  KeyEventArgs e)
        {
            keysPressed.Add(e.KeyCode);
        }

        public void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PointF playerCenter = new PointF(player.Position.X + 15, player.Position.Y + 15);
                PointF mousePos = e.Location;
                Bullets.Add(new Bullet(playerCenter, mousePos));
            }
        }

        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            keysPressed.Remove(e.KeyCode);
            player.StopWalking();
        }

        public void GameLoop(object sender, EventArgs e)
        {
            player.Walk(keysPressed, this.ClientSize);
            UpdateGame();
            Invalidate();
        }

        private void UpdateGame()
        {
            // update bullets
            for (int i = Bullets.Count - 1; i >= 0; i--)
            {
                Bullets[i].Update();
                if (Bullets[i].IsOffScreen(this.ClientSize))
                {
                    Bullets.RemoveAt(i);
                }
            }

            // Update Slimes & Cek tabrakan dengan peluru
            for (int i = slimes.Count - 1; i >= 0; i--)
            {
                var slime = slimes[i];
                slime.TargettingPlayer(player);

                // Cek tabrakan dengan player
                if (player.isHit(slime))
                {
                    player.TakeDamage(slime.Damage);
                    UpdateHealthBar();
                }

                // Cek tabrakan dengan peluru
                for (int j = Bullets.Count - 1; j >= 0; j--)
                {
                    if (slime.isHit(Bullets[j]))
                    {
                        slime.TakeDamage(1);
                        Bullets.RemoveAt(j);
                        if (slime.Health <= 0)
                        {
                            slimes.RemoveAt(i);
                            UpdateScore();
                            break;
                        }
                    }
                }

            }
        }

        private void UpdateScore()
        {
            ScoreCounter++;
            Score.Text = $"Score: {ScoreCounter}";
        }

        private void UpdateHealthBar()
        {
            HealthBar.Text = $"Health: {player.Health}";
            if (player.Health <= 0)
            {
                Timer.Stop();

                // ntar diganti form baru (score, restart, exit)
                MessageBox.Show("Game Over! Your score: " + ScoreCounter);
                Application.Exit();
            }
        }

        private void EnemySpawner()
        {
            // green
            System.Windows.Forms.Timer GreenTimer = new System.Windows.Forms.Timer();
            GreenTimer.Interval = 1000;
            GreenTimer.Tick += (s, e) => 
            {
                var rand = new Random();
                var spawnPos = new PointF(
                    rand.Next(0, this.ClientSize.Width - 30),
                    rand.Next(0, this.ClientSize.Height - 30)
                );
                slimes.Add(new Green(spawnPos));
            };
            GreenTimer.Start();

            // red
            System.Windows.Forms.Timer RedTimer = new System.Windows.Forms.Timer();
            RedTimer.Interval = 2500;
            RedTimer.Tick += (s, e) =>
            {
                var rand = new Random();
                var spawnPos = new PointF(
                    rand.Next(0, this.ClientSize.Width - 30),
                    rand.Next(0, this.ClientSize.Height - 30)
                );
                slimes.Add(new Red(spawnPos));
            };
            RedTimer.Start();

            // blue
            System.Windows.Forms.Timer BlueTimer = new System.Windows.Forms.Timer();
            BlueTimer.Interval = 5000;
            BlueTimer.Tick += (s, e) =>
            {
                var rand = new Random();
                var spawnPos = new PointF(
                    rand.Next(0, this.ClientSize.Width - 30),
                    rand.Next(0, this.ClientSize.Height - 30)
                );
                slimes.Add(new Blue(spawnPos));
            };
            BlueTimer.Start();

            //black
            System.Windows.Forms.Timer BlackTimer = new System.Windows.Forms.Timer();
            BlackTimer.Interval = 10000;
            BlackTimer.Tick += (s, e) =>
            {
                var rand = new Random();
                var spawnPos = new PointF(
                    rand.Next(0, this.ClientSize.Width - 30),
                    rand.Next(0, this.ClientSize.Height - 30)
                );
                slimes.Add(new Black(spawnPos));
            };
            BlackTimer.Start();
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Resource.background__2_, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

            player.Draw(e.Graphics);

            foreach (var slime in slimes)
            {
                slime.DrawWithAnimation(e.Graphics);
            }

            foreach (var Bullet in Bullets)
            {
                Bullet.Draw(e.Graphics);
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            player = new Player(new PointF(this.ClientSize.Width/2, this.ClientSize.Height/2));
            slimes.Clear();
            ScoreCounter = 0;
            Score.Text = "Score: 0";
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
