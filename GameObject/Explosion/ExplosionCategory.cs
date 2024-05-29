using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ExplosionCategory : Leaf
    {
        public enum Type
        {
            Explosion,
            ExplosionRoot,
            Unitialized
        }

        public ExplosionCategory(GameObject.Name name, SpriteGame.Name spriteName)
            : base(name, spriteName)
        {

        }

        ~ExplosionCategory()
        {

        }
    }
}
