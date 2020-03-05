using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [Tooltip("Referencia al NavMeshAgent")]
    [SerializeField]
    private NavMeshAgent agent;

    [Tooltip("Referencia a la cámara")]
    [SerializeField]
    private Camera cam;

    [Tooltip("Referencia al cartel de Game Over")]
    [SerializeField]
    private GameObject gameOver;

    #region Unity Messages

    private void Update() {
        // Comprueba si se usa el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0)) {
            // Lanza un rayo desde la cámara hacia la posición del ratón para comprobar en qué posición ha clicado el jugador
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                // Indica al personaje la posición a la que debe ir
                agent.SetDestination(hit.point);
            }
        }
    }

    #endregion

    #region Public Messages

    public IEnumerator GameOver() {
        // Muestra el cartel de game over
        gameOver.SetActive(true);
        // Espera 3 segundos antes de reiniciar el juego
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }

    #endregion
}