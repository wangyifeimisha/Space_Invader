using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SoundSource_Link : DLink
    {
    }
    public class SoundSource : SoundSource_Link
    {
        public IrrKlang.ISoundSource sndSource;
        public IrrKlang.ISound sndSettings = null;

        SoundSource.Name name;

        public enum Name
        {
            Theme,
            AlienMarch1,
            AlienMarch2,
            AlienMarch3,
            AlienMarch4,
            Missile,
            ExplosionPlayer,
            InvaderKilled,
            Saucer,
            SaucerKilled,

            Uninitialized
        }

        public SoundSource()
        : base()
        {
            this.sndSource = null;
            this.sndSettings = null;
            this.name = SoundSource.Name.Uninitialized;
        }

        public void SetSoundSource(SoundSource.Name newName, IrrKlang.ISoundSource pSoundSource)
        {
            this.name = newName;
            this.sndSource = pSoundSource;
        }

        public void GetSoundDetails()
        {
            //Print name of sound.
            Debug.WriteLine("\nName: {0}\n", this.ReturnName());

            //Check if previous sound exist, if not print null.
            if (this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else    //else print the previous sound name.
            {
                SoundSource pTmp = (SoundSource)this.pPrev;
                Debug.WriteLine("Prev: {0}", pTmp.ReturnName());
            }

            //Check if next sound exist, if not print null.
            if (this.pNext == null)
            {
                Debug.WriteLine("Next: null\n");
            }
            else    //else print the next sound name.
            {
                SoundSource pTmp = (SoundSource)this.pNext;
                Debug.WriteLine("Next: {0}\n", pTmp.ReturnName());
            }
        }

        public void Clean()
        {
            this.sndSource = null;
            this.name = SoundSource.Name.Uninitialized;
        }

        //Set a name.
        public void SetName(SoundSource.Name newName)
        {
            this.name = newName;
        }

        //Return the sound name.
        public SoundSource.Name ReturnName()
        {
            return (this.name);
        }
    }
}
