using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TextureMan_Link : ManBase
    {
        public Texture_Link poActive;
        public Texture_Link poReserve;
    }

    public class TextureMan : TextureMan_Link
    {
        //---------------------------------------------------------------------------------------------------------
        // Private Constructor
        //---------------------------------------------------------------------------------------------------------
        private TextureMan(int initialReserveSize = 2, int newGrowthSize = 2)
        :   base()     //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pTextureCompare = new Texture();       //Initialize compare texture with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        // Singleton Static Method
        //---------------------------------------------------------------------------------------------------------
        //Create new texture manager.
        public static void Create(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if texture manager is not present
            if (_pInstance == null)
            {
                //If not, create a new texture manager.
                _pInstance = new TextureMan(initialReserveSize, newGrowthSize);
            }

            //Null object texture.
            Texture pTexture = TextureMan.Add(Texture.Name.NullObject, "HotPink.tga");
            Debug.Assert(pTexture != null);

            //Default texture.
            pTexture = TextureMan.Add(Texture.Name.Default, "HotPink.tga");
            Debug.Assert(pTexture != null);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------------------------------
        //Add texture to active list in manager.
        public static Texture Add(Texture.Name newName, string pTextureName)
        {
            //Get texture manager instance.
            TextureMan pMan = TextureMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //add texture to active list.
            Texture pTmp = (Texture)pMan.BaseAdd();

            //Verify texture is added.
            Debug.Assert(pTmp != null);
            Debug.Assert(pTextureName != null);

            //Set new texture data.
            pTmp.SetTexture(newName, pTextureName);

            //Print texture added.
            Debug.WriteLine("\n\n***Added Texture:\"" + newName + "\" to Active List***");

            //Return added texture.
            return (pTmp);
        }

        //Search from active list.
        public static Texture Search(Texture.Name newName)
        {
            //Get texture manager instance.
            TextureMan pMan = TextureMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing texture with given name.
            pMan.pTextureCompare.SetName(newName);

            //Search active list with comparing texture.
            Texture pTmp = (Texture)pMan.BaseSearch(pMan.pTextureCompare);

            //Return.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(Texture pNewData)
        {
            //Get texture manager instance.
            TextureMan pMan = TextureMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Verify texture to delete is present.
            Debug.Assert(pNewData != null);

            //Delete texture from active list.
            pMan.BaseDelete(pNewData);
            
        }

        //Print the manager details.
        public static void Print()
        {
            //Get texture manager instance.
            TextureMan pMan = TextureMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print texture manager.
            pMan.BasePrint("TEXTURE");
        }

        public static void Destroy()
        {
            //Get texture manager instance.
            TextureMan pMan = TextureMan.GetPrivateInstance();

            //Print all texture data.
            Print();

            //Delete the texture manager.
            _pInstance = null;
        }

        //--------------------------------------------------------------------------------
        // Derived Functions
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new texture.
            DLink pTmp = new Texture();

            //Verify new texture is created.
            Debug.Assert(pTmp != null);

            //Return new texture.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            // This is used in baseSearch() 
            Debug.Assert(pCompareWith != null);
            Debug.Assert(pCompareTo != null);

            //Convert links to texture to compare.
            Texture pTmp1 = (Texture)pCompareWith;
            Texture pTmp2 = (Texture)pCompareTo;

            //Compare with names.
            if (pTmp1.ReturnName() == pTmp2.ReturnName())
            {
                return true;    //If name is equal.
            }

            return false;       //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            Debug.Assert(pResetLink != null);

            //Convert link to texture.
            Texture pTmp = (Texture)pResetLink;

            //Reset texture.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to texture.
            Texture pTmp = (Texture)pPrintLink;

            //Print texture details.
            pTmp.GetTexture();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to texture.
            Texture pTmp = (Texture)pPrintLink;

            //Print texture name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static TextureMan GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        private static TextureMan _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private readonly Texture pTextureCompare;

    }
}
