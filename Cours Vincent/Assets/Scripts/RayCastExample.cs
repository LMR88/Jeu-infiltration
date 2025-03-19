using UnityEngine;

public class RayCastExample : MonoBehaviour
{
    public float rayLength = 10f;
    public Color rayColor = Color.green;
    public Color hitColor = Color.red;
    public GameObject raycastOrigin;
    public Vector3 direction;

    void Update()
    {
        Ray ray = new Ray(raycastOrigin.transform.position, direction);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.name == "Player")
            {
                Debug.Log("Objet touche : " + hit.collider.gameObject.name);
                GetComponent<Renderer>().material.color = hitColor;

                // Rayon debug qui touche un objet
                Debug.DrawRay(raycastOrigin.transform.position, direction * hit.distance, hitColor);
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.grey;
                // Rayon debug sans impact
                Debug.DrawRay(raycastOrigin.transform.position, direction * rayLength, rayColor);
            }
        }
    }
}
