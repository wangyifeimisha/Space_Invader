using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        //-----------------------------------------------------------------------------------------
        //Constructor.
        //-----------------------------------------------------------------------------------------
        public AlienFactory(SpriteBatch.Name spriteBatchName, SpriteBatch.Name boxSpriteBatchName)
        {
            this.pSpriteBatch = SpriteBatchMan.Search(spriteBatchName);
            Debug.Assert(this.pSpriteBatch != null);

            this.pBoxSpriteBatch = SpriteBatchMan.Search(boxSpriteBatchName);
            Debug.Assert(this.pBoxSpriteBatch != null);
        }

        public GameObject CreateAliens(GameObject.Name name, AlienCategory.Type type, float posX = 0.0f, float posY = 0.0f, ScenePlay scene = null)
        {
            GameObject pGameObj = null;

            switch (type)
            {
                case AlienCategory.Type.Squid:
                    pGameObj = new Squid(name, SpriteGame.Name.SquidA, posX, posY);
                    break;

                case AlienCategory.Type.Crab:
                    pGameObj = new Crab(name, SpriteGame.Name.CrabA, posX, posY);
                    break;

                case AlienCategory.Type.Octopus:
                    pGameObj = new Octopus(name, SpriteGame.Name.OctopusA, posX, posY);
                    break;

                case AlienCategory.Type.Saucer:
                    pGameObj = new Saucer(name, SpriteGame.Name.Saucer, posX, posY);
                    break;

                case AlienCategory.Type.Grid:
                    pGameObj = new AlienGrid(name, SpriteGame.Name.NullObject, 0.0f, 0.0f, scene);
                    break;

                case AlienCategory.Type.Column:
                    pGameObj = new AlienColumn(name, SpriteGame.Name.NullObject, 0.0f, 0.0f);

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

        //-----------------------------------------------------------------------------------------
        //Data.
        //-----------------------------------------------------------------------------------------
        private readonly SpriteBatch pSpriteBatch;
        private readonly SpriteBatch pBoxSpriteBatch;
    }
}
