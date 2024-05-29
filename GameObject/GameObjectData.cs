using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameObjectData_Link : DLink
    {

    }
    public class GameObjectData : GameObjectData_Link
    {
        //-----------------------------------------------------------------------------------------
        //Constructor.
        //-----------------------------------------------------------------------------------------
        public GameObjectData()
        :   base()
        {
            this.privClear();
        }

        //-----------------------------------------------------------------------------------------
        //Methods.
        //-----------------------------------------------------------------------------------------
        public void SetGameObject(GameObject pGameObject)
        {
            Debug.Assert(pGameObject != null);
            this.pGameObj = pGameObject;
        }

        private void privClear()
        {
            this.pGameObj = null;
        }

        //Return name.
        public GameObject.Name ReturnName()
        {
            return (this.pGameObj.ReturnName());
        }

        //Print details.
        public void Print()
        {
            Debug.Assert(this.pGameObj != null);
            Debug.WriteLine("\n\t     GameObject: {0}", this.GetHashCode());

            this.pGameObj.Print();
        }

        //Reset data.
        public void Clean()
        {
            this.pGameObj = null;
        }

        //-----------------------------------------------------------------------------------------
        //Data.
        //-----------------------------------------------------------------------------------------
        public GameObject pGameObj;
    }
}
