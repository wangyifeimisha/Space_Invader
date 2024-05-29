using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class Command
    {
        //Derived classes must implement.
        abstract public void Execute(float deltaTime);
    }
}
