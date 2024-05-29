using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class SoundMan_Link : ManBase
    {
        public SoundSource_Link poActive;
        public SoundSource_Link poReserve;
    }

    public class SoundMan : SoundMan_Link
    {
        //---------------------------------------------------------------------------------------------------------
        // Private Constructor
        //---------------------------------------------------------------------------------------------------------
        private SoundMan(int initialReserveSize = 2, int newGrowthSize = 2)
        : base()     //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.sndEngine = new IrrKlang.ISoundEngine();
            this.pSoundCompare = new SoundSource();       //Initialize compare sound with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        // Singleton Static Method
        //---------------------------------------------------------------------------------------------------------
        //Create new sound manager.
        public static void NewSound(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if sound manager is not present
            if (_pInstance == null)
            {
                //If not, create a new sound manager.
                _pInstance = new SoundMan(initialReserveSize, newGrowthSize);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        //Static Methods.
        //---------------------------------------------------------------------------------------------------------
        //Add sound to active list in manager.
        public static SoundSource Add(SoundSource.Name newName, string pSoundName)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //add sound to active list.
            SoundSource pTmp = (SoundSource)pMan.BaseAdd();

            //Verify sound is added.
            Debug.Assert(pTmp != null);

            //Set new sound data.
            pTmp.SetSoundSource(newName, pMan.sndEngine.AddSoundSourceFromFile(pSoundName));

            //Print sound added.
            Debug.WriteLine("\n\n***Added Sound Source:\"" + newName + "\" to Active List***");

            //Return added sound.
            return (pTmp);
        }

        //Play the sound source.
        public static void PlaySound(SoundSource.Name newName, bool loop, bool startPause, bool effects)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            SoundSource pTmp = Search(newName);
            Debug.Assert(pTmp != null);
       
            Debug.Assert(pTmp.sndSource != null);
            pTmp.sndSettings = pMan.sndEngine.Play2D(pTmp.sndSource, loop, startPause, effects);
        }

        //Stop sound.
        public static void StopSound(SoundSource.Name newName)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            SoundSource pTmp = Search(newName);
            Debug.Assert(pTmp != null);

            Debug.Assert(pTmp.sndSettings != null);
            pTmp.sndSettings.Stop();
        }

        //Set volume.
        public static void SetVolume(SoundSource.Name newName, float value)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            SoundSource pTmp = Search(newName);
            Debug.Assert(pTmp != null);

            Debug.Assert(pTmp.sndSettings != null);
            pTmp.sndSettings.Volume = value;
        }

        //Pause sound.
        public static void PauseSound(SoundSource.Name newName, bool value)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            SoundSource pTmp = Search(newName);
            Debug.Assert(pTmp != null);

            Debug.Assert(pTmp.sndSettings != null);
            pTmp.sndSettings.Paused = value;
        }
        //Stop all sounds.
        public static void PauseAllSound(bool value)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            pMan.sndEngine.SetAllSoundsPaused(value);
        }

        //Is currently playing.
        public static bool IsPlaying(SoundSource.Name newName)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            SoundSource pTmp = Search(newName);
            Debug.Assert(pTmp != null);

            Debug.Assert(pTmp.sndSource != null);

            return (pMan.sndEngine.IsCurrentlyPlaying(pTmp.sndSource.Name));
        }

        //Search from active list.
        public static SoundSource Search(SoundSource.Name newName)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //set comparing sound with given name.
            pMan.pSoundCompare.SetName(newName);

            //Search active list with comparing sound.
            SoundSource pTmp = (SoundSource)pMan.BaseSearch(pMan.pSoundCompare);

            //Return.
            return (pTmp);
        }

        public static void Delete(SoundSource pNewData)
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Verify sound to delete is present.
            Debug.Assert(pNewData != null);

            //Delete sound from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print sound manager.
            pMan.BasePrint("SOUND");
        }

        public static void Destroy()
        {
            //Get sound manager instance.
            SoundMan pMan = SoundMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print all sound data.
            Print();

            //Delete the sound manager.
            _pInstance = null;
        }

        //--------------------------------------------------------------------------------
        //Derived Functions.
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new sound.
            DLink pTmp = new SoundSource();

            //Verify new sound is created.
            Debug.Assert(pTmp != null);

            //Return new sound.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            // This is used in baseSearch() 
            Debug.Assert(pCompareWith != null);
            Debug.Assert(pCompareTo != null);

            //Convert links to sound to compare.
            SoundSource pTmp1 = (SoundSource)pCompareWith;
            SoundSource pTmp2 = (SoundSource)pCompareTo;

            //Compare with names.
            if (pTmp1.ReturnName() == pTmp2.ReturnName())
            {
                return true;    //If name is equal.
            }

            return false;       //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            Debug.Assert(pResetLink != null);

            //Convert link to sound.
            SoundSource pTmp = (SoundSource)pResetLink;

            //Reset sound.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to sound.
            SoundSource pTmp = (SoundSource)pPrintLink;

            //Print sound details.
            pTmp.GetSoundDetails();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to sound.
            SoundSource pTmp = (SoundSource)pPrintLink;

            //Print sound name.
            return (pTmp.ReturnName());
        }
        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static SoundMan GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }

        //---------------------------------------------------------------------------------------------------------
        //Static Data
        //---------------------------------------------------------------------------------------------------------
        private static SoundMan _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private readonly SoundSource pSoundCompare;
        IrrKlang.ISoundEngine sndEngine = null;
    }
}
