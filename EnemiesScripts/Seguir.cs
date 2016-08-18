using UnityEngine;
using System.Collections;

public class Seguir : MonoBehaviour {

	public Transform m_Jugador;

		// Update is called once per frame
		void Update () {
		GetComponent<NavMeshAgent>().destination = m_Jugador.position;
		}
}

