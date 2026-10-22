using Damage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XP;

public class XPDisplaySubscriber : MonoBehaviour
{
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private XPContainer xpContainer;
    [SerializeField] private Image xpBarImage;
    [SerializeField] private TMP_Text levelText;
    private void OnEnable()
    {
        playerSpawner.onPlayerSpawned += OnPlayerSpawned;
    }

    private void OnDisable()
    {
        playerSpawner.onPlayerSpawned -= OnPlayerSpawned;
        if(xpContainer != null)
        {
            xpContainer.onXPChanged -= OnXPChanged;
            xpContainer.onLevelUp -= OnLevelUp;
        }
    }

    private void OnXPChanged(int currentXP, int maxXP)
    {
        xpBarImage.fillAmount = currentXP / maxXP;
    }

    private void OnLevelUp(int currentLevel)
    {
        levelText.text = currentLevel.ToString();
    }

    private void Init(int currentXP, int maxXP, int currentLevel)
    {
        xpBarImage.fillAmount = currentXP / maxXP;
        levelText.text = currentLevel.ToString();
    }

    private void OnPlayerSpawned(GameObject player)
    {
        xpContainer = player.GetComponent <XPContainer>();
        xpContainer.onXPChanged += OnXPChanged;
        xpContainer.onLevelUp += OnLevelUp;
    }
}
