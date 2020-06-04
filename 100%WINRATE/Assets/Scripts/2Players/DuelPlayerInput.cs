using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerInput : MonoBehaviourPun
{
    public event Action<Vector2> onMoveInput;
    public event Action onShootInput;

    private bool canInputShoot = true;
    private bool canInputMove = true;

    public void ToggleMoveInputs(bool toggle)
    {
        canInputMove = toggle;
        onMoveInput?.Invoke(Vector2.zero);
    }

    public void ToggleShootInput(bool toggle)
    {
        canInputShoot = toggle;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (canInputShoot)
            {
                GetShootInput();
            }
            if (canInputMove)
            {
                GetMoveInput();
            }
        }
    }

    private void GetMoveInput()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        onMoveInput?.Invoke(new Vector2(X, Y));
    }

    private void GetShootInput()
    {
        if (Input.GetMouseButton(0))
        {
            onShootInput?.Invoke();
        }
    }
}
