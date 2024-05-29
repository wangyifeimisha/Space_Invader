using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ShipMan
    {
        public enum State
        {
            Ready,
            MissileFlying,
            Dead,
            NoMoveLeft,
            NoMoveRight
        }

        private ShipMan()
        {
            // Store the states
            this.pStateReady = new ShipStateReady();
            this.pStateMissileFlying = new ShipStateMissileFlying();
            this.pStateDead = new ShipStateDead();
            this.pStateMoveRight = new ShipMoveRight();
            this.pStateMoveLeft = new ShipMoveLeft();

            // set active
            this.pShip = null;
            this.pMissile = null;

            pSB_Box = SpriteBatchMan.Search(SpriteBatch.Name.Boxes);
            pSB_Player = SpriteBatchMan.Search(SpriteBatch.Name.Player);
        }

        public static void NewShip()
        {
            // make sure its the first time
            Debug.Assert(instance == null);

            // Do the initialization
            if (instance == null)
            {
                instance = new ShipMan();
            }

            Debug.Assert(instance != null);

            // Stuff to initialize after the instance was created
            ActivateShip();
            instance.pShip.SetState(ShipMan.State.Ready);

        }

        public static void Destroy()
        {
            instance = null;
        }
        private static ShipMan GetPrivateInstance()
        {
            Debug.Assert(instance != null);

            return instance;
        }

        public static Ship GetShip()
        {
            ShipMan pShipMan = ShipMan.GetPrivateInstance();

            Debug.Assert(pShipMan != null);

            return pShipMan.pShip;
        }

        public static ShipState GetState(State state)
        {
            ShipMan pShipMan = ShipMan.GetPrivateInstance();
            Debug.Assert(pShipMan != null);

            ShipState pShipState = null;

            switch (state)
            {
                case ShipMan.State.Ready:
                    pShipState = pShipMan.pStateReady;
                    break;

                case ShipMan.State.MissileFlying:
                    pShipState = pShipMan.pStateMissileFlying;
                    break;

                case ShipMan.State.Dead:
                    pShipState = pShipMan.pStateDead;
                    break;

                case ShipMan.State.NoMoveLeft:
                    pShipState = pShipMan.pStateMoveRight;
                    break;

                case ShipMan.State.NoMoveRight:
                    pShipState = pShipMan.pStateMoveLeft;
                    break;

                default:
                    Debug.Assert(false);
                    break;
            }

            return pShipState;
        }

        public static Missile GetMissile()
        {
            ShipMan pShipMan = ShipMan.GetPrivateInstance();

            Debug.Assert(pShipMan != null);
            Debug.Assert(pShipMan.pMissile != null);

            return pShipMan.pMissile;
        }

        public static Missile ActivateMissile()
        {
            ShipMan pShipMan = ShipMan.GetPrivateInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Missile pMissile = new Missile(GameObject.Name.Missile, SpriteGame.Name.PlayerShot, 400, 100);
            pShipMan.pMissile = pMissile;

            SoundMan.PlaySound(SoundSource.Name.Missile, false, false, false);

            // Attached to SpriteBatches
            SpriteBatch pSB_Aliens = SpriteBatchMan.Search(SpriteBatch.Name.Aliens);
            SpriteBatch pSB_Boxes = SpriteBatchMan.Search(SpriteBatch.Name.Boxes);

            pMissile.ActivateCollisionSprite(pSB_Boxes);
            pMissile.ActivateGameSprite(pSB_Aliens);

            // Attach the missile to the missile root
            GameObject pMissileGroup = GameObjectMan.Search(GameObject.Name.MissileGroup);
            Debug.Assert(pMissileGroup != null);

            // Add to GameObject Tree - {update and collisions}
            pMissileGroup.Add(pShipMan.pMissile);

            return pShipMan.pMissile;
        }


        public static Ship ActivateShip()
        {
            ShipMan pShipMan = ShipMan.GetPrivateInstance();
            Debug.Assert(pShipMan != null);

            // copy over safe copy
            Ship pShip = new Ship(GameObject.Name.Ship, SpriteGame.Name.Player, 336, 100);
            pShip.ActivateCollisionSprite(pShipMan.pSB_Box);
            pShip.ActivateGameSprite(pShipMan.pSB_Player);
            pShipMan.pShip = pShip;

            //Attach to root.
            GameObject pShipRoot = GameObjectMan.Search(GameObject.Name.ShipRoot);
            Debug.Assert(pShipRoot != null);

            // Add to GameObject Tree - {update and collisions}
            pShipRoot.Add(pShipMan.pShip);

            return pShipMan.pShip;
        }

        // Data: ----------------------------------------------
        private static ShipMan instance = null;

        // Active
        private Ship pShip;
        private Missile pMissile;

        // Reference
        private ShipStateReady pStateReady;
        private ShipStateMissileFlying pStateMissileFlying;
        private ShipStateDead pStateDead;
        private ShipMoveRight pStateMoveRight;
        private ShipMoveLeft pStateMoveLeft;

        SpriteBatch pSB_Box;
        SpriteBatch pSB_Player;
    }
}
