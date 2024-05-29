﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class WallCategory : Leaf
    {
        public enum Type
        {
            WallGroup,
            Right,
            Left,
            Bottom,
            Top,
            Unitialized
        }

        protected WallCategory(GameObject.Name name, SpriteGame.Name spriteName, WallCategory.Type type)
            : base(name, spriteName)
        {
            this.type = type;
        }

        ~WallCategory()
        {
        }

        public WallCategory.Type GetCategoryType()
        {
            return this.type;
        }

        // Data: ---------------
        // this is just a placeholder, who knows what data will be stored here
        protected WallCategory.Type type;

    }
}
