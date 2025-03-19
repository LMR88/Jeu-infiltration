using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;
    [SerializeField]
    private float fightSpeed;
    [SerializeField]
    private float crawlSpeed;
    [SerializeField]
    private Transform CameraReferenceTransform;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField] 
    private GameObject colliderHead;
    [SerializeField] 
    private GameObject triggerAttack;
    private bool crouch = false;
    private bool run;
    private bool fight;
    private bool crawl;
    private bool canMove = true;
    private float originalSpeed;
    [SerializeField] int damages = 30;
    

    private Rigidbody rbComponent;
    private Animator animatorComponent;
    void Start()
    {
        originalSpeed = moveSpeed;
        rbComponent = GetComponent<Rigidbody>();
        if (rbComponent == null)
        {
            Debug.LogWarning("No rb detected on" + gameObject.name);
        }
        

        animatorComponent = GetComponent<Animator>();
        if (animatorComponent == null)
        {
            Debug.LogWarning("No animator detected on" + gameObject.name);
        }
    }
    void Update()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 camForward = CameraReferenceTransform.forward;
        Vector3 deplacement = verticalInput * camForward + horizontalInput * CameraReferenceTransform.right;
        deplacement.y = 0;
        if (canMove)
        {
            Vector3 deplacementFinal = Vector3.ClampMagnitude(deplacement,1) * moveSpeed;
            rbComponent.velocity = new Vector3(deplacementFinal.x, rbComponent.velocity.y, deplacementFinal.z);
        }
        if (deplacement != Vector3.zero)
        {
            rbComponent.rotation = Quaternion.Lerp(transform.rotation,Quaternion.LookRotation(deplacement), Time.deltaTime * rotationSpeed);
        }
        if (horizontalInput != 0 || verticalInput != 0)
        {
            animatorComponent.SetBool("isWalking", true);
        }
        else 
        {
            animatorComponent.SetBool("isWalking", false);
            animatorComponent.SetBool("isRunning", false);
        }
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            fight = !fight;
        }
        if (fight)
        {
            animatorComponent.SetBool("isFighting", true);
            moveSpeed = fightSpeed;
            if (Input.GetMouseButtonDown(0))
            {
                animatorComponent.SetBool("isPunch", true);
                canMove = false;
                StartCoroutine(WaitForAttack());
                StartCoroutine(TriggerActivation());
            }
            else if (Input.GetMouseButtonDown(1))
            {
                animatorComponent.SetBool("isCombo", true);
                canMove = false;
                StartCoroutine(WaitForAttack());
                StartCoroutine(TriggerActivation());
            }
        }
        else
        {
            animatorComponent.SetBool("isFighting", false);
            moveSpeed = originalSpeed;
        
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                run = true;
                animatorComponent.SetBool("isRunning", true);
                moveSpeed = runSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                run = false;
                animatorComponent.SetBool("isRunning", false);
                moveSpeed = originalSpeed;
            }
        
            if (Input.GetKeyDown(KeyCode.V))
            {
                crouch = !crouch;
            }

            if (crouch)
            {
                animatorComponent.SetBool("isCrouch", true);
                moveSpeed = crouchSpeed;
                colliderHead.SetActive(false);
            }
            else 
            {
                animatorComponent.SetBool("isCrouch", false);
                colliderHead.SetActive(true);
                if (run)
                {
                    moveSpeed = runSpeed;
                }
                else
                {
                    moveSpeed = originalSpeed;
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                crawl = !crawl;
            }

            if (crawl)
            {
                animatorComponent.SetBool("isCrawl", true);
                moveSpeed = crawlSpeed;
                colliderHead.SetActive(false);
            }
            else 
            {
                animatorComponent.SetBool("isCrawl", false);
                colliderHead.SetActive(true);
                if (run)
                {
                    moveSpeed = runSpeed;
                }
                else
                {
                    moveSpeed = originalSpeed;
                }
            }
            if (Input.GetMouseButtonDown(0) && canMove)
            {
                animatorComponent.SetBool("isKilling", true);
                canMove = false;
                StartCoroutine(WaitForAnim("isKilling"));
            }
        }
        
    }
    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(0.5f);
        animatorComponent.SetBool("isPunch", false);
        animatorComponent.SetBool("isCombo", false);
        yield return new WaitForSeconds(1.2f);
        canMove = true;
    }
    IEnumerator TriggerActivation()
    {
        triggerAttack.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        triggerAttack.SetActive(false);
    }

    IEnumerator WaitForAnim(string anim)
    {
        yield return new WaitForSeconds(0.5f);
        animatorComponent.SetBool(anim, false);
        yield return new WaitForSeconds(2f);
        canMove = true;
        
    }
}