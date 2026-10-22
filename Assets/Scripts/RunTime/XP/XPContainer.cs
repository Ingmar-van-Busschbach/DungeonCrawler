using UnityEngine;
using System.Collections.Generic;
using System;

namespace XP
{
    public class XPContainer : MonoBehaviour
    {
        [SerializeField] private List<int> xpNeededPerLevelUp = new();
        private int currentXP = 0;
        private int currentLevel = 0;

        public event Action<int, int> onXPChanged;
        public event Action<int> onLevelUp;
        public event Action<int, int, int> init;

        public int CurrentXP
        {
            get
            {
                return currentXP;
            }
            set
            {
                value = Mathf.Abs(value);
                currentXP = value;
                if (currentLevel < xpNeededPerLevelUp.Count - 1)
                {
                    if (currentXP > xpNeededPerLevelUp[currentLevel])
                    {
                        currentLevel++;
                        currentXP = 0;
                        onLevelUp?.Invoke(currentLevel);
                    }
                }
                else
                {
                    if (currentXP > xpNeededPerLevelUp[currentLevel])
                    {
                        currentXP = xpNeededPerLevelUp[currentLevel];
                    }
                }
                onXPChanged?.Invoke(currentXP, xpNeededPerLevelUp[currentLevel]);
            }
        }

        private void Start()
        {
            init?.Invoke(currentXP, xpNeededPerLevelUp[currentLevel], currentLevel);
        }
    }
}