﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShieldColumn : Composite
    {
        public ShieldColumn(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }
        ~ShieldColumn()
        {
        }
        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate Col reaction            
            other.VisitShieldColumn(this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs ShieldColumn
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void VisitGrid(AlienGrid a)
        {
            // AlienGrid vs ShieldGrid
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(a, pGameObj);
        }

        public override void VisitBomb(Bomb b)
        {
            // Bomb vs ShieldColumn
            ColPair.Collide(b, (GameObject)Iterator.GetChild(this));
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        // ---------------------------------------------
        // Data
        // ---------------------------------------------


    }
}
