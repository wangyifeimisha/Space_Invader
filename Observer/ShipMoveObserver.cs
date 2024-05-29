using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShipMoveObserver : ColObserver
    {
        public ShipMoveObserver()
        {
        }
        public override void Notify()
        {
            WallCategory pWall = (WallCategory)this.pSubject.pObjB;

            if (pWall.GetCategoryType() == WallCategory.Type.Right)
            {
                Ship pShip = ShipMan.GetShip();
                pShip.SetState(ShipMan.State.NoMoveRight);
            }
            else if (pWall.GetCategoryType() == WallCategory.Type.Left)
            {
                Ship pShip = ShipMan.GetShip();
                pShip.SetState(ShipMan.State.NoMoveLeft);
            }
            else
            {
                Debug.Assert(false);
            }
        }
    }
}

