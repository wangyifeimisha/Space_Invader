using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ColPair_Link : DLink
    {

    }
    public class ColPair : ColPair_Link
    {

        //-----------------------------------------------------------------------------------------
        //Enum.
        //-----------------------------------------------------------------------------------------
        public enum Name
        {
            Missile_Alien,
            Missile_Saucer,
            Missile_Wall,
            Misslie_Shield,
            Missile_Bomb,

            Alien_Wall,
            Alien_Player,
            Alien_Shield,

            Bomb_Wall,
            Bomb_Shield,
            Bomb_Player,
            
            Player_Wall,
            Saucer_Wall,

            NullObject,
            Not_Initialized
        }

        //-----------------------------------------------------------------------------------------
        //Constructor.
        //-----------------------------------------------------------------------------------------
        public ColPair()
            : base()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = ColPair.Name.Not_Initialized;

            this.pSubject = new ColSubject();
            Debug.Assert(this.pSubject != null);
        }
        //-----------------------------------------------------------------------------------------
        //Methods.
        //-----------------------------------------------------------------------------------------
        public void SetColPair(ColPair.Name colpairName, GameObject pTreeRootA, GameObject pTreeRootB)
        {
            Debug.Assert(pTreeRootA != null);
            Debug.Assert(pTreeRootB != null);

            this.treeA = pTreeRootA;
            this.treeB = pTreeRootB;
            this.name = colpairName;
        }

        public void Clean()
        {
            this.treeA = null;
            this.treeB = null;
            this.name = ColPair.Name.Not_Initialized;
        }

        public ColPair.Name ReturnName()
        {
            return this.name;
        }

        public void Process()
        {
            Collide(this.treeA, this.treeB);
        }

        static public void Collide(GameObject pSafeTreeA, GameObject pSafeTreeB)
        {
            // A vs B
            GameObject pNodeA = pSafeTreeA;
            GameObject pNodeB = pSafeTreeB;

            while (pNodeA != null)
            {
                // Restart compare
                pNodeB = pSafeTreeB;

                while (pNodeB != null)
                {
                    
                    // Get rectangles
                    ColRect rectA = pNodeA.GetCollisionObject().pColRect;
                    ColRect rectB = pNodeB.GetCollisionObject().pColRect;

                    // test them
                    if (ColRect.Intersect(rectA, rectB))
                    {
                        // Boom - it works (Visitor in Action)
                        pNodeA.Accept(pNodeB);
                        
                        break;
                    }

                    pNodeB = (GameObject)Iterator.GetSibling(pNodeB);
                }

                pNodeA = (GameObject)Iterator.GetSibling(pNodeA);
            }
        }

        public void SetName(ColPair.Name newName)
        {
            this.name = newName;
        }

        public void Attach(ColObserver observer)
        {
            this.pSubject.Attach(observer);
        }
        public void NotifyListeners()
        {
            this.pSubject.Notify();
        }
        public void SetCollision(GameObject pObjA, GameObject pObjB)
        {
            Debug.Assert(pObjA != null);
            Debug.Assert(pObjB != null);

            // GameObject pAlien = AlienCategory.GetAlien(objA, objB);
            this.pSubject.pObjA = pObjA;
            this.pSubject.pObjB = pObjB;
        }

        public void GetColPair()
        {
            // TO DO ...
        }

        //-----------------------------------------------------------------------------------------
        //Data.
        //-----------------------------------------------------------------------------------------
        public ColPair.Name name;
        public GameObject treeA;
        public GameObject treeB;
        public ColSubject pSubject;
    }
}
