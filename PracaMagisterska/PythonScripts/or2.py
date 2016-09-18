import numpy as np
import matplotlib.pyplot as plt
from PIL import Image
import cv2

I = cv2.imread("C:\\Users\\Ojtek\\Documents\\DATABASE2\\001004.png")
I = I[:,:,0]
print I
p = np.asarray(I).astype('int8')
w,h = I.shape
x, y = np.mgrid[0:h:500j, 0:w:500j]
# dy, dx = np.gradient(p)
skip = (slice(None, None, 3), slice(None, None, 3))
dx = cv2.Sobel(I,cv2.CV_8U ,1,0,ksize=5)
dy = cv2.Sobel(I,cv2.CV_8U ,0,1,ksize=5)
fig, ax = plt.subplots()
im = ax.imshow(I, extent=[x.min(), x.max(), y.min(), y.max()])
plt.colorbar(im)
# ax.quiver(x, y, dx.T, dy.T)
Q = ax.quiver(x[skip], y[skip], dx[skip].T, dy[skip].T, color='r', units='x',
               linewidths=(2,), edgecolors=('k'), headaxislength=5)

qk = plt.quiverkey(Q, 0.5, 0.03, 1, r'$1 \frac{m}{s}$',
                   fontproperties={'weight': 'bold'})
#
ax.set(aspect=1, title='Strzalki')
plt.show()