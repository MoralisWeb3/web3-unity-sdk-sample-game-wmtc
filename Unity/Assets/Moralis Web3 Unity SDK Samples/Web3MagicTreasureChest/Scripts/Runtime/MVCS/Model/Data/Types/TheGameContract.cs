
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using MoralisUnity.Samples.Shared.Data.Types;
using MoralisUnity.Web3Api.Models;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types
{
	public class MessageHolder
    {
		public string message = "";
    }

	/// <summary>
	/// Wrapper class for a Web3API Eth Contract.
	/// </summary>
	public class TheGameContract : Contract
	{

		// Properties -------------------------------------
		public override ChainList ChainList { get { return ChainList.cronos_testnet; } }


		// Fields -----------------------------------------
		private const bool IsLogging = true;


		// Initialization Methods -------------------------
		protected override void SetContractDetails()
		{

			_address = "0xB6d9A56eF58fFC434eA494425671d032bd1a7f6c";
			_abi = "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"goldContractAddress\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"getGold\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"balance\",\"type\":\"uint256\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getMessage01\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"message\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"getMessage02\",\"outputs\":[{\"internalType\":\"string\",\"name\":\"message\",\"type\":\"string\"}],\"stateMutability\":\"pure\",\"type\":\"function\"}]";

		}


		/// <summary>
		/// Format for ABI:
		///		*  ExecuteContractFunction - requires string
		///		*  RunContractFunction - requires object[]. This must be manually created from the string
		/// </summary>
		/// <returns></returns>
		protected override object[] GetAbiObject()
        {
			ContractAbi contractAbi = new ContractAbi();

			List<object> cInputParams = new List<object>();
			cInputParams.Add(new { internalType = "address", name = "goldContractAddress", type = "address" });
			contractAbi.AddConstructor(cInputParams);

			// getGold
			List<object> ggInputParams = new List<object>();
			List<object> ggOutputParams = new List<object>();
			ggOutputParams.Add(new { internalType = "uint256", name = "balance", type = "uint256" });
			contractAbi.AddFunction("getGold", "view", ggInputParams, ggOutputParams);

			// getMessage01
			List<object> gtInputParams = new List<object>();
			List<object> gtOutputParams = new List<object>();
			gtOutputParams.Add(new { internalType = "string", name = "message", type = "string" });
			contractAbi.AddFunction("getMessage01", "pure", gtInputParams, gtOutputParams);

			// getMessage02
			List<object> gt2InputParams = new List<object>();
			List<object> gt2OutputParams = new List<object>();
			gt2InputParams.Add(new { internalType = "string", name = "message", type = "string" });
			gt2OutputParams.Add(new { internalType = "string", name = "message", type = "string" });
			contractAbi.AddFunction("getMessage02", "view", gt2InputParams, gt2OutputParams);
			return contractAbi.ToObjectArray();
		}


		// General Methods --------------------------------
		/*
		 * LEARNINGS. 
		 * 
		 * 1. WHEN null is expected for ExecuteContractFunctionAsync, both of these work...
		 * object[] args = null;
		 * 
		 * object[] args =
		 * {
		 * };
		 * 
		 * 
		 * 
		 */

		public async UniTask<string> getMessage01()
		{
			object[] args =
			{
			};

			string result = await RunContractFunctionAsync("getMessage01", args, IsLogging);

			return result;
		}

		public async UniTask<string> getMessage02()
		{
			string message = "hello 02";
			object[] args =
			{
				message
			};

			string result = await RunContractFunctionAsync("getMessage02", args, IsLogging);

			return result;
		}




		public async UniTask<string> MintPropertyNftAsync (PropertyData propertyData)
		{
			string metadata = propertyData.GetMetadata();
			object[] args =
			{
				metadata
			};
			
			string result = await ExecuteContractFunctionAsync("mintPropertyNft", args, IsLogging);
			return result;
		}
		

		public async UniTask<string> BurnPropertyNftAsync (PropertyData propertyData)
		{
			int tokenId = propertyData.TokenId;
			
			if (tokenId == PropertyData.NullTokenAddress)
			{
				Debug.Log("BurnPropertyNftAsync() failed. tokenId must be NOT null. " +
				          "Was this NFT just created? Leave and return to Scene so it gets loaded from online");
				return "";
			}
				
			object[] args =
			{
				tokenId
			};
			
			const bool isLogging = true;
			return await ExecuteContractFunctionAsync("burnPropertyNft", args, isLogging);
		}


		// Event Handlers ---------------------------------
		public async Task<string> BurnPropertyNftsAsync(List<PropertyData> propertyDatas)
		{
			int[] tokenIds = new int[propertyDatas.Count];
			for  (int i = 0; i < propertyDatas.Count; i++)
			{
				int tokenId = propertyDatas[i].TokenId;
			
				if (tokenId == PropertyData.NullTokenAddress)
				{
					Debug.Log("BurnPropertyNftsAsync() failed. tokenId must be NOT null. " +
					          "Was this NFT just created? Leave and return to Scene so it gets loaded from online");
					return "";
				}
			
				tokenIds[i] = tokenId;
			}
			
			object[] args =
			{
				tokenIds
			};
			
			const bool isLogging = true;
			string result = await ExecuteContractFunctionAsync("burnPropertyNfts", args, isLogging);
			return result;
		}
	}
}
