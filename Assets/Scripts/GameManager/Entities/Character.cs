using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	// El GameManager del cual dependen los personajes
	protected GameManager theGameManager;

	// VARIABLES DE SALTO
	public float jumpForce;                // Potencia del salto
	public float jumpTime;                 // Duración del salto
	protected float jumpTimeCounter;       // Contador interno para el tiempo de salto
	protected bool stoppedJumping;         // Comprueba si dejó de saltar
	protected bool grounded { get; set; }  // Determina si el personaje está tocando el suelo
	protected bool falling  { get; set; }  // Determina si el personaje se encuentra cayendo

	// VARIABLES DE ATAQUE
	public float attackDamage;                 // Potencia del daño que inflige el personaje
	public float attackTime;                   // Tiempo que el personaje permanece atacando
	protected float attackTimeCounter;         // Contador interno para el tiempo de ataque
	public bool isAttacking { get; set; }   // Determina si el personaje está en modo de ataque

	// VARIABLES DE SALUD
	public float maxHealth;                      // Salud máxima que posee el personaje
	protected float actualHealth { get; set; }   // Salud actual que el personaje tiene en ejecución
	protected bool immune;                       // Determina si el personaje se encuentra dañado
	public bool defeated;                     // Determina si el personaje está debilitado
	public LifeManager theLifeBar;

	// Elementos del GameObject
	protected SpriteRenderer mySpriteRenderer; // SpriteRender para la intermitencia
	protected Rigidbody2D myRigidbody;         // Rigidbody2D para el movimiento y velocidad
	protected Collider2D myCollider;           // Collider2D para las colisiones con enemigos y objetos
	protected Animator myAnimator;             // Animator para la maquina de estados

	// DETECTORES DE SUELO
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float groundCheckRadius;

	// El personaje puede ejecutar acciones?
	public bool enable;

    public virtual void Start () {
        theGameManager = FindObjectOfType<GameManager>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();

        actualHealth = maxHealth;
    }

	public void RestoreLife (float lifeAmount){
		if (actualHealth + lifeAmount < maxHealth){
			actualHealth += lifeAmount;
		} else {
			actualHealth = maxHealth;
		}
		if (theLifeBar != null)
			theLifeBar.RestoreLife((int)(lifeAmount));
	}

	public void ReduceLife (float lifeAmount) {
		if (actualHealth - lifeAmount > 0){
			actualHealth -= lifeAmount;
		} else {
			actualHealth = 0;
			defeated = true;
		}
		if (theLifeBar != null)
			theLifeBar.ReduceLife((int)(lifeAmount));
	}

	public void ResetLife () {
		actualHealth = maxHealth;
		defeated = false;
		if (theLifeBar != null)
			theLifeBar.ResetLife();
	}

	public bool isDefeated() {
		return defeated;
	}

}
