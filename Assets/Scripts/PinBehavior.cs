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
    public float start;
    //variables for dash cooldown
    public static float cooldownRate = 1.0f;
    public static float cooldown = 0.0f;
    public float timelastDashEnded;

    Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update(){
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed* Time.fixedDeltaTime);
        transform.position = newPosition;
        Dash();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with " + collided);
        if (collided == "Ball" || collided == "Wall")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverMenu");
        }
    }
    private void Dash()
    {
        if (dashing == true)
        {
            float currenttime = Time.time;
            float timeDashing = currenttime - start;
            if (timeDashing > dashDuration)
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
            }
        }
    }
}
