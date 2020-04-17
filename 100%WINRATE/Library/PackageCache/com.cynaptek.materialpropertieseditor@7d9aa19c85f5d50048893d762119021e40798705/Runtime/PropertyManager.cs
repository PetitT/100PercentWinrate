using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialProperties
{
    [RequireComponent(typeof(MaterialFinder))]
    public class PropertyManager : MonoBehaviour
    {
        public void AddProperty<T>() where T : PropertyInfo
        {
            bool hasAddedComponent = false;

            foreach (Transform child in transform)
            {
                if (child.GetComponent<T>() == null)
                {
                    child.gameObject.AddComponent<T>();
                    hasAddedComponent = true;
                    return;
                }
            }

            if (!hasAddedComponent)
            {
                GameObject newObject = new GameObject();
                newObject.transform.SetParent(gameObject.transform);
                newObject.name = "Properties " + transform.childCount.ToString();
                newObject.AddComponent<T>();
            }
        }
    }
}

