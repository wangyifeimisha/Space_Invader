/*using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Explosion : GameSprite
    {
        public Explosion(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 0);
        }

        public override void Accept(CollisionVisitor other)
        {
            Debug.Assert(false);
        }

        public override void Delete()
        {
            base.Delete();
        }

        public override void Dump()
        {
            throw new NotImplementedException();
        }

        
    }
}
*/