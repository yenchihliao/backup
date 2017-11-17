from address import Address
from pybitcointools import *
def KeyGen():
    valid_private_key = False
    while not valid_private_key:
	private_key = random_key()
	decoded_private_key = decode_privkey(private_key, 'hex')
	valid_private_key = 0 < decoded_private_key < N
#here we get private key in hex and decimal
    wif_encoded_private_key = encode_privkey(decoded_private_key, 'wif')
#here we get private key compressed(wif)
    compressed_private_key = private_key + '01'
#here we get private key compressed(hex)
    public_key = fast_multiply(G, decoded_private_key)
#here we get public key's x and y
    hex_encoded_public_key = encode_pubkey(public_key, 'hex')#here we get public key hex
    (public_key_x, public_key_y) = public_key
    if(public_key_y % 2) == 0:
	compressed_prefix = '02';
    else:
	compressed_prefix = '03'
    hex_compressed_public_key = compressed_prefix + encode(public_key_x, 16)
#here we get Compressed public key(hex)
    bitcoin_addr = pubkey_to_address(public_key)
#here we get bitcoin address in base58
    bitcoin_addr2 = pubkey_to_address(hex_compressed_public_key)
#here we get bitcoin address from compressed public key
    ret = Address(private_key, public_key, bitcoin_addr, bitcoin_addr2)
    return ret

import json
import requests
import string
def AddLookup(add):
    r = requests.get('https://api.blockchair.com/bitcoin-cash/dashboards/address/%s?' % add)
    try:
	UTXOs = json.loads(r.text)['data']
	for UTXO in UTXOs:
	    v = UTXO["sum_value"]
	value = string.atoi(v)
	return float(value)/ (10**8)
    except:
	return 'The Address you enter does not exist'
	

