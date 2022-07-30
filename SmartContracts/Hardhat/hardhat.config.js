///////////////////////////////////////////////////////////
// REQUIRES
///////////////////////////////////////////////////////////
require("dotenv").config();
require("@nomicfoundation/hardhat-toolbox");
require("hardhat-gas-reporter");

///////////////////////////////////////////////////////////
// HELPERS
///////////////////////////////////////////////////////////
const getHDWallet = () => 
{
  const { MNEMONIC, PRIVATE_KEY } = process.env;

  if (MNEMONIC && MNEMONIC !== "") 
  {
    return 
    {
      mnemonic: MNEMONIC
    }
  }

  if (PRIVATE_KEY && PRIVATE_KEY !== "") 
  {
    return [PRIVATE_KEY]
  }
  throw Error("Private Key Not Set! Please set up .env");
}


///////////////////////////////////////////////////////////
// EXPORTS
///////////////////////////////////////////////////////////

/**
 * @type import('hardhat/config').HardhatUserConfig
 */
module.exports = {
  networks: {
    hardhat: {},
    cronosTestnet: {
      url: "https://evm-t3.cronos.org/", //Cronos Testnet
      chainId: 338,
      accounts: getHDWallet(),
      gasPrice: 5000000000000
    }
  },
  etherscan: {
    apiKey:  {
      cronosTestnet: process.env.CRONOSCAN_TESTNET_API_KEY
    },
    customChains: [
      {
        network: "cronosTestnet",
        chainId: 338,
        urls: {
          apiURL: "https://api-rinkeby.etherscan.io/api",
          browserURL: "https://rinkeby.etherscan.io"
        }
      }
    ]
  },
  gasReporter: {
    currency: 'USD',
    enabled: true
  },
  solidity: "0.8.9",
};

///////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////
task("cct", "Clean, Compile, & Test the Greeter.sol").setAction(async () => {

  // Works!
  await hre.run("clean");
  await hre.run("compile");
  await hre.run("test");
});