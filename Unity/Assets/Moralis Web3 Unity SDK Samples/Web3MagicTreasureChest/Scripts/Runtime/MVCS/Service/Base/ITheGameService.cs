
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

		// VARIOUS RETURNS
        UniTask<bool> IsRegisteredAsync();
		UniTask<int> GetGoldAsync();

		// STRING RETURNS
		UniTask<string> RegisterAsync();
		UniTask<string> UnregisterAsync();
		
		UniTask<string> SetGoldAsync(int targetBalance);
		UniTask<string> SetGoldByAsync(int deltaBalance);
        UniTask<List<TreasurePrizeDto>> AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto);
        UniTask<List<TreasurePrizeDto>> SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto);
    }
}
