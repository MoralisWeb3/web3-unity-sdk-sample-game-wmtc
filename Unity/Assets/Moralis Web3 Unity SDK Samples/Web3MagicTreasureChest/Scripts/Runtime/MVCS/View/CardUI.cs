using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the TreasureChest for the game
	/// </summary>
	public class CardUI : MonoBehaviour
	{
		// Properties -------------------------------------
		public Animator Animator { get { return _animator;}}
		
		// Fields -----------------------------------------
		[Header("References (Scene)")] 
		[SerializeField] 
		private Animator _animator = null;
		
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
