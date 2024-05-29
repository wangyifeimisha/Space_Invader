using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteGameMan_MLink : ManBase
    {
        public SpriteGame_Base poActive;
        public SpriteGame_Base poReserve;
    }

    public class SpriteGameMan : SpriteGameMan_MLink
    {
        //---------------------------------------------------------------------------------------------------------
        // Private Constructor
        //---------------------------------------------------------------------------------------------------------
        private SpriteGameMan(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new SpriteGame();       //Initialize compare sprite with default data.
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
                _pInstance = new SpriteGameMan(initialReserveSize, newGrowthSize);
            }

            //Null object texture.
            SpriteGame pGSprite = SpriteGameMan.Add(SpriteGame.Name.NullObject, Image.Name.NullObject, 0, 0, 0, 0);
            Debug.Assert(pGSprite != null);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------------------------------
        //Add sprite to active list in manager.
        public static SpriteGame Add(SpriteGame.Name newName, Image.Name pImageName, float x, float y, float width, float height, Azul.Color pColor = null)
        {
            //Get sprite manager instance.
            SpriteGameMan pMan = SpriteGameMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Add sprite to active list.
            SpriteGame pTmp = (SpriteGame)pMan.BaseAdd();

            //Verify sprite is added.
            Debug.Assert(pTmp != null);

            //Search image with given image name.
            Image pImage = ImageMan.Search(pImageName);
            Debug.Assert(pImage != null);

            //Set new sprite data.
            pTmp.SetSprite(newName, pImage, x, y, width, height, pColor);

            //Print sprite added.
            Debug.WriteLine("\n\n***Added Sprite:\"" + newName + "\" to Active List***");

            //Return added sprite.
            return (pTmp);
        }

        //Search from active list.
        public static SpriteGame Search(SpriteGame.Name newName = SpriteGame.Name.Uninitialized)
        {
            //Get sprite manager instance.
            SpriteGameMan pMan = SpriteGameMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing sprite with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing sprite.
            SpriteGame pTmp = (SpriteGame)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(SpriteGame pNewData)
        {
            //Get sprite manager instance.
            SpriteGameMan pMan = SpriteGameMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify game sprite is present to delete.
            Debug.Assert(pNewData != null);

            //Delete game sprite from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get sprite manager instance.
            SpriteGameMan pMan = SpriteGameMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print sprite manager.
            pMan.BasePrint("SPRITE");
        }

        public static void Destroy()
        {
            //Get sprite manager instance.
            SpriteGameMan pMan = SpriteGameMan.GetPrivateInstance();
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
            //Create new sprite.
            DLink pTmp = new SpriteGame();

            //Verify new sprite is created.
            Debug.Assert(pTmp != null);

            //Return new sprite.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to sprite to compare.
            SpriteGame pTmp1 = (SpriteGame)pCompareWith;
            SpriteGame pTmp2 = (SpriteGame)pCompareTo;

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
            //Convert link to sprite.
            SpriteGame pTmp = (SpriteGame)pResetLink;

            //Reset sprite.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to sprite.
            SpriteGame pTmp = (SpriteGame)pPrintLink;

            //Print sprite details.
            pTmp.GetSprite();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to sprite.
            SpriteGame pTmp = (SpriteGame)pPrintLink;

            //Print sprite name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static SpriteGameMan GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        private static SpriteGameMan _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private readonly SpriteGame pDataCompare;
    }
}
