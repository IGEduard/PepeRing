using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterCastingSpell : MonoBehaviour
{
    CharacterManager characterCastingSpell;
    private void Awake() {
        characterCastingSpell = GetComponentInParent<CharacterManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (characterCastingSpell.isFiringSpell)
        {
            Destroy(gameObject);
        }
    }
}
