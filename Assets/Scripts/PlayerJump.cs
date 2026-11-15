using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// 스페이스바 / 마우스 클릭 / 화면 터치로 점프하는 플레이어.
/// 이 스크립트를 붙이면 Rigidbody2D와 BoxCollider2D가 자동으로 추가된다.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerJump : MonoBehaviour
{
    [Tooltip("점프할 때 위로 주는 속도")]
    [SerializeField] private float jumpForce = 8f;

    [Tooltip("중력 배율 (클수록 빨리 떨어짐)")]
    [SerializeField] private float gravityScale = 2.5f;

    private Rigidbody2D rb;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravityScale;
        rb.freezeRotation = true; // 점프하다 네모가 굴러가지 않게
    }

    private void Update()
    {
        if (isGrounded && JumpPressed())
        {
            // Unity 6에서는 velocity 대신 linearVelocity를 쓴다
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }
    }

    /// <summary>
    /// 이번 프레임에 점프 입력이 들어왔는지 확인.
    /// Unity 6 기본값인 새 Input System과 예전 Input Manager 둘 다 지원한다.
    /// </summary>
    private bool JumpPressed()
    {
#if ENABLE_INPUT_SYSTEM
        bool key = Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        bool mouse = Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
        bool touch = Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame;
        return key || mouse || touch;
#else
        return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
#endif
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 아래쪽에서 부딪힌 경우만 "땅에 닿음"으로 본다 (벽 옆면 제외)
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
