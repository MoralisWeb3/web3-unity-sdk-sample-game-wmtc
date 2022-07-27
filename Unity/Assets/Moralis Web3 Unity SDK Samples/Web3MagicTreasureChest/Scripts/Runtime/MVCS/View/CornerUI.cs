using TMPro;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the top navigation
	/// </summary>
	public class CornerUI : MonoBehaviour
	{
		// Properties -------------------------------------
		public TMP_Text Text { get { return _text; }}

		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		[SerializeField] 
		private TMP_Text _text = null;

		//[Header("References (Project)")] 

		//  Unity Methods----------------------------------
		protected void Start()
		{

		}
		
		protected void Update()
		{

		}
		
		// General Methods --------------------------------

		
		// Event Handlers ---------------------------------

	}
}
