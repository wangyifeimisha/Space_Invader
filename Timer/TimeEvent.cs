using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TimeEvent_Link : DLink
    {
    }

    public class TimeEvent : TimeEvent_Link
    {
        //-----------------------------------------------------------------------------------------
        // Enum
        //-----------------------------------------------------------------------------------------
        public enum Name
        {
            SpriteAnimation,
            ExplosionRemove,
            SaucerBomb,
            AlienExplosion,
            SaucerSpawn,
            BombSpawn,

            Uninitialized
        }

        //-----------------------------------------------------------------------------------------
        // Constructor
        //-----------------------------------------------------------------------------------------
        public TimeEvent()
        :   base()
        {
            this.name = TimeEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        //-----------------------------------------------------------------------------------------
        // Methods
        //-----------------------------------------------------------------------------------------
        public void SetTimeEvent(TimeEvent.Name eventName, Command pCommand, float deltaTimeToTrigger)
        {
            //Verify command is present.
            Debug.Assert(pCommand != null);

            //Assign name.
            this.name = eventName;

            //Assign Command.
            this.pCommand = pCommand;

            //Asssign delta time.
            this.deltaTime = deltaTimeToTrigger;

            //Set trigger time.
            this.triggerTime = TimerMan.GetCurrTime() + deltaTimeToTrigger;
        }

        //Process.
        public void Process()
        {     
            //Verify command is not null.
            Debug.Assert(this.pCommand != null);
            
            //Execute Command.
            this.pCommand.Execute(deltaTime);
        }

        //Reset Data.
        public new void ClearLink()
        {
            this.name = TimeEvent.Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
            this.deltaTime = 0.0f;
        }

        public void Clean()
        {
            base.ClearLink();
            this.ClearLink();
        }
        //Set name.
        public void SetName(TimeEvent.Name newName)
        {
            this.name = newName;
        }

        //Return name.
        public Name ReturnName()
        {
            return (this.name);
        }

        //Get time event details.
        public void GetTimeEvent()
        {
            //Print name of time event, command and trigger time, delta time.
            Debug.WriteLine("\nEvent Name: {0}  Command Name: {1}" +
                            "\nTrigger Time: {2}  Delta Time: {3}\n", this.ReturnName(), this.pCommand, 
                            this.triggerTime, this.deltaTime);

            //Check if previous time event exist, if not print null.
            if (this.pPrev == null)
            {
                Debug.WriteLine("Prev: null");
            }
            else        //else print the previous time event name.
            {
                TimeEvent pTmp = (TimeEvent)this.pPrev;
                Debug.WriteLine("Prev: {0}", pTmp.ReturnName());
            }

            //Check if next time event exist, if not print null.
            if (this.pNext == null)
            {
                Debug.WriteLine("Next: null\n");
            }
            else        //else print the next time event name.
            {
                TimeEvent pTmp = (TimeEvent)this.pNext;
                Debug.WriteLine("Next: {0}\n", pTmp.ReturnName());
            }
        }

        //-----------------------------------------------------------------------------------------
        // Data
        //-----------------------------------------------------------------------------------------
        public Name name;
        public Command pCommand;
        public float triggerTime;
        public float deltaTime;
    }
}
