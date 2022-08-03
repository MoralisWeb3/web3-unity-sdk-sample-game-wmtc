using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MoralisUnity.Samples.Shared;
using MoralisUnity.Samples.Shared.Components;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
	public struct Reward 
	{
		public string Title;
		public uint Type;
		public uint Price;
	}
	
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
		// Related To: Service
		///////////////////////////////////////////

		// GETTER Methods -------------------------
		public async UniTask<bool> IsRegisteredAsync()
		{
			// Call Service. Sync Model
			bool isRegistered = await _theGameService.IsRegisteredAsync();
			_theGameModel.IsRegistered.Value = isRegistered;

			// Call Service. Sync Model
			int gold = await _theGameService.GetGoldAsync();
			_theGameModel.Gold.Value = gold;

			// Call Service
			string msgSender = await _theGameService.GetMsgSenderAsync();

			// Call Service. Sync Model
			List<TreasurePrizeDto> treasurePrizeDtos = await GetTreasurePrizesAsync();
			_theGameModel.TreasurePrizeDtos.Value = treasurePrizeDtos;

			return _theGameModel.IsRegistered.Value;
		}

		public bool IsRegisteredCached()
		{
			return _theGameModel.IsRegistered.Value;
		}

		
		public async UniTask<List<TreasurePrizeDto>> GetTreasurePrizesAsync()
		{
			List<TreasurePrizeDto> treasurePrizeDtos = await _theGameService.GetTreasurePrizesAsync();
			return treasurePrizeDtos;
		}
		
		public async UniTask<Reward> GetRewardsHistoryAsync()
		{
			Reward result = await _theGameService.GetRewardsHistoryAsync();
			return result;
		}

		// SETTER Methods -------------------------
		public async UniTask UnregisterAsync()
		{
			await _theGameService.UnregisterAsync();
			_theGameModel.IsRegistered.Value = await IsRegisteredAsync();
			
			// Wait for contract values to sync so the client will see the changes
			await _theGameService.DelayExtraAfterStateChange();
		}

		public async UniTask RegisterAsync()
		{
			await _theGameService.RegisterAsync();
			_theGameModel.IsRegistered.Value = await IsRegisteredAsync();
			
			// Wait for contract values to sync so the client will see the changes
			await _theGameService.DelayExtraAfterStateChange();
		}


        public async UniTask<int> SetGoldByAsync(int delta)
		{
			await _theGameService.SetGoldByAsync(delta);
			int gold = await _theGameService.GetGoldAsync();
			_theGameModel.Gold.Value = gold;
			
			// Wait for contract values to sync so the client will see the changes
			await _theGameService.DelayExtraAfterStateChange();
			
			return gold;
		}


		public async UniTask<List<TreasurePrizeDto>> AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
		{
			await _theGameService.AddTreasurePrizeAsync(treasurePrizeDto);
			
			// Wait for contract values to sync so the client will see the changes
			await _theGameService.DelayExtraAfterStateChange();

			List<TreasurePrizeDto> treasurePrizeDtos = await GetTreasurePrizesAsync();
			_theGameModel.TreasurePrizeDtos.Value = treasurePrizeDtos;
			
			return treasurePrizeDtos;
		}


		public async UniTask<List<TreasurePrizeDto>> SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto)
		{
			await _theGameService.SellTreasurePrizeAsync(treasurePrizeDto);

			// Wait for contract values to sync so the client will see the changes
			await _theGameService.DelayExtraAfterStateChange();

			List<TreasurePrizeDto> treasurePrizeDtos = await GetTreasurePrizesAsync();
			_theGameModel.TreasurePrizeDtos.Value = treasurePrizeDtos;
			return treasurePrizeDtos;
		}

		public async UniTask<List<TreasurePrizeDto>> DeleteAllTreasurePrizeAsync()
		{
			await _theGameService.DeleteAllTreasurePrizeAsync();

			// Wait for contract values to sync so the client will see the changes
			await _theGameService.DelayExtraAfterStateChange();

			List<TreasurePrizeDto> treasurePrizeDtos = await GetTreasurePrizesAsync();
			_theGameModel.TreasurePrizeDtos.Value = treasurePrizeDtos;
			return treasurePrizeDtos;
		}

		public async UniTask StartGameAndGiveRewardsAsync(int goldAmount)
		{
			if (goldAmount > _theGameModel.Gold.Value)
            {
				Debug.LogWarning("Not enough gold to play. Cancel. That is ok. Go sell nfts or unregister.");
            }
			else
			{
				await _theGameService.StartGameAndGiveRewardsAsync(goldAmount);

				// Call Service. Sync Model
				List<TreasurePrizeDto> treasurePrizeDtos = await GetTreasurePrizesAsync();
				_theGameModel.TreasurePrizeDtos.Value = treasurePrizeDtos;
			
				// Call Service. Sync Model
				int gold = await _theGameService.GetGoldAsync();
				_theGameModel.Gold.Value = gold;
			
				// Wait for contract values to sync so the client will see the changes
				await _theGameService.DelayExtraAfterStateChange();
			
				Reward reward = await _theGameService.GetRewardsHistoryAsync();
				
				// TODO: Remove after both services work 100%
				StringBuilder output = new StringBuilder();
				output.AppendHeaderLine($"GetRewardsHistoryAsync()...\n");
				output.AppendBullet($"Gold Spent = {goldAmount}");
				output.AppendBullet($"reward.Title = {reward.Title}");
				output.AppendBullet($"reward.Type = {reward.Type}");
				output.AppendBullet($"reward.Price = {reward.Price}");
				Debug.LogWarning(output);

			}
		}

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

		public async UniTask ShowLoadingDuringMethodAsync(Func<UniTask> task)
		{
			await _theGameView.ShowLoadingDuringMethodAsync(task);
		}
	}
}
