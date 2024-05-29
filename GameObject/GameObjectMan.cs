using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameObjectMan_MLink : ManBase
    {
        public GameObjectData_Link poActive;
        public GameObjectData_Link poReserve;
    }

    public class GameObjectMan : GameObjectMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private GameObjectMan(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new GameObjectData();   //Initialize compare game object data with default data.
            this.pNullGameObject = new GameObjectNull();    //Create new null object.

            //Set Compare object as null object.
            this.pDataCompare.pGameObj = this.pNullGameObject;

            //Set player1 score.
            this.player1Score = 0;

            //Set high score.
            this.playerHighScore = 0;

            //Set no of lives.
            this.livesLeft = 3;

        }

        //---------------------------------------------------------------------------------------------------------
        // Singleton Static Method
        //---------------------------------------------------------------------------------------------------------
        public static void Create(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if sprite manager is not present
            if (_pInstance == null)
            {
                //If not, create a new sprite manager.
                _pInstance = new GameObjectMan(initialReserveSize, newGrowthSize);
            }
        }

        public static void Destroy()
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print all sprite data.
            Print();

            //Delete the sprite manager.
            _pInstance = null;
        }

        public static GameObjectData Attach(GameObject pGameObject)
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Add game object to active list.
            GameObjectData pTmp = (GameObjectData)pMan.BaseAdd();

            //Verify Game object data is added.
            Debug.Assert(pTmp != null);

            //Set new game object data.
            pTmp.SetGameObject(pGameObject);

            //Print game object added.
            Debug.WriteLine("\n\n***Added Game Object data to Active List***");

            //Return added game object data.
            return (pTmp);
        }

        public static GameObject Search(GameObject.Name newName)
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing game object with given name.
            pMan.pDataCompare.pGameObj.name = newName;

            //Search active list with comparing game object.
            GameObjectData pTmp = (GameObjectData)pMan.BaseSearch(pMan.pDataCompare);

            //Verify data is present.
            Debug.Assert(pTmp != null);

            //Return data.
            return (pTmp.pGameObj);
        }

        public static void Delete(GameObjectData pNewData)
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify game sprite is present to delete.
            Debug.Assert(pNewData != null);

            //Delete game sprite from active list.
            pMan.BaseDelete(pNewData);
        }

        public static void Delete(GameObject pNode)
        {
            Debug.Assert(pNode != null);
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();

            GameObject pSafetyNode = pNode;

            // OK so we have a linked list of trees (Remember that)

            // 1) find the tree root (we already know its the most parent)

            GameObject pTmp = pNode;
            GameObject pRoot = null;
            while (pTmp != null)
            {
                pRoot = pTmp;
                pTmp = (GameObject)Iterator.GetParent(pTmp);
            }

            // 2) pRoot is the tree we are looking for
            // now walk the active list looking for pRoot

            GameObjectData pTree = (GameObjectData)pMan.BaseGetActive();

            while (pTree != null)
            {
                if (pTree.pGameObj == pRoot)
                {
                    // found it
                    break;
                }
                // Goto Next tree
                pTree = (GameObjectData)pTree.pNext;
            }

            // 3) pTree is the tree that holds pNode
            //  Now remove the node from that tree

            Debug.Assert(pTree != null);
            Debug.Assert(pTree.pGameObj != null);

            // Is pTree.poGameObj same as the node we are trying to delete?
            // Answer: should be no... since we always have a group (that was a good idea)

            Debug.Assert(pTree.pGameObj != pNode);

            GameObject pParent = (GameObject)Iterator.GetParent(pNode);
            Debug.Assert(pParent != null);

            // Make sure there is no child before the delete
            GameObject pChild = (GameObject)Iterator.GetChild(pNode);
            Debug.Assert(pChild == null);

            // remove the node
            pParent.Delete(pNode);

            // FOUND the bug!!!!
            pParent.Update();

            // TODO - Recycle pNode

        }

        public static void Update()
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Get active list.
            GameObjectData pTmp = (GameObjectData)pMan.BaseGetActive();
                        
            //Traverse the list.
            while (pTmp != null)
            {
                ReverseIterator pRev = new ReverseIterator(pTmp.pGameObj);

                Component pNode = pRev.First();
                while (!pRev.IsDone())
                {
                    GameObject pGameObj = (GameObject)pNode;
     
                    pGameObj.Update();

                    pNode = pRev.Next();
                }

                //Get next game object data.
                pTmp = (GameObjectData)pTmp.pNext;
            }

        }

        public static void Print()
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print Game Object details.
            pMan.BasePrint("GAME OBJECT");
        }
        public static void UpdateScore()
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Set font message.
            pMan.p1Font = FontMan.Search(Font.Name.Score1);
            pMan.pHighFont = FontMan.Search(Font.Name.HighScore);

            //Update score 1
            if (pMan.player1Score < 100)
            {
                pMan.p1Font.UpdateMessage("00" + pMan.player1Score);
            }
            else if(pMan.player1Score < 1000)
            {
                pMan.p1Font.UpdateMessage("0" + pMan.player1Score);
            }
            else if (pMan.player1Score < 10000)
            {
                pMan.p1Font.UpdateMessage("" + pMan.player1Score);
            }
            else
            {
                pMan.p1Font.UpdateMessage("9999");
            }

            //Update HighScore.
            if (pMan.playerHighScore < 100)
            {
                pMan.pHighFont.UpdateMessage("00" + pMan.playerHighScore);
            }
            else if (pMan.playerHighScore < 1000)
            {
                pMan.pHighFont.UpdateMessage("0" + pMan.playerHighScore);
            }
            else if (pMan.playerHighScore < 10000)
            {
                pMan.pHighFont.UpdateMessage("" + pMan.playerHighScore);
            }
            else
            {
                pMan.pHighFont.UpdateMessage("9999");
            }

        }

        public static bool DecreaseLife(bool val = false)
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Set font message.
            pMan.pLifeFont = FontMan.Search(Font.Name.Life);
            pMan.livesLeft--;


            if(val == true)
            {
                pMan.livesLeft = 0;
            }
            pMan.pLifeFont.UpdateMessage("" + pMan.livesLeft);

            if (pMan.livesLeft == 0)
            {
                SceneContext.SetState(SceneContext.Scene.Over);
                pMan.livesLeft = 3;
                pMan.player1Score = 0;
                return true;
            }

            return false;
        }

        public static void UpdatePlayerScore(int score)
        {
            //Get game object manager instance.
            GameObjectMan pMan = GameObjectMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            pMan.player1Score += score;

            if (pMan.player1Score > pMan.playerHighScore)
            {
                pMan.playerHighScore = pMan.player1Score;
            }
        }
        //--------------------------------------------------------------------------------
        //Derived Functions.
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new game object data.
            DLink pTmp = new GameObjectData();

            //Verify new game object data is created.
            Debug.Assert(pTmp != null);

            //Return new game object data.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to game object data to compare.
            GameObjectData pTmp1 = (GameObjectData)pCompareWith;
            GameObjectData pTmp2 = (GameObjectData)pCompareTo;

            //Compare with names.
            if (pTmp1.pGameObj.ReturnName() == pTmp2.pGameObj.ReturnName())
            {
                return true;        //If name is equal.
            }

            return false;       //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            //Convert link to game object data.
            GameObjectData pTmp = (GameObjectData)pResetLink;

            //Reset game object data.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to game object data.
            GameObjectData pTmp = (GameObjectData)pPrintLink;

            //Print game object data details.
            pTmp.Print();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to game object data.
            GameObjectData pTmp = (GameObjectData)pPrintLink;

            //Print game object data name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static GameObjectMan GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }

        //----------------------------------------------------------------------
        // Static Data.
        //----------------------------------------------------------------------
        private static GameObjectMan _pInstance = null;

        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private readonly GameObjectNull pNullGameObject;
        private GameObjectData pDataCompare;
        private int player1Score;
        private int playerHighScore;
        private int livesLeft;
        private Font p1Font;
        private Font pHighFont;
        private Font pLifeFont;
    }
}
