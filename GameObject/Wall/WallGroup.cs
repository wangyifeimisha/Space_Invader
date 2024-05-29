using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class WallGroup : Composite
    {
        public WallGroup(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 1);
        }

        ~WallGroup()
        {

        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an Alien
            // Call the appropriate Col reaction            
            other.VisitWallGroup(this);
        }
        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);
            base.Update();
        }

        public override void VisitGrid(AlienGrid a)
        {
            // BirdGroup vs WallGroup
            // Debug.WriteLine("collide: {0} with {1}", a, this);

            // BirdGroup vs WallGroup
            //              go down a level in Wall Group.
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(a, pGameObj);
        }
        public override void VisitMissileGroup(MissileGroup m)
        {
            // MissileRoot vs WallRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }
        public override void VisitMissile(Missile m)
        {
            // Missile vs WallRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }
        public override void VisitBombRoot(BombRoot b)
        {
            // BombRoot vs WallRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(b);
            ColPair.Collide(pGameObj, this);
        }
        public override void VisitBomb(Bomb b)
        {
            // Bomb vs WallRoot
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(b, pGameObj);
        }

        public override void VisitShipRoot(ShipRoot s)
        {
            // ShipRoot vs WallRight.
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(s, pGameObj);
        }

        public override void VisitSaucerRoot(SaucerRoot s)
        {
            // ShipRoot vs WallRight.
            GameObject pGameObj = (GameObject)Iterator.GetChild(s);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitSaucer(Saucer s)
        {
            // ShipRoot vs WallRight.
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(s, pGameObj);
        }

    }
}
