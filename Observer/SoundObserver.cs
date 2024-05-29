using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SoundObserver : ColObserver
    {
        public SoundObserver()
        {
          
        }
        public override void Notify()
        {
            SoundMan.PlaySound(SoundSource.Name.ExplosionPlayer, false, false, false);
        }

    }
}
