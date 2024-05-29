using System.Diagnostics;

namespace SpaceInvaders
{
    class BirdFactory
    {
        //-----------------------------------------------------------------------------------------
        //Data.
        //-----------------------------------------------------------------------------------------
        private readonly SpriteBatch pSpriteBatch;
        private readonly SpriteBatch pBoxSpriteBatch;

        //-----------------------------------------------------------------------------------------
        //Constructor.
        //-----------------------------------------------------------------------------------------
        public BirdFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName)
        {
            this.pSpriteBatch = SpriteBatchManager.Search(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pBoxSpriteBatch = SpriteBatchManager.Search(boxSpriteBatchName);
            Debug.Assert(this.pBoxSpriteBatch != null);
        }

        public GameObject CreateBirds(GameObject.Name name, BirdCategory.Type type, float posX = 0.0f, float posY = 0.0f)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case BirdCategory.Type.Green:
                    //pGameObj = new GreenBird(name, GameSprite.Name.GreenBird, posX, posY);
                    break;

                case BirdCategory.Type.Red:
                    //pGameObj = new RedBird(name, GameSprite.Name.RedBird, posX, posY);
                    break;

                case BirdCategory.Type.White:
                    //pGameObj = new WhiteBird(name, GameSprite.Name.WhiteBird, posX, posY);
                    break;

                case BirdCategory.Type.Yellow:
                    //pGameObj = new YellowBird(name, GameSprite.Name.YellowBird, posX, posY);
                    break;

                case BirdCategory.Type.Grid:
                    pGameObj = new BirdGrid(name, GameSprite.Name.NullObject, 0.0f, 0.0f);
                    break;

                case BirdCategory.Type.Column:
                    pGameObj = new BirdColumn(name, GameSprite.Name.NullObject, 0.0f, 0.0f);

                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add it to the GameObjectManager
            Debug.Assert(pGameObj != null);

            // Attached to Group
            pGameObj.ActivateGameSprite(this.pSpriteBatch);
            pGameObj.ActivateCollisionSprite(this.pBoxSpriteBatch);

            return (pGameObj);
        }
    }
}
