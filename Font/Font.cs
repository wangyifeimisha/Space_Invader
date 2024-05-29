using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class Font_DLink : DLink
    {
    }

    public class Font : Font_DLink
    {
        public enum Name
        {
            TestMessage,
            TestOneOff,
            Life,
            Score1,
            HighScore,
            Score2,

            NullObject,
            Uninitialized
        };

        public Font()
            : base()
        {
            this.name = Name.Uninitialized;
            this.pSpriteFont = new SpriteFont();
        }

        ~Font()
        {

        }

        public void UpdateMessage(String pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(this.pSpriteFont != null);
            this.pSpriteFont.UpdateMessage(pMessage);
        }

        public void Set(Font.Name name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            this.pSpriteFont.Set(name, pMessage, glyphName, xStart, yStart);
            this.pSpriteFont.SetColor(1.0f, 1.0f, 1.0f);
        }

        public void SetColor(float red, float green, float blue)
        {
            this.pSpriteFont.SetColor(red, green, blue);
        }

        public void Clean()
        {
            this.name = Name.Uninitialized;
            this.pSpriteFont.Set(Font.Name.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        public void GetFont()
        {
        }   
        
        public Name ReturnName()
        {
            return (this.name);
        }

        // ----------------------------------------------------------------
        // Data 
        // ----------------------------------------------------------------
        public Name name;
        public SpriteFont pSpriteFont;
        static private String pNullString = "null";
        
    }
}
