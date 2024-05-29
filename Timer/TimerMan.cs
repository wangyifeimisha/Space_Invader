using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TimerMan_MLink : ManBase
    {
        public TimeEvent_Link poActive;
        public TimeEvent_Link poReserve;
    }

    public class TimerMan : TimerMan_MLink
    {
        //---------------------------------------------------------------------------------------------------------
        // Private Constructor
        //---------------------------------------------------------------------------------------------------------
        private TimerMan(int initialReserveSize = 2, int newGrowthSize = 2)
        : base()       //Delegate.
        {
            // At this point ImageMan is created, now initialize the reserve
            this.BaseInitialize(initialReserveSize, newGrowthSize);

            this.pDataCompare = new TimeEvent();        //Initialize compare time event with default data.
        }

        //---------------------------------------------------------------------------------------------------------
        // Singleton Static Method
        //---------------------------------------------------------------------------------------------------------
        public static void Create(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            // make sure values are ressonable 
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Verify only one instance is present.
            Debug.Assert(_pInstance == null);

            //Check if timer manager is not present.
            if (_pInstance == null)
            {
                //If not, create a new timer manager.
                _pInstance = new TimerMan(initialReserveSize, newGrowthSize);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------------------------------
        //Add sprite to active list in manager.
        public static TimeEvent Add(TimeEvent.Name timeName, Command pCommand, float deltaTime)
        {
            //Get timer manager instance.
            TimerMan pMan = TimerMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Add time event to active list.
            TimeEvent pTmp = (TimeEvent)pMan.BaseAdd();

            //Verify time event is added.
            Debug.Assert(pTmp != null);

            //Verify command is present.
            Debug.Assert(pCommand != null);

            //verify delta time is greater than 0.
            Debug.Assert(deltaTime >= 0.0f);

            //Set time event with given data.
            pTmp.SetTimeEvent(timeName, pCommand, deltaTime);

            //Print time event added.
            Debug.WriteLine("\n\n***Added Time Event:\"" + timeName + "\" to Active List***");

            //Return added time event.
            return (pTmp);
        }


        //Search from active list.
        public static TimeEvent Search(TimeEvent.Name newName)
        {
            //Get timer manager instance.
            TimerMan pMan = TimerMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Verify Time event to comapre is present.
            Debug.Assert(pMan.pDataCompare != null);

            //Reset comparing time event.
            pMan.pDataCompare.Clean();

            //else, set comparing time event with given name.
            pMan.pDataCompare.SetName(newName);

            //Search active list with comparing time event.
            TimeEvent pTmp = (TimeEvent)pMan.BaseSearch(pMan.pDataCompare);

            //Return data.
            return (pTmp);
        }

        //Delete from active list.
        public static void Delete(TimeEvent pNewData)
        {
            //Get timer manager instance.
            TimerMan pMan = TimerMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //verify image is present to delete.
            Debug.Assert(pNewData != null);

            //Delete image from active list.
            pMan.BaseDelete(pNewData);
        }

        //Print the manager details.
        public static void Print()
        {
            //Get timer manager instance.
            TimerMan pMan = TimerMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print timer manager.
            pMan.BasePrint("TIMER");
        }


        public static void Destroy()
        {
            //Get timer manager instance.
            TimerMan pMan = TimerMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Print all sprite data.
            Print();

            //Delete the sprite manager.
            _pInstance = null;
        }

        public static void PauseUpdate(float delta)
        {
            // Get the instance
            TimerMan pMan = TimerMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            // walk the list
            TimeEvent pEvent = (TimeEvent)pMan.BaseGetActive();

            while (pEvent != null)
            {
                pEvent.triggerTime += delta;
                // advance the pointer
                pEvent = (TimeEvent)pEvent.pNext;
            }
        }

        //Update Function.
        public static void Update(float totalTime)
        {
            //Get timer manager instance.
            TimerMan pMan = TimerMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //Assign total time
            pMan.currTime = totalTime;

            //Get active list.
            TimeEvent pEvent = (TimeEvent)pMan.BaseGetActive();
            TimeEvent pNextEvent = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            // ToDo Fix: List needs to be sorted
            while (pEvent != null)
            {
                // Difficult to walk a list and remove itself from the list
                // so squirrel away the next event now, use it at bottom of while
                pNextEvent = (TimeEvent)pEvent.pNext;

                if (pMan.currTime >= pEvent.triggerTime)
                {
                    // call it
                    pEvent.Process();

                    // remove from list
                    pMan.BaseDelete(pEvent);
                }

                // advance the pointer
                pEvent = pNextEvent;
            }
        }

        //Get current time.
        public static float GetCurrTime()
        {
            //Get timer manager instance.
            TimerMan pMan = TimerMan.GetPrivateInstance();
            Debug.Assert(pMan != null);

            //return current time.
            return (pMan.currTime);
        }

        //--------------------------------------------------------------------------------
        // Derived Functions
        //--------------------------------------------------------------------------------

        //Derived function to create new Data.
        override protected DLink derivedCreateData()
        {
            //Create new time event.
            DLink pTmp = new TimeEvent();

            //Verify new time event is created.
            Debug.Assert(pTmp != null);

            //Return new time event.
            return (pTmp);
        }

        //Derived function to compare with Data name.
        override protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo)
        {
            //Convert links to time event to compare.
            TimeEvent pTmp1 = (TimeEvent)pCompareWith;
            TimeEvent pTmp2 = (TimeEvent)pCompareTo;

            //Compare with names.
            if (pTmp1.ReturnName() == pTmp2.ReturnName())
            {
                return true;    //If name is equal.
            }

            return false;   //If name's are not equal.
        }

        //Derived function to reset data.
        override protected void derivedReset(DLink pResetLink)
        {
            //Convert link to time event.
            TimeEvent pTmp = (TimeEvent)pResetLink;

            //Reset time event.
            pTmp.Clean();
        }

        //Derived function print data.
        override protected void derivedPrint(DLink pPrintLink)
        {
            //Convert link to time event.
            TimeEvent pTmp = (TimeEvent)pPrintLink;

            //Print time event details.
            pTmp.GetTimeEvent();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            //Convert link to time event.
            TimeEvent pTmp = (TimeEvent)pPrintLink;

            //Print time event name.
            return (pTmp.ReturnName());
        }

        //--------------------------------------------------------------------------------
        //PRIVATE
        //--------------------------------------------------------------------------------
        private static TimerMan GetPrivateInstance()
        {
            //Verify no instance is present.
            Debug.Assert(_pInstance != null);

            //return new instance.
            return (_pInstance);
        }

        //---------------------------------------------------------------------------------------------------------
        // Static Data
        //---------------------------------------------------------------------------------------------------------
        private static TimerMan _pInstance = null;

        //---------------------------------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------------------------------
        private TimeEvent pDataCompare;
        protected float currTime;
    }
}
