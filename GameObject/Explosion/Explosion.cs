using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Explosion : ExplosionCategory
    {
        public Explosion(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 0);
        }

        public override void Update()
        {
            base.Update();
        }

        ~Explosion()
        {

        }

        public override void Delete()
        {
            this.pColObj.pColRect.Set(0, 0, 0, 0);
            base.Update();

            // Update the parent (missile root)
            GameObject pParent = (GameObject)this.pParent;
            pParent.Update();

            // Now remove it
            base.Delete();
        }

        public override void Accept(ColVisitor other)
        {
            other.VisitExplosion(this);
        }


    }
}
