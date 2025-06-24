using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class Black : Slime
    {
        public Black(PointF startPosition) : base(startPosition)
        {
            Health = 10;
            Damage = 1;
            Speed = 7;
            Position = startPosition;
            frames = new Image[]
            {
                Resource.Hytam1,
                Resource.Hytam1,
                Resource.Hytam2,
                Resource.Hytam2,
            };
        }

    }
}
