using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelBoundsManager : Singleton<DuelBoundsManager>
{
    public Transform topRight;
    public Transform bottomLeft;
    [HideInInspector] public float securityDistance;

    [HideInInspector] public float mapWidth;
    [HideInInspector] public float mapHeight;

    protected override void Awake()
    {
        base.Awake();
        securityDistance = DuelDataManager.Instance.boundsTeleportSecurityDistance;
        mapWidth = Vector2.Distance(bottomLeft.transform.position, new Vector2(topRight.transform.position.x, bottomLeft.transform.position.y));
        mapHeight = Vector2.Distance(bottomLeft.transform.position, new Vector2(bottomLeft.transform.position.x, topRight.transform.position.y));
    }
}
