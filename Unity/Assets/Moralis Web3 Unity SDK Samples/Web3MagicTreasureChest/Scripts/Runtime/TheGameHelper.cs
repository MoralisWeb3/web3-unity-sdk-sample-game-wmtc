using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS
{
    /// <summary>
    /// Helper Methods
    /// </summary>
    public static class TheGameHelper
    {
        // General Methods --------------------------------
        

        public static uint GetRewardType(string name)
        {
            if (name == RewardGold)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public static string GetRewardTitle(string name)
        {
            if (name == RewardGold)
            {
                return "Gold";
            }
            else
            {
                return "Prize";
            }
        }
        
        public static string GetRewardTypeNameByType(uint t)
        {
            if (t == 1)
            {
                return "Gold";
            }
            else
            {
                return "Prize";
            }
        }
        
        public const string RewardGold = "GOLD";
        public const string RewardPrize = "PRIZE";

        public static T InstantiatePrefab<T>(T prefab, Transform parent, Vector3 worldPosition) where T : Component
        {
            T instance = GameObject.Instantiate<T>(prefab, parent);
            instance.gameObject.name = instance.GetType().Name;
            instance.transform.position = worldPosition;
            return instance;
        }

        public static void SetButtonText(Button button, bool isActive, string activeText, string notActiveText)
        {
            if (isActive)
            {
                SetButtonText(button, activeText);
            }
            else
            {
                SetButtonText(button, notActiveText);
            }
        }
        
        public static void SetButtonText(Button button, string text)
        {
            TMP_Text tmp_Text = button.GetComponentInChildren<TMP_Text>();
            tmp_Text.text = text;
        }

        public static void SetButtonVisibility(Button button, bool isVisible)
        {
            CanvasGroup canvasGroup = button.GetComponentInChildren<CanvasGroup>();

            if (isVisible)
            {
                canvasGroup.alpha = 1;
            }
            else
            {
                canvasGroup.alpha = 0;
            }
        }


    }
}