using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelPlayerInput : MonoBehaviourPun
{
    public event Action<Vector2> onMoveInput;
    public event Action onShootInput;

    private bool canInput = true;

    public void ToggleInputs(bool toggle)
    {
        canInput = toggle;
        onMoveInput?.Invoke(Vector2.zero);
    }

    private void Update()
    {
        if (canInput)
        {
            if (photonView.IsMine)
            {
                GetMoveInput();
                GetShootInput();
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
