using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class Collider
    {
        public PointF Position { get; set; }
        public Size size { get; set; }
        public RectangleF Bounds => new RectangleF(Position.X, Position.Y, size.Width, size.Height);
        public bool Intersects(Collider collider)
        {
            return this.Bounds.IntersectsWith(collider.Bounds);
        }
    }
}
