using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SaucerSpawnEvent : Command
    {
        
        public SaucerSpawnEvent()
        {
            pSB_Box = SpriteBatchMan.Search(SpriteBatch.Name.Boxes);
            pSB_Saucer = SpriteBatchMan.Search(SpriteBatch.Name.Saucer);
            pRand = new Random();
        }

        override public void Execute(float deltaTime)
        {
            Saucer pSauc = new Saucer(GameObject.Name.Saucer, SpriteGame.Name.Saucer, 600.0f, 700.0f);
            pSauc.ActivateCollisionSprite(pSB_Box);
            pSauc.ActivateGameSprite(pSB_Saucer);

            GameObject pSaucerRoot = GameObjectMan.Search(GameObject.Name.SaucerRoot);
            Debug.Assert(pSaucerRoot != null);
            Debug.Assert(pSauc != null);
            pSaucerRoot.Add(pSauc);

            SoundMan.PlaySound(SoundSource.Name.Saucer, true, false, false);

            SaucerBombSpawnEvent pEvent = new SaucerBombSpawnEvent(pSauc);
            TimerMan.Add(TimeEvent.Name.SaucerBomb, pEvent, pRand.Next(1, 4));

            // Add itself back to timer
            deltaTime = pRand.Next(15, 25);
            TimerMan.Add(TimeEvent.Name.SaucerSpawn, this, deltaTime);
        }

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        SpriteBatch pSB_Box;
        SpriteBatch pSB_Saucer;
        Random pRand;
    }
}
