using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Projectile laserPrefab;
    // public System.Action killed;
    private bool _laserActive; // { get; private set; }
    private float timeOfLastShoot = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeOfLastShoot = Time.time - 1f;
    }

    private void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || LeftButton.leftPressed) 
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || RightButton.rightPressed) 
        {
            position.x += speed * Time.deltaTime;
        }

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Clamp the position of the character so they do not go out of bounds
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);
        transform.position = position;
        /*
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            Shoot();
            //Debug.Log("0");
        }
        */
        if(timeOfLastShoot + 2f < Time.time)
        {
            Shoot();
        }
    }
    
    private void Shoot()
    {    
        // Only one laser can be active at a given time so first check that
        // there is not already an active laser
        if (!_laserActive)
        {         
            Projectile laser = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            laser.destroyed += LaserDestroyed;
            _laserActive = true;
            timeOfLastShoot = Time.time;
        }
        
    }

    private void LaserDestroyed() // Projectile laser)
    {
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") ||
            other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            //SceneManager.LoadScene(0); //Main Menu
            SceneManager.LoadScene(2); //Summary
            /*if (killed != null) {
                killed.Invoke();
            }*/
        }
    }
    
}
