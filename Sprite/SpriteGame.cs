using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteGame_Base : SpriteBase
    {

    }
    public class SpriteGame : SpriteGame_Base
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
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
            Brick_LeftTop0,
            Brick_LeftTop1,
            Brick_LeftBottom,
            Brick_RightTop0,
            Brick_RightTop1,
            Brick_RightBottom,

            NullObject,
            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public SpriteGame()
        : base()
        {
            this.name = SpriteGame.Name.Uninitialized;

            // Use the default - it will be replaced in the Set
            this.pImage = ImageMan.Search(Image.Name.Default);
            Debug.Assert(this.pImage != null);

            this.pScreenRect = new Azul.Rect();
            Debug.Assert(this.pScreenRect != null);
            this.pScreenRect.Clear();

            // here is the actual new
            this.pAzulColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.pAzulColor != null);

            // here is the actual new
            this.pAzulSprite = new Azul.Sprite(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.pScreenRect, pTmpColor);
            Debug.Assert(this.pAzulSprite != null);

            this.x = pAzulSprite.x;
            this.y = pAzulSprite.y;
            this.sx = pAzulSprite.sx;
            this.sy = pAzulSprite.sy;
            this.angle = pAzulSprite.angle;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------
        //Update.
        public override void Update()
        {
            this.pAzulSprite.x = this.x;
            this.pAzulSprite.y = this.y;
            this.pAzulSprite.sx = this.sx;
            this.pAzulSprite.sy = this.sy;
            this.pAzulSprite.angle = this.angle;

            this.pAzulSprite.Update();
        }

        //Render.
        public override void Render()
        {
            this.pAzulSprite.Render();
        }

        //Used to set sprite.
        public void SetSprite(Name newName, Image pNewImage, float x, float y, float width, float height, Azul.Color pColor)
        {
            Debug.Assert(pNewImage != null);
            Debug.Assert(this.pScreenRect != null);
            Debug.Assert(this.pAzulSprite != null);

            this.pScreenRect.Set(x, y, width, height);
            this.pImage = pNewImage;
            this.name = newName;

            if (pColor == null)
            {
                Debug.Assert(SpriteGame.pTmpColor != null);
                SpriteGame.pTmpColor.Set(1, 1, 1);

                this.pAzulColor.Set(pTmpColor);
            }
            else
            {
                this.pAzulColor.Set(pColor);
            }

            this.pAzulSprite.Swap(pImage.GetAzulTexture(), pImage.GetAzulRect(), this.pScreenRect, this.pAzulColor);

            this.x = pAzulSprite.x;
            this.y = pAzulSprite.y;
            this.sx = pAzulSprite.sx;
            this.sy = pAzulSprite.sy;
            this.angle = pAzulSprite.angle;
        }

        //Used to print sprite.
        public void GetSprite()
        {
            //Print name of sprite, image and texture.
            Debug.WriteLine("\nName: {0}  Image Name: {1}\nTexture Name: {2}\n", this.ReturnName(), this.pImage.ReturnName(), this.pImage.GetTextureName());

            //Check if previous sprite exist, if not print null.
            if (this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else    //else print the previous sprite name.
            {
                SpriteGame pTmp = (SpriteGame)this.pPrev;
                Debug.WriteLine("Prev: {0}", pTmp.ReturnName());
            }

            //Check if next sprite exist, if not print null.
            if (this.pNext == null)
            {
                Debug.WriteLine("Next: null\n");
            }
            else    //else print the next sprite name.
            {
                SpriteGame pTmp = (SpriteGame)this.pNext;
                Debug.WriteLine("Next: {0}\n", pTmp.ReturnName());
            }
        }

        //Swap Color of sprite.
        public void SwapColor(Azul.Color pColor)
        {
            Debug.Assert(pColor != null);
            Debug.Assert(this.pAzulColor != null);
            Debug.Assert(this.pAzulSprite != null);

            this.pAzulColor.Set(pColor);
            this.pAzulSprite.SwapColor(pColor);
        }

        //Swap Color of sprite.
        public void SwapColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.pAzulColor != null);
            Debug.Assert(this.pAzulSprite != null);

            this.pAzulColor.Set(red, green, blue, alpha);
            this.pAzulSprite.SwapColor(this.pAzulColor);
        }

        //Swap Image of Sprite.
        public void SwapImage(Image pNewImage)
        {
            Debug.Assert(pNewImage != null);
            Debug.Assert(this.pAzulSprite != null);

            this.pImage = pNewImage;

            this.pAzulSprite.SwapTexture(this.pImage.GetAzulTexture());
            this.pAzulSprite.SwapTextureRect(this.pImage.GetAzulRect());
        }

        //Get Sprite Rectangle.
        public Azul.Rect GetScreenRect()
        {
            Debug.Assert(this.pScreenRect != null);
            return this.pScreenRect;
        }

        //Reset sprite details.
        private void ResetData()
        {
            //Set image to null.
            this.pImage = null;
      
            //Set name.
            this.name = Name.Uninitialized;

            //Set data with sprite data.
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.angle = 0.0f;
        }

        public void Clean()
        {
            this.ResetData();
        }

        //Set name.
        public void SetName(SpriteGame.Name newName)
        {
            this.name = newName;
        }

        //Return name of sprite.
        public Name ReturnName()
        {
            return (this.name);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        static private Azul.Color pTmpColor = new Azul.Color(1, 1, 1);

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        public float x;
        public float y;
        public float sx;
        public float sy;
        public float angle;

        private Name name;
        public Image pImage;
        private Azul.Sprite pAzulSprite;
        private readonly Azul.Rect pScreenRect;
        private readonly Azul.Color pAzulColor;


    }
}
