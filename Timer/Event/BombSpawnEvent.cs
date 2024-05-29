using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class BombSpawnEvent : Command
    {
        AlienGrid pGrid = null;
        public BombSpawnEvent(GameObject pGrid)
        {
            this.pGrid = (AlienGrid)pGrid;
        }

        override public void Execute(float deltaTime)
        {
            pGrid.ActivateBomb();

            // Add itself back to timer
            TimerMan.Add(TimeEvent.Name.BombSpawn, this, deltaTime);

        }
    }
}
