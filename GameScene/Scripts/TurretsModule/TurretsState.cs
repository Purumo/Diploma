using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using GameScene.BulletsModule;

namespace GameScene.TurretsModule
{
    public abstract class TurretsState
    {
        protected Vector3 currentDirection;

        protected TurretsController controller;
        public TurretsState(TurretsController controller) => this.controller = controller;

        public abstract void Move();
        public abstract void Sleep();
        public abstract void Up();
        public abstract void Down();
        public abstract void Right();
        public abstract void Left();
        public abstract void Shoot();
        public abstract void Switching();
    }
}
