using System;
using Cysharp.Threading.Tasks;
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
		}


		// General Methods --------------------------------
		
		///////////////////////////////////////////
		// Related To: Model
		///////////////////////////////////////////
		public void AddPropertyData(PropertyData propertyData)
		{
			_theGameModel.AddPropertyData(propertyData);
		}
		
		
		public void RemovePropertyData(PropertyData propertyData)
		{
			_theGameModel.RemovePropertyData(propertyData);
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

		
		///////////////////////////////////////////
		// Related To: Service
		///////////////////////////////////////////


		// Event Handlers ---------------------------------
		private void SceneManagerComponent_OnSceneLoadingEvent(SceneManagerComponent sceneManagerComponent)
		{
			if (_theGameView.BaseScreenCoverUI.IsVisible)
			{
				_theGameView.BaseScreenCoverUI.IsVisible = false;
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
