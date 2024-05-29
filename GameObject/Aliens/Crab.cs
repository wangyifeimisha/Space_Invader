using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Crab : AlienCategory
    {
        public Crab(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY)
            : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;
        }

        ~Crab()
        {
        }

        public override void Accept(ColVisitor other)
        {
            // Call the appropriate Col reaction            
            other.VisitCrab(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // Crab vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Crab vs Bird
            GameObject pGameObj = (GameObject)Iterator.GetChild(m);
            ColPair.Collide(pGameObj, this);
        }

        public override void VisitMissile(Missile m)
        {
            // Bird vs Missile
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // Missile vs Crab.
            ColPair pColPair = ColPairMan.GetActiveColPair();
            pColPair.SetCollision(m, this);
            pColPair.NotifyListeners();
        }

        public override void Update()
        {
            //   Debug.WriteLine("update: {0}", this);
            base.Update();
        }
    }
}
