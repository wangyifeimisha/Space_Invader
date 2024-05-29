using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SceneStart : SceneState
    {
        
        public SceneStart()
        {
            this.Initialize();
        }

        public override void Handle()
        {
            
        }

        public override void Initialize()
        {
            this.poSpriteBatchMan = new SpriteBatchMan(3, 1);
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);

            SpriteBatchMan.Add(SpriteBatch.Name.Texts);

            TextureMan.Add(Texture.Name.Consolas36pt, "Consolas36pt.tga");
            TextureMan.Add(Texture.Name.SpaceInvaders, "SpaceInvaders.tga");

            ImageMan.Add(Image.Name.SquidA, Texture.Name.SpaceInvaders, 61, 3, 8, 8);
            ImageMan.Add(Image.Name.OctopusA, Texture.Name.SpaceInvaders, 3, 3, 12, 8);
            ImageMan.Add(Image.Name.CrabA, Texture.Name.SpaceInvaders, 33, 3, 11, 8);
            ImageMan.Add(Image.Name.Saucer, Texture.Name.SpaceInvaders, 99, 3, 16, 8);

            pOctopus = SpriteGameMan.Add(SpriteGame.Name.OctopusA, Image.Name.OctopusA, 230, 250, 49, 33, new Azul.Color(0.0f, 1.0f, 0.0f, 1.0f));

            pCrab = SpriteGameMan.Add(SpriteGame.Name.CrabA, Image.Name.CrabA, 230, 300, 45, 33);

            pSquid = SpriteGameMan.Add(SpriteGame.Name.SquidA, Image.Name.SquidA, 230, 350, 33, 33);

            pSaucer = SpriteGameMan.Add(SpriteGame.Name.Saucer, Image.Name.Saucer, 230, 400, 50, 35);
            


            GlyphMan.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<1>", Glyph.Name.Consolas36pt, 26, 740);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "HI-SCORE", Glyph.Name.Consolas36pt, 265, 740);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SCORE<2>", Glyph.Name.Consolas36pt, 503, 740);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 63, 700);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 302, 700);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "0000", Glyph.Name.Consolas36pt, 540, 700);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "PLAY", Glyph.Name.Consolas36pt, 295, 525);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "SPACE    INVADERS", Glyph.Name.Consolas36pt, 180, 600);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "*SCORE  ADVANCE  TABLE*", Glyph.Name.Consolas36pt, 130, 450);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=?  MYSTERY", Glyph.Name.Consolas36pt, 262, 400);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=30  POINTS", Glyph.Name.Consolas36pt, 262, 350);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=20  POINTS", Glyph.Name.Consolas36pt, 262, 300);
            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "=10  POINTS", Glyph.Name.Consolas36pt, 262, 250);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "(Press Space to Continue)", Glyph.Name.Consolas36pt, 127, 150);

            FontMan.Add(Font.Name.TestMessage, SpriteBatch.Name.Texts, "CREDIT 00", Glyph.Name.Consolas36pt, 680, 100);
        }

        public override void Update(float systemTime)
        {
            
            this.pOctopus.Update();
            this.pSquid.Update();
            this.pCrab.Update();
            this.pSaucer.Update();


            // walk through all objects and push to flyweight
            GameObjectMan.Update(); 
        }

        public override void Draw()
        {
            this.pOctopus.Render();
            this.pSquid.Render();
            this.pCrab.Render();
            this.pSaucer.Render();
            // draw all objects
            SpriteBatchMan.Draw();
        }

        public override void Entering()
        {
            // update SpriteBatchMan()
            SpriteBatchMan.SetActive(this.poSpriteBatchMan);
        }
        public override void Leaving()
        {
            // update SpriteBatchMan()
            this.TimeAtPause = TimerMan.GetCurrTime();
        }

        // ---------------------------------------------------
        // Data
        // ---------------------------------------------------
        public SpriteBatchMan poSpriteBatchMan;
        SpriteGame pOctopus, pCrab, pSquid, pSaucer;
        // public bool pauseDemoUpdate = true;
    }
}
