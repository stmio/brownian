from matplotlib import pyplot as plt
from scipy.stats import gaussian_kde
from numpy import linspace

with open("./brownian.txt", "r") as f:
    data = f.readlines()
data = [float(i.strip()) for i in data]

kde = gaussian_kde(data)
dist_space = linspace(min(data), max(data), 100)

plt.plot(dist_space, kde(dist_space))
plt.savefig("graph.png")

