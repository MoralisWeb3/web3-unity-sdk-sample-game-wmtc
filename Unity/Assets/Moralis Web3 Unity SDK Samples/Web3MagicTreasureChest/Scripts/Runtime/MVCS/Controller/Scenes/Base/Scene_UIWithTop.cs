using MoralisUnity.Samples.Shared.UI;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI;
using UnityEngine;
using WalletConnectSharp.Unity;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
	/// <summary>
	/// Any shared functionality for all Scene-specific UIs
	/// </summary>
	public class Scene_UIWithTop : Scene_BaseUI
	{
		// Properties -------------------------------------
		public TopUI TopUI { get { return _topUI; } }

		public bool IsObservingOnTheGameModelChanged
		{
			get
			{
				return _isObservingOnTheGameModelChanged;
			}
			set
			{
				_isObservingOnTheGameModelChanged = value;
				if (_isObservingOnTheGameModelChanged)
				{
					TheGameSingleton.Instance.TheGameController.OnTheGameModelChangedRefresh();
				}
			}
		}
	
		// Fields -----------------------------------------
		[Header("References (Base)")]
		[SerializeField]
		private TopUI _topUI = null;

		[SerializeField]
		private WalletConnect _walletConnect;

		
		/// <summary>
		/// Determines if the ui will auto-update.
		/// This is sometimes disabled to build suspense
		/// </summary>
		private bool _isObservingOnTheGameModelChanged = true;
		
		// Unity Methods ----------------------------------
		protected override void Start()
		{
			base.Start();
			
			// AddListener - Update View When Model Changes
			TheGameSingleton.Instance.TheGameController.OnTheGameModelChanged.AddListener(OnTheGameModelChanged);
			TheGameSingleton.Instance.TheGameController.OnTheGameModelChangedRefresh();
		}
		
		protected void OnDestroy()
		{
			// Some scenes like Scene02_Authentication do not want the WalletConnect to exist prior
			// to scene load. So always clean it up from this scene
			if (_walletConnect != null)
			{
				GameObject.Destroy(_walletConnect);
			}
		}
		
		// General Methods --------------------------------

		
		// Event Handlers ---------------------------------
		public void OnTheGameModelChanged(TheGameModel theGameModel)
		{
			if (!_isObservingOnTheGameModelChanged)
			{
				return;
			}
			_topUI.GoldCornerUI.Text.text = string.Format("{000:000}", theGameModel.Gold.Value);
			_topUI.CollectionUI.Text.text = string.Format("{00:00}/10", theGameModel.TreasurePrizeDtos.Value.Count);
		}
	}
}
