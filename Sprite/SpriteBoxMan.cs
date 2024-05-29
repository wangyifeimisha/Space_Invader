using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBoxMan_MLink : ManBase
    {
        public SpriteBox_Base poActive;
        public SpriteBox_Base poReserve;
    }
    class SpriteBoxMan : SpriteBoxMan_MLink
    {
        //---------------------------------------------------------------------------------------------------------
        // Private Constructor
        //---------------------------------------------------------------------------------------------------------
        private SpriteBoxMan(int initialReserveSize = 2, int newGrowthSize = 2)
        : base()   //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new SpriteBox();    //Initialize compare box sprite with default data.
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

            //Check if box sprite manager is not present
            if (_pInstance == null)
            {
                //If not, create a new box sprite manager.
                _pInstance = new SpriteBoxMan(initialReserveSize, newGrowthSize);
            }
        }

        //Add to active List.
        public static SpriteBox Add(SpriteBox.Name name, float x, float y, float width, float height, Azul.Color newColor = null)
        {
            //Get box sprite manager instance.
            SpriteBoxMan pMan = SpriteBoxMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Add box sprite to active list.
            SpriteBox pTmp = (SpriteBox)pMan.BaseAdd();

            //Verify box sprite is added.
            Debug.Assert(pTmp != null);

            //Set new box sprite data.
            pTmp.SetBox(name, x, y, width, height, newColor);

            //Print box sprite added.
            Debug.WriteLine("\n\n***Added Box Sprite to Active List***");

            //Return added box sprite.
            return (pTmp);
        }

        //Search from active list.
        public static SpriteBox Search(SpriteBox.Name newName)
        {
            //Get box sprite manager instance.
            SpriteBoxMan pMan = SpriteBoxMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing box sprite with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing box sprite.
            SpriteBox pTmp = (SpriteBox)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(SpriteBox pNewData)
        {
            //Get box sprite manager instance.
            SpriteBoxMan pMan = SpriteBoxMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify game sprite is present to delete.
            Debug.Assert(pNewData != null);

            //Delete game sprite from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get box sprite manager instance.
            SpriteBoxMan pMan = SpriteBoxMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print box sprite manager.
            pMan.BasePrint("BOX SPRITE");
        }

        public static void Destroy()
        {
            //Get box sprite manager instance.
            SpriteBoxMan pMan = SpriteBoxMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print all sprite data.
            Print();

            //Delete the sprite manager.
            _pInstance = null;
        }

        //--------------------------------------------------------------------------------
        //Derived Functions.
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new box sprite.
            DLink pTmp = new SpriteBox();

            //Verify new sprite is created.
            Debug.Assert(pTmp != null);

            //Return new sprite.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to box sprite to compare.
            SpriteBox pTmp1 = (SpriteBox)pCompareWith;
            SpriteBox pTmp2 = (SpriteBox)pCompareTo;

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
            //Convert link to box sprite.
            SpriteBox pTmp = (SpriteBox)pResetLink;

            //Reset box sprite.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to box sprite.
            SpriteBox pTmp = (SpriteBox)pPrintLink;

            //Print box sprite details.
            pTmp.GetSpriteBox();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to box sprite.
            SpriteBox pTmp = (SpriteBox)pPrintLink;

            //Print box sprite name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static SpriteBoxMan GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        private static SpriteBoxMan _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private readonly SpriteBox pDataCompare;
    }
}
