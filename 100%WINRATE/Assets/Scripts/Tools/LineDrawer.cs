using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField] GameObject linePrefab;
    [SerializeField] Transform bottomLeft;
    [SerializeField] Transform topRight;
    [SerializeField] Transform lineParent;

    float mapWidth;
    float mapHeigth;
    [SerializeField] float distanceBetweenLines;
    [SerializeField] float distanceFromCamera = 100;

    private void Start()
    {
        DrawLines();
    }

    private void DrawLines()
    {
        mapWidth = topRight.position.x - bottomLeft.position.x;
        mapHeigth = topRight.position.y - bottomLeft.position.y;

        float remainingWidth = mapWidth;
        float remainingHeigth = mapHeigth;


        while (remainingWidth > 0)
        {
            Vector3 pos0 = new Vector3(bottomLeft.position.x + (mapWidth - remainingWidth), bottomLeft.position.y, distanceFromCamera);
            Vector3 pos1 = new Vector3(bottomLeft.position.x + (mapWidth - remainingWidth), bottomLeft.position.y + mapHeigth, distanceFromCamera);
            CreateLine(pos0, pos1);
            remainingWidth -= distanceBetweenLines;
        }

        while (remainingHeigth > 0)
        {
            Vector3 pos0 = new Vector3(bottomLeft.position.x, bottomLeft.position.y + (mapHeigth - remainingHeigth), distanceFromCamera);
            Vector3 pos1 = new Vector3(bottomLeft.position.x + mapWidth, bottomLeft.position.y +(mapHeigth - remainingHeigth), distanceFromCamera);
            CreateLine(pos0, pos1);
            remainingHeigth -= distanceBetweenLines;
        }
    }

    private void CreateLine(Vector3 pos0, Vector3 pos1)
    {
        GameObject newLine = Instantiate(linePrefab);
        newLine.transform.SetParent(lineParent);
        LineRenderer renderer = newLine.GetComponent<LineRenderer>();
        renderer.SetPosition(0, pos0);
        renderer.SetPosition(1, pos1);
        newLine.transform.position = new Vector3(newLine.transform.position.x, newLine.transform.position.y, 1000000);
    }
}
