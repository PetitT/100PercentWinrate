using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLootAnim : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToAnimate;
    [SerializeField] private Transform center;
    [SerializeField] private float speed;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        foreach (var item in objectsToAnimate)
        {
            item.transform.RotateAround(center.transform.position, center.transform.forward, speed * Time.deltaTime);
        }
    }
}
