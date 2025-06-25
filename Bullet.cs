using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class Bullet : IDrawable
    {
        public PointF Position { get; set; }
        private PointF Direction;
        private float Speed = 20;
        public Size size = new Size(16, 16);

        public Bullet(PointF startPos, PointF targetPos)
        {
            Position = startPos;

            float dx = targetPos.X - startPos.X;
            float dy = targetPos.Y - startPos.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);
            Direction = new PointF(dx / length * Speed, dy / length * Speed);
        }

        public void Update()
        {
            Position = new PointF(Position.X + Direction.X, Position.Y + Direction.Y);
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Yellow, Position.X, Position.Y, size.Width, size.Height);
        }

        public bool IsOffScreen(Size screenSize)
        {
            return Position.X < 0 || Position.Y < 0 || Position.X > screenSize.Width || Position.Y > screenSize.Height;
        }
    }
}
