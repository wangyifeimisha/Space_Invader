using System.Diagnostics;

namespace SpaceInvaders
{
    public class ColObject
    {
        // -------------------------------------------------------
        // Constructor
        // -------------------------------------------------------
        public ColObject(SpriteProxy pSpriteProxy)
        {
            Debug.Assert(pSpriteProxy != null);

            // Create Collision Rect
            // Use the reference sprite to set size and shape
            // need to refactor if you want it different
            SpriteGame pSprite = pSpriteProxy.pSprite;
            Debug.Assert(pSprite != null);

            // Origin is in the UPPER RIGHT 
            this.pColRect = new ColRect(pSprite.GetScreenRect());
            Debug.Assert(this.pColRect != null);

            // Create the sprite
            this.pColSprite = SpriteBoxMan.Add(SpriteBox.Name.Box, this.pColRect.x, this.pColRect.y, this.pColRect.width, this.pColRect.height);
            Debug.Assert(this.pColSprite != null);
            this.pColSprite.SetLineColor(1.0f, 0.0f, 0.0f);
        }

        public void UpdatePos(float x, float y)
        {
            this.pColRect.x = x;
            this.pColRect.y = y;

            this.pColSprite.x = this.pColRect.x;
            this.pColSprite.y = this.pColRect.y;

            this.pColSprite.SetScreenRect(this.pColRect.x, this.pColRect.y, this.pColRect.width, this.pColRect.height);
            this.pColSprite.Update();
        }

        // -------------------------------------------------------
        // Data
        // -------------------------------------------------------
        public SpriteBox pColSprite;
        public ColRect pColRect;

    }
}