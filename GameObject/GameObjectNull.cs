using System;
using System.Diagnostics;
namespace SpaceInvaders
{
    public class GameObjectNull : Leaf
    {
        public GameObjectNull()
        :   base(GameObject.Name.NullObject, SpriteGame.Name.NullObject)
        {

        }

        public override void Accept(ColVisitor other)
        {
            // Important: at this point we have an GameObjectNull
            // Call the appropriate collision reaction            
            other.VisitNullGameObject(this);

        }
        public override void Update()
        {
            // do nothing - its a null object
        }

        //private static SpriteGameProxyNull psSpriteGameProxyNull = new SpriteGameProxyNull();
    }
}
