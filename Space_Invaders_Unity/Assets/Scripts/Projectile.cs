using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public Vector3 direction = Vector3.up;
    
    public System.Action destroyed; // <Projectile> destroyed;
    /*public new BoxCollider2D collider { get; private set; }

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnDestroy()
    {
        if (destroyed != null) {
            destroyed.Invoke(this);
        }
    }
*/
    private void Update()
    {
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.destroyed.Invoke();
        Destroy(this.gameObject);
    }

    /*
    private void CheckCollision(Collider2D other)
    {
        Bunker bunker = other.gameObject.GetComponent<Bunker>();

        if (bunker == null || bunker.CheckCollision(collider, transform.position)) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckCollision(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        CheckCollision(other);
    }
        */
}
