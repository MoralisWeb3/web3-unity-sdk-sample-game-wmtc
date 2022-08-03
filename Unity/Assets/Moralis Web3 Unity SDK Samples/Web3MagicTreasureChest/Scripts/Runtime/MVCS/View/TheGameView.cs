using System;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared;
using MoralisUnity.Samples.Shared.Audio;
using MoralisUnity.Samples.Shared.Components;
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
		public async UniTask ShowLoadingDuringMethodAsync(Func<UniTask> task)
		{
			await ShowLoadingDuringMethodAsync(
				true,
				false,
				SharedConstants.Loading,
				async delegate ()
				{
					await task();
				});
		}
		
		
		/// <summary>
		/// Show a loading screen, during method execution
		/// </summary>
		public async UniTask ShowLoadingDuringMethodAsync(
			bool isVisibleInitial, 
			bool isVisibleFinal, 
			string message, 
			Func<UniTask> task)
		{
			//Debug.Log($"START {message} ");
			BaseScreenCoverUI.IsVisible = isVisibleInitial;	
			BaseScreenCoverUI.MessageText.text = message;
			await task();
			BaseScreenCoverUI.IsVisible = isVisibleFinal;
			//Debug.Log($"END {message} ");
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
