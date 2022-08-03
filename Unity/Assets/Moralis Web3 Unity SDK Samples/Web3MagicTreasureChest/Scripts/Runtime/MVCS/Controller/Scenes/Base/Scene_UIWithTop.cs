using MoralisUnity.Samples.Shared.UI;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Controller
{
	/// <summary>
	/// Any shared functionality for all Scene-specific UIs
	/// </summary>
	public class Scene_UIWithTop : Scene_BaseUI
	{
		// Properties -------------------------------------
		public TopUI TopUI { get { return _topUI; } }
	
		// Fields -----------------------------------------
		[Header("References (Base)")]
		[SerializeField]
		private TopUI _topUI = null;
		
		// Unity Methods ----------------------------------
		protected override void Start()
		{
			base.Start();
			
			// AddListener - Update View When Model Changes
			TheGameSingleton.Instance.TheGameController.OnTheGameModelChanged.AddListener(OnTheGameModelChanged);
			TheGameSingleton.Instance.TheGameController.OnTheGameModelChangedRefresh();
		}
		
		// General Methods --------------------------------

		
		// Event Handlers ---------------------------------
		public void OnTheGameModelChanged(TheGameModel theGameModel)
		{
			_topUI.GoldCornerUI.Text.text = string.Format("{000:000}", theGameModel.Gold.Value);
			_topUI.CollectionUI.Text.text = string.Format("{00:00}/10", theGameModel.TreasurePrizeDtos.Value.Count);
		}
	}
}
