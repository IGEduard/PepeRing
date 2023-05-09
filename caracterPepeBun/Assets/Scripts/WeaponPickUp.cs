using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickUp : Interactable
{
    public WeaponItem weapon;

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);

        PickUpItem(playerManager);
    }

    private void PickUpItem(PlayerManager playerManager)
    {
        PlayerInventoryManager playerInventory;
        PlayerLocomotionManager playerMovement;
        PlayerAnimatorManager animatorHandler;

        playerInventory = playerManager.GetComponent<PlayerInventoryManager>();
        playerMovement = playerManager.GetComponent<PlayerLocomotionManager>();
        animatorHandler = playerManager.GetComponentInChildren<PlayerAnimatorManager>();

        playerMovement.rigidbody.velocity = Vector3.zero;
        animatorHandler.PlayTargetAnimation("PickUp", true);
        playerInventory.weaponsInventory.Add(weapon);
        playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
        playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
        playerManager.itemInteractableGameObject.SetActive(true);
        Destroy(gameObject);
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
