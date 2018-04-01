// import * as StellarSdk from 'stellar-sdk';
let StellarSdk = require('stellar-sdk')
// console.log(StellarSdk.StrKey.isValidEd25519PublicKey("GCDMFH3RSZR3FLBHSUYPLF2XAG5TWZQDHNX5XG4UELVXICNBESDFMXTJ"));
let testString = "GCDMFH3RSZR3FLBHSUYPLF2XAG5TWZQDHNX5XG4UELVXICNBESDFMXTJ";
console.log(StellarSdk.StrKey.decodeEd25519PublicKey(testString));
console.log("=== END ===");