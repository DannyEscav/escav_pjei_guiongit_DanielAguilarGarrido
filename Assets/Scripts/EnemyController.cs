using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    [Tooltip("Referencia al NavMeshAgent")]
    [SerializeField]
    private NavMeshAgent agent;

    [Tooltip("Referencia al Trasform del jugador")]
    [SerializeField]
    private Transform player;

    [Tooltip("Referencia a los puntos de ruta")]
    [SerializeField]
    private Transform routePoints;

    // Índice del punto actual de patrulla
    private int currentRoutePoint;

    #region Unity Messages

    private void Start() {
        // Primer punto de patrulla
        currentRoutePoint = Random.Range(0, routePoints.childCount);
        agent.SetDestination(routePoints.GetChild(currentRoutePoint).position);
        // Lanza la corrutina para comprobar l asituación del jugador
        StartCoroutine(CheckForPlayer());
    }

    private void OnTriggerEnter(Collider other) {
        // Comprueba si se trata del jugador
        if (other.CompareTag("Player")) {
            // Final del juego
            StartCoroutine(player.GetComponent<PlayerController>().GameOver());
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up, player.position - transform.position);
    }

    #endregion

    private IEnumerator CheckForPlayer() {
        // Variables auxiliares antes del bucle infinito
        RaycastHit hit;
        WaitForSeconds wfs = new WaitForSeconds(0.25f);
        int newRoutePoint;
        while (true) {
            // Comprueba si el enemigo tiene el jugador a la vista
            if (Physics.Raycast(transform.position + Vector3.up, player.position - transform.position, out hit)) {
                // Si no se trata de una pared, se pone a perseguirlo
                if (!hit.collider.CompareTag("Wall")) {
                    agent.SetDestination(player.position);
                } else if (!agent.hasPath) {
                    // El enemigo no tiene punto al que ir, luego elige uno para patrullar, distinto al anterior utilizado
                    do {
                        newRoutePoint = Random.Range(0, routePoints.childCount);
                    } while (currentRoutePoint == newRoutePoint);
                    currentRoutePoint = newRoutePoint;
                    agent.SetDestination(routePoints.GetChild(currentRoutePoint).position);
                }
            }
            yield return wfs;
        }
    }
}