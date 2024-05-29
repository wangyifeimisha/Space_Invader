using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Leaf : GameObject
    {
        public Leaf(GameObject.Name gameName, SpriteGame.Name spriteName)
               : base(gameName, spriteName)
        {
            this.holder = Container.LEAF;
        }

        override public void Add(Component c)
        {
            Debug.Assert(false);
        }

        override public void Delete(Component c)
        {
            Debug.Assert(false);
        }

        override public void Dump()
        {
            //Debug.WriteLine(" GameObject Name: {0} ({1})", this.ReturnName(), this.GetHashCode());
        }

        override public Component GetFirstChild()
        {
            Debug.Assert(false);
            return null;
        }

        override public void PrintNode()
        {
            Debug.WriteLine(" GameObject Name: {0} ({1}) parent:{2}", this.ReturnName(), this.GetHashCode(), Iterator.GetParent(this).GetHashCode());
        }

    }
}
