using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class GameObject : Component
    {
        
        //-----------------------------------------------------------------------------------------
        // Enum
        //-----------------------------------------------------------------------------------------
        public enum Name
        {
            Crab,
            Squid,
            Octopus,

            RedAlien,
            GreenAlien,
            BlueAlien,
            WhiteAlien,

            AlienGrid,
            AlienColumn_0,
            AlienColumn_1,
            AlienColumn_2,
            AlienColumn_3,
            AlienColumn_4,
            AlienColumn_5,
            AlienColumn_6,
            AlienColumn_7,
            AlienColumn_8,
            AlienColumn_9,
            AlienColumn_10,

            WallGroup,
            WallRight,
            WallLeft,
            WallTop,
            WallBottom,

            ShieldRoot,
            ShieldGrid,
            ShieldColumn_0,
            ShieldColumn_1,
            ShieldColumn_2,
            ShieldColumn_3,
            ShieldColumn_4,
            ShieldColumn_5,
            ShieldColumn_6,
            ShieldBrick,

            Ship,
            ShipRoot,

            Missile,
            MissileGroup,

            Explosion,
            ExplosionRoot,

            Bomb,
            BombRoot,

            Saucer,
            SaucerRoot,

            NullObject,
            Uninitialized
        }

        //-----------------------------------------------------------------------------------------
        //Constructor.
        //-----------------------------------------------------------------------------------------

        protected GameObject(GameObject.Name newName)
        : base()
        {
            this.name = newName;
            this.x = 0.0f;
            this.y = 0.0f;
            this.pSpriteProxy = null;
        }

        
        protected GameObject(GameObject.Name newName, SpriteGame.Name spriteName)
        : base()
        {
            this.name = newName;
            this.x = 0.0f;
            this.y = 0.0f;
            this.bMarkForDeath = false;
            this.pSpriteProxy = SpriteProxyMan.Add(spriteName);

            this.pColObj = new ColObject(this.pSpriteProxy);
            Debug.Assert(this.pColObj != null);
        }

        //-----------------------------------------------------------------------------------------
        //Methods.
        //-----------------------------------------------------------------------------------------
        //Update.
        public virtual void Update()
        {
            Debug.Assert(this.pSpriteProxy != null);
            this.pSpriteProxy.x = this.x;
            this.pSpriteProxy.y = this.y;

            Debug.Assert(this.pColObj != null);
            this.pColObj.UpdatePos(this.x, this.y);
            Debug.Assert(this.pColObj.pColSprite != null);
            this.pColObj.pColSprite.Update();
        }

        protected void BaseUpdateBoundingBox(Component pStart)
        {
            GameObject pNode = (GameObject)pStart;

            // point to ColTotal
            ColRect ColTotal = this.pColObj.pColRect;
            //TODO...

            // Get the first child
            pNode = (GameObject)Iterator.GetChild(pNode);

            if (pNode != null)
            {
                // Initialized the union to the first block
                ColTotal.Set(pNode.pColObj.pColRect);

                // loop through sliblings
                while (pNode != null)
                {
                    ColTotal.Union(pNode.pColObj.pColRect);

                    // go to next sibling
                    pNode = (GameObject)Iterator.GetSibling(pNode);
                }

                //this.poColObj.poColRect.Set(201, 201, 201, 201);
                this.x = this.pColObj.pColRect.x;
                this.y = this.pColObj.pColRect.y;

                //  Debug.WriteLine("x:{0} y:{1} w:{2} h:{3}", ColTotal.x, ColTotal.y, ColTotal.width, ColTotal.height);
            }
        }

        public void ActivateCollisionSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            Debug.Assert(this.pColObj != null);
            pSpriteBatch.Attach(this.pColObj.pColSprite);
        }
        public void ActivateGameSprite(SpriteBatch pSpriteBatch)
        {
            Debug.Assert(pSpriteBatch != null);
            pSpriteBatch.Attach(this.pSpriteProxy);
        }

        public virtual void Delete()
        {
            // Find the SpriteNode
            Debug.Assert(this.pSpriteProxy != null);
            SpriteNode pSpriteNode = this.pSpriteProxy.GetSpriteNode();

            // Remove it from the Man
            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Delete(pSpriteNode);

            // Remove collision sprite from spriteBatch

            Debug.Assert(this.pColObj != null);
            Debug.Assert(this.pColObj.pColSprite != null);
            pSpriteNode = this.pColObj.pColSprite.GetSpriteNode();

            Debug.Assert(pSpriteNode != null);
            SpriteBatchMan.Delete(pSpriteNode);

            // Remove from GameObjectMan

            GameObjectMan.Delete(this);
        }

        public void SetCollisionColor(float red, float green, float blue)
        {
            Debug.Assert(this.pColObj != null);
            Debug.Assert(this.pColObj.pColSprite != null);

            this.pColObj.pColSprite.SetLineColor(red, green, blue);
        }

        public void Print()
        {
            //Print name of GameObject.
            Debug.WriteLine("\nName: {0}\n", this.ReturnName());

            if (this.pSpriteProxy != null)
            {
                Debug.WriteLine("\t\t   pSpriteProxy: {0}", this.pSpriteProxy.ReturnName());
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.pSpriteProxy.ReturnSpriteName());
            }
            else
            {
                Debug.WriteLine("\t\t   pSpriteProxy: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);
        }

        public ColObject GetCollisionObject()
        {
            Debug.Assert(this.pColObj != null);
            return this.pColObj;
        }

        public Name ReturnName()
        {
            return (this.name);
        }

        //-----------------------------------------------------------------------------------------
        //Data.
        //-----------------------------------------------------------------------------------------
        public GameObject.Name name;

        public float x;
        public float y;

        public bool bMarkForDeath;

        public ColObject pColObj;
        public SpriteProxy pSpriteProxy;
    }
}
