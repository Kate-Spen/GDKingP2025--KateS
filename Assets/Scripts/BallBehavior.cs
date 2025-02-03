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
    public float maxLaunchSpped;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public float launchDuration;
    public float timeLastLaunch;
    private void Start()
    {
        secondsToMaxSpeed = 30;
        minSpeed = 0.75f;
        maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }
    Vector2 getRandomPosition()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);
        Debug.Log("rx: " + randX + "ry: " + randY);
        Vector2 v = new Vector2(randX, randY);
        return v;
    }
    private float getDifficultypercentage()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }
    private void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }
    //private void FixedUpdate()
    //{
    //    Vector2 currentPosition = gameObject.GetComponent<Transform>().position;
    //    if (onCooldown() == false)
    //    {
    //        if (launching == true)
    //        {
    //            float currentLaunchTime = Time.time - timeLaunchStart;
    //            if (currentLaunchTime > launchDuration)
    //            {
    //                startCooldown();
    //            }
    //        }
    //        else
    //        {
    //            launch();
    //        }
    //    }
    //}
    //public void launch()
    //{
    //    targetPosition = target.transform.position;
    //        if (launching == false)
    //    {
    //        launching = true;
    //    }
    //}
}