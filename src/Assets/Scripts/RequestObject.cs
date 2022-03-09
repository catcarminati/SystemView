using UnityEngine;

public class RequestObject : MonoBehaviour
{
    public int Key;
    public string CallingApplicationName;
    public string DestinationAppName;
    public Vector3 TargetPosition;
    public float speed = 10;
    public bool IsProcessed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TargetPosition != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPosition, speed * Time.deltaTime);
        }
    }
}
