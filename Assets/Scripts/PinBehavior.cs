using System.Collections;
using UnityEngine;

public class PinBehavior : MonoBehaviour
{
    public float speed;
    public float baseSpeed = 2.0f;
    public float dashSpeed = 5.0f;
    public bool dashing;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    public float dashDuration;
    public float timeDashStart;
    public float start;
    //variables for dash cooldown
    public static float cooldownRate = 1.0f;
    public static float cooldown = 0.0f;
    public float timelastDashEnded;
    Rigidbody2D body;
    public AudioSource[] audioSources;

    Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        cam = Camera.main;
        body = GetComponent<Rigidbody2D>();
        dashing = false;
        audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Dash();
    }
    void FixedUpdate(){
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed* Time.fixedDeltaTime);
        body.MovePosition(newPosition);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;
        if (collided == "Ball" || collided == "Wall")
        {
            StartCoroutine(WaitForSoundAndTransition());
        }
    }
    private IEnumerator WaitForSoundAndTransition()
    {
        audioSources[0].Play();
        yield return new WaitForSeconds(audioSources[0].clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
    private void Dash()
    {
        if (dashing == true)
        {
            float howLong = Time.time - timeDashStart;
            if (howLong > dashDuration)
            {
                dashing = false;
                speed = baseSpeed;
                cooldown = cooldownRate;
            }
        }
        else
        {
            cooldown = cooldown - Time.deltaTime;
            if(cooldown < 0.0)
            {
                cooldown = 0.0f;
            }
            if (Input.GetMouseButtonDown(0) == true && cooldown == 0.0)
            {
                dashing = true;
                speed = dashSpeed;
                start = Time.time;
                if (audioSources[1].isPlaying)
                {
                    audioSources[1].Stop();
                }
                audioSources[1].Play();
            }
        }
    }
}
