using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJoystick : MonoBehaviour
{
    public Image InnerCircle;

    private Vector3 startPosition;
    private InputBase playerInput;

    public void Initialize()
    {
        startPosition = transform.position;

        if(gameObject.activeSelf)
            Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        playerInput = GameManager.Player.playerController.inputBase;
    }

    private void Update()
    {
        if(playerInput.IsInput())
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                transform.position = Input.GetTouch(0).position;

            InnerCircle.transform.localPosition = playerInput.MoveDirection() * InnerCircle.rectTransform.sizeDelta;
        }
        else
        {
            transform.position = startPosition;
            InnerCircle.transform.localPosition = Vector2.zero;
        }
    }
}
