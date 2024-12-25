using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] GameObject explosionPrefab;

    Rigidbody2D rb;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetSpeed(Vector2 direction) {
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        var explosion = ObjectPool.Instance.GetObject(explosionPrefab);
        explosion.transform.position = transform.position;
        ObjectPool.Instance.PushObject(gameObject);
    }
}