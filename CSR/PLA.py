import numpy as np

MaxLine  = 400
MaxFeature = 4
F = open('data', 'r')

features = np.zeros([MaxLine, MaxFeature])
labels = np.zeros(MaxLine)

for i, line in enumerate(F):
    list = line.split()
    for j,  word in enumerate(list):
	if j != MaxFeature:
	    features[i][j] = float(word)
	else :
	    labels[i] = int(word)

w = np.zeros(MaxFeature)

updated = True
while updated:
    updated = False
    for i, f in enumerate(features):
	if np.sign(w.T.dot(f)) != labels[i] :
	    print w.dot(f), labels[i]
	    print "feature", i, f
	    w  = w + labels[i] * f
	    print "weight updated", w
	    updated = True
	    break
print w
