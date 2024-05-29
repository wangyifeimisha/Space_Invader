using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class YellowBird : Leaf
    {
        public YellowBird(GameObject.Name name, GameSprite.Name spriteName, float posX, float posY, float width, float height, float angle)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
            this.sx = width;
            this.sy = height;
            this.angle = angle;
        }

        override public void Move()
        {
            this.y += delta;
            if (this.y < 0.0f)
            {
                delta *= -1;
            }
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
