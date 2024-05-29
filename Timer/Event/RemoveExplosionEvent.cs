using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveExplosionEvent : Command
    {

        public RemoveExplosionEvent(Explosion exe, bool val = false)
        {
            this.explode = exe;
            this.isPlayer = val;
        }

        public override void Execute(float deltaTime)
        {
            this.explode.Delete();

            if (this.isPlayer)
            {
                ShipMan.ActivateShip();
                ShipMan.GetShip().SetState(ShipMan.State.Ready);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private Explosion explode;
        private bool isPlayer;
    }
}
