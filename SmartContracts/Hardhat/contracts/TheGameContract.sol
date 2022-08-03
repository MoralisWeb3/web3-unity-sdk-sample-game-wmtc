// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "hardhat/console.sol";
import "contracts/Gold.sol";
import "contracts/TreasurePrize.sol";




///////////////////////////////////////////////////////////
// CLASS
//      *   Description         :   The proxy contact
//                                  for all other contracts
//      *   Deployment Address  :   
///////////////////////////////////////////////////////////
contract TheGameContract
{

    ///////////////////////////////////////////////////////////
    // STRUCTS
    ///////////////////////////////////////////////////////////
    struct Reward 
    {
        string Title;
        uint Type;
        uint Price;
    }

    ///////////////////////////////////////////////////////////
    // FIELDS
    //      *   Values stored on contract
    ///////////////////////////////////////////////////////////


    // Stores address of the Gold contract, to be called
    address _goldContractAddress;

    // Stores address of the TreasurePrize contract, to be called
    address _treasurePrizeContractAddress;

    bool _isHackyRegisteredBool = false;

    mapping(address => bool) private _isRegistered;

    // Stores the most recent reward
    mapping (address => Reward) private _lastReward;

    ///////////////////////////////////////////////////////////
    // CONSTRUCTOR
    //      *   Runs when contract is executed
    ///////////////////////////////////////////////////////////
    constructor(address goldContractAddress, address treasurePrizeContractAddress) 
    {
        _goldContractAddress = goldContractAddress;
        _treasurePrizeContractAddress = treasurePrizeContractAddress;

        console.log(
            "TheGameContract.constructor() _goldContractAddress = %s, _treasurePrizeContractAddress = %s",
            _goldContractAddress,
            _treasurePrizeContractAddress
        );
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: DEBUGGING
    ///////////////////////////////////////////////////////////
    function getMsgSender() public view returns (string memory msgSender)
    {
        msgSender = Strings.toHexString(uint256(uint160(msg.sender)), 20);
    }

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REGISTRATION
    ///////////////////////////////////////////////////////////
    function isRegistered() public view returns (bool isPlayerRegistered)
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        isPlayerRegistered = _isHackyRegisteredBool;
    }


    function register() public
    {
        _isRegistered[msg.sender] = true;
        _isHackyRegisteredBool = true;
        setGold(100);
    }


    function unregister() public
    {
        _isRegistered[msg.sender] = false;
        _isHackyRegisteredBool = false;
        setGold(0);
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REWARDS
    ///////////////////////////////////////////////////////////
    function startGameAndGiveRewards(uint256 goldAmount) public
    {
        require(goldAmount > 0, "goldAmount must be > 0 to start the game");

        require(getGold() >= goldAmount, "getGold() must be >= goldAmount to start the game");

        require(_isHackyRegisteredBool, "Must be registered to start the game.");

        // Deduct gold
        setGoldBy(-int(goldAmount));

        uint random = randomRange (0, 100, 1);
        uint price = random;
        uint theType = 0;
        string memory title = "";

        if (random < 50)
        {
            // REWARD: Gold!
            theType = 1;
            title = "This is gold.";
            setGoldBy (int(random));
        } 
        else 
        {
            // REWARD: Prize!
            theType = 2;
            title = "This is an nft.";

            //NOTE: Metadata structure must match in both: TheGameContract.sol and TreasurePrizeDto.cs
            string memory metadata = string(abi.encodePacked("Title=", title, "|Price=", price));
            mintNft (metadata);     
        }

        _lastReward[msg.sender] = Reward (
        {
            Title: title,
            Type: theType,
            Price: price
        });

    }

    function getRewardsHistory() public view returns (string memory rewardTitle, uint rewardType, uint rewardPrice )
    {
        require(_isHackyRegisteredBool, "Must be registered to start the game.");

        rewardTitle = _lastReward[msg.sender].Title;
        rewardType = _lastReward[msg.sender].Type;
        rewardPrice = _lastReward[msg.sender].Price;
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: RANDOM
    ///////////////////////////////////////////////////////////
    function randomRange (uint min, uint max, uint nonce) public view returns (uint) 
    {
        // The nonce is especially useful for unit-tests where the block **MAYBE** will not change enough
        uint randomnumber = uint(keccak256(abi.encodePacked(block.timestamp, block.difficulty, msg.sender, nonce))) % (max);
        randomnumber = randomnumber + min;
        return randomnumber;
    }
    

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: GOLD
    ///////////////////////////////////////////////////////////
    function getGold() public view returns (uint256 balance)
    {
        balance = Gold(_goldContractAddress).getGold(msg.sender);
    }


    function setGold(uint256 targetBalance) public
    {
        Gold(_goldContractAddress).setGold(msg.sender, targetBalance);
    }


    function setGoldBy(int delta) public
    {
        Gold(_goldContractAddress).setGoldBy(msg.sender, delta); 
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: TREASUREPRIZE
    ///////////////////////////////////////////////////////////
    function mintNft(string memory tokenURI) public 
    {
        TreasurePrize(_treasurePrizeContractAddress).mintNft(msg.sender, tokenURI);
    }


    function burnNft(uint256 tokenId) public
    {
        TreasurePrize(_treasurePrizeContractAddress).burnNft(tokenId);
    }


    function burnNfts(uint256[] calldata tokenIds) public
    {
        TreasurePrize(_treasurePrizeContractAddress).burnNfts(tokenIds); 
    }
}


