using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienGrid : Composite
    {
        public AlienGrid(GameObject.Name name, SpriteGame.Name spriteName, float posX, float posY, ScenePlay scene)
        : base(name, spriteName)
        {
            this.x = posX;
            this.y = posY;

            this.pColObj.pColSprite.SetLineColor(1, 1, 1);

            this.delta = 2.0f;
            this.alientCount = 0;
            this.counter = 0;

            this.scene = scene;
            this.gridStartSpeed = 0.5f;
            this.gridStartLocation = 600.0f;

            pSB_Bombs = SpriteBatchMan.Search(SpriteBatch.Name.Bombs);
            pSB_Box = SpriteBatchMan.Search(SpriteBatch.Name.Boxes);
        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an BirdGroup
            // Call the appropriate collision reaction            
            other.VisitGrid(this);
        }

        public override void VisitMissileGroup(MissileGroup m)
        {
            // BirdColumn vs MissileGroup
            Debug.WriteLine("         collide:  {0} <-> {1}", m.name, this.name);

            // MissileGroup vs Columns
            GameObject pGameObj = (GameObject)Iterator.GetChild(this);
            ColPair.Collide(m, pGameObj);
        }

        public override void Update()
        {
            base.BaseUpdateBoundingBox(this);

            base.Update();
        }
        public void MoveGrid()
        {

            ForwardIterator pFor = new ForwardIterator(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                counter++;
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.x += this.delta;

                pNode = pFor.Next();
            }
            alientCount = counter;
            counter = 0;
        }

        public void MoveGridDown()
        {

            ForwardIterator pFor = new ForwardIterator(this);

            Component pNode = pFor.First();
            while (!pFor.IsDone())
            {
                GameObject pGameObj = (GameObject)pNode;
                pGameObj.y -= 3.0f;

                pNode = pFor.Next();
            }
        }

        public void ActivateBomb()
        {
            Random srand = new Random();
            Component pNode = ForwardIterator.GetChild(this);
            
            
            if (pNode != null)
            {
                Component pColNode = pNode;
                int columnCount = 0, i = 1;

                //Count no of columns.
                while (pNode != null)
                {
                    columnCount++;
                    pNode = ForwardIterator.GetSibling(pNode);

                }

                //Go to the random column.
                int tmp = srand.Next(1, columnCount + 1);
                while (i++ < tmp)
                {
                    pColNode = ForwardIterator.GetSibling(pColNode);
                }

                //Get first child.
                pNode = ForwardIterator.GetChild(pColNode);
                GameObject pGameObject = (GameObject)pNode;

                //Spawn random bombs.
                Bomb pBomb = null;

                switch (srand.Next(0, 3))
                {
                    case 0:
                        pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.PlungerShotA, new FallDagger(), pGameObject.x, pGameObject.y - 30f);
                        pBomb.ActivateCollisionSprite(pSB_Box);
                        pBomb.ActivateGameSprite(pSB_Bombs);
                        break;

                    case 1:
                        pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.RollingShotA, new FallStraight(), pGameObject.x, pGameObject.y - 30f);
                        pBomb.ActivateCollisionSprite(pSB_Box);
                        pBomb.ActivateGameSprite(pSB_Bombs);
                        break;
                    case 2:
                        pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.SquigglyShotA, new FallZigZag(), pGameObject.x, pGameObject.y - 30f);
                        pBomb.ActivateCollisionSprite(pSB_Box);
                        pBomb.ActivateGameSprite(pSB_Bombs);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }

                GameObject pBombRoot = GameObjectMan.Search(GameObject.Name.BombRoot);
                Debug.Assert(pBombRoot != null);
                Debug.Assert(pBomb != null);
                pBombRoot.Add(pBomb);
            }
        }

        public float GetAlienCount()
        {
            return (this.alientCount);
        }

        public float GetDelta()
        {
            return this.delta;
        }

        public void SetDelta(float inDelta)
        {
            this.delta = inDelta;
        }

        public override void Delete()
        {
            //Delete created events.
            TimeEvent pSpawn = TimerMan.Search(TimeEvent.Name.BombSpawn);
            TimerMan.Delete(pSpawn);

            TimeEvent pSpriteAnim = TimerMan.Search(TimeEvent.Name.SpriteAnimation);
            while (pSpriteAnim != null)
            {
                TimerMan.Delete(pSpriteAnim);
                pSpriteAnim = TimerMan.Search(TimeEvent.Name.SpriteAnimation);
            }

            this.scene.RespawnGrid(gridStartLocation, gridStartSpeed);
            this.gridStartLocation -= 100.0f;
            this.gridStartSpeed -= 0.2f;
            if(this.gridStartLocation < 500.0f)
            {
                this.gridStartLocation = 500.0f;
            }

            if(this.gridStartSpeed < 0.3f)
            {
                this.gridStartSpeed = 0.3f;
            }
        }

        // Data: ---------------
        private float delta;
        public float alientCount = 0;
        private float counter = 0;
        private ScenePlay scene;
        private float gridStartLocation = 600.0f;
        private float gridStartSpeed = 0.5f;

        // Attached to SpriteBatches
        SpriteBatch pSB_Bombs;
        SpriteBatch pSB_Box;
    }
}
