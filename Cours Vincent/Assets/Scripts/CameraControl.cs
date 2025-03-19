using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Transform cible;
    [SerializeField]
    private float vitesseRotation = 2;
    [SerializeField]
    private float hauteur = 2;
    [SerializeField]
    private float distance = 5;

    private float rotationX = 0;
    private float rotationY = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * vitesseRotation;
        rotationX -= Input.GetAxis("Mouse Y") * vitesseRotation;

        rotationX = Mathf.Clamp(rotationX, -30, 30);

        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        transform.rotation = rotation;

        Vector3 position = cible.position + rotation * new Vector3(0, 0, -distance) + new Vector3(0, hauteur, 0);
        transform.position = position;

    }
}
