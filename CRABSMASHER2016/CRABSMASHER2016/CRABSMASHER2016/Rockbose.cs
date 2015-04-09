using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Enemy { }
    class Rockbose : Enemy
    {
        delegate void Attack();
        Attack attack;
        int hp;
        int maxHp;
        Vector2 position;
        int Hp { get { return hp; }
            set
            {
                if (value < maxHp)
                    hp =  value;
                if (value > maxHp)
                    hp = maxHp;
                if ((hp /maxHp) < 0.3f)
                    attack = () => Console.WriteLine("Rage");
            }
        }
        public void Update(Vector2 playerPos)
        {
            //Vector2 DistensProsenage = new Vector2();
            //if (Converter.Vector2.AnyGreater(DistensProsenage, playerPos - position))
            {
                
            }
        }
    }
}
