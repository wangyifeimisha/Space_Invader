using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SpriteBase : DLink
    {
        public SpriteBase()
        :   base()
        {
            this.pBackSpriteNode = null;
        }

        public SpriteNode GetSpriteNode()
        {
            Debug.Assert(this.pBackSpriteNode != null);
            return this.pBackSpriteNode;
        }
        public void SetSpriteNode(SpriteNode pSpriteBatchNode)
        {
            Debug.Assert(pSpriteBatchNode != null);
            this.pBackSpriteNode = pSpriteBatchNode;
        }

        //Abstract functions that derived class should implement.
        abstract public void Update();
        abstract public void Render();

        // Data:
        private SpriteNode pBackSpriteNode;
    }
}
