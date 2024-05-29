using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SaucerRoot : Composite
    {
        public SaucerRoot(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 1);
        }

        public override void Accept(ColVisitor other)
        { 
            // Call the appropriate Col reaction            
            other.VisitSaucerRoot(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdColumn vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // BirdColumn vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            // Go to first child
            GameObject saucer = (GameObject)Iterator.GetChild(this);
            if(saucer != null)
            {
                base.BaseUpdateBoundingBox(this);
            }
            else
            {
                this.x = -20.0f;
                this.y = -20.0f;
            }

            base.Update();
        }

    }
}
