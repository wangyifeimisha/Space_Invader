using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SpriteNode_Link : DLink
    {

    }
    public class SpriteNode : SpriteNode_Link
    {
        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        private SpriteBase pSpriteBase;
        private SpriteNodeMan pBackSpriteNodeMan;

        //---------------------------------------------------------------------------------------------------------
        // Constructor
        //---------------------------------------------------------------------------------------------------------
        public SpriteNode()
        : base()
        {
            this.pSpriteBase = null;
            this.pBackSpriteNodeMan = null;
        }

        //---------------------------------------------------------------------------------------------------------
        // Methods.
        //---------------------------------------------------------------------------------------------------------
        //Used for Game Sprite.
        public void SetSpriteNode(SpriteBase pNode, SpriteNodeMan _pSpriteNodeMan)
        {
            Debug.Assert(pNode != null);
            this.pSpriteBase = pNode;

            // Set the back pointer
            // Allows easier deletion in the future
            Debug.Assert(pSpriteBase != null);
            this.pSpriteBase.SetSpriteNode(this);

            Debug.Assert(_pSpriteNodeMan != null);
            this.pBackSpriteNodeMan = _pSpriteNodeMan;
        }

        //Get Base sprite.
        public SpriteBase GetSpriteBase()
        {
            return this.pSpriteBase;
        }

        public SpriteNodeMan GetSBDataMan()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan;
        }

        public SpriteBatch GetSpriteBatch()
        {
            Debug.Assert(this.pBackSpriteNodeMan != null);
            return this.pBackSpriteNodeMan.GetSpriteBatch();
        }

        //Clean.
        public void Clean()
        {
            this.pSpriteBase = null;
        }
    }
}
