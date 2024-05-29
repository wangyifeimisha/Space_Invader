using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerExplosionEvent : Command
    {
        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public PlayerExplosionEvent(SpriteGame.Name spriteName)
        {
            //Get the game sprite.
            this.pSprite = SpriteGameMan.Search(spriteName);

            //Verify game sprite is added.
            Debug.Assert(this.pSprite != null);

            // initialize references
            this.pCurrImage = null;

            // list
            this.pFirstImage = null;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods
        //---------------------------------------------------------------------------------------------------------

        public void Attach(Image.Name imageName)
        {
            //Search for image.
            Image pImage = ImageMan.Search(imageName);

            //Verify image.
            Debug.Assert(pImage != null);

            // Create a new holder
            ImageNode pImageNode = new ImageNode(pImage);
            Debug.Assert(pImageNode != null);

            // Attach it to the Animation Sprite ( Push to front )
            SLink.AddToFront(ref this.pFirstImage, pImageNode);

            // Set the first one to this image
            this.pCurrImage = pImageNode;
        }

        public override void Execute(float deltaTime)
        {
            // advance to next image 
            ImageNode pImageNode = (ImageNode)this.pCurrImage.pSNext;

            // if at end of list, set to first
            if (pImageNode == null)
            {
                pImageNode = (ImageNode)pFirstImage;
            }

            // squirrel away for next timer event
            this.pCurrImage = pImageNode;

            // change image
            this.pSprite.SwapImage(pImageNode.pImage);            

            // Add itself back to timer
            TimerMan.Add(TimeEvent.Name.AlienExplosion, this, deltaTime);
        }

        //---------------------------------------------------------------------------------------------------------
        //Data
        //---------------------------------------------------------------------------------------------------------
        private readonly SpriteGame pSprite;
        private SLink pCurrImage;
        private SLink pFirstImage;
    }
}
