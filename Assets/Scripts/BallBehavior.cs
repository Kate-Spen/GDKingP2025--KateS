using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;
    public int secondsToMaxSpeed;
    Vector2 targetPosition;
    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public float launchDuration;
    public float timeLastLaunch;
    Rigidbody2D body;
    public bool rerouting;
    public bool launching;
    public float timeLaunchStart;
    private void Start()
    {
        rerouting = false;
        body = GetComponent<Rigidbody2D>();
        initialPosition();
    }
    Vector2 getRandomPosition()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        //Debug.Log("rx: " + randX + "ry: " + randY);
        Vector2 v = new Vector2(randX, randY);
        return v;
    }
    private float getDifficultypercentage()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Wall")
        {
            targetPosition = getRandomPosition();
        }
       if(collision.gameObject.tag == "Ball")
        {
            Reroute(collision);
        }
    }
    public void initialPosition()
    {
        transform.position = getRandomPosition();
        targetPosition = getRandomPosition();
        launching = false;
    }
    public void Reroute(Collision2D collision)
    {
        GameObject otherBall = collision.gameObject;
        if(rerouting == true)
        {
            otherBall.GetComponent<BallBehavior>().rerouting = false;
            Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            targetPosition = Vector2.Reflect(targetPosition, contact).normalized;
            launching = false;
            float separationDistance = 0.1f;
            body.position += contact * separationDistance;
        }
        else
        {
            rerouting = true;
        }
    }
    private void FixedUpdate()
    {
        body = GetComponent<Rigidbody2D>();
        Vector2 currentPosition = body.position;
        if (onCooldown() == false)
        {
            if (launching == true)
            {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration)
                {
                    startCooldown();
                }
            }
            else
            {
                launch();
            }
            Vector2 currentPos = body.position;
            float distance = Vector2.Distance(currentPos, targetPosition);
            if (distance > 0.1)
            {
                float difficulty = getDifficultyPercentage();
                float currentSpeed;
                if (launching == true)
                {
                    float launchingForHowLong = Time.time - timeLaunchStart;
                    if (launchingForHowLong > launchDuration)
                    {
                        startCooldown();
                    }
                    currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed,
                    difficulty);
                }
                else
                {
                    currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
                }
                Debug.Log("tdt " + Time.deltaTime);
                Debug.Log("csb " + currentSpeed);
                currentSpeed = currentSpeed * Time.deltaTime;
                Debug.Log("csa " + currentSpeed);
                Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition,
                currentSpeed);
                //transform.position = newPosition;
                body.MovePosition(newPosition);

            }
            else
            { // You are at target
                if (launching == true)
                {
                    startCooldown();
                }
                targetPosition = getRandomPosition();
            }
        }
    }
    public float getDifficultyPercentage()
    {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad /
        secondsToMaxSpeed);
        return difficulty;
    }

    public void launch()
    {
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;
        if (launching == false)
        {
            timeLaunchStart = Time.time;
            launching = true;
        }
    }
    public bool onCooldown()
    {
        bool result = false;
        float timeSinceLastLaunch = Time.time - timeLastLaunch;
        if (timeSinceLastLaunch < cooldown)
        {
            result = true;
        }
        return result;
    }
    public void startCooldown()
    {
        timeLastLaunch = Time.time;
        launching = false;
    }
    public void setBounds(float miX, float maX, float miY, float maY)
    {
        minX = miX;
        maxX = maX;
        minY = miY;
        maxY = maY;
    }
    public void setTarget(GameObject pin)
    {
        target = pin;
    }
}