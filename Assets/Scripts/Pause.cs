using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    [Tooltip("Referencia al texto del botón")]
    [SerializeField]
    private Text buttonText;

    #region Unity Messages

    private void Start() {
        // Se asegura que la pausa está desactivada al inicio
        Time.timeScale = 1;
    }

    #endregion

    #region Public Messages

    public void ButtonClicked() {
        if (Time.timeScale == 1) {
            PauseGame();
        } else {
            ResumeGame();
        }
    }

    #endregion

    private void PauseGame() {
        // Pausa del juego
        Time.timeScale = 0;
        buttonText.text = "Reanudar";
    }

    private void ResumeGame() {
        // Reanudación del juego
        Time.timeScale = 1;
        buttonText.text = "Pausa";
    }
}