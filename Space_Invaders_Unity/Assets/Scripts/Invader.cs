using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Invader : MonoBehaviour
{
    public SpriteRenderer _spriteRenderer;// { get; private set; }
    public Sprite[] animationSprites;// = new Sprite[0];
    public float animationTime = 1f;
    public int _animationFrame;// { get; private set; }
    //public int score = 10;
    public System.Action killed; // <Invader> killed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = animationSprites[0];
    }

    private void Start()
    {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    private void AnimateSprite()
    {
        _animationFrame++;

        // Loop back to the start if the animation frame exceeds the length
        if (_animationFrame >= this.animationSprites.Length) 
        {
            _animationFrame = 0;
        }

        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) 
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);       
        }
    }
}
