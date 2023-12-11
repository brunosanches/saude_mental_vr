using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems; // Required for Event System
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class VRInput : MonoBehaviour
{
    public Camera vrCamera;
    public EventSystem eventSystem;
    public float maxRayDistance = 100.0f;
    public LayerMask layerMask; // Set this to the layer your button is on
    public RayInteractor interactor1;
    public RayInteractor interactor2;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) // Replace "Fire1" with your VR controller input
        {
            //Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
            RaycastHit hit1;
            RaycastHit hit2;

            Debug.Log("Cliquei trigger");
            Debug.Log(Physics.Raycast(interactor1.Ray, out hit1, maxRayDistance, layerMask));
            if (Physics.Raycast(interactor1.Ray, out hit1, maxRayDistance, layerMask))
            {
                Button button = hit1.collider.GetComponent<Button>();
                Debug.Log("Ray no button:");
                Debug.Log(button);
                if (!button.IsUnityNull())
                {
                    Debug.Log("Cliquei no button");
                    button.onClick.Invoke();
                }
            }
            else
            {
                Debug.Log(Physics.Raycast(interactor2.Ray, out hit2, maxRayDistance, layerMask));
                if (Physics.Raycast(interactor2.Ray, out hit2, maxRayDistance, layerMask))
                {
                    Button button = hit2.collider.GetComponent<Button>();
                    Debug.Log("Ray no button:");
                    Debug.Log(button);
                    if (!button.IsUnityNull())
                    {
                        Debug.Log("Cliquei no button");
                        button.onClick.Invoke();
                    }
                }
            }

           

        }
    }
}
