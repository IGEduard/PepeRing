using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : Interactable
{
    Animator animator;
    OpenChest openChest;
    public Transform playerStandingPosition;
    public GameObject itemSpawner;
    public WeaponItem itemInChest;

    private void Awake() {
        animator = GetComponent<Animator>();
        openChest = GetComponent<OpenChest>();
    }
    public override void Interact(PlayerManager playerManager)
    {
        //base.Interact(playerManager);
        Vector3 rotationDirection = transform.position - playerManager.transform.position;
        rotationDirection.y = 0;
        rotationDirection.Normalize();

        Quaternion tr = Quaternion.LookRotation(rotationDirection);
        Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 300 * Time.deltaTime);
        playerManager.transform.rotation = targetRotation;

        playerManager.OpenChestInteraction(playerStandingPosition);
        animator.Play("Chest Open");
        StartCoroutine(SpawnItemInChest());

        WeaponPickUp weaponPickUp = itemSpawner.GetComponent<WeaponPickUp>();
        if (weaponPickUp != null){
            weaponPickUp.weapon = itemInChest;
        }
    }

    private IEnumerator SpawnItemInChest(){
        yield return new WaitForSeconds(1f);
        Instantiate(itemSpawner, transform);
        Destroy(openChest);
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
