using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace PostProcessController
{
    [ExecuteInEditMode]
    public abstract class PostProcessController<T> : MonoBehaviour where T : VolumeComponent
    {
        private T component;
        public T Component { get => component; private set => component = value; }

        [SerializeField] private Volume postProcessVolume;
        private bool initialized = false;

        protected virtual void Awake()
        {
            CheckForComponent();
        }

        private bool GetVolumeComponent()
        {
            return postProcessVolume.profile.TryGet(out component);
        }

        protected virtual void Update()
        {
            SetComponentValues();
        }

        private void OnValidate()
        {
            SetComponentValues();
        }

        private void SetComponentValues()
        {
            if (initialized)
            {
                if (CheckForComponent())
                {
                    UpdateComponent();
                }
            }
            else if (postProcessVolume)
            {
                if (CheckForComponent())
                {
                    SetBaseValues();
                    initialized = true;
                }
            }
        }

        private bool CheckForComponent()
        {
            if (!postProcessVolume)
            {
                return false;
            }
            if (!Component)
            {
                return GetVolumeComponent();
            }
            else
            {
                return true;
            }
        }

        protected abstract void UpdateComponent();
        protected abstract void SetBaseValues();

    }
}
