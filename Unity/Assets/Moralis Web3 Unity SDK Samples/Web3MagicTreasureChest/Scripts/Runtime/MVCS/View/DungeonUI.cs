using MoralisUnity.Samples.Shared;
using MoralisUnity.Sdk.DesignPatterns.Creational.Singleton.SingletonMonobehaviour;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.View.UI
{
	/// <summary>
	/// Handles the Dungeon for the game
	/// </summary>
	public class DungeonUI : SingletonMonobehaviour<DungeonUI>
	{
		// Properties -------------------------------------
		

		// Fields -----------------------------------------


		//  Unity Methods----------------------------------
		protected void Awake()
		{
			SharedHelper.SafeDontDestroyOnLoad(gameObject);
		
		}


		// General Methods --------------------------------


        // Event Handlers ---------------------------------


    }
}
