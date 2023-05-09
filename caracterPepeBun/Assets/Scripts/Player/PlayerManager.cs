using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    Animator animator;
    CameraHandler cameraHandler;
    PlayerStatsManager playerStatsManager;
    PlayerEffectsManager playerEffectsManager;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerLocomotionManager playerLocomotionManager;
    
    

    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableGameObject;

    protected override void Awake() {
        base.Awake();
        cameraHandler = FindObjectOfType<CameraHandler>();
        backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
        inputHandler = GetComponent<InputHandler>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        animator = GetComponent<Animator>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        interactableUI = FindObjectOfType<InteractableUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraHandler = CameraHandler.singleton;
        
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = animator.GetBool("isInteracting");
        canDoCombo = animator.GetBool("canDoCombo");
        isUsingRightHand = animator.GetBool("isUsingRightHand");
        isUsingLeftHand = animator.GetBool("isUsingLeftHand");
        isInvulnerable = animator.GetBool("isInvulnerable");
        isFiringSpell = animator.GetBool("isFiringSpell");
        animator.SetBool("isTwoHandingWeapon", isTwoHandingWeapon);
        animator.SetBool("isBlocking", isBlocking);
        animator.SetBool("isInAir", isInAir);
        animator.SetBool("isDead", playerStatsManager.isDead);
        
        inputHandler.TickInput(delta);
        playerAnimatorManager.canRotate = animator.GetBool("canRotate");
        playerLocomotionManager.HandleRollingAndSprinting(delta);
        playerLocomotionManager.HandleJumping();
        
        playerStatsManager.RegenerateStamina();
        
        CheckForInteractableObject();
    }

    protected override void FixedUpdate() {
        base.FixedUpdate();
        float delta = Time.fixedDeltaTime;

        playerLocomotionManager.HandleMovement(delta);
        playerLocomotionManager.HandleFalling(delta, playerLocomotionManager.moveDirection);
        playerLocomotionManager.HandleRotation(delta);
        playerEffectsManager.HandleAllBuildUpEffects();

    }

    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        //inputHandler.sprintFlag = false;
        inputHandler.lightAttack_Input = false;
        inputHandler.heavyAttack_Input = false;
        inputHandler.parry_Input = false;
        inputHandler.d_Pad_Down = false;
        inputHandler.d_Pad_Left = false;
        inputHandler.d_Pad_Right = false;
        inputHandler.d_Pad_Up = false;
        inputHandler.examine_Input = false;
        inputHandler.jump_Input = false;
        inputHandler.inventory_Input = false;
        //isSprinting = inputHandler.b_Input;

        float delta = Time.deltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }

        if (isInAir)
        {
            playerLocomotionManager.inAirTimer = playerLocomotionManager.inAirTimer + Time.deltaTime;
        }
    }

    #region Player Interactions

    public void CheckForInteractableObject()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();
                if (interactableObject != null)
                {
                    string interactableText = interactableObject.interactableText;
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);

                    if (inputHandler.examine_Input)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if (interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if(itemInteractableGameObject != null && inputHandler.examine_Input)
            {
                itemInteractableGameObject.SetActive(false);
            }
        }
    }

    public void OpenChestInteraction(Transform playerStandsHereWhenOpeningChest)
    {
        playerLocomotionManager.rigidbody.velocity = Vector3.zero; // stops the player form ice skating
        transform.position = playerStandsHereWhenOpeningChest.transform.position;
        playerAnimatorManager.PlayTargetAnimation("Open Chest", true);
    }

    public void PassThroughFogWallInteraction(Transform fogWallEntrance){
        // make sure player faces fog wall first
        playerLocomotionManager.rigidbody.velocity = Vector3.zero; // stops the player form ice skating
        Vector3 rotationDirection = fogWallEntrance.transform.forward;
        Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
        transform.rotation = turnRotation;
        // rotate over time so it does not look as rigid

        playerAnimatorManager.PlayTargetAnimation("Pass Through Fog", true);
    }

    #endregion
}
