""" 
#this is python demo code for keys and Addresses generation
#copy form mastering bitcoin page 81, 82
#importing pybitcointools instead of bitcoin
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
#base10 multiply not found, fast_multiply used instead
public_key = fast_multiply(G, decoded_private_key)
print "Public Key (x, y) coordinate is: ", public_key

