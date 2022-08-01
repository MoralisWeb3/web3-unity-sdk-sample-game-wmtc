// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "hardhat/console.sol";
import "contracts/Gold.sol";


///////////////////////////////////////////////////////////
// CLASS
//      *   Description         :   The proxy contact
//                                  for all other contracts
//      *   Deployment Address  :   
///////////////////////////////////////////////////////////
contract TheGameContract
{

    ///////////////////////////////////////////////////////////
    // FIELDS
    //      *   Values stored on contract
    ///////////////////////////////////////////////////////////


    // Stores address of the Gold contract, to be called
    address _goldContractAddress;

    // Determines if the player has registered yet
    mapping (address => uint256) _isRegistered;


    ///////////////////////////////////////////////////////////
    // CONSTRUCTOR
    //      *   Runs when contract is executed
    ///////////////////////////////////////////////////////////
    constructor(address goldContractAddress) 
    {
        _goldContractAddress = goldContractAddress;

        console.log(
            "TheGameContract.constructor() goldContractAddress = %s, ",
            goldContractAddress
        );
    }


    ///////////////////////////////////////////////////////////
    // FUNCTIONS: REGISTRATION
    ///////////////////////////////////////////////////////////
    function isRegistered() public view returns (uint256 isPlayerRegistered)
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // CONSIDER TO ADD MORE SECURITY CHECKS TO EVERY FUNCTION
        // require(msg.sender == _owner);
        isPlayerRegistered = _isRegistered[msg.sender];
    }


    function register() public
    {
        _isRegistered[msg.sender] = 1;
        setGold(99);
    }


    function unregister() public
    {
        _isRegistered[msg.sender] = 0;
        setGold(0);
    }

    ///////////////////////////////////////////////////////////
    // FUNCTIONS: GOLD
    ///////////////////////////////////////////////////////////
    function getGold() public view returns (uint256 balance)
    {
        balance = Gold(_goldContractAddress).getGold();
    }


    function setGold(uint256 targetBalance) public
    {
        Gold(_goldContractAddress).setGold(targetBalance);
    }


    function setGoldBy(int delta) public
    {
        Gold(_goldContractAddress).setGoldBy(delta); 
    }
   
}


