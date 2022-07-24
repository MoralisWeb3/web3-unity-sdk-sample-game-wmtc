
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
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
		UniTask<List<PropertyData>> LoadPropertyDatasAsync();
		UniTask<PropertyData> SavePropertyDataAsync(PropertyData propertyData);
		UniTask DeletePropertyDataAsync(PropertyData propertyData);
		UniTask DeleteAllPropertyDatasAsync(List<PropertyData> propertyDatas);
	}
}
