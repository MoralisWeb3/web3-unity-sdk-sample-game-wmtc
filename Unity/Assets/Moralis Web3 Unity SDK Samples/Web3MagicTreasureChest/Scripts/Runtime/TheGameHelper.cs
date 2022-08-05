using System.Collections.Generic;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI;
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

        public static string CreateNewRewardTitle(string name)
        {
            if (name == RewardGold)
            {
                return "Gold";
            }
            else
            {
                List<string> words = new List<string> { "Great", "Amazing", "Worthy" };
                string word = words[UnityEngine.Random.Range(0, words.Count)];
                return $"{word} Prize";
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
        
        public static int GetAudioClipIndexClick()
        {
            return 0;
        }
        
        public static int GetAudioClipIndexChestHit01()
        {
            return 1;
        }
        
        public static int GetAudioClipIndexChestHit02()
        {
            return 2;
        }
        
        public static int GetAudioClipIndexWinSound()
        {
            return 3;
        }
        
        public static int GetAudioClipIndexByReward(Reward reward)
        {
            if (reward.Type == 1)
            {
                return 4;
            }
            else
            {
                return 5;
            }
        }
        
        
        public const string RewardGold = "GOLD";
        public const string RewardPrize = "PRIZE";

        public static void SetCardUIForReward(CardUI cardUI, Reward reward)
        {
            if (reward.Type == TheGameHelper.GetRewardType(RewardGold))
            {
                cardUI.SpriteRenderer.sprite = cardUI.GoldSprite;
            }
            else
            {
                cardUI.SpriteRenderer.sprite = cardUI.PrizeSprite;
            }
        }
        
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

        public static string FormatTextGold(int gold)
        {
            return $"({gold} Gold)";
        }
    }
}