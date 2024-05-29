using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class RemoveShipObserver : ColObserver
    {
        
        public RemoveShipObserver()
        {
            this.pShipDestroyed = null;
        }

        public RemoveShipObserver(RemoveShipObserver s)
        {
            this.pShipDestroyed = s.pShipDestroyed;
        }

        public override void Notify()
        {

            this.pShipDestroyed = (Ship)this.pSubject.pObjB;
            Debug.Assert(this.pShipDestroyed != null);

            if (pShipDestroyed.bMarkForDeath == false)
            {
                pShipDestroyed.bMarkForDeath = true;
                //   Delay
                RemoveShipObserver pObserver = new RemoveShipObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }

        public override void Execute()
        {
            // Let the gameObject deal with this... 
            this.pShipDestroyed.Delete();
            Ship pShip = ShipMan.GetShip();
            pShip.SetState(ShipMan.State.Dead);

            if (GameObjectMan.DecreaseLife() == false)
            {

                //Explosion sprite.
                Explosion exp = new Explosion(GameObject.Name.Explosion, SpriteGame.Name.PlayerExplosionA, this.pShipDestroyed.x, this.pShipDestroyed.y);

                // Attached to SpriteBatches
                SpriteBatch pSB_Aliens = SpriteBatchMan.Search(SpriteBatch.Name.Explosion);
                SpriteBatch pSB_Boxes = SpriteBatchMan.Search(SpriteBatch.Name.Boxes);

                exp.ActivateGameSprite(pSB_Aliens);
                exp.ActivateCollisionSprite(pSB_Boxes);

                // Attach the missile to the missile root
                GameObject pExplosion = GameObjectMan.Search(GameObject.Name.ExplosionRoot);
                Debug.Assert(pExplosion != null);

                // Add to GameObject Tree - {update and collisions}
                pExplosion.Add(exp);

                //Remove explosion event.
                RemoveExplosionEvent rEve = new RemoveExplosionEvent(exp, true);
                TimerMan.Add(TimeEvent.Name.ExplosionRemove, rEve, 1.0f);
            }
            else
            {
                ShipMan.Destroy();
                SoundMan.Destroy();
            }
        }

        // --------------------------------------
        // Data
        // --------------------------------------
        private GameObject pShipDestroyed;
    }
}
