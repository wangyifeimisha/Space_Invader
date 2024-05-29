using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WhiteBird : BirdCategory
    {
        public WhiteBird(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        public override void Accept(CollisionVisitor other)
        {
            // Important: at this point we have an WhiteBird
            // Call the appropriate collision reaction            
            other.VisitWhiteBird(this);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
