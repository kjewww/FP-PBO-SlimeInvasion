using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShooterGame2D
{
    public abstract class Entity
    {
        public int Health { get; set; }
        public int Damage { get; set; }
        public int Ammo { get; set; }
        public float Speed { get; set; }

        public abstract void TakeDamage(int damage);
    }
}
