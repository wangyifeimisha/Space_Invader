using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteNodeMan_Link : ManBase
    {
        public SpriteNode_Link poActive = null;
        public SpriteNode_Link poReserve = null;
    }

    public class SpriteNodeMan : SpriteNodeMan_Link
    {
        //---------------------------------------------------------------------------------------------------------
        //Constructor
        //---------------------------------------------------------------------------------------------------------
        public SpriteNodeMan(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     //Delegate.
        {
            // At this point SBMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);
            this.pBackSpriteBatch = null;

            this.pDataCompare = new SpriteNode();       //Initialize compare sprite data with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------
        public void Set(SpriteBatch.Name name, int initialReserveSize, int newGrowthSize)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Assign name.
            this.name = name;

            //Add to reserve list.
            this.BaseSetReserve(initialReserveSize, newGrowthSize);
        }

        // Attach Sprite
        public SpriteNode Attach(SpriteBase pNode)
        {
            //Add sprite data to active list.
            SpriteNode pTmp = (SpriteNode)this.BaseAdd();

            //Verify sprite data is added.
            Debug.Assert(pTmp != null);

            //Set new sprite data.
            pTmp.SetSpriteNode(pNode, this);

            //Return added sprite data.
            return (pTmp);
        }

        public void Draw()
        {
            //Get active list.
            SpriteNode pTmp = (SpriteNode)this.BaseGetActive();

            //Traverse the list.
            while(pTmp != null)
            {
                //Render Base Sprite.
                pTmp.GetSpriteBase().Render();

                //Get next active base sprite.
                pTmp = (SpriteNode)pTmp.pNext;
            }
        }

        public void Delete(SpriteNode pNewData)
        {
            //verify game sprite is present to delete.
            Debug.Assert(pNewData != null);

            //Delete game sprite from active list.
            this.BaseDelete(pNewData);
        }

        //Print the Man details.
        public void Print()
        {
            //Print sprite data Man.
            this.BasePrint("SPRITE DATA");
        }

        public SpriteBatch GetSpriteBatch()
        {
            return this.pBackSpriteBatch;
        }
        public void SetSpriteBatch(SpriteBatch _pSpriteBatch)
        {
            this.pBackSpriteBatch = _pSpriteBatch;
        }

        //--------------------------------------------------------------------------------
        // Derived Functions
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new sprite data.
            DLink pTmp = new SpriteNode();

            //Verify new sprite data is created.
            Debug.Assert(pTmp != null);

            //Return new sprite data.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to sprite data to compare.
            SpriteNode pTmp1 = (SpriteNode)pCompareWith;
            SpriteNode pTmp2 = (SpriteNode)pCompareTo;

            //Stubbed this function.
            //Compare with names.
            if (pTmp1 == pTmp2)
            {
                return false;        //If name is equal.
            }

            return false;       //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            //Convert link to sprite data.
            SpriteNode pTmp = (SpriteNode)pResetLink;

            //Reset sprite data.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to sprite data.
            SpriteNode pTmp = (SpriteNode)pPrintLink;
            //pTmp.ReturnName();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to sprite data.
            SpriteNode pTmp = (SpriteNode)pPrintLink;
            //return (pTmp.ReturnName().ToString());
            return null;
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private readonly SpriteNode pDataCompare;
        private SpriteBatch.Name name;
        private SpriteBatch pBackSpriteBatch;
    }
}
