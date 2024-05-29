using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class MissileCategory : Leaf
    {
        public enum Type
        {
            Missile,
            MissileGroup,
            Unitialized
        }

        public MissileCategory(GameObject.Name name, SpriteGame.Name spriteName)
            : base(name, spriteName)
        {

        }

        ~MissileCategory()
        {

        }
    }
}
