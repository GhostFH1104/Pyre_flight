using UnityEngine;
using UnityEngine.InputSystem;

public class Bird_script : MonoBehaviour
{
    public Rigidbody2D birdbody;

    public float FlapStrength;

    public Counter_script counter;

    public bool birdisdead = false;

    public Animator animator;

    public SpriteRenderer Birdsprite;

    public Sprite DeadBirdSprite;
    Audio_manager audioManager;
    public InputActionProperty flapAction;

    private bool gameStarted = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_manager>();
    }

    void Start()
    {
        counter = GameObject.FindGameObjectWithTag("Counter_logic").GetComponent<Counter_script>();

        Time.timeScale = 0f;
        gameStarted = false;
    }

    void Update()
    {
        if (!gameStarted)
        {
            bool anyPressed = (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
                           || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
                           || (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                           || (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame)
                           || (flapAction.action != null && flapAction.action.WasPressedThisFrame());

            if (anyPressed)
            {
                gameStarted = true;
                Time.timeScale = 1f;
            }
            else
            {
                return;
            }
        }

        if (Keyboard.current != null && Keyboard.current.spaceKey.isPressed && !birdisdead)
        {
            birdbody.linearVelocity = new Vector2(0, 1) * FlapStrength;
            animator.SetBool("Flapping", true);
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed && !birdisdead)
        {
            birdbody.linearVelocity = new Vector2(0, 1) * FlapStrength;
            animator.SetBool("Flapping", true);
        }
        else if (Gamepad.current != null && Gamepad.current.aButton.isPressed && !birdisdead)
        {
            birdbody.linearVelocity = new Vector2(0, 1) * FlapStrength;
            animator.SetBool("Flapping", true);
        }
        else if (flapAction.action != null && flapAction.action.IsPressed() && !birdisdead)
        {
            birdbody.linearVelocity = new Vector2(0, 1) * FlapStrength;
            animator.SetBool("Flapping", true);
        }
        else
        {
            animator.SetBool("Flapping", false);
        }
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !birdisdead)
        {
            audioManager.PlaySFX(audioManager.flap);
        }
        else if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame && !birdisdead)
        {
            audioManager.PlaySFX(audioManager.flap);
        }
        else if (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame && !birdisdead)
        {
            audioManager.PlaySFX(audioManager.flap);
        }
        else if (flapAction.action != null && flapAction.action.WasPressedThisFrame() && !birdisdead)
        {
            audioManager.PlaySFX(audioManager.flap);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            birdisdead = true;
            counter.Gameover();
            animator.SetBool("Flapping", false);
            animator.enabled = false;
            Birdsprite.sprite = DeadBirdSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Death"))
        {
            birdisdead = true;
            counter.Gameover();

            animator.SetBool("Flapping", false);
            animator.enabled = false;

            Birdsprite.sprite = DeadBirdSprite;
        }
    }
}