using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MoralisUnity.Samples.Shared.Components;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
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
		public PendingMessage PendingMessageForDeletion { get { return _theGameService.PendingMessageForDeletion; }}
		public PendingMessage PendingMessageForSave { get { return _theGameService.PendingMessageForSave; }}


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
		public async UniTask<bool> IsRegisteredUserAsync()
		{
			return await _theGameService.IsRegisteredUserAsync();
		}

		public async UniTask UnregisterUserAsync()
		{
			await _theGameService.UnregisterUserAsync();
		}

		public async UniTask RegisterUserAsync()
		{
			await _theGameService.RegisterUserAsync();
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
				// Keep warning for a bit in case of undesirable results
				Debug.LogWarning("DOTween.KillAll();");
				DOTween.KillAll();
				//var tweens = DOTween.PlayingTweens();
				//for (int i = 0; i < tweens.Count; i++)
				//{
				//	tweens[i]?.Kill();
				//}
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

        public void GiveRewards()
        {
			Debug.Log("GiveRewards");

			// Must 'set' to trigger events properly
			_theGameModel.Gold.Value++;

			// Must 'set' to trigger events properly
			var list = _theGameModel.TreasurePrizeDtos.Value;
			list.Add(new TreasurePrizeDto("new one"));
			_theGameModel.TreasurePrizeDtos.Value = list;
        }
    }

}
