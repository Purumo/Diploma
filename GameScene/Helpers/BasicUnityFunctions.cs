using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace GameScene.Helpers
{
    public static class BasicUnityFunctions
    {
        public static GameObject GetGameObject(string pathSceneObject)
        {
            return GameObject.Find(pathSceneObject);
        }
        public static Rigidbody2D GetRigidbody(string pathSceneObject)
        {
            Rigidbody2D objBody = null;
            if (objBody == null) 
                objBody = GetGameObject(pathSceneObject).GetComponent<Rigidbody2D>();
            return objBody;
        }
        public static Transform GetTransform(string pathSceneObject)
        {
            Transform objBody = null;
            if (objBody == null)
                objBody = GetGameObject(pathSceneObject).GetComponent<Transform>();
            return objBody;
        }
    }
}