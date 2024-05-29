﻿using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BombCategory : Leaf
    {
        public enum Type
        {
            Bomb,
            BombRoot,
            Unitialized
        }

        protected BombCategory(GameObject.Name name, SpriteGame.Name spriteName, BombCategory.Type bombType)
            : base(name, spriteName)
        {
            this.type = bombType;
        }

        // Data: ---------------
        ~BombCategory()
        {
        }

        // this is just a placeholder, who knows what data will be stored here
        protected BombCategory.Type type;

    }
}
