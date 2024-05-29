using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveRight : ShipState
    {
        public override void Handle(Ship pShip)
        {
            pShip.SetState(ShipMan.State.Ready);
        }

        public override void MoveRight(Ship pShip)
        {
            pShip.x += pShip.shipSpeed;
            this.Handle(pShip);
        }

        public override void MoveLeft(Ship pShip)
        {
            
        }

        public override void ShootMissile(Ship pShip)
        {
            
        }
    }
}
