using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {
	private Transform target;//la posicion del enemigo
	public GameObject Efecto_Impacto;//las particulas de la explosion 
	[Header("Valores de la bala/Misil")]
	public float explosionradius = 0f;//el alcance de la explosion
	public float damage=0f;//daño que provocara
	public float speed=70f;//su velocidad de vuelo

	public void Seek (Transform _target)//aqui se pasa la posicion del objetivo
	{
		target = _target;//se asigna la variable
	}

	void Update()
	{
		if (target==null) { //esta validacion es por si hay una bala en el aire sin objetivo para que se destruya
			Destroy (gameObject);
			return;
		}
		Vector3 dir = target.position - transform.position;//estas instrucciones indican  que la bala ha de
		float distancethisframe = speed * Time.deltaTime;//dirigirse al objetivo
		if (dir.magnitude<=distancethisframe) {
			Hit_Target ();
			return;
		}
		transform.Translate (dir.normalized*distancethisframe,Space.World);//esto regula el movimiento de la bala en relacion
		transform.LookAt (target);                                    //a los fps por segundo y se asegura que mire hacia delante
	}
	void Hit_Target()//el metodo invoca las particulas  relacionada con la explosion 
	{ GameObject effectins = (GameObject)Instantiate (Efecto_Impacto, transform.position, transform.rotation);
		Destroy (effectins, 0.8f);
		if (explosionradius > 0f)//si el radio de la explosion en mayor a 0 significa que hay daño en area
		{
			Explosion ();
		} else  //en caso contrario solo hay daño directo
		{
			Damage (target); 
		}
		Destroy (gameObject);//se destruye la bala de la escena
	}
	void Damage(Transform Zombiev2) //recibe la posicion del enemigo
	{
		Zombiev2 e=Zombiev2.GetComponent<Zombiev2> ();//obtiene los componentes del enemigo para aplicar daño a la entidad
		if (e!=null) 									//invocando el metodo correspondiente
		{
			e.Take_Damage (damage);
		}

	}
	void Explosion()//en caso de explosion se dispara un area donde se detecta los objetos con la marca de enemigos
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, explosionradius);
		foreach (Collider collider in colliders)
		{
			if (collider.tag=="enemigo") 
			{
				Damage (collider.transform);	
			}
		}
	}

	void OnDrawGizmos()//funcion que dibuja el area afectada por la explosion
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, explosionradius);
	}
}
