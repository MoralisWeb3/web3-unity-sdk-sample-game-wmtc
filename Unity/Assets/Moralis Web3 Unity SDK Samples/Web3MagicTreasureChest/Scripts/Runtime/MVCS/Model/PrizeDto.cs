
using MoralisUnity.Samples.Shared.Data.Types;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model
{
	/// <summary>
	/// Stores data for the game
	/// </summary>
	public class PrizeDto : Nft
	{
		// Properties -------------------------------------


		// Fields -----------------------------------------


		// Initialization Methods -------------------------
		public PrizeDto(string ownerAddress, string metadata) : base (ownerAddress, metadata)
		{
		}


		// General Methods --------------------------------


		// Event Handlers ---------------------------------

	}
}
