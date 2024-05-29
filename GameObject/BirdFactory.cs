using System.Diagnostics;

namespace SpaceInvaders
{
    public class BirdFactory
    {
        //-----------------------------------------------------------------------------------------
        //Data.
        //-----------------------------------------------------------------------------------------
        SpriteBatch pSpriteBatch;

        //-----------------------------------------------------------------------------------------
        //Constructor.
        //-----------------------------------------------------------------------------------------
        public BirdFactory(SpriteBatch.Name spriteBatchName)
        {
            this.pSpriteBatch = SpriteBatchManager.Search(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);
        }

        public GameObject CreateBirds(GameObject.Type type, float posX, float posY, float width, float height, float angle)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case GameObject.Type.Green:
                    pGameObj = new GreenBird(GameObject.Name.GreenBird, GameSprite.Name.GreenBird, posX, posY, width, height, angle);
                    break;

                case GameObject.Type.Red:
                    pGameObj = new RedBird(GameObject.Name.RedBird, GameSprite.Name.RedBird, posX, posY, width, height, angle);
                    break;

                case GameObject.Type.White:
                    pGameObj = new WhiteBird(GameObject.Name.WhiteBird, GameSprite.Name.WhiteBird, posX, posY, width, height, angle);
                    break;

                case GameObject.Type.Yellow:
                    pGameObj = new YellowBird(GameObject.Name.YellowBird, GameSprite.Name.YellowBird, posX, posY, width, height, angle);
                    break;

                default:
                    // something is wrong
                    Debug.Assert(false);
                    break;
            }

            // add it to the GameObjectManager
            Debug.Assert(pGameObj != null);
            GameObjectManager.Attach(pGameObj);

            // Attached to Group
            this.pSpriteBatch.Attach(pGameObj.pProxySprite);

            return (pGameObj);
        }
    }
}
