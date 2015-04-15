using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    class Cooldown
    {
        private short Timer { get; set; }
        private short MaxCount;
        private short Increment;
        public Cooldown(bool startOnCooldown = false)
        {
            MaxCount = 32;
            Timer = startOnCooldown ? (short)0 : MaxCount;
            Increment = 1;

        }
        public Cooldown(short MaxCount, short Increment = 1, bool startOnCooldown = false)
        {
            this.MaxCount = MaxCount;
            Timer = startOnCooldown ? (short)0 : MaxCount;
            this.Increment = Increment;
        }
        public void Update()
        {
            Timer += (short)(!(Timer >= MaxCount) ? Increment : 0);
        }
        public bool Output()
        {
            return (Timer == MaxCount) ? true : false;
        }
        public bool OutputInvert()
        {
            return (this.Output()) ? false : true;
        }
        public void Use()
        {
            if (Output())
            {
                Timer = 0;
            }
        }
        public void UseHalf()
        {
            if (Timer >= MaxCount / 2)
            {
                Timer = (short)(MaxCount / 2);
            }
        }
        public void Refresh()
        {
            Timer = MaxCount;
        }
        public float GetPercentage()
        {
            double x = (double)Timer / (double)MaxCount;
            var y = Timer / MaxCount;
            return (float)Math.Round(x, 2);
        }
        public void SetTimer(short s)
        {
            if (s >= MaxCount)
                Timer = MaxCount;
            else
                Timer = s;

        }
        public short GetMaxCount()
        {
            return MaxCount;
        }
    }
}
