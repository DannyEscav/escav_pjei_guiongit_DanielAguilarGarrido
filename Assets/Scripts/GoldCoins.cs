using UnityEngine;

public class GoldCoins : MonoBehaviour {

    [Tooltip("Referencia al manager del respawn de las monedas")]
    [SerializeField]
    private CoinRespawnManager manager;

    #region Unity Messages

    private void OnTriggerEnter(Collider other) {
        // Comprueba si es el jugador el que ha entrado en contacto para notificar la recolección
        if (other.CompareTag("Player")) {
            manager.CoinCollected();
        }
    }

    #endregion
}