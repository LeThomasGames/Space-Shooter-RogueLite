using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ParallaxBackground : MonoBehaviour
{
    public float scrollSpeed = 0.02f;
    private Material mat;
    private Vector2 offset;
    private Rigidbody2D rb;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        rb = PlayerData.Instance.Movement.rb;
        ResizeToCamera();
    }

    void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime * rb.linearVelocityX;
        offset.y += scrollSpeed * Time.deltaTime * rb.linearVelocityY;
        mat.mainTextureOffset = offset;
    }

    void ResizeToCamera()
    {
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Screen.width / Screen.height;

        transform.localScale = new Vector3(width, height, 1f);
    }
    void OnEnable()
    {
        ScreenSizeObserver.OnScreenSizeChanged += HandleScreenResize;
    }

    void OnDisable()
    {
        ScreenSizeObserver.OnScreenSizeChanged -= HandleScreenResize;
    }

    void HandleScreenResize(Vector2Int newSize)
    {
        ResizeToCamera(); 
    }
}
