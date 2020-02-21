using HappyMrsChicken.Entities;
using HappyMrsChicken.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyMrsChicken.Components
{
    /// <summary>
    /// This class is responsible for maintaining, updating the state of the chicken. When the state is updated we play the corresponding animation and sound
    /// This class will be called by other classes in response to events such as Inputs (keyboard, mouse) and game events (collision).
    /// </summary>
    public class ChickenStateAction : ComponentBase
    {
        FiniteStateMachine fsm;
        public ChickenStateAction(int entityId, FiniteStateMachine fsm) : base(entityId)
        {
            this.fsm = fsm;
            fsm.SetState(Idle);
        }

        public void SetState(ChickenState state)
        {
            switch (state)
            {
                case ChickenState.Idle:
                    {
                        fsm.SetState(Idle);
                        break;
                    }
                case ChickenState.MoveDown:
                    {
                        fsm.SetState(MoveDown);
                        break;
                    }
                case ChickenState.MoveUp:
                    {
                        fsm.SetState(MoveUp);
                        break;
                    }
                case ChickenState.MoveLeft:
                    {
                        fsm.SetState(MoveLeft);
                        break;
                    }
                case ChickenState.MoveRight:
                    {
                        fsm.SetState(MoveRight);
                        break;
                    }
            }
        }
        private void Idle()
        {
            var anim = EntityManager.Instance.GetComponent<ChickenAnimation>(EntityId);
            if (anim.CurrentState != ChickenState.Idle)
            {
                anim.SetAnimation(ChickenState.Idle);
                anim.Pause();
            }
        }

        private void MoveLeft()
        {
            var velocity = EntityManager.Instance.GetComponent<Velocity>(EntityId);
            Move(ChickenState.MoveLeft, -velocity.CurrentSpeed, 0);
        }

        private void MoveRight()
        {
            var velocity = EntityManager.Instance.GetComponent<Velocity>(EntityId);
            Move(ChickenState.MoveRight, velocity.CurrentSpeed, 0);
        }

        private void MoveUp()
        {
            var velocity = EntityManager.Instance.GetComponent<Velocity>(EntityId);
            Move(ChickenState.MoveUp, 0, -velocity.CurrentSpeed);
        }

        private void MoveDown()
        {
            var velocity = EntityManager.Instance.GetComponent<Velocity>(EntityId);
            Move(ChickenState.MoveDown, 0, velocity.CurrentSpeed);
        }

        private void Move(ChickenState state, int deltaX, int deltaY)
        {
            var anim = EntityManager.Instance.GetComponent<ChickenAnimation>(EntityId);
            if (anim.CurrentState != state)
            {
                anim.SetAnimation(state);
                anim.Play();
            }
            var pos = EntityManager.Instance.GetComponent<Position>(EntityId);
            if (!DoesColliedWithTerrain(pos, deltaX, deltaY))
            {
                pos.X += deltaX;
                pos.Y += deltaY;
            }

            var collider = SystemManager.Instance.Get<Collider>();
            var list = collider.GetCollisions(EntityId);
            foreach(var item in list)
            {
                collider.UnregisterTarget(EntityId, item);
                var corn = SystemManager.Instance.Get<Corn>();
                corn.OnCollide(EntityId);
                anim.SetAnimation(ChickenState.Eat);
            }
        }

        private bool DoesColliedWithTerrain(Position p, int deltaX, int deltaY)
        {
            var tm = SystemManager.Instance.Get<TileManager>();
            var tiles = tm.GetTilesUnderArea(
                p.X + deltaX,
                p.Y + deltaY,
                p.X + deltaX + p.Size.X,
                p.Y + deltaY + p.Size.Y);
            foreach (var tile in tiles)
            {
                if (tile.IsPassable == false)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
