using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveSaucerObserver : ColObserver
    {
        public RemoveSaucerObserver()
        {
            this.rand = new Random();
            this.pSacuer = null;
        }
        public RemoveSaucerObserver(RemoveSaucerObserver b)
        {
            this.rand = b.rand;
            this.pSacuer = b.pSacuer;
        }
        public override void Notify()
        {
             this.pSacuer = (Saucer)this.pSubject.pObjB;

            if(this.pSubject.pObjA.name == GameObject.Name.Missile)
            { 
                GameObjectMan.UpdatePlayerScore(rand.Next(100, 250));
                GameObjectMan.UpdateScore();
            }
            Debug.Assert(this.pSacuer != null);

            SoundMan.StopSound(SoundSource.Name.Saucer);
            SoundMan.PlaySound(SoundSource.Name.SaucerKilled, false, false, false);

            if (pSacuer.bMarkForDeath == false)
            {
                pSacuer.bMarkForDeath = true;
                //   Delay
                RemoveSaucerObserver pObserver = new RemoveSaucerObserver(this);
                DelayedObjectMan.Attach(pObserver);
            }
        }
        public override void Execute()
        {
            // Let the gameObject deal with this... 
            GameObject pB = (GameObject)Iterator.GetParent(this.pSacuer);
            this.pSacuer.Delete();
            pB.Update();

            //Explosion sprite.
            Explosion exp = new Explosion(GameObject.Name.Explosion, SpriteGame.Name.SaucerExplosion, this.pSacuer.x, this.pSacuer.y);

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
            RemoveExplosionEvent rEve = new RemoveExplosionEvent(exp);
            TimerMan.Add(TimeEvent.Name.ExplosionRemove, rEve, 0.5f);
        }

        // --------------------------------------
        // Data
        // --------------------------------------
        private GameObject pSacuer;
        private Random rand;
    }
}
