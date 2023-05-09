using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmout;
    public float mouseX;
    public float mouseY;

    public bool examine_Input;  // q - y xBox - triangle PS - north button gamepad
    public bool useItem_Input;  // e - x xBox - square PS - west button gamepad
    public bool roll_Input;     // space - b xBox - circle PS - east button gamepad
    public bool jump_Input;     // f - a xBox - cross PS - south button gamepad

    public bool twoHand_Input;
    public bool lightAttack_Input;  // 
    public bool heavyAttack_Input;  // 
    public bool block_Input;    // left shoulder gamepad
    public bool parry_Input;    // left trigger gamepad
    public bool criticalAttack_Input;
    
    public bool inventory_Input;
    public bool lockOn_Input;
    public bool right_Stick_Right_Input;
    public bool right_Stick_Left_Input;

    public bool d_Pad_Up;
    public bool d_Pad_Down;
    public bool d_Pad_Right;
    public bool d_Pad_Left;

    public bool rollFlag;
    public bool twoHandFlag;
    public bool sprintFlag;
    public bool comboFlag;
    public bool lockOnFlag;
    public bool inventoryFlag;

    public float rollInputTimer;

    public Transform criticalAttackRayCastStartPoint;


    PlayerControls inputActions;
    PlayerCombatManager playerCombatManager;
    PlayerInventoryManager playerInventoryManager;
    PlayerManager playerManager;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerEffectsManager playerEffectsManager;
    PlayerStatsManager playerStatsManager;
    BlockingCollider blockingCollider;
    PlayerWeaponSlotManager playerWeaponSlotManager;
    UIManager uiManager;
    CameraHandler cameraHandler;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerManager = GetComponent<PlayerManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        uiManager = FindObjectOfType<UIManager>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        blockingCollider = GetComponentInChildren<BlockingCollider>();
    }


    public void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.PlayerActions.LightAttack.performed += i => lightAttack_Input = true;
            inputActions.PlayerActions.HeavyAttack.performed += i => heavyAttack_Input = true;
            inputActions.PlayerActions.Block.performed += i => block_Input = true;
            inputActions.PlayerActions.Block.canceled += i => block_Input = false;
            inputActions.PlayerActions.Parry.performed += i => parry_Input = true;
            inputActions.PlayerInteractions.DPadRight.performed += i => d_Pad_Right = true;
            inputActions.PlayerInteractions.DPadLeft.performed += i => d_Pad_Left = true;
            inputActions.PlayerActions.Examine.performed += i => examine_Input = true;
            inputActions.PlayerActions.UseItem.performed += i => useItem_Input = true;
            inputActions.PlayerActions.Roll.performed += i => roll_Input = true;
            inputActions.PlayerActions.Roll.canceled += i => roll_Input = false;
            inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
            inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
            inputActions.PlayerActions.LockOn.performed += i => lockOn_Input = true;
            inputActions.PlayerActions.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;
            inputActions.PlayerActions.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;
            inputActions.PlayerActions.TwoHand.performed += i => twoHand_Input = true;
            inputActions.PlayerActions.CriticalAttack.performed += i => criticalAttack_Input = true;
        }
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        if(playerStatsManager.isDead){
            return;
        }
        
        HandleMoveInput(delta);
        HandleRollInput(delta);
        HandleCombatInput(delta);
        HandleQuickSlotsInput();
        //HandleInteractingButtonInput();
        //HandleJumpInput();
        HandleInventoryInput();
        HandleLockOnInput();
        HandleTwoHandInput();
        HandleCriticalAttackInput();
        HandleUseConsumableInput();
    }

    private void HandleMoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmout = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleRollInput(float delta)
    {
        //roll_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
        //sprintFlag = roll_Input;

        if (roll_Input)
        {
            rollInputTimer += delta;

            if (playerStatsManager.currentStamina <= 0)
            {
                roll_Input = false;
                sprintFlag = false;
            }
            if (moveAmout > 0.5f && playerStatsManager.currentStamina > 0){
                sprintFlag = true;
            }
        }
        else
        {
            sprintFlag = false;

            if (rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                rollFlag = true;
            }
            rollInputTimer = 0;
        }
    }

    private void HandleCombatInput(float delta)
    {

        if (lightAttack_Input)
        {
            playerCombatManager.HandleLightAttackAction();
        }
        if (heavyAttack_Input)
        {
            playerCombatManager.HandleStrongAttackAction();
        }

        if(block_Input){
            playerCombatManager.HandleBlockAction();
        }
        else{
            playerManager.isBlocking = false;

            if ( blockingCollider.blockingCollider.enabled){
                blockingCollider.DisableBlockingCollider();
            }
        }

        if(parry_Input){
            if (twoHandFlag){
                // handle weapon art if two hand maybe
            } else {
                playerCombatManager.HandleParryAction();
            }// handle parry if shield
        }
    }

    private void HandleQuickSlotsInput()
    {
        if (d_Pad_Right)
        {
            playerInventoryManager.ChangeRightWeapon();
        }
        else if (d_Pad_Left)
        {
            playerInventoryManager.ChangeLeftWeapon();
        }
    }

    private void HandleInventoryInput()
    {

        if (inventory_Input)
        {
            inventoryFlag = !inventoryFlag;

            if (inventoryFlag)
            {
                uiManager.OpenSelectWindow();
                uiManager.UpdateUI();
                uiManager.hudWindow.SetActive(false);
            }
            else
            {
                uiManager.CloseEquipmentWindow();
                uiManager.CloseSelectWindow();
                uiManager.CloseAllInventoryWindows();
                uiManager.hudWindow.SetActive(true);
            }
        }
    }
    
    private void HandleLockOnInput(){
        if (lockOn_Input && lockOnFlag == false){
            
            lockOn_Input = false;
            //lockOnFlag = true;
            cameraHandler.HandleLockOn();
            if (cameraHandler.nearestLockOnTarget != null){
                cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                lockOnFlag = true;
            }
        }
        else if (lockOn_Input && lockOnFlag){
            lockOn_Input = false;
            lockOnFlag = false;
            cameraHandler.ClearLockOnTargets();
            //clear lock on targets
        }

        if (lockOnFlag && right_Stick_Left_Input) {
            right_Stick_Left_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.leftLockTarget != null){
                cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
            }
        }

        if (lockOnFlag && right_Stick_Right_Input) {
            right_Stick_Right_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.rightLockTarget != null){
                cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
            }
        }

        cameraHandler.SetCameraHeight();
    }

    public void HandleTwoHandInput(){
        if(twoHand_Input){
            twoHand_Input = false;
            twoHandFlag = !twoHandFlag;
            if(twoHandFlag)
            {
                playerManager.isTwoHandingWeapon = true;
                playerWeaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
                playerWeaponSlotManager.LoadTwoHandIKTargets(true);
            }
            else
            {
                playerManager.isTwoHandingWeapon = false;
                playerWeaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
                playerWeaponSlotManager.LoadWeaponOnSlot(playerInventoryManager.leftWeapon, true);
                playerWeaponSlotManager.LoadTwoHandIKTargets(false);
            }
        }
    }

    private void HandleCriticalAttackInput(){
        if(criticalAttack_Input){
            criticalAttack_Input = false;
            playerCombatManager.AttemptBackStabOrRiposte();
        }
        
    }

    private void HandleUseConsumableInput(){
        if (useItem_Input){
            useItem_Input = false;
            // use current consumable
            playerInventoryManager.currentConsumable.AttemptToConsumeItem(playerAnimatorManager, playerWeaponSlotManager, playerEffectsManager);
        }
    }

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
