using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileGenerator : MonoBehaviour {

	public GameObject destructionPoint;
	public ObjectPooler theObjectPooler;

	private bool generateProjectile;
	private float projectileDamage;

	void Update () {
		if (generateProjectile){
			GameObject newProjectile = theObjectPooler.GetPooledObject();
			newProjectile.transform.position = transform.position;
			newProjectile.transform.rotation = transform.rotation;
			newProjectile.GetComponent<ObjectDestroyer>().destructionPoint = destructionPoint;
			newProjectile.GetComponent<Projectile>().ResetProjectile();
			newProjectile.GetComponent<Projectile>().damage = projectileDamage;
			newProjectile.SetActive(true);

			generateProjectile = false;
		}
	}

	public void GenerateProjectile (float damage) {
		generateProjectile = true;
		projectileDamage = damage;
	}

}
