// Solidity program to demonstrate 
// how to deploy a library
pragma solidity ^0.5.0;

//https://www.geeksforgeeks.org/solidity-libraries/#:~:text=A%20library%20contract%20is%20defined,as%20it%20cannot%20store%20ethers.
// Defining Library
library LibExample 
{

  // Defining structure
    struct Constants {

        // Declaring variables
        uint Pi;             
        uint EulerNb;        
        uint PythagoraConst; 
        uint TheodorusConst; 
    }

    
    ///////////////////////////////////////////////////////////
    // FUNCTIONS: HELPERS
    ///////////////////////////////////////////////////////////

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


    // Function to power of 
    // an unsigned integer
    function pow(
      uint a, uint b) public view returns (
      uint, address) 
    {
        return (a ** b, address(this));
    }

    //https://ethereum.stackexchange.com/questions/60684/i-want-get-random-number-between-100-999-as-follows
    //Basically you get a number from 0 - 899, and then add 100 to match your offset.
    function random() internal returns (uint) 
    {
        uint randomnumber = uint(keccak256(abi.encodePacked(now, msg.sender, nonce))) % 900;
        randomnumber = randomnumber + 100;
        nonce++;
        return randomnumber;
    }
}

// Defining calling contract
contract LibraryExample {
    
    // Deploying library using 
    // "for" keyword
    using LibExample for unit;
    address owner = address(this);
    
    // Calling function pow to 
    // calculate power 
    function getPow(
      uint num1, uint num2) public view returns (
      uint, address) {
        return num1.pow(num2);
    }
}