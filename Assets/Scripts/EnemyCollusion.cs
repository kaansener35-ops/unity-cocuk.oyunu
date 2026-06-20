using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyCollision : MonoBehaviour
{
    public GameObject losePanel;
    public TextMeshProUGUI loseText;
    public float waitBeforeRestart = 1.5f;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // 1) OYUN ZATEN BITTIYSE (tüm collectible'lar toplandıysa) HİÇBİR ŞEY YAPMA
        if (UpdateCollectibleCount.gameEnded)
            return;

        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;
        StartCoroutine(ShowLoseAndRestart());
    }

    private System.Collections.IEnumerator ShowLoseAndRestart()
    {
        if (losePanel != null)
            losePanel.SetActive(true);

        if (loseText != null)
            loseText.text = "HAYATIM YUUH ONLARDAN KACC YANI SIMDILIK EHEHEH";

        yield return new WaitForSeconds(waitBeforeRestart);

        // oyun bittiginde sahneyi bastan baslat
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
}
