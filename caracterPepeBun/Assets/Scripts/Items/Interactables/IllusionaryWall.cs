using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionaryWall : MonoBehaviour
{
    public bool wallHasBeenHit;
    public Material illusionaryWallMaterial;
    public float alpha;
    public float fadeTimer = 2.5f;
    public BoxCollider wallCollider;

    public AudioSource audioSource;
    public AudioClip illusionaryWallSound;

    public MeshRenderer meshRenderer;

    private void Awake()
    {
        illusionaryWallMaterial = Instantiate(illusionaryWallMaterial);
        meshRenderer.material = illusionaryWallMaterial;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wallHasBeenHit){
            FadeIllusionaryWall();
        }
    }

    public void FadeIllusionaryWall(){
        alpha = meshRenderer.material.color.a;
        alpha = alpha - Time.deltaTime / fadeTimer;
        Color fadedWallColor = new Color(1, 1, 1, alpha);
        meshRenderer.material.color = fadedWallColor;

        if (wallCollider.enabled){
            wallCollider.enabled = false;
            audioSource.PlayOneShot(illusionaryWallSound);
        }

        if (alpha <= 0){
            Destroy(this);
        }
    }
}
