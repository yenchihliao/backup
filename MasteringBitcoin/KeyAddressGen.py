""" 
#this is python demo code for keys and Addresses generation
#copy form mastering bitcoin page 81, 82
#importing pybitcointools instead of bitcoin which shown on the book
"""

from pybitcointools import *

#Generate a random private key
valid_private_key = False
while not valid_private_key :
    private_key = random_key()
    decoded_private_key = decode_privkey(private_key, 'hex')
    valid_private_key = 0 < decoded_private_key < N

print "private key (hex) is: ", private_key
print "private key (decimal) is: ", decoded_private_key

#Convert private key to WIF formast
wif_encoded_private_key = encode_privkey(decoded_private_key, 'wif')
print "Private Key Compressd (wif) is: ", wif_encoded_private_key

#Add suffix "01" to indicate a compressed private key
compressed_private_key = private_key + '01'
print "Private Key Compressed (hex) is: ", compressed_private_key

#Generate a WIF format from the compressed private key (WIF-compressed)
wif_compressed_private_key = encode_privkey(
    decode_privkey(compressed_private_key, 'hex'), 
    'wif')
print "Private Key (WIF-Compressed) is: ", wif_compressed_private_key

#Multiply the EC generator point G with the private key to get a public key point
#base10 multiply(which is used on the book) not found, fast_multiply used instead
public_key = fast_multiply(G, decoded_private_key)
print "Public Key (x, y) coordinate is: ", public_key

#encode as hex, prefix 04 (uncompressed Bitcoin Address)
hex_encoded_public_key = encode_pubkey(public_key, 'hex')
print "Public Key (hex) is: ", hex_encoded_public_key

#compress public key, adjust prefix depending on whether y is even or odd(comporessed pubkey)
(public_key_x, public_key_y) = public_key
if (public_key_y % 2) == 0 :
    compressed_prefix = '02'
else:
    compressed_prefix = '03'
hex_compressed_public_key = compressed_prefix + encode(public_key_x, 16)
print "Compressed Public Key (hex) is: ", hex_compressed_public_key

#Generate bitcoin address from pubkey
print "bitcoin Address (b58check) is: ", pubkey_to_address(public_key)

#Generate bitcoin address from compressed public key
print "Compressed bitcoin Address (b58check) is: ", pubkey_to_address(hex_compressed_public_key)




