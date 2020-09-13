using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public GameObject UI_VRMenuGameobject;
    // Start is called before the first frame update
    void Start()
    {
        UI_VRMenuGameobject.SetActive(false); 
    }

    public void OnWorldsButtonClicked()
    {
    	Debug.Log("Worlds Button is Clicked");
    }
    public void OnGoHomeButtonClicked()
    {
    	Debug.Log("Go Home Button is Clicked");
    }    
    public void OnChangeAvatarButtonClicked()
    {
    	Debug.Log("Change Avatar Button is Clicked");
    }   
}
