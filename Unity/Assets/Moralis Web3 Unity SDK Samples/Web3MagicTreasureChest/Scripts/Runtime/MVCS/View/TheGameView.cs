using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MoralisUnity.Samples.Shared;
using MoralisUnity.Samples.Shared.Audio;
using MoralisUnity.Samples.Shared.Components;
using MoralisUnity.Samples.Shared.Helpers;
using MoralisUnity.Samples.Shared.UI;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the UI for the game
	///		* See <see cref="TheGameSingleton"/>
	/// </summary>
	public class TheGameView : MonoBehaviour
	{
		// Properties -------------------------------------
		public SceneManagerComponent SceneManagerComponent { get { return _sceneManagerComponent;}}
		public BaseScreenMessageUI BaseScreenCoverUI { get { return _baseScreenMessageUI; }}
		
		
		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		[SerializeField] 
		private SceneManagerComponent _sceneManagerComponent = null;
		
		[SerializeField] 
		private BaseScreenMessageUI _baseScreenMessageUI = null;

		//[Header("References (Project)")] 
	
		// General Methods --------------------------------
		/// <summary>
		/// Show a loading screen, during method execution
		/// </summary>
		public async UniTask ShowMessageDuringMethodAsync(
			string message, 
			Func<UniTask> task)
		{
			BaseScreenCoverUI.BlocksRaycasts = true;
			await TweenHelper.AlphaDoFade(BaseScreenCoverUI, 0, 1, 0.25f);
			BaseScreenCoverUI.MessageText.text = message;
			await task();
			await TweenHelper.AlphaDoFade(BaseScreenCoverUI, 1, 0, 0.25f);
			BaseScreenCoverUI.BlocksRaycasts = false;
		}
		
		public async UniTask ShowMessageWithDelayAsync(string message, int delayMilliseconds)
		{
			BaseScreenCoverUI.BlocksRaycasts = true;
			await TweenHelper.AlphaDoFade(BaseScreenCoverUI, 0, 1, 0.25f);
			BaseScreenCoverUI.MessageText.text = message;
			await UniTask.Delay(delayMilliseconds);
			await TweenHelper.AlphaDoFade(BaseScreenCoverUI, 1, 0, 0.25f);
			BaseScreenCoverUI.BlocksRaycasts = false;
		}

		/// <summary>
		/// Play generic click sound
		/// </summary>
		public void PlayAudioClipClick()
		{
			SoundManager.Instance.PlayAudioClip(0);
		}
		
		// Event Handlers ---------------------------------

	}
}
