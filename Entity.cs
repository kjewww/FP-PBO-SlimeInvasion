using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public class Entity
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public PointF Position { get; set; }
        protected Size size = new(64, 64);

        protected Image[] frames;
        protected int currentFrame = 0;
        protected int frameCount = 4;
        protected int animationCounter = 0;
        protected int animationSpeed = 8;
        protected bool isFacingLeft = false;

        public Entity(PointF startPosition)
        {
            Position = startPosition;
            Health = 100;
            Damage = 1;
            Speed = 10;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
        }

    }
}
