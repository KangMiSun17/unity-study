using UnityEngine;

/// <summary>
/// 일정 간격(랜덤)으로 화면 오른쪽에 장애물 프리팹을 만들어 내는 스포너.
/// 씬의 빈 오브젝트에 붙이고, 인스펙터에서 Obstacle Prefab 칸에 프리팹을 끌어다 놓는다.
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    [Tooltip("만들어 낼 장애물 프리팹 (Project 창에서 끌어다 놓기)")]
    [SerializeField] private GameObject obstaclePrefab;

    [Tooltip("장애물이 나타나는 X 위치 (화면 오른쪽 바깥)")]
    [SerializeField] private float spawnX = 12f;

    [Tooltip("장애물이 나타나는 Y 위치 (땅 윗면 위)")]
    [SerializeField] private float spawnY = -2f;

    [Tooltip("다음 장애물까지 최소 대기 시간(초)")]
    [SerializeField] private float minInterval = 1.2f;

    [Tooltip("다음 장애물까지 최대 대기 시간(초)")]
    [SerializeField] private float maxInterval = 2.5f;

    private float timer;

    private void Start()
    {
        if (obstaclePrefab == null)
        {
            Debug.LogError("ObstacleSpawner: Obstacle Prefab 칸이 비어 있어요. 인스펙터에서 프리팹을 넣어 주세요.", this);
            enabled = false;
            return;
        }
        timer = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Spawn();
            timer = Random.Range(minInterval, maxInterval);
        }
    }

    private void Spawn()
    {
        Vector3 position = new Vector3(spawnX, spawnY, 0f);
        Instantiate(obstaclePrefab, position, Quaternion.identity);
    }
}
