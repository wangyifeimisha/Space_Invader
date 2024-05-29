using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class SceneState
    {
        public SceneState()
        {
            this.TimeAtPause = TimerMan.GetCurrTime();
        }
        public abstract void Handle();
        public abstract void Initialize();
        public abstract void Update(float systemTime);
        public abstract void Draw();
        public abstract void Entering();
        public abstract void Leaving();

        // Data
        public float TimeAtPause;
    }
}
