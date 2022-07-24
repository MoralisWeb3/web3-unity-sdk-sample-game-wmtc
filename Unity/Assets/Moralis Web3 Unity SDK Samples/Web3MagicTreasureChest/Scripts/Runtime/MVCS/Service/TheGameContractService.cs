using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service
{
	/// <summary>
	/// Handles communication with external sources (e.g. database/servers/contracts)
	///		* See <see cref="TheGameSingleton"/> 
	/// </summary>
	public class TheGameContractService : ITheGameService
	{
		// Properties -------------------------------------
		public PendingMessage PendingMessageForDeletion { get { return _pendingMessageForDeletion; }}
		public PendingMessage PendingMessageForSave { get { return _pendingMessageForSave; }}
		
		
		// Fields -----------------------------------------
		private readonly PendingMessage _pendingMessageForDeletion = new PendingMessage("Please confirm transaction in your wallet", 0);
		private readonly PendingMessage _pendingMessageForSave = new PendingMessage("Please confirm transaction in your wallet", 0);
		private readonly PropertyContract _propertyContract = null;


		// Initialization Methods -------------------------
		public TheGameContractService()
		{
			_propertyContract = new PropertyContract();
		}
		
		
		// General Methods --------------------------------
		public async UniTask<List<PropertyData>> LoadPropertyDatasAsync()
		{
			// Create Method Return Value
			List<PropertyData> propertyDatas = new List<PropertyData>();
			
			// Check System Status
			bool hasMoralisUser = await TheGameSingleton.Instance.HasMoralisUserAsync();
			if (!hasMoralisUser)
			{
				Debug.LogError(TheGameConstants.ErrorMoralisUserRequired);
				return null;
			}
			
			// Get NFT Info
			NftOwnerCollection nftOwnerCollection = await Moralis.Web3Api.Token.GetNFTOwners(
				_propertyContract.Address,
				ChainList.mumbai);
			
			// Create Method Return Value
			foreach (NftOwner nftOwner in nftOwnerCollection.Result)
			{
				string ownerAddress = nftOwner.OwnerOf;
				string tokenIdString = nftOwner.TokenId;
				string metadata = nftOwner.TokenUri;
				//Debug.Log($"nftOwner ownerAddress={ownerAddress} tokenIdString={tokenIdString} metadata={metadata}");
				propertyDatas.Add(PropertyData.CreateNewPropertyDataFromMetadata(ownerAddress, tokenIdString, metadata));
			}
		
			// Finalize Method Return Value
			return propertyDatas;
		}

		
		public async UniTask<PropertyData> SavePropertyDataAsync(PropertyData propertyData)
		{
			// Mint the NFT
			string newTokenIdString = await _propertyContract.MintPropertyNftAsync(propertyData);
			
			// Update Method Return Value
			propertyData = PropertyData.CreateNewPropertyDataFromMetadata(propertyData.OwnerAddress, newTokenIdString, propertyData.GetMetadata());

			// Finalize Method Return Value
			return propertyData;
		}

		
		public async UniTask DeletePropertyDataAsync(PropertyData propertyData)
		{
			// Burn the NFT
			string result = await _propertyContract.BurnPropertyNftAsync(propertyData);
			Debug.Log($"DeletePropertyDataAsync() Result = {result}");
		}

		
		public async UniTask DeleteAllPropertyDatasAsync(List<PropertyData> propertyDatas)
		{
			// Burn the NFT List
			string result = await _propertyContract.BurnPropertyNftsAsync(propertyDatas);
			Debug.Log($"DeleteAllPropertyDatasAsync() result = {result}");
	
		}

		// Event Handlers ---------------------------------

	}
}
