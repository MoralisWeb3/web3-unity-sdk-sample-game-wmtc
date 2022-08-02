using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MoralisUnity.Samples.Shared.Components;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
	/// <summary>
	/// Stores data for the game
	///		* See <see cref="TheGameSingleton"/> - Handles the core functionality of the game
	/// </summary>
	public class TheGameController
	{
		// Events -----------------------------------------
		public TheGameModelUnityEvent OnTheGameModelChanged = new TheGameModelUnityEvent();
		public void OnTheGameModelChangedRefresh() { OnTheGameModelChanged.Invoke(_theGameModel); }


		// Properties -------------------------------------
		public PendingMessage PendingMessageForDeletion { get { return _theGameService.PendingMessageForDeletion; } }
		public PendingMessage PendingMessageForSave { get { return _theGameService.PendingMessageForSave; } }


		// Fields -----------------------------------------
		private readonly TheGameModel _theGameModel = null;
		private readonly TheGameView _theGameView = null;
		private readonly ITheGameService _theGameService = null;
		private const int DelayAfterContractStateChange = 5000; // wait X seconds for backend to change. Based on limited trials, this Works! 


		// Initialization Methods -------------------------
		public TheGameController(
			TheGameModel theGameModel,
			TheGameView theGameView,
			ITheGameService theGameService)
		{
			_theGameModel = theGameModel;
			_theGameView = theGameView;
			_theGameService = theGameService;

			_theGameView.SceneManagerComponent.OnSceneLoadingEvent.AddListener(SceneManagerComponent_OnSceneLoadingEvent);
			_theGameView.SceneManagerComponent.OnSceneLoadedEvent.AddListener(SceneManagerComponent_OnSceneLoadedEvent);

			_theGameModel.Gold.OnValueChanged.AddListener((a) => OnTheGameModelChangedRefresh());
			_theGameModel.TreasurePrizeDtos.OnValueChanged.AddListener((a) => OnTheGameModelChangedRefresh());
			_theGameModel.IsRegistered.OnValueChanged.AddListener((a) => OnTheGameModelChangedRefresh());
		}


		// General Methods --------------------------------

		///////////////////////////////////////////
		// Related To: Model
		///////////////////////////////////////////



		///////////////////////////////////////////
		// Related To: View
		///////////////////////////////////////////
		public void PlayAudioClipClick()
		{
			_theGameView.PlayAudioClipClick();
		}


		public async void LoadIntroSceneAsync()
		{
			// Wait, So click sound is audible
			await UniTask.Delay(100);

			string sceneName = _theGameModel.TheGameConfiguration.IntroSceneData.SceneName;
			_theGameView.SceneManagerComponent.LoadScene(sceneName);
		}


		public async void LoadAuthenticationSceneAsync()
		{
			// Wait, So click sound is audible
			await UniTask.Delay(100);

			string sceneName = _theGameModel.TheGameConfiguration.AuthenticationSceneData.SceneName;
			_theGameView.SceneManagerComponent.LoadScene(sceneName);
		}


		public async void LoadViewCollectionSceneAsync()
		{
			// Wait, So click sound is audible
			await UniTask.Delay(100);

			string sceneName = _theGameModel.TheGameConfiguration.ViewCollectionSceneData.SceneName;
			_theGameView.SceneManagerComponent.LoadScene(sceneName);
		}

		public async void LoadSettingsSceneAsync()
		{
			// Wait, So click sound is audible
			await UniTask.Delay(100);

			string sceneName = _theGameModel.TheGameConfiguration.SettingsSceneData.SceneName;
			_theGameView.SceneManagerComponent.LoadScene(sceneName);
		}

		public async void LoadDeveloperConsoleSceneAsync()
		{
			// Wait, So click sound is audible
			await UniTask.Delay(100);

			string sceneName = _theGameModel.TheGameConfiguration.DeveloperConsoleSceneData.SceneName;
			_theGameView.SceneManagerComponent.LoadScene(sceneName);
		}

		public async void LoadGameSceneAsync()
		{
			// Wait, So click sound is audible
			await UniTask.Delay(100);

			string sceneName = _theGameModel.TheGameConfiguration.GameSceneData.SceneName;
			_theGameView.SceneManagerComponent.LoadScene(sceneName);
		}


		public async void LoadPreviousSceneAsync()
		{
			// Wait, So click sound is audible before scene changes
			await UniTask.Delay(100);

			_theGameView.SceneManagerComponent.LoadScenePrevious();
		}


		public async UniTask ShowLoadingDuringMethodAsync(
			bool isVisibleInitial,
			bool isVisibleFinal,
			string message,
			Func<UniTask> task)
		{
			await _theGameView.ShowLoadingDuringMethodAsync(isVisibleInitial, isVisibleFinal, message, task);
		}



		///////////////////////////////////////////
		// Related To: Service
		///////////////////////////////////////////
		public async UniTask<bool> IsRegisteredAsync()
		{
			// Call Service. Sync Model
			bool isRegistered = await _theGameService.IsRegisteredAsync();
			_theGameModel.IsRegistered.Value = isRegistered;

			// Call Service. Sync Model
			int gold = await _theGameService.GetGoldAsync();
			_theGameModel.Gold.Value = gold;

			// Call Service. Sync Model
			List<TreasurePrizeDto> treasurePrizeDtos = await GetTreasurePrizesAsync();
			_theGameModel.TreasurePrizeDtos.Value = treasurePrizeDtos;

			return _theGameModel.IsRegistered.Value;
		}

		public bool IsRegisteredCached()
		{
			return _theGameModel.IsRegistered.Value;
		}


		public async UniTask UnregisterAsync()
		{
			await _theGameService.UnregisterAsync();

			// Wait for contract values to sync so the client will see the changes
			await UniTask.Delay(DelayAfterContractStateChange);

			// Refresh data model
			_theGameModel.ResetAllData();
			await IsRegisteredAsync();
		}


		public async UniTask RegisterAsync()
		{
			await _theGameService.RegisterAsync();

			// Wait for contract values to sync so the client will see the changes
			await UniTask.Delay(DelayAfterContractStateChange);

			// Refresh data model
			await IsRegisteredAsync();
		}


        public async UniTask<int> AddGold(int delta)
		{
			if (delta <= 0)
			{
				Debug.LogError("to add, the delta must be > 0.");
				return 0;
			}


			await _theGameService.SetGoldByAsync(delta);

			// Wait for contract values to sync so the client will see the changes
			await UniTask.Delay(DelayAfterContractStateChange);

			int gold = await _theGameService.GetGoldAsync();
			_theGameModel.Gold.Value = gold;
			return gold;
		}


		public async UniTask<int> SpendGold(int delta)
		{
			if (delta <= 0)
			{
				Debug.LogError("to spend, the delta must be > 0.");
				return 0;
			}

			await _theGameService.SetGoldByAsync(-delta);

			// Wait for contract values to sync so the client will see the changes
			await UniTask.Delay(DelayAfterContractStateChange);

			int gold = await _theGameService.GetGoldAsync();
			_theGameModel.Gold.Value = gold;
			return gold;
		}


		public async UniTask<List<TreasurePrizeDto>> AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
		{
			string result = await _theGameService.AddTreasurePrizeAsync(treasurePrizeDto);

			// Wait for contract values to sync so the client will see the changes
			await UniTask.Delay(DelayAfterContractStateChange);

			List<TreasurePrizeDto> treasurePrizeDtos = await GetTreasurePrizesAsync();
			_theGameModel.TreasurePrizeDtos.Value = treasurePrizeDtos;
			return treasurePrizeDtos;
		}


		public async UniTask<List<TreasurePrizeDto>> SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
		{
			string result = await _theGameService.SellTreasurePrizeAsync(treasurePrizeDto);

			// Wait for contract values to sync so the client will see the changes
			await UniTask.Delay(DelayAfterContractStateChange);

			List<TreasurePrizeDto> treasurePrizeDtos = await GetTreasurePrizesAsync();
			_theGameModel.TreasurePrizeDtos.Value = treasurePrizeDtos;
			return treasurePrizeDtos;
		}


		public async UniTask<List<TreasurePrizeDto>> GetTreasurePrizesAsync()
		{
			List<TreasurePrizeDto> treasurePrizeDtos = await _theGameService.GetTreasurePrizesAsync();
			return treasurePrizeDtos;
		}

		public async UniTask<string> StartGameAndGiveRewardsAsync(int goldAmount)
		{
			if (goldAmount > _theGameModel.Gold.Value)
            {
				Debug.LogWarning("Not enough gold to play. Cancel. That is ok. Go sell nfts or unregister.");
				return "";
            }
			string result = await _theGameService.StartGameAndGiveRewardsAsync(goldAmount);

			// Wait for contract values to sync so the client will see the changes
			await UniTask.Delay(DelayAfterContractStateChange);

			var result2 = await _theGameService.GetRewardsHistoryAsync();
			Debug.Log("types... " + result2);

			return result2;
		}



		// Event Handlers ---------------------------------
		private void SceneManagerComponent_OnSceneLoadingEvent(SceneManagerComponent sceneManagerComponent)
		{
			if (_theGameView.BaseScreenCoverUI.IsVisible)
			{
				_theGameView.BaseScreenCoverUI.IsVisible = false;
			}


			if (DOTween.TotalPlayingTweens() > 0)
			{
				Debug.LogWarning("DOTween.KillAll();");
				DOTween.KillAll();
			}
		}

		private void SceneManagerComponent_OnSceneLoadedEvent(SceneManagerComponent sceneManagerComponent)
		{
			// Do anything?
		}


		public void QuitGame()
		{
			if (Application.isEditor)
			{
				UnityEditor.EditorApplication.isPlaying = false;
			}
			else
			{
				Application.Quit();
			}
		}
	}
}
