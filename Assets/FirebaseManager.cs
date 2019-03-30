using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFirebaseUnity;

public class FirebaseManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
        
        var firebase = Firebase.CreateNew("https://fyp-warehouse.firebaseio.com/");

        firebase.Child("Messages").Push("{ \"name\": \"simple-firebase-unity\", \"message\": \"awesome!\"}", true);

        firebase.OnDeleteSuccess += (Firebase sender, DataSnapshot snapshot) => {
            Debug.Log("[OK] Delete from " + sender.Endpoint + ": " + snapshot.RawJson);
        };

        //firebase.OnUpdateFailed += UpdateFailedHandler;
        // Method signature: void UpdateFailedHandler(Firebase sender, FirebaseError err)

        firebase.GetValue("print=pretty");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
