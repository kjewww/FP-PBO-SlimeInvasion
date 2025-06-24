using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class Blue : Slime
    {
        public Blue(PointF startPosition) : base(startPosition)
        {
            Health = 5;
            Damage = 1;
            Speed = 7;
            Position = startPosition;
            frames = new Image[]
            {
                Resource.biru1,
                Resource.biru1,
                Resource.biru2,
                Resource.biru2,
            };
        }
    }
}
