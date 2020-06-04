using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelMapBoundsAffected : MonoBehaviour
{    
    [SerializeField] private GameObject body;

    private float mapHeight;
    private float mapWidth;
    private float securityDistance;
    private Transform bottomLeft;
    private Transform topRight;

    private void Start()
    {
        mapHeight = DuelBoundsManager.Instance.mapHeight;
        mapWidth = DuelBoundsManager.Instance.mapWidth;
        securityDistance = DuelBoundsManager.Instance.securityDistance;
        bottomLeft = DuelBoundsManager.Instance.bottomLeft;
        topRight = DuelBoundsManager.Instance.topRight;
    }

    private void Update()
    {
        Vector2 pos = body.transform.position;

        if(pos.x < bottomLeft.position.x)
        {
            Vector2 newPosition = new Vector2(body.transform.position.x + mapWidth - securityDistance, body.transform.position.y);
            body.transform.position = newPosition;
        }
        if(pos.x > topRight.position.x)
        {
            Vector2 newPosition = new Vector2(body.transform.position.x - mapWidth + securityDistance, body.transform.position.y);
            body.transform.position = newPosition;
        }
        if (pos.y < bottomLeft.position.y)
        {
            Vector2 newPosition = new Vector2(body.transform.position.x, body.transform.position.y + mapHeight - securityDistance);
            body.transform.position = newPosition;
        }
        if(pos.y > topRight.position.y)
        {
            Vector2 newPosition = new Vector2(body.transform.position.x, body.transform.position.y - mapHeight + securityDistance);
            body.transform.position = newPosition;
        }
    }
}
