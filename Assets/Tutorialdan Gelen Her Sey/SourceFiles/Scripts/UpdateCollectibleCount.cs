using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UpdateCollectibleCount : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI collectibleText;

    [Header("Win UI")]
    public GameObject birthdayPanel;        // Canvas'ta, baslangicta kapali
    public TextMeshProUGUI birthdayText;    // "TEBRİKLER" yazisi

    [Header("Level Transition")]
    [SerializeField] private string nextSceneName = "Level2";
    [SerializeField] private float transitionDelay = 1.5f;

    /// <summary>Shared flag that prevents the lose condition from firing after the win.</summary>
    public static bool gameEnded = false;

    [Header("Collectible Tags")]
    public string[] collectibleTags = new string[] { "Collectible", "PickUp", "Pickup" };

    private int _lastCount = -1;

    /// <summary>Resets the static flag when the scene loads so each scene starts fresh.</summary>
    void Awake()
    {
        gameEnded = false;
    }

    void Start()
    {
        UpdateCollectibleDisplay();
    }

    void Update()
    {
        UpdateCollectibleDisplay();
    }

    /// <summary>Counts remaining collectibles, updates the HUD, and triggers win when count reaches zero.</summary>
    private void UpdateCollectibleDisplay()
    {
        int totalCollectibles = 0;

        for (int i = 0; i < collectibleTags.Length; i++)
        {
            if (string.IsNullOrEmpty(collectibleTags[i]))
                continue;

            GameObject[] found = GameObject.FindGameObjectsWithTag(collectibleTags[i]);
            totalCollectibles += found.Length;
        }

        if (collectibleText != null)
            collectibleText.text = $"TÜM MANTARLARI BUL: {totalCollectibles}";

        // Ignore the very first frame in case collectibles haven't spawned yet.
        if (_lastCount == -1)
        {
            _lastCount = totalCollectibles;
            return;
        }

        if (totalCollectibles == 0 && _lastCount > 0 && !gameEnded)
        {
            gameEnded = true;
            StartCoroutine(WinTransition());
        }

        _lastCount = totalCollectibles;
    }

    /// <summary>Shows the congratulations panel briefly, then loads the next level.</summary>
    private IEnumerator WinTransition()
    {
        if (birthdayPanel != null)
            birthdayPanel.SetActive(true);

        if (birthdayText != null)
            birthdayText.text = "TEBRİKLER TÜM MANTARLARI TOPLADIN";

        yield return new WaitForSeconds(transitionDelay);

        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}
