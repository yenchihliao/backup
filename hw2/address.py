class Address(object):
    def __init__(self, priv, pub, add1, add2):
	self.priv  = priv
	self.pub = pub
	self.add1 = add1
	self.add2 = add2
    def show(self):
	print("private key:", self.priv)
	print("public key:", self.pub)
	print("bitcoin addresses: %s and %s" % (self.add1, self.add2))
