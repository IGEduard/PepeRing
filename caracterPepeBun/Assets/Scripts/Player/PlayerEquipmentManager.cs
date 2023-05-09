using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerInventoryManager playerInventoryManager;
    PlayerStatsManager playerStatsManager;

    [Header("Equipment Model Changers")]
    // head equipment
    HelmetModelChanger helmetModelChanger;
    // torso equipment
    TorsoModelChanger torsoModelChanger;
    // leg equipment
    HipModelChanger hipModelChanger;
    LeftLegModelChanger leftLegModelChanger;
    RightLegModelChanger rightLegModelChanger;
    
    // hand equipment
    LeftHandModelChanger leftHandModelChanger;
    RightHandModelChanger rightHandModelChanger;

    [Header("Default Naked Models")]
    public GameObject nakedHeadModel;
    public string nakedTorsoModel;
    public string nakedHipModel;
    public string nakedLeftLegModel;
    public string nakedRightLegModel;
    public string nakedLeftHandModel;
    public string nakedRightHandModel;

    public BlockingCollider blockingCollider;

    private void Awake() 
    {
        inputHandler = GetComponent<InputHandler>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        
        helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
        torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();

        hipModelChanger = GetComponentInChildren<HipModelChanger>();
        leftLegModelChanger = GetComponentInChildren<LeftLegModelChanger>();
        rightLegModelChanger = GetComponentInChildren<RightLegModelChanger>();

        leftHandModelChanger = GetComponentInChildren<LeftHandModelChanger>();
        rightHandModelChanger = GetComponentInChildren<RightHandModelChanger>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EquipAllEquipmentModelsOnStart();
    }

    public void OpenBlockingCollider(){
        if(inputHandler.twoHandFlag){
            blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.rightWeapon);
        }
        else {
            blockingCollider.SetColliderDamageAbsorption(playerInventoryManager.leftWeapon);
        }
        
        blockingCollider.EnableBlockingCollider();
    }

    public void CloseBlockingCollider(){
        blockingCollider.DisableBlockingCollider();
    }
    
    private void EquipAllEquipmentModelsOnStart()
    {
        // head equipment
        helmetModelChanger.UnEquipAllHelmetModels();
        if (playerInventoryManager.currentHelmetEquipment != null)
        {
            nakedHeadModel.SetActive(false);
            helmetModelChanger.EquipHelmetModelByName(playerInventoryManager.currentHelmetEquipment.helmetModelName);

            playerStatsManager.physicalDamageAbsorptionHead = playerInventoryManager.currentHelmetEquipment.physicalDefense;
        }
        else
        {
            nakedHeadModel.SetActive(true); // might combine with hood on head
            //helmetModelChanger.EquipHelmetModelByName(nakedHeadModel); // equip default head

            playerStatsManager.physicalDamageAbsorptionHead = 0;
        }
        
        // torso equipment
        torsoModelChanger.UnEquipAllTorsoModels();
        if (playerInventoryManager.currentTorsoEquipment != null)
        {
            torsoModelChanger.EquipTorsoModelByName(playerInventoryManager.currentTorsoEquipment.torsoModelName);

            playerStatsManager.physicalDamageAbsorptionTorso = playerInventoryManager.currentTorsoEquipment.physicalDefense;
        }
        else
        {
            torsoModelChanger.EquipTorsoModelByName(nakedTorsoModel); // equip default torso (naked)

            playerStatsManager.physicalDamageAbsorptionTorso = 0;
        }

        // leg equipment
        hipModelChanger.UnEquipAllHipModels();
        leftLegModelChanger.UnEquipAllLegModels();
        rightLegModelChanger.UnEquipAllLegModels();

        if (playerInventoryManager.currentLegEquipment != null){
            hipModelChanger.EquipHipModelByName(playerInventoryManager.currentLegEquipment.hipModelName);
            leftLegModelChanger.EquipLegModelByName(playerInventoryManager.currentLegEquipment.leftLegModelName);
            rightLegModelChanger.EquipLegModelByName(playerInventoryManager.currentLegEquipment.rightLegModelName);

            playerStatsManager.physicalDamageAbsorptionLegs = playerInventoryManager.currentLegEquipment.physicalDefense;
        }
        else
        {
            hipModelChanger.EquipHipModelByName(nakedHipModel);
            leftLegModelChanger.EquipLegModelByName(nakedLeftLegModel);
            rightLegModelChanger.EquipLegModelByName(nakedRightLegModel);

            playerStatsManager.physicalDamageAbsorptionLegs = 0;
        }

        // hand equipment
        leftHandModelChanger.UnEquipAllHandModels();
        rightHandModelChanger.UnEquipAllHandModels();

        if (playerInventoryManager.currentHandEquipment != null){
            leftHandModelChanger.EquipHandModelByName(playerInventoryManager.currentHandEquipment.leftHandModelName);
            rightHandModelChanger.EquipHandModelByName(playerInventoryManager.currentHandEquipment.rightHandModelName);

            playerStatsManager.physicalDamageAbsorptionHands = playerInventoryManager.currentHandEquipment.physicalDefense;
        }
        else {
            leftHandModelChanger.EquipHandModelByName(nakedLeftHandModel);
            rightHandModelChanger.EquipHandModelByName(nakedRightHandModel);

            playerStatsManager.physicalDamageAbsorptionHands = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
