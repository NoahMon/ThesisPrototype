//by EvolveGames
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace EvolveGames
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("PlayerController")]
        [SerializeField] public Transform Camera;
        [SerializeField] public ItemChange Items;
        [SerializeField, Range(1, 10)] float walkingSpeed = 3.0f;
        [Range(0.1f, 5)] public float CroughSpeed = 1.0f;
        [SerializeField, Range(2, 20)] float RuningSpeed = 4.0f;
        [SerializeField, Range(0, 20)] float jumpSpeed = 6.0f;
        [SerializeField, Range(0.5f, 10)] float lookSpeed = 2.0f;
        [SerializeField, Range(10, 120)] float lookXLimit = 80.0f;
        [Space(20)]
        [Header("Advance")]
        [SerializeField] float RunningFOV = 65.0f;
        [SerializeField] float SpeedToFOV = 4.0f;
        [SerializeField] float CroughHeight = 1.0f;
        [SerializeField] float gravity = 20.0f;
        [SerializeField] float timeToRunning = 2.0f;
        [HideInInspector] public bool canMove = true;
        [HideInInspector] public bool CanRunning = true;

        [Space(20)]
        [Header("Climbing")]
        [SerializeField] bool CanClimbing = true;
        [SerializeField, Range(1, 25)] float Speed = 2f;
        bool isClimbing = false;

        [Space(20)]
        [Header("HandsHide")]
        [SerializeField] bool CanHideDistanceWall = true;
        [SerializeField, Range(0.1f, 5)] float HideDistance = 1.5f;
        [SerializeField] int LayerMaskInt = 1;

        [Space(20)]
        [Header("Input")]
        [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;


        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        bool isCrough = false;
        float InstallCroughHeight;
        float rotationX = 0;
        [HideInInspector] public bool isRunning = false;
        Vector3 InstallCameraMovement;
        float InstallFOV;
        Camera cam;
        [HideInInspector] public bool Moving;
        [HideInInspector] public float vertical;
        [HideInInspector] public float horizontal;
        [HideInInspector] public float Lookvertical;
        [HideInInspector] public float Lookhorizontal;
        float RunningValue;
        float installGravity;
        bool WallDistance;
        [HideInInspector] public float WalkingValue;
        bool hammerCheck =false;

        private GameObject collidedObject = null;
        private GameObject Hammer;
        public GameObject LightsOff;
        public GameObject LightsOn;
        public GameObject Teleport;
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            if (Items == null && GetComponent<ItemChange>()) Items = GetComponent<ItemChange>();
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            InstallCroughHeight = characterController.height;
            InstallCameraMovement = Camera.localPosition;
            InstallFOV = cam.fieldOfView;
            RunningValue = RuningSpeed;
            installGravity = gravity;
            WalkingValue = walkingSpeed;
            Hammer = GameObject.FindGameObjectWithTag("ActualHammer");
            Hammer.SetActive(false);
            LightsOff.SetActive(false);
            LightsOn.SetActive(false);
        }

        void Update()
        {
            RaycastHit CroughCheck;
            RaycastHit ObjectCheck;

            if (!characterController.isGrounded && !isClimbing)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            isRunning = !isCrough ? CanRunning ? Input.GetKey(KeyCode.LeftShift) : false : false;
            vertical = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Vertical") : 0;
            horizontal = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Horizontal") : 0;
            if (isRunning) RunningValue = Mathf.Lerp(RunningValue, RuningSpeed, timeToRunning * Time.deltaTime);
            else RunningValue = WalkingValue;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * vertical) + (right * horizontal);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isClimbing)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }
            characterController.Move(moveDirection * Time.deltaTime);
            Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

            if (Cursor.lockState == CursorLockMode.Locked && canMove)
            {
                Lookvertical = -Input.GetAxis("Mouse Y");
                Lookhorizontal = Input.GetAxis("Mouse X");

                rotationX += Lookvertical * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

                if (isRunning && Moving) cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
                else cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
            }

            if (Input.GetKey(CroughKey))
            {
                isCrough = true;
                float Height = Mathf.Lerp(characterController.height, CroughHeight, 5 * Time.deltaTime);
                characterController.height = Height;
                WalkingValue = Mathf.Lerp(WalkingValue, CroughSpeed, 6 * Time.deltaTime);

            }
            else if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.up), out CroughCheck, 0.8f, 1))
            {
                if (characterController.height != InstallCroughHeight)
                {
                    isCrough = false;
                    float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 6 * Time.deltaTime);
                    characterController.height = Height;
                    WalkingValue = Mathf.Lerp(WalkingValue, walkingSpeed, 4 * Time.deltaTime);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Detection"))
            {
                QuestionPopUp.Instance.popUpPanel.SetActive(true);
                QuestionPopUp.Instance.DisplayQuestion();
                QuestionPopUp.Instance.ToggleCursorState(true);
                QuestionPopUp.Instance.ToggleFPSController(false);
                collidedObject = other.gameObject;
            }
            if (other.CompareTag("Speed Up"))
            {
                Destroy(other.gameObject);
                StartCoroutine(WalkFast());
            }
            if (other.CompareTag("Hammer"))
            {
                Destroy(other.gameObject);
                StartCoroutine(DestroyWall());
            }
            if (other.CompareTag("Wall") && hammerCheck == true)
            {
                StartCoroutine(DestroyWallDelayed(other.gameObject));
            }
            if (other.CompareTag("Flash"))
            {
                LightsOn.SetActive(true);
                Destroy(other.gameObject);
            }
            if (other.CompareTag("Lights Out"))
            {
                LightsOff.SetActive(true);
                Destroy(other.gameObject);
            }
            if (other.CompareTag("Slow Walk"))
            {
                StartCoroutine(Freeze());
                Destroy(other.gameObject);
            }
        }
        IEnumerator DestroyWall()
        {
            hammerCheck = true;
            Hammer.SetActive(true);

            yield return new WaitForSeconds(10);

            hammerCheck = false;
            Hammer.SetActive(false);
        }
        IEnumerator DestroyWallDelayed(GameObject wall)
        {
            GameObject Particles = wall.transform.Find("DustExplosion").gameObject;
            Particles.SetActive(true);
            Debug.Log("Wall down");

            yield return new WaitForSeconds(1);

            Destroy(wall);
        }
        IEnumerator WalkFast()
        {
            WalkingValue = 3f;
            yield return new WaitForSeconds(5);
            WalkingValue = 1.25f;
        }
        IEnumerator Freeze()
        {
            WalkingValue = 0.5f;
            Debug.Log("Not Sonic");
            yield return new WaitForSeconds(7);
            WalkingValue = 1.25f;
            Debug.Log("Sonic");
        }
        private void OnTriggerExit(Collider other)
        {
            if (collidedObject != null && other.gameObject == collidedObject)
            {
                Destroy(collidedObject);
                collidedObject = null; 
            }
        }
    }
}