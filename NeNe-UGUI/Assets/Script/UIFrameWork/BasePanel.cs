using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeNe.LGL.Panel
{
    public abstract class BasePanel : MonoBehaviour
    {
        public abstract void OnEnter();

        public abstract void OnPause();

        public abstract void OnResume();

        public abstract void OnExit();

        public abstract void AddListener();

        public abstract void RemoveListener();

        public abstract void Return();


        public virtual void Test()
        {
            Debug.Log("Test");
        }
        public virtual void Init()
        {
            CanvasGroup canvasGroup=  gameObject.AddComponent<CanvasGroup>() ?? gameObject.GetComponent<CanvasGroup>();
        }
    }
}
