
namespace SpaceInvaders
{
    abstract public class BirdCategory : Leaf
    {
        public enum Type
        {
            // temporary location --> move this
            Red,
            Yellow,
            Green,
            White,

            Column,
            Grid,

            Uninitialized
        }

        public BirdCategory(GameObject.Name name, GameSprite.Name spriteName)
            : base(name, spriteName)
        {

        }
    }
}