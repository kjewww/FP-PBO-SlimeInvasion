using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class Player : Entity, IDrawable
    {
        private bool IsWalking = false;

        private Image[] framesIdle;
        private int currentFrameIdle = 0;
        private int frameCountIdle = 4;
        private int animationCounterIdle = 0;
        private int animationSpeedIdle = 16;

        public Player(PointF startPosition) : base(startPosition)
        {
            Health = 100;
            Damage = 1;
            Speed = 10;
            Position = startPosition;

            frames = new Image[]
            {
                Resource.wizard_1,
                Resource.wizard_2,
                Resource.wizard_3,
                Resource.wizard_4,
            };

            framesIdle = new Image[]
            {
                Resource.wizard_idle1,
                Resource.wizard_idle2,
                Resource.wizard_idle1,
                Resource.wizard_idle2,
            };
        }

        public void Draw(Graphics g)
        {
            Image frame;

            if (IsWalking)
            {
                frame = frames[currentFrame];
                animationCounter++;
                if (animationCounter >= animationSpeed)
                {
                    currentFrame = (currentFrame + 1) % frameCount;
                    animationCounter = 0;
                }
            }
            else
            {
                frame = framesIdle[currentFrameIdle];
                animationCounterIdle++;
                if (animationCounterIdle >= animationSpeedIdle)
                {
                    currentFrameIdle = (currentFrameIdle + 1) % frameCountIdle;
                    animationCounterIdle = 0;
                }

            }

            if (isFacingLeft)
            {
                g.DrawImage(
                    frame,
                    new Rectangle((int)Position.X + size.Width, (int)Position.Y, -size.Width, size.Height), // width negatif = flip
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
        }

        public bool isHit(Slime enemy)
        {
            RectangleF playerRect = new RectangleF(Position.X, Position.Y, 64, 64);
            RectangleF enemyRect = new RectangleF(enemy.Position.X, enemy.Position.Y, 64, 64);
            return playerRect.IntersectsWith(enemyRect);
        }

        public void GetPotion()
        {

        }

        public void Walk(HashSet<Keys> keys, Size boundary)
        {
            PointF nextPos = Position;

            if (keys.Contains(Keys.W) || keys.Contains(Keys.Up))
            {
                nextPos.Y -= Speed;
                IsWalking = true;
            }
            if (keys.Contains(Keys.S) || keys.Contains(Keys.Down))
            {
                nextPos.Y += Speed;
                IsWalking = true;
            }
            if (keys.Contains(Keys.A) || keys.Contains(Keys.Left))
            {
                nextPos.X -= Speed;
                IsWalking = true;
                isFacingLeft = true;
            }
            if (keys.Contains(Keys.D) || keys.Contains(Keys.Right))
            {
                nextPos.X += Speed;
                IsWalking = true;
                isFacingLeft = false;
            }

            nextPos.X = Math.Max(0, Math.Min(nextPos.X, boundary.Width - size.Width));
            nextPos.Y = Math.Max(0, Math.Min(nextPos.Y, boundary.Height - size.Height));

            Position = nextPos;
        }

        public void StopWalking()
        {
            IsWalking = false;
            currentFrameIdle = 0;
            animationCounterIdle = 0;
        }
    }
}
