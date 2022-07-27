
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service
{
	/// <summary>
	/// Handles communication with external sources (e.g. database/servers/contracts)
	///		* See <see cref="TheGameSingleton"/> 
	/// </summary>
	public interface ITheGameService 
	{
		// Properties -------------------------------------
		PendingMessage PendingMessageForDeletion { get; }
		PendingMessage PendingMessageForSave { get; }
		
		// General Methods --------------------------------

        UniTask<bool> IsRegisteredUserAsync();
        UniTask<bool> RegisterUserAsync();
		UniTask<bool> UnregisterUserAsync();
		UniTask<int> AddGold(int delta);
		UniTask<int> SpendGold(int delta);
        UniTask<List<TreasurePrizeDto>> AddTreasurePrize(TreasurePrizeDto treasurePrizeDto);
        UniTask<List<TreasurePrizeDto>> SellTreasurePrize(TreasurePrizeDto treasurePrizeDto);
    }
}
