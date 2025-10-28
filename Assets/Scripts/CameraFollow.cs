using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset = new(0, 0, -10);

    [Header("Auto Bounds")]
    [SerializeField] private Tilemap borderTilemap;  // �ڵ� �νĿ�
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private bool useBounds = false;

    private void Start()
    {

        // ���� Boarder ������Ʈ ������ �ڵ� �Ҵ�
        if (borderTilemap == null)
        {
            var border = GameObject.Find("Boarder");
            if (border != null)
                borderTilemap = border.GetComponent<Tilemap>();
        }

        // Ÿ�ϸʿ��� ��� ���
        if (borderTilemap != null)
        {
            var bounds = borderTilemap.localBounds;
            Vector3 worldMin = borderTilemap.transform.TransformPoint(bounds.min);
            Vector3 worldMax = borderTilemap.transform.TransformPoint(bounds.max);
            minBounds = worldMin;
            maxBounds = worldMax;
            useBounds = true;

            Debug.Log($"[CameraFollow] Bounds detected: min={minBounds}, max={maxBounds}");
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = new(
            target.position.x + offset.x,
            target.position.y + offset.y,
            offset.z
        );

        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        if (useBounds)
        {
            // ī�޶� ũ�� ����ؼ� ȭ�� �� �ȳѰ�
            float camHeight = Camera.main.orthographicSize;
            float camWidth = camHeight * Camera.main.aspect;

            smoothed.x = Mathf.Clamp(smoothed.x, minBounds.x + camWidth, maxBounds.x - camWidth);
            smoothed.y = Mathf.Clamp(smoothed.y, minBounds.y + camHeight, maxBounds.y - camHeight);
        }

        smoothed.z = offset.z;
        transform.position = smoothed;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        transform.position = new Vector3(
            newTarget.position.x + offset.x,
            newTarget.position.y + offset.y,
            offset.z
        );
    }
}
