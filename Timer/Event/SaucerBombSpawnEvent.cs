using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SaucerBombSpawnEvent : Command
    {
        public SaucerBombSpawnEvent(Saucer sauc)
        {
            this.pSauc = sauc;
            pSB_Bombs = SpriteBatchMan.Search(SpriteBatch.Name.Bombs);
            pSB_Box = SpriteBatchMan.Search(SpriteBatch.Name.Boxes);
        }

        override public void Execute(float deltaTime)
        {
            if (this.pSauc != null)
            {
                Bomb pBomb = new Bomb(GameObject.Name.Bomb, SpriteGame.Name.PlungerShotA, new FallDagger(), this.pSauc.x, this.pSauc.y - 30.0f);
                pBomb.ActivateCollisionSprite(pSB_Box);
                pBomb.ActivateGameSprite(pSB_Bombs);

                GameObject pBombRoot = GameObjectMan.Search(GameObject.Name.BombRoot);
                Debug.Assert(pBombRoot != null);
                Debug.Assert(pBomb != null);
                pBombRoot.Add(pBomb);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        Saucer pSauc;
        SpriteBatch pSB_Bombs;
        SpriteBatch pSB_Box;
    }
}
