using MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Model.Data.Types;
using MoralisUnity.Sdk.Exceptions;
using UnityEngine;

namespace MoralisUnity.Samples.Web3MagicTreasureChest.MVCS.Service
{
	/// <summary>
	/// Creates a concrete <see cref="ITheGameService"/>
	/// based on <see cref="TheGameServiceType"/>
	/// </summary>
	public class TheGameServiceFactory
	{
		// Properties -------------------------------------
		
		
		// Fields -----------------------------------------
		
		
		// General Methods --------------------------------
		public ITheGameService Create (TheGameServiceType theGameServiceType)
		{
			Debug.Log($"TheGameServiceFactory.Create() type = {theGameServiceType}");
			
			ITheGameService theGameService = null;
			switch (theGameServiceType)
			{
				case TheGameServiceType.Contract:
					theGameService = new TheGameContractService();
					break;
				case TheGameServiceType.LocalDiskStorage:
					theGameService = new TheGameLocalDiskStorageService();
					break;
				default:
					SwitchDefaultException.Throw(theGameServiceType);
					break;
			}

			return theGameService;
		}

		
		// Event Handlers ---------------------------------
	}
}
