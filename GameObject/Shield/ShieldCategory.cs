﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShieldCategory : Leaf
    {
        public enum Type
        {
            Root,
            Column,
            Brick,
            Grid,

            LeftTop0,
            LeftTop1,
            LeftBottom,
            RightTop0,
            RightTop1,
            RightBottom,

            Unitialized
        }
        protected ShieldCategory(GameObject.Name name, SpriteGame.Name spriteName, ShieldCategory.Type shieldType)
            : base(name, spriteName)
        {
            this.type = shieldType;
        }
        
        ~ShieldCategory()
        {
        }
        public ShieldCategory.Type GetCategoryType()
        {
            return this.type;
        }

        // --------------------------------------------------------------------
        // Data
        // --------------------------------------------------------------------

        // this is just a placeholder, who knows what data will be stored here
        protected ShieldCategory.Type type;

    }
}
