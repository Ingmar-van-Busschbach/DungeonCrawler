using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private DungeonWrapper dungeonWrapper;
    [SerializeField] private Image loadingScreen;
    [SerializeField] private Image loadBar;

    private void OnEnable()
    {
        dungeonWrapper = GetComponent<DungeonWrapper>();
        dungeonWrapper.onCompletionChanged += OnCompletionChanged;
    }

    private void OnDisable()
    {
        dungeonWrapper.onCompletionChanged -= OnCompletionChanged;
    }

    private void OnCompletionChanged(float ratio)
    {
        loadBar.fillAmount = ratio;
        if (ratio == 1)
        {
            loadingScreen.gameObject.SetActive(false);
        }
    }
}
