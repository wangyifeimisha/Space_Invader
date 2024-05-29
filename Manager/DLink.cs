using System.Diagnostics;

namespace SpaceInvaders
{
    public abstract class DLink
    {
        //---------------------------------------------------------------------------------------------------------
        //Constructor.
        //---------------------------------------------------------------------------------------------------------
        protected DLink()
        {
            //Clear links.
            this.ClearLink();
        }

        //---------------------------------------------------------------------------------------------------------
        //Methods.
        //---------------------------------------------------------------------------------------------------------
        //Clear pNext and pPrev.
        public void ClearLink()
        {
            this.pNext = null;
            this.pPrev = null;
        }

        //Add to the front of the given head node.
        public static void AddAtFront(ref DLink pHead, DLink pNewData)
        {
            //Check if data to add is not null.
            Debug.Assert(pNewData != null);

            //If head is null.
            if (pHead == null)
            {
                pNewData.pNext = null;
                pNewData.pPrev = null;
            }
            else        //If head is not null.
            {
                pNewData.pNext = pHead;
                pNewData.pPrev = null;
                pHead.pPrev = pNewData;
            }

            //Assign head as the added data.
            pHead = pNewData;
        }

        //Add to the end of the given head node.
        public static void AddAtEnd(ref DLink pHead, DLink pNewNode)
        {
            //Check if data to add is not null.
            Debug.Assert(pNewNode != null);

            //If head is null.
            if (pHead == null)
            {
                pNewNode.pNext = null;
                pNewNode.pPrev = null;
                pHead = pNewNode;
            }
            else        //If head is not null.
            {
                DLink pTmp = pHead;

                while (pTmp.pNext != null)
                {
                    pTmp = pTmp.pNext;
                }
                pNewNode.pNext = null;
                pNewNode.pPrev = pTmp;
                pTmp.pNext = pNewNode;
            }
        }

        //Delete from the front of the given head node.
        public static DLink DeleteAtFront(ref DLink pHead)
        {
            //Temporary data in place of head.
            DLink pTmp = pHead;

            //If head is null, no data to delete.
            if (pTmp == null)
            {
                return (pTmp);
            }
            else if (pHead.pNext == null)   //If only one data is present.
            {
                pHead = null;
            }
            else if (pHead != null)     //else traverse the list.
            {
                pHead = pHead.pNext;
                pHead.pPrev = null;
            }

            //Clear links of deleted node.
            pTmp.ClearLink();
            //Return deleted node.
            return (pTmp);      
        }

        //Delete a node from the given head node.
        public static void DeleteByNode(ref DLink pHead, DLink pNewData)
        {
            //Check if node to delete has a next or prev links.
            if (pNewData.pNext == null && pNewData.pPrev == null)
            {
                pHead = null;
            }
            else if (pNewData.pPrev == null)    //Check if there is a prev link. 
            {
                pHead = pHead.pNext;
                pHead.pPrev = null;
            }
            else if (pNewData.pNext == null)    //Check if there is a next link.
            {
                pNewData.pPrev.pNext = null;
            }
            else                    //If no next or prev links are present.
            {
                pNewData.pPrev.pNext = pNewData.pNext;
                pNewData.pNext.pPrev = pNewData.pPrev;
            }

            //Clear links of deleted node.
            pNewData.ClearLink();
        }

        // --------------------------------------------
        //  Methods dealing with Last Node
        // --------------------------------------------
        public static void DeleteByNode(ref DLink pHead, ref DLink pEnd, DLink pNode)
        {
            // protection
            Debug.Assert(pHead != null);
            Debug.Assert(pEnd != null);
            Debug.Assert(pNode != null);

            // Quick HACK... might be a bug... need to diagram

            // 4 different conditions... 
            if (pNode.pPrev != null)
            {	// middle part 1/2
                pNode.pPrev.pNext = pNode.pNext;

                // last node
                if (pNode == pEnd)
                {
                    pEnd = pNode.pPrev;
                }
            }
            else
            {  // first
                pHead = pNode.pNext;

                if (pNode == pEnd)
                {
                    // Only one node
                    pEnd = pNode.pNext;
                }
                else
                {
                    // Only first not the last
                    // do nothing more
                }
            }

            if (pNode.pNext != null)
            {	// middle node part 2/2
                pNode.pNext.pPrev = pNode.pPrev;
            }

            // remove any lingering links
            // HUGELY important - otherwise its crossed linked 
            pNode.ClearLink();
        }

        public static void AddAtFront(ref DLink pHead, ref DLink pEnd, DLink pNode)
        {
            // Will work for Active or Reserve List

            // add to front
            Debug.Assert(pNode != null);

            // add node
            if (pHead == null)
            {
                // push to the front
                pHead = pNode;
                pEnd = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // push to front
                pNode.pPrev = null;
                pNode.pNext = pHead;

                // update head
                pHead.pPrev = pNode;
                pHead = pNode;

                // update end
                // Adding to front --> end doesn't change
            }

            // worst case, pHead, pEnd was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
            Debug.Assert(pEnd != null);
        }
        public static void AddAtEnd(ref DLink pHead, ref DLink pEnd, DLink pNode)
        {
            // Will work for Active or Reserve List

            // add to front
            Debug.Assert(pNode != null);

            // add node
            if (pEnd == null)
            {
                // no nodes on list
                pHead = pNode;
                pEnd = pNode;
                pNode.pNext = null;
                pNode.pPrev = null;
            }
            else
            {
                // add to end
                pEnd.pNext = pNode;
                pNode.pPrev = pEnd;
                pNode.pNext = null;
                pEnd = pNode;

                // update front
                // Adding to end --> front doesn't change
            }

            // worst case, pHead,pEnd was null initially, now we added a node so... this is true
            Debug.Assert(pHead != null);
            Debug.Assert(pEnd != null);
        }

        //---------------------------------------------------------------------------------------------------------
        //Data.
        //---------------------------------------------------------------------------------------------------------
        public DLink pNext;
        public DLink pPrev;
    }
}
