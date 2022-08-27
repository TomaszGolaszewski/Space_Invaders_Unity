using UnityEngine;

public class Invaders : MonoBehaviour
{

    [Header("Invaders")]
    public Invader[] prefabs; // = new Invader[4];

    //public AnimationCurve speed = new AnimationCurve();
    public float speed = 0.3f;
    public Vector3 _direction = Vector3.right; //{ get; private set; }
    /*public Vector3 initialPosition { get; private set; }
    public System.Action<Invader> killed;

    public int AmountKilled { get; private set; }
    public int AmountAlive => TotalAmount - AmountKilled;
    public int TotalAmount => rows * columns;
    public float PercentKilled => (float)AmountKilled / (float)TotalAmount;
    */
    [Header("Grid")]
    public int rows = 4;
    public int columns = 8;
    /*
    [Header("Missiles")]
    public Projectile missilePrefab;
    public float missileSpawnRate = 1f;
    */
    private void Awake()
    {
        //initialPosition = transform.position;

        // Form the grid of invaders
        for (int row = 0; row < this.rows; row++)
        {
            float width = 0.5f * (this.columns - 1);
            float height = 0.5f * (this.rows - 1);

            Vector2 centerOffset = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(centerOffset.x, (0.5f * row) + centerOffset.y, 0f);

            for (int col = 0; col < this.columns; col++)
            {
                // Create an invader and parent it to this transform
                Invader invader = Instantiate(this.prefabs[row], this.transform);
                /*
                invader.killed += OnInvaderKilled;
*/
                // Calculate and set the position of the invader in the row
                Vector3 position = rowPosition;
                position.x += 0.5f * col;
                invader.transform.localPosition = position;              
            }
        }
    }
    /*
    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), missileSpawnRate, missileSpawnRate);
    }

    private void MissileAttack()
    {
        int amountAlive = AmountAlive;

        // No missiles should spawn when no invaders are alive
        if (amountAlive == 0) {
            return;
        }

        foreach (Transform invader in transform)
        {
            // Any invaders that are killed cannot shoot missiles
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }

            // Random chance to spawn a missile based upon how many invaders are
            // alive (the more invaders alive the lower the chance)
            if (Random.value < (1f / (float)amountAlive))
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }
    */
    private void Update()
    {
        // Evaluate the speed of the invaders based on how many have been killed
        //float speed = this.speed.Evaluate(PercentKilled);
        this.transform.position += _direction * speed * Time.deltaTime;

        // Transform the viewport to world coordinates so we can check when the
        // invaders reach the edge of the screen
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        // The invaders will advance to the next row after reaching the edge of
        // the screen
        foreach (Transform invader in this.transform)
        {
            // Skip any invaders that have been killed
            if (!invader.gameObject.activeInHierarchy) 
            {
                continue;
            }

            // Check the left edge or right edge based on the current direction
            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 0.5f))
            {
                AdvanceRow();
                break;
            }
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 0.5f))
            {
                AdvanceRow();
                break;
            }  
        }
    }
    
    private void AdvanceRow()
    {
        // Flip the direction the invaders are moving
        //_direction = new Vector3(-_direction.x, 0f, 0f);
        _direction *= -1.0f;

        // Move the entire grid of invaders down a row
        Vector3 position = this.transform.position;
        position.y -= 0.1f;
        this.transform.position = position;
    }
/*
    private void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);
        AmountKilled++;
        killed(invader);
    }

    public void ResetInvaders()
    {
        AmountKilled = 0;
        direction = Vector3.right;
        transform.position = initialPosition;

        foreach (Transform invader in transform) {
            invader.gameObject.SetActive(true);
        }
    }
    */
}
