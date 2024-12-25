using System.Collections;
using UnityEngine;

public class BulletShell : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] float duration = 0.5f;
    [SerializeField] float fadeSpeed = 0.01f;

    Rigidbody2D rb;
    SpriteRenderer sprite;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        // ejection speed and angle
        float angel = Random.Range(-30f, 30f);
        rb.linearVelocity = Quaternion.AngleAxis(angel, Vector3.forward) * Vector3.up * speed;
        rb.gravityScale = 3;

        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);

        StartCoroutine(Stop());
    }

    IEnumerator Stop() {
        yield return new WaitForSeconds(duration);
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0;

        while (sprite.color.a > 0) {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.g, sprite.color.a - fadeSpeed);
            yield return new WaitForFixedUpdate();
        }
        ObjectPool.Instance.PushObject(gameObject);
    }
}