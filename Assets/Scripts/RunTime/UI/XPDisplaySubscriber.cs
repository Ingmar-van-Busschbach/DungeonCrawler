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
            xpContainer.onLevelMax -= OnLevelMax;
            xpContainer.init -= Init;
        }
    }

    private void OnXPChanged(int currentXP, int maxXP)
    {
        xpBarImage.fillAmount = ((float)currentXP / maxXP);
    }

    private void OnLevelUp(int currentLevel)
    {
        levelText.text = (currentLevel + 1).ToString();
    }

    private void OnLevelMax()
    {
        levelText.text = "Max";
    }

    private void Init(int currentXP, int maxXP, int currentLevel)
    {
        xpBarImage.fillAmount = ((float)currentXP / maxXP);
        levelText.text = (currentLevel + 1).ToString();
    }

    private void OnPlayerSpawned(GameObject player)
    {
        xpContainer = player.GetComponent <XPContainer>();
        xpContainer.onXPChanged += OnXPChanged;
        xpContainer.onLevelUp += OnLevelUp;
        xpContainer.onLevelMax += OnLevelMax;
        xpContainer.init += Init;
    }
}
