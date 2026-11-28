using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using TMPro;

public class Player : MonoBehaviourPun
{
    private Rigidbody2D rb;
    [Header("Movement")]
    public float speed = 5f;
    [Header("Nickname")]
    public TMP_Text nicknameText;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GetComponents();
        AssignName();
    }

    void GetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void AssignName()
    {
        nicknameText.text = photonView.Owner.NickName;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (photonView.IsMine == false)
        {
            return;
        }

        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.wKey.isPressed)
        {
            moveY = 1f;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            moveY = -1f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            moveX = 1f;
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            moveX = -1f;
        }

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        rb.linearVelocity = moveDirection * speed;
    }
}
