﻿using System;
using System.Xml;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GlyphMan_MLink : ManBase
    {
        public Glyph_DLink poActive;
        public Glyph_DLink poReserve;
    }

    class GlyphMan : GlyphMan_MLink
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private GlyphMan(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);

            this.pRefNode = (Glyph)this.derivedCreateData();
        }
        ~GlyphMan()
        {

        }

        //----------------------------------------------------------------------
        // Static Manager methods can be implemented with base methods 
        // Can implement/specialize more or less methods your choice
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GlyphMan(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {

        }
        public static Glyph Add(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            GlyphMan pMan = GlyphMan.GetPrivateInstance();

            Glyph pNode = (Glyph)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.SetGlyph(name, key, textName, x, y, width, height);
            return pNode;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        {
            System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            // I'm sure there is a better way to do this... but this works for now
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement: //Display the end of the element 
                        if (reader.Name == "character")
                        {
                            // have all the data... so now create a glyph
                            // Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
                            GlyphMan.Add(glyphName, key, textName, x, y, width, height);
                        }
                        break;
                }
            }

            // Debug.Write("\n");
        }

        public static void Delete(Glyph pNode)
        {
            Debug.Assert(pNode != null);
            GlyphMan pMan = GlyphMan.GetPrivateInstance();
            pMan.BaseDelete(pNode);
        }
        public static Glyph Search(Glyph.Name name, int key)
        {
            GlyphMan pMan = GlyphMan.GetPrivateInstance();

            // Compare functions only compares two Nodes
            pMan.pRefNode.name = name;
            pMan.pRefNode.key = key;

            Glyph pData = (Glyph)pMan.BaseSearch(pMan.pRefNode);
            return pData;
        }

        public static void Print()
        {
            GlyphMan pMan = GlyphMan.GetPrivateInstance();
            Debug.Assert(pMan != null);
            pMan.BasePrint("GLYPH");
        }
        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected Boolean derivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Glyph pDataA = (Glyph)pLinkA;
            Glyph pDataB = (Glyph)pLinkB;

            Boolean status = false;

            if (pDataA.ReturnName() == pDataB.ReturnName() && pDataA.key == pDataB.key)
            {
                status = true;
            }

            return status;
        }

        override protected DLink derivedCreateData()
        {
            DLink pNode = new Glyph();
            Debug.Assert(pNode != null);
            return pNode;
        }

        override protected void derivedReset(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Glyph pNode = (Glyph)pLink;
            pNode.Clean();
        }

        override protected void derivedPrint(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Glyph pNode = (Glyph)pLink;

            Debug.Assert(pNode != null);
            pNode.GetGlyph();
        }

        //Derived function to print name of data.
        protected override Enum derivedPrintName(DLink pPrintLink)
        {
            Debug.Assert(pPrintLink != null);

            //Convert link to image.
            Glyph pTmp = (Glyph)pPrintLink;

            //Print image name.
            return (pTmp.ReturnName());
        }
        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GlyphMan GetPrivateInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private static GlyphMan pInstance = null;
        private Glyph pRefNode;
    }
}
