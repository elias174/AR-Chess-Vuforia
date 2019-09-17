using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vb_anim : MonoBehaviour, IVirtualButtonEventHandler
{
    // Start is called before the first frame update
    private GameObject vbuttonObject;
    private GameObject piece;

    void Start()
    {
        piece = GameObject.Find("Rook");
        vbuttonObject = GameObject.Find("VirtualButton");
        vbuttonObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb) {
        Debug.Log("Button down");
        piece.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("Button realesead");
    }
}
