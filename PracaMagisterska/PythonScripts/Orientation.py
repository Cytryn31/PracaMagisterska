import cv2
import matplotlib.pyplot as plt
import numpy as np
import numpy
import scipy
from scipy import ndimage
import math

def isRotationMatrix(R):
    Rt = np.transpose(R)
    shouldBeIdentity = np.dot(Rt, R)
    I = np.identity(3, dtype=R.dtype)
    n = np.linalg.norm(I - shouldBeIdentity)
    return n < 1e-6


# Calculates rotation matrix to euler angles
# The result is the same as MATLAB except the order
# of the euler angles ( x and z are swapped ).
def rotationMatrixToEulerAngles(R):
    assert (isRotationMatrix(R))

    sy = math.sqrt(R[0, 0] * R[0, 0] + R[1, 0] * R[1, 0])

    singular = sy < 1e-6

    if not singular:
        x = math.atan2(R[2, 1], R[2, 2])
        y = math.atan2(-R[2, 0], sy)
        z = math.atan2(R[1, 0], R[0, 0])
    else:
        x = math.atan2(-R[1, 2], R[1, 1])
        y = math.atan2(-R[2, 0], sy)
        z = 0

    return np.array([x, y, z])
img = cv2.imread("C:\\Users\\Ojtek\\Documents\\DATABASE2\\001004.png")
h, w, channels = img.shape
x, y = np.mgrid[0:h:500j, 0:w:500j]

laplacian = cv2.Laplacian(img,cv2.CV_8U )

for matrix in laplacian:
    print "Next"
    print matrix

# sobelx = cv2.Sobel(img,cv2.CV_8U ,1,0,ksize=5)
# sobely = cv2.Sobel(img,cv2.CV_8U ,0,1,ksize=5)

# plt.subplot(2,2,1),plt.imshow(img,cmap = 'gray')
# plt.title('Original'), plt.xticks([]), plt.yticks([])
# plt.subplot(2,2,2),plt.imshow(laplacian,cmap = 'gray')
# plt.title('Laplacian'), plt.xticks([]), plt.yticks([])
# plt.subplot(2,2,3),plt.imshow(sobelx,cmap = 'gray')
# plt.title('Sobel X'), plt.xticks([]), plt.yticks([])
# plt.subplot(2,2,4),plt.imshow(sobely,cmap = 'gray')
# plt.title('Sobel Y'), plt.xticks([]), plt.yticks([])
#
# plt.show()
#
#
#
# [dy, dx] = np.gradient(laplacian)
# skip = (slice(None, None, 3), slice(None, None, 3))
#
# fig, ax = plt.subplots()
# im = ax.imshow(img.transpose(img.FLIP_TOP_BOTTOM),
#                extent=[x.min(), x.max(), y.min(), y.max()])
# plt.colorbar(im)
# ax.quiver(x[skip], y[skip], dx[skip].T, dy[skip].T)
#
# ax.set(aspect=1, title='Quiver Plot')
# plt.show()
