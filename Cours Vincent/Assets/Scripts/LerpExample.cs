using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LerpExample : MonoBehaviour
{
    [Range(0f, 1f)]
    public float proportion = 0.5f;

    //float positionX;
    //float positionY = 0;
    //float positionZ = 5;

    public GameObject object1;
    public GameObject object2;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //exemple si on veut interpoler selon un seul axe
        //positionX = Mathf.Lerp(object1.transform.position.x, object2.transform.position.x, proportion);
        //transform.position = new Vector3(positionX, positionY, positionZ);

        //interpolation sur les trois axes
        transform.position = Vector3.Lerp(object1.transform.position, object2.transform.position, proportion);
    }
}
