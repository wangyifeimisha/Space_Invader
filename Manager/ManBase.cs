using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class ManBase
    {
        //-----------------------------------------------------------------------------------------
        //Methods derived class should implement
        //-----------------------------------------------------------------------------------------
        abstract protected DLink derivedCreateData();
        abstract protected bool derivedCompare(DLink pCompareWith, DLink pCompareTo);
        abstract protected void derivedReset(DLink pResetLink);
        abstract protected void derivedPrint(DLink pPrintLink);
        abstract protected Enum derivedPrintName(DLink pPrintLink);

        //-----------------------------------------------------------------------------------------
        //Constructor
        //-----------------------------------------------------------------------------------------
        protected ManBase()
        {
            //Set active list to null.
            this._pActive = null;

            //Set reserve list to null.
            this._pReserved = null;

            //Set reserve size.
            _reserveSize = 0;

            //Set active size.
            _activeSize = 0;

            //Set peak active size.
            _peakActiveSize = 0;

            //Set growth size.
            _growthSize = 0;
        }
        protected void BaseInitialize(int initialReserveSize = 2, int newGrowthSize = 2)
        {
            //Verify that initial reserve size and the growth size is not less than 0.
            Debug.Assert(initialReserveSize > 0 && newGrowthSize > 0);

            //Set growth size value.
            _growthSize = newGrowthSize;

            //Create reserved list with the initial reserve size.
            this.AddReservedList(initialReserveSize);           
        }

        //-----------------------------------------------------------------------------------------
        //Methods
        //-----------------------------------------------------------------------------------------
        //Set reserved list of base class.
        protected void BaseSetReserve(int reserveSize = 2, int growthSize = 2)
        {
            //Verify that initial reserve size and the growth size is not less than 0.
            Debug.Assert(reserveSize > 0 && growthSize > 0);

            //Set growth size.
            this._growthSize = growthSize;

            //Check if new reserve size is greater than current reserve size.
            if(reserveSize > this._reserveSize)
            {
                //If yes, refill the reserve list with reserve size - current reserve size.
                this.AddReservedList(reserveSize - this._reserveSize);
            }
        }

        //Add data to base active list.
        protected DLink BaseAdd()
        {
            //If deleted node is null, refill the reserve list.
            if (this._pReserved == null)
            {
                //refill with growth size.
                this.AddReservedList(this._growthSize);
            }

            //Delete first node from reserved list.
            DLink deletedData = ManBase.DeleteAtFront(ref this._pReserved);
            Debug.Assert(deletedData != null);

            //Decrement reserve size.
            _reserveSize--;

            //Reset data.
            this.derivedReset(deletedData);

            //Add to the front of active list.
            ManBase.AddAtFront(ref this._pActive, deletedData);           
            //increment active size.
            _activeSize++;

            //Check if active size is greater than peak active size.
            if(_activeSize > _peakActiveSize)
            {
                _peakActiveSize = _activeSize;  //if yes, assign peak active size.
            }

            //return created data.
            return (deletedData);
        }

        //Used to fill the reserve list.
        private void AddReservedList(int newReserveSize)
        {
            //Check if reserve size to fill is greater than 0.
            Debug.Assert(newReserveSize > 0);

            int i = 0;
            while (i++ < newReserveSize)
            {
                //Create a new derived data.
                DLink newData = this.derivedCreateData();

                //Verify data is created.
                Debug.Assert(newData != null);

                //Add the data to front of reserve list.
                ManBase.AddAtFront(ref this._pReserved, newData);  
            }

            //increment the reserve size.
            _reserveSize += newReserveSize;
        }

        //Used to search data from active list.
        protected DLink BaseSearch(DLink pDataToFind)
        {
            //Verify data to search is present.
            Debug.Assert(pDataToFind != null);

            //Assign temporary head.
            DLink pTmp = this._pActive;

            //traverse the active list.
            while (pTmp != null)
            {
                //Compare the given data using derived compare.
                if (derivedCompare(pTmp, pDataToFind))
                {
                    break;      //If data found break.
                }
                pTmp = pTmp.pNext;
            }

            //return search result.
            return (pTmp);
        }

        //Used to delete data from active list.
        protected void BaseDelete(DLink pDataToDelete)
        {
            //Verify that data to delete is present.
            Debug.Assert(pDataToDelete != null);

            //Delete the data from active list.
            ManBase.DeleteByNode(ref this._pActive, pDataToDelete);

            //Decerement active size.
            _activeSize--;

            //Reset deleted data.
            this.derivedReset(pDataToDelete);

            //Add the deleted data to front of reserve list.
            ManBase.AddAtFront(ref this._pReserved, pDataToDelete);

            //Increment reserve size.
            _reserveSize++;            
        }

        //Return active list.
        protected DLink BaseGetActive()
        {
            return (this._pActive);
        }

        //--------------------------------------------------------------------------------------------------------------
        //Print Methods.
        //--------------------------------------------------------------------------------------------------------------

        //Print All.
        protected void BasePrint(string p)
        {
            Debug.WriteLine("\n\n  ***///" + p + " MANAGER \\\\\\***\n");
            Debug.WriteLine("          growthSize: {0} ",_growthSize);
            Debug.WriteLine("           NumActive: {0} ", _activeSize);
            Debug.WriteLine("       peakNumActive: {0} ", _peakActiveSize);
            Debug.WriteLine("         NumReserved: {0} ", _reserveSize);
            Debug.WriteLine("            NumTotal: {0} ", _reserveSize + _activeSize);

            DLink pTmp = null;

            //If active list empty.
            if (this._pActive == null)
            {
                Debug.WriteLine("\n\n    Active Head:  null\n");
            }
            else    //else print active head.
            {
                pTmp = this._pActive;

                Debug.WriteLine("\n    Active Head: " + this.derivedPrintName(pTmp) + "\n");
            }

            //If reserve list empty.
            if (this._pReserved == null)
            {
                Debug.WriteLine("\n   Reserve Head:  null\n");
            }
            else    //else print reserve head.
            {
                pTmp = this._pReserved;

                Debug.WriteLine("\n    Reserve Head: " + this.derivedPrintName(pTmp) + "\n");
            }

            //Print active list.
            this.PrintActiveList();
            //Print reserve list.
            this.PrintReserveList();
        }

        //Print Active List.
        private void PrintActiveList()
        {
            //Assign temporary active list head.
            DLink pTmp = this._pActive;

            //If active list is empty.
            if(pTmp == null)
            {
                Debug.WriteLine("\n\n------Active List Empty------\n");
                return;
            }

            //else print.
            Debug.WriteLine("\n\n------Active List------");
            int i = 0;

            while(pTmp != null)
            {
                Debug.WriteLine("\n\nNode {0}:", ++i);
                this.derivedPrint(pTmp);
                pTmp = pTmp.pNext;
            }
        }     

        //Print Links in Reserve List.
        private void PrintReserveList()
        {
            //Assign temporary reserve list head.
            DLink pTmp = this._pReserved;

            //If reserve list is empty.
            if (pTmp == null)
            {
                Debug.WriteLine("\n\n------Reserve List Empty------\n");
                return;
            }

            //else print.
            Debug.WriteLine("\n\n------Reserve List------");
            int i = 0;

            while (pTmp != null)
            {
                Debug.WriteLine("\n\nNode {0}:", ++i);
                this.derivedPrint(pTmp);
                pTmp = pTmp.pNext;
            }
        }


        //--------------------------------------------------------------------------------------------------
        //DLink Functions.
        //--------------------------------------------------------------------------------------------------
        //Add to the front of list.
        public static void AddAtFront(ref DLink pHead, DLink pNewData)
        {
            // Will work for Active or Reserve List
            Debug.Assert(pNewData != null);

            DLink.AddAtFront(ref pHead, pNewData);

            // worst case, pHead was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
        }

        //Delete from front of list.
        public static DLink DeleteAtFront(ref DLink pHead)
        {
            // There should always be something on list
            Debug.Assert(pHead != null);

            return (DLink.DeleteAtFront(ref pHead));
        }

        //Delete a node from list.
        public static void DeleteByNode(ref DLink pHead, DLink pNewData)
        {
            // protection
            Debug.Assert(pHead != null);
            Debug.Assert(pNewData != null);

            DLink.DeleteByNode(ref pHead, pNewData);
        }

        //-----------------------------------------------------------------------------------------
        //Data
        //-----------------------------------------------------------------------------------------
        private DLink _pActive;
        private DLink _pReserved;

        private int _growthSize = 0;
        private int _reserveSize = 0;
        private int _activeSize = 0;
        private int _peakActiveSize = 0;
    }
}
