
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

		// Web3 API Call - Various Return Types
		Task<List<TreasurePrizeDto>> GetTreasurePrizesAsync();

		// RunContractFunction - Various Return Types
		UniTask<bool> IsRegisteredAsync();
		UniTask<int> GetGoldAsync();
		UniTask<string> GetRewardsHistoryAsync();
		UniTask<string> GetMsgSender();

		// ExecuteContractFunction - Must Be String Return Type
		UniTask<string> RegisterAsync();
		UniTask<string> UnregisterAsync();
		UniTask<string> SetGoldAsync(int targetBalance);
		UniTask<string> SetGoldByAsync(int deltaBalance);
		UniTask<string> AddTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto);
		UniTask<string> SellTreasurePrizeAsync(TreasurePrizeDto treasurePrizeDto);
		UniTask<string> StartGameAndGiveRewardsAsync(int goldAmount);
        
    }
}
