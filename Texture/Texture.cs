using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Texture_Link : DLink
    {
    }
    public class Texture : Texture_Link
    {
        //-----------------------------------------------------------------------------------------
        // Enum
        //-----------------------------------------------------------------------------------------
        public enum Name
        {
            SpaceInvaders,
            Consolas36pt,
            Default,
            NullObject,
            Uninitialized
        }

        //-----------------------------------------------------------------------------------------
        // Constructor
        //-----------------------------------------------------------------------------------------
        public Texture()
        :   base()
        {
            //Set default values.
            this.ResetData();
        }

        //-----------------------------------------------------------------------------------------
        // Methods
        //-----------------------------------------------------------------------------------------
        //Used to set texture.
        public void SetTexture(Name newName, string pTextureName)
        {
            Debug.Assert(pTextureName != null);
            Debug.Assert(this.pAzulTexture != null);

            //If file is present.
            if (System.IO.File.Exists(pTextureName))
            {
                //Assign the new texture.
                this.pAzulTexture = new Azul.Texture(pTextureName, Azul.Texture_Filter.NEAREST, Azul.Texture_Filter.NEAREST);
            }

            //Verify texture is allocated.
            Debug.Assert(this.pAzulTexture != null);

            //Add name to texture if given else add uninitialized.
            this.name = newName;
        }

        //Used to print the texture.
        public void GetTexture()
        {
            //Print name of texture.
            Debug.WriteLine("\nName: {0}\n", this.ReturnName());

            //Check if previous texture exist, if not print null.
            if (this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else    //else print the previous texture name.
            {
                Texture pTmp = (Texture)this.pPrev;
                Debug.WriteLine("Prev: {0}", pTmp.ReturnName());
            }

            //Check if next texture exist, if not print null.
            if (this.pNext == null)
            {
                Debug.WriteLine("Next: null\n");
            }
            else    //else print the next texture name.
            {
                Texture pTmp = (Texture)this.pNext;
                Debug.WriteLine("Next: {0}\n", pTmp.ReturnName());
            }
        }

        //Return the texture.
        public Azul.Texture GetAzulTexture()
        {
            Debug.Assert(this.pAzulTexture != null);
            return (this.pAzulTexture);
        }

        //Reset the texture data to default.
        private void ResetData()
        {
            //Verify default texture exist.
            Debug.Assert(Texture.pDefaultAzulTexture != null);

            //Reset to default texture "HotPink.tga"
            this.pAzulTexture = pDefaultAzulTexture;
            Debug.Assert(this.pAzulTexture != null);

            //Reset name to Default.
            this.name = Name.Default;
        }

        public void Clean()
        {
            this.ResetData();
        }

        //Set a name.
        public void SetName(Texture.Name newName)
        {
            this.name = newName;
        }

        //Return the texture name.
        public Name ReturnName()
        {
            return (this.name);
        }

        //---------------------------------------------------------------------------------------------------------
        //Static Data
        //---------------------------------------------------------------------------------------------------------
        static private readonly Azul.Texture pDefaultAzulTexture = new Azul.Texture("HotPink.tga");

        //-----------------------------------------------------------------------------------------
        // Data
        //-----------------------------------------------------------------------------------------
        private Name name;
        private Azul.Texture pAzulTexture;
    }
}
