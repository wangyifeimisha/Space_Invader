using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class InputObserver : DLink
    {
        public enum Name
        {
            MoveLeftObserver,
            MoveRightObserver,
            ShootObserver,
            Uninitialized
        }

        // define this in concrete
        abstract public void Notify();

        public InputSubject pSubject;
    }
}
