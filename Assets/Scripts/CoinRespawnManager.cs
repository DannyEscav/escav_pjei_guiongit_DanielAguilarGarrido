using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinRespawnManager : MonoBehaviour {

    [Tooltip("Referencia al GameObject de las monedas")]
    [SerializeField]
    private GameObject coins;
    [Tooltip("Referencia al GameObject de las monedas")]
    [SerializeField]
    private GameObject player;

    [Space]

    [Tooltip("Referencia al contador de monedas")]
    [SerializeField]
    private Text coinsCounterText;
    [Tooltip("Referencia al contador de tiempo para el respawn")]
    [SerializeField]
    private Text respawnTimeText;

    [Space]

    [Tooltip("Segundos entre respawns de las monedas")]
    [SerializeField]
    private float secondsBetweenRespawn;

    // Índice de posición en que se encuentran las monedas
    private int coinsIndex;
    // Contador de monedas recolectadas
    private int collectedCoinsCnt;
    // Segundos hasta el próximo respawn
    private float nextRespawn;

    #region Unity Messages

    private void Start() {
        // Coloca las monedas en una posición aleatoria
        coinsIndex = Random.Range(0, transform.childCount);
        coins.transform.position = transform.GetChild(coinsIndex).position;
        // Lanza la corrutina para controlar el respawn cada cierto tiempo de las monedas
        StartCoroutine(RespawnCoins());
    }

    #endregion

    #region Public Messages
    
    public void CoinCollected() {
        // Actualiza el contador de monedas y coloca las monedas en una nueva posición aleatoria
        ++collectedCoinsCnt;
        coinsCounterText.text = collectedCoinsCnt.ToString();
        SetCoinsNewPosition();
    }

    #endregion

    private IEnumerator RespawnCoins() {
        // Bucle infinito
        while (true) {
            // Actualiza el tiempo hasta el próximo respawn
            yield return null;
            nextRespawn -= Time.deltaTime;
            respawnTimeText.text = nextRespawn.ToString("00.00");
            // Comprueba si es momento de llevar a cabo el respawn
            if (nextRespawn <= 0.0f) {
                SetCoinsNewPosition();
            }
        }
    }

    private void SetCoinsNewPosition() {
        // Busca un índice aleatorio distinto al anterior y lo suficientemente lejano del jugador
        int newIndex;
        do {
            newIndex = Random.Range(0, transform.childCount);
        } while ((newIndex == coinsIndex) || (Vector3.Distance(player.transform.position, transform.GetChild(newIndex).position) < 3));
        // Coloca las monedas en la nueva posición
        coinsIndex = newIndex;
        coins.transform.position = transform.GetChild(coinsIndex).position;
        // Resetea el tiempo del próximo respawn
        nextRespawn = secondsBetweenRespawn;
        respawnTimeText.text = nextRespawn.ToString("00.00");
    }
}