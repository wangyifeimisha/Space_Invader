using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class AlienExplosionEvent : Command
    {
        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private readonly GameSprite pSprite;
        private SLink pCurrImage;
        private SLink pFirstImage;

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public AlienExplosionEvent(GameSprite.Name spriteName)
        {
            //Get the game sprite.
            this.pSprite = GameSpriteManager.Search(spriteName);

            //Verify game sprite is added.
            Debug.Assert(this.pSprite != null);

            // initialize references
            this.pCurrImage = null;

            // list
            this.pFirstImage = null;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods.
        //---------------------------------------------------------------------------------------------------------


        public void Attach(Image.Name imageName)
        {
            //Search for image.
            Image pImage = ImageManager.Search(imageName);

            //Verify image.
            Debug.Assert(pImage != null);

            // Create a new holder
            ImageHolder pImageHolder = new ImageHolder(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            SLink.AddToFront(ref this.pFirstImage, pImageHolder);

            // Set the first one to this image
            this.pCurrImage = pImageHolder;
        }

        public override void Execute(float deltaTime)
        {
            // advance to next image 
            ImageHolder pImageHolder = (ImageHolder)this.pCurrImage.pSNext;

            // if at end of list, set to first
            if (pImageHolder == null)
            {
                pImageHolder = (ImageHolder)pFirstImage;
            }

            // squirrel away for next timer event
            this.pCurrImage = pImageHolder;

            // change image
            this.pSprite.SwapImage(pImageHolder.pImage);            

            // Add itself back to timer
            TimerManager.Add(TimeEvent.Name.AlienExplosion, this, deltaTime);
        }
    }
}
