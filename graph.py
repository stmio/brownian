from matplotlib import pyplot as plt
from scipy.stats import gaussian_kde
from numpy import linspace

with open("./brownian.txt", "r") as f:
    data = f.readlines()
data = [float(i.strip()) for i in data]
data.append(0.0) 

kde = gaussian_kde(data)
dist_space = linspace(min(data), max(data), 100)

plt.xlim([0, max(data)])
plt.ylim([0, max(kde(dist_space)) + 0.0001])

plt.xlabel("Speed (pixels per second)")
plt.ylabel("Probability density")

plt.plot(dist_space, kde(dist_space))
plt.savefig("graph.png")

