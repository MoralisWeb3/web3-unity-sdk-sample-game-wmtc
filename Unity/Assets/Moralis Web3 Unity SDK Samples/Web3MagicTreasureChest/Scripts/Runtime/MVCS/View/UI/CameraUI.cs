using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared;
using MoralisUnity.Samples.Shared.Helpers;
using MoralisUnity.Sdk.DesignPatterns.Creational.Singleton.SingletonMonobehaviour;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	public enum CameraPosition
	{
		Intro,
		Authentication,
		Settings,
		DeveloperConsole,
		ViewCollection,
		Game

	}

	/// <summary>
	/// Handles the Camera for the game
	///		* The camera was animated in earlier iterations. Now it does not animate.
	/// </summary>
	public class CameraUI : SingletonMonobehaviour<CameraUI>
	{
		// Properties -------------------------------------
		public GameObject Contents { get { return _contents; }}
		
		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		
		[SerializeField] 
		private GameObject _contents = null;

		[SerializeField]
		private List<ReferenceCamera> _referenceCameras = null;

		private Dictionary<CameraPosition, int> indexByDungeonArea = new Dictionary<CameraPosition, int>();

		//  Unity Methods----------------------------------
		protected void Awake()
		{
			SharedHelper.SafeDontDestroyOnLoad(gameObject);
		
		}

		protected void Start()
		{
			
			if (indexByDungeonArea.Count == 0)
            {
				indexByDungeonArea.Add(CameraPosition.Intro, 0);
				indexByDungeonArea.Add(CameraPosition.Authentication, 1);
				indexByDungeonArea.Add(CameraPosition.Settings, 2);
				indexByDungeonArea.Add(CameraPosition.DeveloperConsole, 3);
				indexByDungeonArea.Add(CameraPosition.ViewCollection, 4);
				indexByDungeonArea.Add(CameraPosition.Game, 5);
			}
		}


		// General Methods --------------------------------
		public async UniTask TweenTo2(CameraPosition dungeonArea)
		{

			float duration = 0.5f;
			float delay = 0.25f * 1;

			int i = indexByDungeonArea[dungeonArea];

			ReferenceCamera referenceCamera = _referenceCameras[i];

			bool isWaiting = true;
			TweenHelper.TransformDOBlendableMoveBy(_contents, _contents.transform.position,
				referenceCamera.transform.position, duration, delay).onComplete = () =>
				{
					isWaiting = false;
				};

			TweenHelper.TransformDORotate(_contents, _contents.transform.rotation.eulerAngles,
				referenceCamera.transform.rotation.eulerAngles, duration, delay);

			await UniTask.WaitWhile(() => isWaiting);

		}



        // Event Handlers ---------------------------------


    }
}
