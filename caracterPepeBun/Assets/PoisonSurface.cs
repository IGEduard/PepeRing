using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSurface : MonoBehaviour
{
    public float poisonBuildUpAmount = 10;
    public List<CharacterEffectsManager> charactersInsidePoisonSurface;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        CharacterEffectsManager character = other.GetComponent<CharacterEffectsManager>();

        if (character != null) {
            charactersInsidePoisonSurface.Add(character);
        }
    }

    private void OnTriggerExit(Collider other) {
        CharacterEffectsManager character = other.GetComponent<CharacterEffectsManager>();

        if (character != null) {
            charactersInsidePoisonSurface.Remove(character);
        }
    }

    private void OnTriggerStay(Collider other) {
        foreach (CharacterEffectsManager character in charactersInsidePoisonSurface)
        {
            if (character.isPoisoned){
                return;
            }
            character.poisonBuildUp = character.poisonBuildUp + poisonBuildUpAmount * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
