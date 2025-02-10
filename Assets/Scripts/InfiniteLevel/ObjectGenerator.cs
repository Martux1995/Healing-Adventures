using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectSelect{platform,slime};

public class ObjectGenerator : MonoBehaviour {

	// Tipo de objeto que se generará
	public ObjectSelect objectType;

	// Punto que servirá para detectar el momento cuando se generará y destruirá el objeto
	public GameObject generationPoint;
	public GameObject destructionPoint;

	// Distancias mínimas y máximas que se considerarán al momento de generar el objeto
	public float distanceBetweenMin;
	public float distanceBetweenMax;

	// Lista de los objetos que serán generados
	public ObjectPooler[] theObjectPoolers;

	// Selector de un objeto (en el caso que fuesen varios del mismo tipo)
	private int objectSelector;
	
	// Update is called once per frame
	void Update () {
		// transform es el GameObject que contiene este script
		if (transform.position.x < generationPoint.transform.position.x){

			objectSelector = ( theObjectPoolers.Length == 1 ? 0 : Random.Range(0, theObjectPoolers.Length) );

			switch (objectType) {
				case ObjectSelect.platform:
					HandlePlatformGenerator();
					break;
				case ObjectSelect.slime:
					HandleSlimeGenerator();
					break;

			}
		}
	}

	// Maneja la generación de plataformas
	void HandlePlatformGenerator () {

		float distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);
		float platformSize = theObjectPoolers[objectSelector].pooledObject.GetComponent<BoxCollider2D>().size.x;
		//Debug.Log(distanceBetween);
		transform.position = new Vector3(transform.position.x + (platformSize/2) + distanceBetween,
				                         transform.position.y, transform.position.z);

		GameObject newPlatform = theObjectPoolers[objectSelector].GetPooledObject();
		newPlatform.transform.position = transform.position;
		newPlatform.transform.rotation = transform.rotation;
		newPlatform.GetComponent<ObjectDestroyer>().destructionPoint = destructionPoint;
		newPlatform.SetActive(true);

		transform.position = new Vector3(transform.position.x + (platformSize/2),
				                         transform.position.y, transform.position.z);
	}

	// Maneja la generación de bacterias pequeñas (slimes)
	void HandleSlimeGenerator () {
		float distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

		//Debug.Log(distanceBetween);
		transform.position = new Vector3(transform.position.x + distanceBetween,
				                         transform.position.y, transform.position.z);

		GameObject newSlime = theObjectPoolers[objectSelector].GetPooledObject();
		newSlime.transform.position = transform.position;
		newSlime.transform.rotation = transform.rotation;
		newSlime.GetComponent<ObjectDestroyer>().destructionPoint = destructionPoint;
		newSlime.GetComponent<Enemy>().canJump = (Random.Range(0f,1f) > 0.5f);
		newSlime.GetComponent<Enemy>().reset = true;
		newSlime.GetComponent<Enemy>().activePoint = generationPoint.transform;
		newSlime.SetActive(true);
	}

}
