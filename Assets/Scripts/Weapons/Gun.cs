using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gun : MonoBehaviour {
    [SerializeField] protected float interval;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject shellPrefab;

    protected Transform muzzlePos;
    protected Transform shellPos;
    protected Vector2 mousePosition;
    protected Vector2 direction;
    protected float timer;
    protected float flipY;
    protected Animator animator;
    protected Camera playerCamera;

    protected virtual void Start() {
        animator = GetComponent<Animator>();
        muzzlePos = transform.Find("Muzzle");
        shellPos = transform.Find("BulletShell");
        flipY = transform.localScale.y;
        playerCamera = Camera.main;
    }

    protected virtual void Update() {
        // get mouse current position
        mousePosition = playerCamera.ScreenToWorldPoint(Input.mousePosition);

        // weapon cooldown
        if (timer != 0) {
            timer -= Time.deltaTime;
            if (timer <= 0)
                timer = 0;
        }

        // flip weapon based on mouse position
        if (mousePosition.x < transform.position.x)
            transform.localScale = new Vector3(flipY, -flipY, 1);
        else
            transform.localScale = new Vector3(flipY, flipY, 1);

        HandleFire();
    }

    protected virtual void HandleFire() {
        direction = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.right = direction;

        if (Input.GetButton("Fire1")) {
            if (timer == 0) {
                timer = interval;
                Fire();
            }
        }
    }

    protected virtual void Fire() {
        animator.SetTrigger("Shoot");

        // GameObject bullet = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);
        GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
        bullet.transform.position = muzzlePos.position;

        float angel = Random.Range(-5f, 5f);
        bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(angel, Vector3.forward) * direction);

        // Instantiate(shellPrefab, shellPos.position, shellPos.rotation);
        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }
}