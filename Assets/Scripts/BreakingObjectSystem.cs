using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakingObjectSystem : MonoBehaviour
{
    public Material materialToApply;
    public Material defaultMaterial;
    public GameObject Bar;
    public GameObject healthBarSpite;
    public GameObject objectSprite;
    public GameObject player;

    public string breakingAnimation;
    public float playerMaxDistance;
    private float currentPV;
    public float PVmax;
    public float itemDamage;
    public bool isHovered;
    public bool isBreaking;
    float playerDistance;
    bool playerAnimationTrigger = true;
    bool isAttacking;
    float hitTimer;
    public float hitTime;
    bool damageApplied = true;
    bool distanceCkeck;

    public PlayerMovements movements;
    Transform playerPos;
    SpriteRenderer sprite;
    SpriteRenderer playerSprite;
    public Animator animator;
    public Animator playerAnimator;
    
    void Start()
    {        
        playerPos = player.GetComponent<Transform>();
        sprite = objectSprite.GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();
        hitTimer = hitTime;
    }
    private void Awake()
    {
        healthBarSpite.SetActive(false);
        currentPV = PVmax;
    }
    void Update()
    {
        
        Outline();
        Damage();
        CheckDistance();
    }
    void Outline()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && distanceCkeck && hit.collider.gameObject == objectSprite)
        {
            sprite.material = materialToApply;
            isHovered = true;
        }
        else
        {
            sprite.material = defaultMaterial;
            isHovered = false;
        }
    }
    
    void Damage()
    {
        if (isHovered && Input.GetMouseButton(0) && distanceCkeck)
        {
            isAttacking = true;
            movements.Immobilizes(true);
            if (playerAnimationTrigger)
            {
                playerAnimator.SetTrigger("ActionAxe");
                playerAnimationTrigger = false;
            }
        }
        if (isAttacking)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0.3)
            {
                if (damageApplied)
                {
                    ApplyDamage(itemDamage);
                    damageApplied = false;
                }
                playerAnimationTrigger = true;
            }
            if (hitTimer < 0)
            {
                movements.Immobilizes(false);
                hitTimer = hitTime;
                isAttacking= false;
                damageApplied = true;
            }
        }
    }
    private void ApplyDamage(float damageAmount)
    {
        if (PVmax == currentPV)
        {
            healthBarSpite.SetActive(true);
        }

        currentPV -= damageAmount;
        Bar.transform.localScale -= new Vector3(Mathf.Clamp(damageAmount / PVmax, 0f, PVmax), 0f, 0f);

        if (animator != null)
        {
            animator.SetTrigger(breakingAnimation);
        }
        if (currentPV <= 0f)
        {
            movements.Immobilizes(false);
            Destroy(gameObject);
        }
    }
    private void CheckDistance()
    {
        playerDistance = Vector2.Distance(playerPos.position, transform.position);
        distanceCkeck = playerDistance < playerMaxDistance;
    }

}


