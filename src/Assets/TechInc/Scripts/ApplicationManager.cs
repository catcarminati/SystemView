using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ApplicationManager : MonoBehaviour
{
    public Dictionary<string,GameObject> Applications = new Dictionary<string, GameObject>();

    public GameObject App;
    public RequestObject Request;

    private List<RequestObject> Requests = new List<RequestObject>();

    public SpawnZone SpawnZone;

    public int NumRequests = 1;
    // Start is called before the first frame update
    async void Start()
    {
        // TODO get real data
        var newRequest = new RequestObject();
        newRequest.DestinationAppName = "Server";
        newRequest.CallingApplicationName = "Veyron";
        newRequest.Key = 1; // this needs to be unique

        Requests.Add(newRequest);

        var newRequest2 = new RequestObject();
        newRequest2.DestinationAppName = "AccountAPI";
        newRequest2.CallingApplicationName = "Veyron";
        newRequest2.Key = 2; // this needs to be unique

        Requests.Add(newRequest2);

        var newRequest3 = new RequestObject();
        newRequest3.DestinationAppName = "BalanceAPI";
        newRequest3.CallingApplicationName = "AccountAPI";
        newRequest3.Key = 3; // this needs to be unique

        Requests.Add(newRequest3);

        var newRequest4 = new RequestObject();
        newRequest4.DestinationAppName = "VanguardAPI";
        newRequest4.CallingApplicationName = "AccountAPI";
        newRequest4.Key = 4; // this needs to be unique

        Requests.Add(newRequest4);

        var newRequest5 = new RequestObject();
        newRequest5.DestinationAppName = "AccountAPI";
        newRequest5.CallingApplicationName = "BalanceAPI";
        newRequest5.Key = 5; // this needs to be unique

        Requests.Add(newRequest5);

        var newRequest6 = new RequestObject();
        newRequest6.DestinationAppName = "Veyron";
        newRequest6.CallingApplicationName = "AccountAPI";
        newRequest6.Key = 6; // this needs to be unique

        Requests.Add(newRequest6);

        // Spawn application game objects of the Calling / Destination applications from the requests, if the application doesn't already exist
        foreach (var request in Requests)
        {
            if(!Applications.ContainsKey(request.CallingApplicationName))          
            {
                // int x = Random.Range(Room.transform.position,360);
                // int y = Random.Range(-360,360);
                // int z = Random.Range(-360,360);
                
                //Vector3 thePosition = transform.TransformPoint(x, y, z);
                //var obj = Instantiate(App, thePosition, Quaternion.identity);
                var obj = Instantiate(App, SpawnZone.SpawnPoint, Quaternion.identity);
                obj.name = request.CallingApplicationName;

                Applications.Add(request.CallingApplicationName, obj);
            }

            if(!Applications.ContainsKey(request.DestinationAppName))
            {
                // int x = Random.Range(-360,360);
                // int y = Random.Range(-360,360);
                // int z = Random.Range(-360,360);

                // Vector3 thePosition = transform.TransformPoint(x, y, z);
                // var obj = Instantiate(App, thePosition, Quaternion.identity);
                var obj = Instantiate(App, SpawnZone.SpawnPoint, Quaternion.identity);
                obj.name = request.DestinationAppName;

                Applications.Add(request.DestinationAppName,obj);
            }
        }
    }

     Vector3 GetBottomLeftCorner(RectTransform rt)
    {
        Vector3[] v = new Vector3[4];
        rt.GetWorldCorners(v);
        return v[0];
    }

    // Update is called once per frame
    void Update()
    {
        LoadRequests(NumRequests);

        for (int i = 0; i < Requests.Count; i++)
        {
            foreach (var appItem in Applications)
            {
                if(Requests[i].CallingApplicationName == appItem.Key)
                {
                    var reqObj = Instantiate(Request, appItem.Value.transform.position, Quaternion.identity);

                    reqObj.name = Requests[i].Key.ToString();

                    foreach (var app in Applications)
                    {
                        if(Requests[i].DestinationAppName == app.Key)
                        {
                            reqObj.TargetPosition = app.Value.transform.position;
                        }
                    }
                }
            }
            Requests.RemoveAt(i);
            i--;
        }   
    }

      public void LoadRequests(int numRequests)
    {
        var randomNum1 = 0;
        var randomNum2 = 0;
        do
        {
            randomNum1 = Random.Range(0, Applications.Count);
            randomNum2 = Random.Range(0, Applications.Count);
        } 
        while (randomNum1 == randomNum2);

        for (int i = 0; i < numRequests; i++)
        {
            var newRequest = new RequestObject();
            newRequest.DestinationAppName = Applications.Keys.ElementAt(randomNum1);
            newRequest.CallingApplicationName = Applications.Keys.ElementAt(randomNum2);
            newRequest.Key = Requests.Count + 1; // this needs to be unique

            Requests.Add(newRequest);
        }
    }
}
