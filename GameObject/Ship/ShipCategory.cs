using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShipCategory : Leaf
    {
        public enum Type
        {
            Ship,
            ShipRoot,
            Unitialized
        }

        protected ShipCategory(GameObject.Name name, SpriteGame.Name spriteName, ShipCategory.Type shipType)
            : base(name, spriteName)
        {
            this.type = shipType;
        }

        ~ShipCategory()
        {
            
        }

        // Data
        // this is just a placeholder, who knows what data will be stored here
        protected ShipCategory.Type type;

    }
}
