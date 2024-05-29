using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ExplosionRoot : Composite
    {
        public ExplosionRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(0, 0, 1);
        }

        ~ExplosionRoot()
        {

        }

        public override void Accept(ColVisitor other)
        {
            other.VisitExplosionRoot(this);
        }

        public override void Update()
        {
            // Go to first child
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }
    }
}
