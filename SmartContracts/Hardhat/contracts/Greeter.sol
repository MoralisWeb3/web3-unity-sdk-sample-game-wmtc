// SPDX-License-Identifier: MIT
pragma solidity ^0.8.9;


///////////////////////////////////////////////////////////
// IMPORTS
///////////////////////////////////////////////////////////
import "hardhat/console.sol";


///////////////////////////////////////////////////////////
// CLASS
//      *   Description         :   Each contract instance 
//                                  manages CRUD for its 
//                                  greeting text message
//      *   Deployment Address  :   
///////////////////////////////////////////////////////////
contract Greeter
{

    ///////////////////////////////////////////////////////////
    // FIELDS
    //      *   Values stored on contract
    ///////////////////////////////////////////////////////////


    // User address who owns this contract instance
    address _owner;


    // Greeting text message
    string _greeting;


    ///////////////////////////////////////////////////////////
    // CONSTRUCTOR
    //      *   Runs when contract is executed
    ///////////////////////////////////////////////////////////
    constructor() 
    {
        _owner = msg.sender;
        _greeting = "Default Greeting";

        console.log(
            "Greeter.constructor() _owner = %s, _greeting = %s",
            _owner,
            _greeting
        );
    }


    ///////////////////////////////////////////////////////////
    // FUNCTION: SETTER
    //      *   Set greeting 
    //      *   Changes contract state, so requires calling via
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function setGreeting(string memory greeting) public 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        _greeting = greeting;
    }

    
    ///////////////////////////////////////////////////////////
    // FUNCTION: GETTER
    // *    Get greeting 
    // *    Changes no contract state, so requires calling via  
    //      either ExecuteContractFunction or RunContractFunction
    ///////////////////////////////////////////////////////////
    function getGreeting() public view returns (string memory) 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        return _greeting;
    }


    ///////////////////////////////////////////////////////////
    // FUNCTION: EXAMPLE OF TESTING & ERROR CHECKING 
    //      *   Set greeting after error checking
    //      *   Changes contract state, so requires calling via
    //          ExecuteContractFunction
    ///////////////////////////////////////////////////////////
    function setGreetingSafe(string memory greeting) public 
    {
        // DISCLAIMER -- NOT A PRODUCTION READY CONTRACT
        // require(msg.sender == _owner);

        bytes memory greetingBytes = bytes(greeting);
        uint256 greetingBytesLength = greetingBytes.length;

        if (greetingBytesLength == 0)
        {
            // What the revert function will undo all state changes.
            //  * It will allow you to return a value
            //  * It will refund any remaining gas to the caller
            revert("Unexpected value for greeting");
        }

        // The require function should be used to ensure valid conditions, 
        // such as inputs, or contract state variables are met, or to validate return values
        require (greetingBytesLength == 0, "Unexpected value for greeting");

        // Properly functioning code should never reach a failing assert statement; 
        // if this happens there is a bug in your contract which you should fix.
        assert (greetingBytesLength == 0);

        setGreeting (greeting);

    }
}


