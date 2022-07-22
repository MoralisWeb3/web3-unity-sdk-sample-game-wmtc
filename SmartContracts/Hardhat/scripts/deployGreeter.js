///////////////////////////////////////////////////////////
// REQUIRES
///////////////////////////////////////////////////////////
const hre = require("hardhat");
const fs = require('fs');

///////////////////////////////////////////////////////////
// MAIN
///////////////////////////////////////////////////////////
async function main()
{

  ///////////////////////////////////////////////////////////
  // DEPLOYMENT
  ///////////////////////////////////////////////////////////
  const Greeter = await hre.ethers.getContractFactory("Greeter");
  const greeter = await Greeter.deploy();
  await greeter.deployed();


  ///////////////////////////////////////////////////////////
  // UNITY-FRIENDLY OUTPUT
  ///////////////////////////////////////////////////////////
  const abiFile = JSON.parse(fs.readFileSync('./artifacts/contracts/Greeter.sol/Greeter.json', 'utf8'));
  const abi = JSON.stringify(abiFile.abi).replaceAll ('"','\\"',);
  console.log("\n");
  console.log("DEPLOYMENT COMPLETE");
  console.log("		public const string Address  = \"%s\";", greeter.address);
  console.log("		public const string Abi  = \"%s\";", abi);
  console.log("\n");

  ///////////////////////////////////////////////////////////
  // WAIT
  ///////////////////////////////////////////////////////////
  console.log("WAIT...");
  console.log("\n");
  await greeter.deployTransaction.wait(7);


  ///////////////////////////////////////////////////////////
  // VERIFY
  ///////////////////////////////////////////////////////////
  await hre.run("verify:verify", {
    address: greeter.address,
    constructorArguments: [],
  });


  ///////////////////////////////////////////////////////////
  // LOG OUT DATA FOR USAGE IN UNITY
  ///////////////////////////////////////////////////////////
  console.log("VERIFICATION COMPLETE");
  console.log("\n");

}

///////////////////////////////////////////////////////////
// EXECUTE
///////////////////////////////////////////////////////////
main()
    .then(() => process.exit(0))
    .catch((error) => {
      console.error(error);
      process.exit(1);
    });
