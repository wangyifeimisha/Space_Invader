
namespace SpaceInvaders
{
    public abstract class Component : ColVisitor
    {
        // --------------------------------------
        // Enum
        // --------------------------------------
        public enum Container
        {
            LEAF,
            COMPOSITE,
            Unknown
        }
        
        public abstract void Add(Component c);
        public abstract void Delete(Component c);
        public abstract void Dump();
        public abstract Component GetFirstChild();
        public abstract void PrintNode();

        // --------------------------------------
        // Data
        // --------------------------------------
        public Component pParent = null;
        public Component pReverse = null;
        public Container holder = Container.Unknown;
    }
}
