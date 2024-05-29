using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienExplosionObserver : ColObserver
    {
        public AlienExplosionObserver()
        {
            this.pSprite = null;
            this.pAlien = null;
        }

        public override void Notify()
        {
            this.pAlien = (AlienCategory)this.pSubject.pObjB;

            this.pSprite = SpriteGameMan.Search(SpriteGame.Name.PlayerExplosionA);
            this.pSprite.x = this.pAlien.x;
            this.pSprite.y = this.pAlien.y;

            this.pSprite.Update();
            this.pSprite.Render();
        }

        public override void Execute()
        {

        }

        // --------------------------------------
        // Data
        // --------------------------------------
        private SpriteGame pSprite;
        private GameObject pAlien;
    }
}
