﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallBottom : WallCategory
    {
        public WallBottom(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, float width, float height)
            : base(name, spriteName, WallCategory.Type.Bottom)
        {
            this.pColObj.pColRect.Set(posX, posY, width, height);

            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 1);
        }

        ~WallBottom()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate Col reaction            
            other.VisitWallBottom(this);
        }

        public override void VisitBomb(Bomb b)
        {
            //Debug.WriteLine(" ---> Done");
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(b, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            // Go to first child
            base.Update();
        }

        // Data: ---------------

    }
}
