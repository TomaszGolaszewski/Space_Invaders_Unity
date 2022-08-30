using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Projectile laserPrefab;
    private bool _laserActive;
    private float timeOfLastShoot = 0;

    // Start is called before the first frame update
    void Start()
    {
        timeOfLastShoot = Time.time - 1f;
    }

    private void Update()
    {
        Vector3 position = transform.position;

        // Move left
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || LeftButton.leftPressed) 
        {
            position.x -= speed * Time.deltaTime;
        }
        // Move right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || RightButton.rightPressed) 
        {
            position.x += speed * Time.deltaTime;
        }

        // Check edges
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // Clamp the position of the character so they do not go out of bounds
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);
        transform.position = position;

        // Shot one laser per 2s
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

    private void LaserDestroyed()
    {
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Collision with Invaders - go to Summary scene
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            SceneManager.LoadScene(2); //Summary
        }

        // Collision with Invader' missile - subtract score
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            //SceneManager.LoadScene(2); //Summary
            Interface.currentScore -= 4;
            if (Interface.currentScore < 0) Interface.currentScore = 0;
        }
    }  
}
