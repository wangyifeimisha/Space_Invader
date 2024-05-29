using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Composite : GameObject
    {
        // --------------------------------------
        // Constructor
        // --------------------------------------

        public Composite(GameObject.Name gameName, SpriteGame.Name spriteName)
        :   base(gameName, spriteName)
        {
            this.holder = Container.COMPOSITE;
            this.pHead = null;
            this.pLast = null;
        }

        // --------------------------------------
        // Methods
        // --------------------------------------
        override public void Add(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.AddAtEnd(ref this.pHead, ref this.pLast, pComponent);
            pComponent.pParent = this;
        }

        override public void Delete(Component pComponent)
        {
            Debug.Assert(pComponent != null);
            DLink.DeleteByNode(ref this.pHead, ref this.pLast, pComponent);
        }

        override public Component GetFirstChild()
        {
            DLink pNode = this.pHead;

            return (Component)pNode;
        }

        override public void PrintNode()
        {
            if (Iterator.GetParent(this) != null)
            {
                Debug.WriteLine(" GameObject Name:({0}) parent:{1} <---- Composite", this.GetHashCode(), Iterator.GetParent(this).GetHashCode());
            }
            else
            {
                Debug.WriteLine(" GameObject Name:({0}) parent:null <---- Composite", this.GetHashCode());
            }
        }


        public override void Dump()
        {
            DLink pTmp = this.pHead;

            while (pTmp != null)
            {
                Component pComponent = (Component)pTmp;
                pComponent.Dump();

                pTmp = pTmp.pNext;
            }

        }

        // --------------------------------------
        // Data.
        // --------------------------------------
        public DLink pHead;
        public DLink pLast;
    }
}
