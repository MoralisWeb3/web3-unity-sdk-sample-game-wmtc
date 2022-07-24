using System;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Audio;
using MoralisUnity.Samples.Shared.Components;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model
{
	/// <summary>
	/// Handles the UI for the game
	///		* See <see cref="TheGameSingleton"/>
	/// </summary>
	public class TheGameView : MonoBehaviour
	{
		// Properties -------------------------------------
		public TheGameConfiguration TheGameConfiguration { get { return _theGameConfiguration; } }
		public SceneManagerComponent SceneManagerComponent { get { return _sceneManagerComponent;}}
		public BaseScreenCoverUI BaseScreenCoverUI { get { return _baseScreenCoverUI;}}
		
		
		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		[SerializeField] 
		private SceneManagerComponent _sceneManagerComponent = null;
		
		[SerializeField] 
		private BaseScreenCoverUI _baseScreenCoverUI = null;

		[Header("References (Project)")] 
		[SerializeField]
		private TheGameConfiguration _theGameConfiguration = null;
	
		// General Methods --------------------------------
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
