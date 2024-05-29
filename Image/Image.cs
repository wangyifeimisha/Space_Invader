using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Image_Link : DLink
    {
    }
    public class Image : Image_Link
    {
        //-----------------------------------------------------------------------------------------
        // Enum
        //-----------------------------------------------------------------------------------------
        public enum Name
        {
            Wall,

            OctopusA,
            OctopusB,
            CrabA,
            CrabB,
            SquidA,
            SquidB,
            AlienExplosion,
            Saucer,
            SaucerExplosion,
            Player,
            PlayerExplosionA,
            PlayerExplosionB,
            AlienPullYA,
            AlienPullYB,
            AlienPullUpisdeDownYA,
            AlienPullUpsideDownYB,
            PlayerShot,
            PlayerShotExplosion,
            SquigglyShotA,
            SquigglyShotB,
            SquigglyShotC,
            SquigglyShotD,
            PlungerShotA,
            PlungerShotB,
            PlungerShotC,
            PlungerShotD,
            RollingShotA,
            RollingShotB,
            RollingShotC,
            RollingShotD,
            AlienShotExplosion,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z,
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            LessThan,
            GreaterThan,
            Space,
            Equals,
            Asterisk,
            Question,
            Hyphen,

            Brick,
            BrickLeft_Top0,
            BrickLeft_Top1,
            BrickLeft_Bottom,
            BrickRight_Top0,
            BrickRight_Top1,
            BrickRight_Bottom,

            Default,
            NullObject,
            Uninitialized
        }

        //-----------------------------------------------------------------------------------------
        // Constructor
        //-----------------------------------------------------------------------------------------
        public Image() 
        :   base()
        {
            this.pRect = new Azul.Rect();
            Debug.Assert(this.pRect != null);

            //Set default values.
            this.ResetData();
        }

        //-----------------------------------------------------------------------------------------
        // Methods
        //-----------------------------------------------------------------------------------------
        //Used to set image.
        public void SetImage(Name newName, Texture pSrcTexture, float x, float y, float width, float height)
        {
            Debug.Assert(pSrcTexture != null);
            this.pTexture = pSrcTexture;

            //Set Rectangle values.
            this.pRect.Set(x, y, width, height);

            //Assign name.
            this.name = newName;
        }

        //Used to print image details.
        public void GetImage()
        {
            //Print name of image and texture.
            Debug.WriteLine("\nName: {0}  Texture Name: {1}\n", this.ReturnName(), this.GetTextureName());

            //Check if previous texture exist, if not print null.
            if (this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else        //else print the previous texture name.
            {
                Image pTmp = (Image)this.pPrev;
                Debug.WriteLine("Prev: {0}", pTmp.ReturnName());
            }

            //Check if next texture exist, if not print null.
            if (this.pNext == null)
            {
                Debug.WriteLine("Next: null\n");
            }
            else        //else print the next texture name.
            {
                Image pTmp = (Image)this.pNext;
                Debug.WriteLine("Next: {0}\n", pTmp.ReturnName());
            }
        }

        //Return the rectangle..
        public Azul.Rect GetAzulRect()
        {
            Debug.Assert(this.pRect != null);
            return (this.pRect);
        }

        //Return the texture.
        public Azul.Texture GetAzulTexture()
        {
            return (this.pTexture.GetAzulTexture());
        }

        //Return the texture name.
        public Texture.Name GetTextureName()
        {
            return (this.pTexture.ReturnName());
        }

        //Set name.
        public void SetName(Image.Name newName)
        {
            this.name = newName;
        }

        //Reset the image data to default.
        private void ResetData()
        {
            //Create a default texture.
            this.pTexture = null;

            //Create a default rectangle.
            this.pRect.Clear();

            //Assign name.
            this.name = Name.Uninitialized;
        }

        public void Clean()
        {
            this.ResetData();
        }

        //Return the image name.
        public Name ReturnName()
        {
            return (this.name);
        }

        //-----------------------------------------------------------------------------------------
        // Data
        //-----------------------------------------------------------------------------------------
        public Name name;
        private readonly Azul.Rect pRect;
        private Texture pTexture;
    }
}
