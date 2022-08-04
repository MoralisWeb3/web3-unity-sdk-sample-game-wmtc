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

 function fromHexChar(uint8 c) public pure returns (uint8) 
 {
        if (bytes1(c) >= bytes1('0') && bytes1(c) <= bytes1('9')) {
            return c - uint8(bytes1('0'));
        }
        if (bytes1(c) >= bytes1('a') && bytes1(c) <= bytes1('f')) {
            return 10 + c - uint8(bytes1('a'));
        }
        if (bytes1(c) >= bytes1('A') && bytes1(c) <= bytes1('F')) {
            return 10 + c - uint8(bytes1('A'));
        }
        return 0;
    }

   function hexStringToAddress(string memory s) public pure returns (bytes memory) 
   {
        bytes memory ss = bytes(s);
        require(ss.length%2 == 0); // length must be even
        bytes memory r = new bytes(ss.length/2);
        for (uint i=0; i<ss.length/2; ++i) {
            r[i] = bytes1(fromHexChar(uint8(ss[2*i])) * 16 +
                        fromHexChar(uint8(ss[2*i+1])));
        }

        return r;

    }

  function toAddress(string memory s) public pure returns (address) 
  {
        bytes memory _bytes = hexStringToAddress(s);
        require(_bytes.length >= 1 + 20, "toAddress_outOfBounds");
        address tempAddress;

        assembly {
            tempAddress := div(mload(add(add(_bytes, 0x20), 1)), 0x1000000000000000000000000)
        }

        return tempAddress;
    }

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: DEBUGGING
    ///////////////////////////////////////////////////////////
    function getMsgSender(string memory myTest) public pure returns (string memory msgSender)
    {
        address a = toAddress (myTest);
        msgSender = Strings.toHexString(uint256(uint160(a)), 20);
    }

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REGISTRATION
    ///////////////////////////////////////////////////////////
    function isRegistered(string memory myTest) public view returns (bool isPlayerRegistered)
    {
        address a = toAddress (myTest);
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        isPlayerRegistered = _isRegistered[a];
    }

    function isRegistered2(string calldata myTest) public view returns (bool isPlayerRegistered)
    {
        string memory s = myTest;
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        //isPlayerRegistered = _isRegistered[a];
        isPlayerRegistered = true;
    }

    //fails, useraddress is required
    function isRegistered3(uint256 myTest) public view returns (bool isPlayerRegistered)
    {
        uint256 s = myTest;
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        //isPlayerRegistered = _isRegistered[a];
        isPlayerRegistered = true;
    }

    //wORKS!
    function isRegistered4() public view returns (bool isPlayerRegistered)
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        //isPlayerRegistered = _isRegistered[a];
        isPlayerRegistered = true;
    }

    function register() public
    {
        _isRegistered[msg.sender] = true;
        setGold(100);
    }


    function unregister() public
    {
        _isRegistered[msg.sender] = false;
        setGold(0);
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REWARDS
    ///////////////////////////////////////////////////////////
    function startGameAndGiveRewards(uint256 goldAmount) public
    {
        require(goldAmount > 0, "goldAmount must be > 0 to start the game");

        require(getGold(msg.sender) >= goldAmount, "getGold() must be >= goldAmount to start the game");

        require(_isRegistered[msg.sender], "Must be registered to start the game.");

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

    function getRewardsHistory(address userAddress) public view returns (string memory rewardTitle, uint rewardType, uint rewardPrice )
    {
        require(_isRegistered[userAddress], "Must be registered to start the game.");

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
    function getGold(address userAddress) public view returns (uint256 balance)
    {
        balance = Gold(_goldContractAddress).getGold(userAddress);
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


