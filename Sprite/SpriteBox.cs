using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteBox_Base : SpriteBase
    {

    }
    public class SpriteBox : SpriteBox_Base
    {
        //---------------------------------------------------------------------------------------------------------
        // Enum
        //---------------------------------------------------------------------------------------------------------
        public enum Name
        {
            Box,
            Box1,
            Box2,

            Uninitialized
        }

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public SpriteBox()
        :   base()
        {
            this.name = SpriteBox.Name.Uninitialized;

            Debug.Assert(SpriteBox.pTmpRect != null);
            SpriteBox.pTmpRect.Set(0, 0, 1, 1);
            Debug.Assert(SpriteBox.pTmpColor != null);
            SpriteBox.pTmpColor.Set(1, 1, 1);

            // Here is the actual new
            this.pSpriteBox = new Azul.SpriteBox(pTmpRect, pTmpColor);
            Debug.Assert(this.pSpriteBox != null);

            // Here is the actual new
            this.pLineColor = new Azul.Color(1, 1, 1);
            Debug.Assert(this.pLineColor != null);

            this.x = pSpriteBox.x;
            this.y = pSpriteBox.y;
            this.sx = pSpriteBox.sx;
            this.sy = pSpriteBox.sy;
            this.angle = pSpriteBox.angle;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------
        public void SetBox(SpriteBox.Name name, float x, float y, float width, float height, Azul.Color newColor)
        {
            Debug.Assert(this.pSpriteBox != null);
            Debug.Assert(this.pLineColor != null);

            //Assign rectangle.
            Debug.Assert(pTmpRect != null);
            SpriteBox.pTmpRect.Set(x, y, width, height);

            //Assign name.
            this.name = name;

            //Check if given color is null.
            if (newColor == null)
            {
                this.pLineColor.Set(1, 1, 1);
            }
            else
            {
                //else assign given color.
                this.pLineColor.Set(newColor);
            }

            //Assign sprite values.
            this.pSpriteBox.Swap(pTmpRect, this.pLineColor);

            //Assign data with sprite data.
            this.x = pSpriteBox.x;
            this.y = pSpriteBox.y;
            this.sx = pSpriteBox.sx;
            this.sy = pSpriteBox.sy;
            this.angle = pSpriteBox.angle;
        }

        public void SetBox(SpriteBox.Name boxName, float x, float y, float width, float height)
        {
            Debug.Assert(this.pSpriteBox != null);
            Debug.Assert(this.pLineColor != null);

            Debug.Assert(pTmpRect != null);
            SpriteBox.pTmpRect.Set(x, y, width, height);

            this.name = boxName;

            this.pSpriteBox.Swap(pTmpRect, this.pLineColor);
            Debug.Assert(this.pSpriteBox != null);

            this.x = pSpriteBox.x;
            this.y = pSpriteBox.y;
            this.sx = pSpriteBox.sx;
            this.sy = pSpriteBox.sy;
            this.angle = pSpriteBox.angle;
        }

        //Swap Color of box sprite.
        public void SwapColor(Azul.Color pColor)
        {
            Debug.Assert(pColor != null);
            this.pSpriteBox.SwapColor(pColor);
        }

        //Set rectangle.
        public void SetScreenRect(float x, float y, float width, float height)
        {
            this.SetBox(this.name, x, y, width, height);
        }

        //Set line color.
        public void SetLineColor(float red, float green, float blue, float alpha = 1.0f)
        {
            Debug.Assert(this.pLineColor != null);
            this.pLineColor.Set(red, green, blue, alpha);
        }

        //Used to print box sprite.
        public void GetSpriteBox()
        {
            //Print name of box sprite.
            Debug.WriteLine("\nName: {0}\n", this.ReturnName());

            //Check if previous box sprite exist, if not print null.
            if (this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else //else print the previous box sprite name.
            {
                SpriteBox pTmp = (SpriteBox)this.pPrev;
                Debug.WriteLine("Prev: {0}", pTmp.ReturnName());
            }

            //Check if next box sprite exist, if not print null.
            if (this.pNext == null)
            {
                Debug.WriteLine("Next: null\n");
            }
            else    //else print the next box sprite name.
            {
                SpriteBox pTmp = (SpriteBox)this.pNext;
                Debug.WriteLine("Next: {0}\n", pTmp.ReturnName());
            }
        }

        //Update.
        public override void Update()
        {
            this.pSpriteBox.x = this.x;
            this.pSpriteBox.y = this.y;
            this.pSpriteBox.sx = this.sx;
            this.pSpriteBox.sy = this.sy;
            this.pSpriteBox.angle = this.angle;

            this.pSpriteBox.Update();
        }

        //Render.
        public override void Render()
        {
            this.pSpriteBox.Render();
        }

        //Return name of box sprite.
        public Name ReturnName()
        {
            return (this.name);
        }

        //Set name.
        public void SetName(SpriteBox.Name newName)
        {
            this.name = newName;
        }

        //Reset box sprite details.
        private void ResetData()
        {
            //Set name.
            this.name = SpriteBox.Name.Uninitialized;

            this.pLineColor.Set(1, 1, 1);

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

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        static private Azul.Rect pTmpRect = new Azul.Rect();
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
        private Azul.SpriteBox pSpriteBox;
        private readonly Azul.Color pLineColor;
    }
}
