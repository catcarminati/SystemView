using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ApplicationManager : MonoBehaviour
{
    public List<GameObject> Applications = new List<GameObject>();
    public List<string> ApplicationNames = new List<string>();

    public GameObject App;
    public RequestObject Request;
    private List<RequestObject> Requests = new List<RequestObject>();
    // Start is called before the first frame update
    async void Start()
    {
        // TODO get real data
        var newRequest = new RequestObject();
        newRequest.DestinationAppName = "Server";
        newRequest.CallingApplicationName = "Client";
        newRequest.Key = 1; // this needs to be unique

        Requests.Add(newRequest);

        var newRequest2 = new RequestObject();
        newRequest2.DestinationAppName = "Client";
        newRequest2.CallingApplicationName = "Server";
        newRequest2.Key = 2; // this needs to be unique

        Requests.Add(newRequest2);

        var x = 0;
        int y = 1;

        foreach (var request in Requests)
        {

            if(!ApplicationNames.Contains(request.CallingApplicationName))
            {
                var obj = Instantiate(App, new Vector3(x,y,0), Quaternion.identity);
                obj.name = request.CallingApplicationName;
                x += 15;

                Applications.Add(obj);
                ApplicationNames.Add(request.CallingApplicationName);
            }

            if(!ApplicationNames.Contains(request.DestinationAppName))
            {
                var obj = Instantiate(App, new Vector3(x,y,0), Quaternion.identity);
                obj.name = request.DestinationAppName;
                x += 15;

                Applications.Add(obj);
                ApplicationNames.Add(request.DestinationAppName);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Requests.Count; i++)
        {
            if(!Requests[i].IsProcessed)
            {
                foreach (var appItem in Applications)
                {
                    if(Requests[i].CallingApplicationName == appItem.name)
                    {
                        var reqObj = Instantiate(Request, new Vector3(appItem.transform.position.x,
                        appItem.transform.position.y,0),Quaternion.identity);
                        reqObj.name = Requests[i].Key.ToString();

                        foreach (var app in Applications)
                        {
                            if(Requests[i].DestinationAppName == app.name)
                            {
                                reqObj.TargetPosition = app.transform.position;
                            }
                        }
                    }
                }
                Requests.RemoveAt(i);
                i--;
            }
        }   
    }
}
