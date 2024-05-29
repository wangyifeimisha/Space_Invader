using System.Diagnostics;

namespace SpaceInvaders
{
    class AnimationCmd : Command
    {
        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public AnimationCmd(SpriteGame.Name spriteName)
        {
            //Get the game sprite.
            this.pSprite = SpriteGameMan.Search(spriteName);

            //Verify game sprite is added.
            Debug.Assert(this.pSprite != null);

            // initialize references
            this.pCurrImage = null;

            // list
            this.pFirstImage = null;
            this.index = 0;
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
            ImageNode pImageHolder = new ImageNode(pImage);
            Debug.Assert(pImageHolder != null);

            // Attach it to the Animation Sprite ( Push to front )
            SLink.AddToFront(ref this.pFirstImage, pImageHolder);

            // Set the first one to this image
            this.pCurrImage = pImageHolder;
        }

        /*public void PauseAnim(bool val)
        {
            this.timePause = val;
        }*/
        public override void Execute(float deltaTime)
        {
            if(index == 0)
            {
                SoundMan.PlaySound(SoundSource.Name.AlienMarch1, false, false, false);
            }
            else if(index == 1)
            {
                SoundMan.PlaySound(SoundSource.Name.AlienMarch2, false, false, false);
            }
            else if (index == 2)
            {
                SoundMan.PlaySound(SoundSource.Name.AlienMarch3, false, false, false);
            }
            else if (index == 3)
            {
                SoundMan.PlaySound(SoundSource.Name.AlienMarch4, false, false, false);
                index = 0;
            }
            index++;

            // Move the grid
            AlienGrid pGrid = (AlienGrid)GameObjectMan.Search(GameObject.Name.AlienGrid);
            pGrid.MoveGrid();

            // advance to next image 
            ImageNode pImageHolder = (ImageNode)this.pCurrImage.pSNext;

            // if at end of list, set to first
            if (pImageHolder == null)
            {
                pImageHolder = (ImageNode)pFirstImage;
            }

            // squirrel away for next timer event
            this.pCurrImage = pImageHolder;

            // change image
            this.pSprite.SwapImage(pImageHolder.pImage);

            //SPEED CONTROL>
            deltaTime = ((pGrid.GetAlienCount() + (deltaTime * 25.0f)) / 150.0f);
            if (deltaTime < 0.05f)
            {
                deltaTime = 0.05f;
            }

            // Add itself back to timer
            TimerMan.Add(TimeEvent.Name.SpriteAnimation, this, deltaTime);
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private readonly SpriteGame pSprite;
        private SLink pCurrImage;
        private SLink pFirstImage;
        private int index = 0;
    }
}

