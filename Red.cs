using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class Red : Slime
    {
        public Red(PointF startPosition) : base(startPosition)
        {
            Health = 4;
            Damage = 1;
            Speed = 6;
            Position = startPosition;
            frames = new Image[]
            {
                Resource.merah1,
                Resource.merah1,
                Resource.merah2,
                Resource.merah2,
            };
        }
    }
}
