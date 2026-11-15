using UnityEngine;

/// <summary>
/// 왼쪽으로 계속 이동하다가 화면 밖으로 나가면 스스로 사라지는 장애물.
/// 프리팹에 붙여서 쓴다. Rigidbody2D(Kinematic)와 BoxCollider2D가 자동으로 추가된다.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ObstacleMove : MonoBehaviour
{
    [Tooltip("왼쪽으로 움직이는 속도")]
    [SerializeField] private float speed = 6f;

    [Tooltip("이 X 위치보다 왼쪽으로 가면 삭제")]
    [SerializeField] private float destroyX = -15f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Kinematic: 중력이나 충돌에 밀리지 않고, 우리가 준 속도대로만 움직인다
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.freezeRotation = true;
    }

    private void Start()
    {
        rb.linearVelocity = Vector2.left * speed;
    }

    private void Update()
    {
        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
