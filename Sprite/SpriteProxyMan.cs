using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteProxyMan_MLink : ManBase
    {
        public SpriteProxy_Base poActive;
        public SpriteProxy_Base poReserve;
    }

    public class SpriteProxyMan : SpriteProxyMan_MLink
    {
        //----------------------------------------------------------------------
        // Private Constructor
        //----------------------------------------------------------------------
        private SpriteProxyMan(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new SpriteProxy();  //Initialize compare proxy sprite with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        // Singleton Static Method
        //---------------------------------------------------------------------------------------------------------
        //Create new proxy.
        public static void Create(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if proxy sprite manager is not present
            if (_pInstance == null)
            {
                //If not, create a new proxy sprite manager.
                _pInstance = new SpriteProxyMan(initialReserveSize, newGrowthSize);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //Static Methods.
        //---------------------------------------------------------------------------------------------------------
        //Add proxy sprite to active list in manager.
        public static SpriteProxy Add(SpriteGame.Name newName)
        {
            //Get proxy sprite manager instance.
            SpriteProxyMan pMan = SpriteProxyMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Add proxy sprite to active list.
            SpriteProxy pTmp = (SpriteProxy)pMan.BaseAdd();

            //Verify sprite is added.
            Debug.Assert(pTmp != null);

            //Set new proxy sprite data.
            pTmp.SetProxy(newName);

            //Print proxy sprite added.
            Debug.WriteLine("\n\n***Added Proxy Sprite:\"" + newName + "\" to Active List***");

            //Return added proxy sprite.
            return (pTmp);
        }

        public static SpriteProxy Search(SpriteProxy.Name newName)
        {
            //Get proxy sprite manager instance.
            SpriteProxyMan pMan = SpriteProxyMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing proxy sprite with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing sprite.
            SpriteProxy pTmp = (SpriteProxy)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(SpriteGame pNewData)
        {
            //Get proxy sprite manager instance.
            SpriteProxyMan pMan = SpriteProxyMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify image is present to delete.
            Debug.Assert(pNewData != null);

            //Delete image from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get proxy sprite manager instance.
            SpriteProxyMan pMan = SpriteProxyMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print proxy sprite manager.
            pMan.BasePrint("PROXY SPRITE");
        }

        public static void Destroy()
        {
            //Get proxy sprite manager instance.
            SpriteProxyMan pMan = SpriteProxyMan.GetPrivateInstance();
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
            //Create new proxy sprite.
            DLink pTmp = new SpriteProxy();

            //Verify new proxy sprite is created.
            Debug.Assert(pTmp != null);

            //Return new proxy sprite.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to sprite to compare.
            SpriteProxy pTmp1 = (SpriteProxy)pCompareWith;
            SpriteProxy pTmp2 = (SpriteProxy)pCompareTo;

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
            //Convert link to proxy sprite.
            SpriteProxy pTmp = (SpriteProxy)pResetLink;

            //Reset proxy sprite.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to proxy sprite.
            SpriteProxy pTmp = (SpriteProxy)pPrintLink;

            //Print proxy sprite details.
            pTmp.GetProxy();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to proxy sprite.
            SpriteProxy pTmp = (SpriteProxy)pPrintLink;

            //Print proxy sprite name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static SpriteProxyMan GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        private static SpriteProxyMan _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private SpriteProxy pDataCompare;
    }
}
