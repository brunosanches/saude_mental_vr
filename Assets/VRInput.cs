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
    public RayInteractor interactor;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) // Replace "Fire1" with your VR controller input
        {
            //Ray ray = new Ray(vrCamera.transform.position, vrCamera.transform.forward);
            RaycastHit hit;

            Debug.Log("Cliquei trigger");
            
            if (Physics.Raycast(interactor.Ray, out hit, maxRayDistance, layerMask))
            {
                Button button = hit.collider.GetComponent<Button>();
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
