using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class Slime : Entity, IDrawable
    {
        public int Score { get; set; }

        public Slime(PointF startPosition) : base(startPosition)
        {
            Health = 3;
            Damage = 1;
            Speed = 5;
            Score = 1;
            Position = startPosition;

            frames = new Image[]
            {
                Resource.Slime1,
                Resource.Slime2,
                Resource.Slime3,
                Resource.Slime4,
            };
        }

        public bool isHit(Bullet bullet)
        {
            RectangleF enemyRect = new RectangleF(Position.X, Position.Y, size.Width, size.Height);
            RectangleF bulletRect = new RectangleF(bullet.Position.X, bullet.Position.Y, bullet.size.Width, bullet.size.Height);
            return enemyRect.IntersectsWith(bulletRect);
        }

        public void TargettingPlayer(Player player)
        {
            float dx = player.Position.X - Position.X;
            float dy = player.Position.Y - Position.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                dx /= distance;
                dy /= distance;

                if (dx < 0) isFacingLeft = true;
                else if (dx > 0) isFacingLeft = false;

                Position = new PointF(Position.X + dx * Speed, Position.Y + dy * Speed);
            }
        }

        public void Draw(Graphics g)
        {
            Image frame = frames[currentFrame];

            if (isFacingLeft)
            {
                g.DrawImage(
                    frame,
                    new Rectangle((int)Position.X + size.Width, (int)Position.Y, -size.Width, size.Height),
                    new Rectangle(0, 0, frame.Width, frame.Height),
                    GraphicsUnit.Pixel
                );
            }
            else
            {
                g.DrawImage(
                    frame,
                    new Rectangle((int)Position.X, (int)Position.Y, size.Width, size.Height),
                    new Rectangle(0, 0, frame.Width, frame.Height),
                    GraphicsUnit.Pixel
                );
            }
            animationCounter++;
            if (animationCounter >= animationSpeed)
            {
                currentFrame = (currentFrame + 1) % frameCount;
                animationCounter = 0;
            }
        }
    }
}
