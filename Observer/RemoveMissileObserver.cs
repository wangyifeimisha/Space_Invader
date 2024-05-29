using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveMissileObserver : ColObserver
    {
        public RemoveMissileObserver()
        {
            this.pMissile = null;
        }
        public RemoveMissileObserver(RemoveMissileObserver m)
        {
            this.pMissile = m.pMissile;
        }
        public override void Notify()
        {

            this.pMissile = (Missile)this.pSubject.pObjA;
            Debug.Assert(this.pMissile != null);

            if (pMissile.bMarkForDeath == false)
            {
                pMissile.bMarkForDeath = true;
                //   Delay
                RemoveMissileObserver pObserver = new RemoveMissileObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pMissile.Delete();
        }

        // --------------------------------------
        // Data
        // --------------------------------------
        private GameObject pMissile;
    }
}
