using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBatchMan_Link : ManBase
    {
        public SpriteBatch_Link poActive;
        public SpriteBatch_Link poReserve;
    }

    public class SpriteBatchMan : SpriteBatchMan_Link
    {
        //---------------------------------------------------------------------------------------------------------
        // Public Constructor
        //---------------------------------------------------------------------------------------------------------
        public SpriteBatchMan(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()
        {
            // At this point SpriteBatchMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);
            //this.pDataCompare = new SpriteBatch();      //Initialize compare sprite with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        // Private Constructor
        //---------------------------------------------------------------------------------------------------------
        private SpriteBatchMan()
        : base() // <--- Kick the can (delegate)
        {
            SpriteBatchMan.pActiveSBMan = null;
            // initialize derived data here
            SpriteBatchMan.pDataCompare = new SpriteBatch();
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
                _pInstance = new SpriteBatchMan();
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------------------------------
        public static SpriteBatch Add(SpriteBatch.Name newName, int reserveSize = 2, int growSize = 2)
        {
            //Get sprite batch manager instance.
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            //Add sprite batch to active list.
            SpriteBatch pTmp = (SpriteBatch)pMan.BaseAdd();

            //Verify sprite batch is added.
            Debug.Assert(pTmp != null);

            //Set new sprite batch data.
            pTmp.Set(newName, reserveSize, growSize);

            //Print sprite batch added.
            Debug.WriteLine("\n\n***Added Sprite Batch:\"" + newName + "\" to Active List***");

            //Return added sprite batch.
            return (pTmp);
        }

        public static void SetActive(SpriteBatchMan pSBMan)
        {
            SpriteBatchMan pMan = SpriteBatchMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            Debug.Assert(pSBMan != null);
            SpriteBatchMan.pActiveSBMan = pSBMan;
        }

        public static void Draw()
        {
            //Get sprite batch manager instance.
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            //Get active list.
            SpriteBatch pTmp = (SpriteBatch)pMan.BaseGetActive();

            //Traverse the list.
            while (pTmp != null)
            {
                if (pTmp.GetDrawEnable())
                {
                    //Get sprite data manager.
                    SpriteNodeMan pData = pTmp.GetSBDataManager();

                    //Verify sprite data manager is present.
                    Debug.Assert(pData != null);

                    //Draw sprite data.
                    pData.Draw();
                }
                //Get next sprite batch.
                pTmp = (SpriteBatch)pTmp.pNext;
            }
        }

        public static SpriteBatch Search(SpriteBatch.Name newName)
        {
            //Get sprite batch manager instance.
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            //set comparing sprite batch with given name.
            SpriteBatchMan.pDataCompare.SetName(newName);

            //Search active list with comparing sprite batch.
            SpriteBatch pTmp = (SpriteBatch)pMan.BaseSearch(SpriteBatchMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(SpriteBatch pNewData)
        {
            //Get sprite batch manager instance.
            SpriteBatchMan pMan = SpriteBatchMan.pActiveSBMan;
            Debug.Assert(pMan != null);

            //verify game sprite is present to delete.
            Debug.Assert(pNewData != null);

            //Delete game sprite from active list.
            pMan.BaseDelete(pNewData);
        }

        public static void Delete(SpriteNode pSpriteBatchData)
        {
            Debug.Assert(pSpriteBatchData != null);
            SpriteNodeMan pSpriteDataMan = pSpriteBatchData.GetSBDataMan();

            Debug.Assert(pSpriteDataMan != null);
            pSpriteDataMan.Delete(pSpriteBatchData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get sprite batch manager instance.
            SpriteBatchMan pMan = SpriteBatchMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print sprite batch manager.
            pMan.BasePrint("SPRITE BATCH");
        }

        //Destroy instance.
        public static void Destroy()
        {
            //Get sprite batch manager instance.
            SpriteBatchMan pMan = SpriteBatchMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print all sprite data.
            Print();

            //Delete the sprite manager.
            _pInstance = null;
        }
        //--------------------------------------------------------------------------------
        //Derived Functions
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new sprite batch.
            DLink pTmp = new SpriteBatch();

            //Verify new sprite batch is created.
            Debug.Assert(pTmp != null);

            //Return new sprite batch.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to sprite to compare.
            SpriteBatch pTmp1 = (SpriteBatch)pCompareWith;
            SpriteBatch pTmp2 = (SpriteBatch)pCompareTo;

            //Compare with names.
            if (pTmp1.ReturnName() == pTmp2.ReturnName())
            {
                return true;        //If name is equal.
            }

            return false;       //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            //Convert link to sprite batch.
            SpriteBatch pTmp = (SpriteBatch)pResetLink;

            //Reset sprite.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to sprite batch.
            SpriteBatch pTmp = (SpriteBatch)pPrintLink;

            //Print sprite batch details.
            pTmp.GetSpriteBatch();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to sprite batch.
            SpriteBatch pTmp = (SpriteBatch)pPrintLink;

            //Print sprite batch name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static SpriteBatchMan GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        private static SpriteBatchMan _pInstance = null;
        private static SpriteBatchMan pActiveSBMan = null;

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private static SpriteBatch pDataCompare;
    }
}
