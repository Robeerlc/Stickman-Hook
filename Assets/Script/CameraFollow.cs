using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private Rigidbody2D playerRb;
     [HideInInspector] public float offset;
     public float maxOfset;
     public float minX , maxX;
     public float orthographicSizeAfterWin = 2.5f;

    private void Start()
    {
        offset = 0;
        playerRb = player.GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z); 
    }

    private void Update()
    {
        if (playerRb.linearVelocity.x > 0)
        {
            offset += speed * Time.deltaTime * speed;
            if(offset > maxOfset)
            {
                offset = maxOfset;
            }
        }
        else if (playerRb.linearVelocity.x < 0)
         {
            offset -= speed * Time.deltaTime;
            if(offset < -maxOfset)
            {
                offset = -maxOfset;
            }
         }

         float nextX = player.transform.position.x + offset;
         if (nextX < minX) nextX = minX;
         if (nextX > maxX) nextX = maxX;

        gameObject.transform.position = new Vector3(nextX, player.transform.position.y, gameObject.transform.position.z);
         
    }
    public void Win(){
       maxOfset = 0;
       gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, gameObject.transform.position.z);
       Camera cam = GetComponent<Camera>();
       cam.orthographicSize /= orthographicSizeAfterWin;
    }

}
