import numpy as np
import cv2
import math
from  scipy import ndimage
import matplotlib.pyplot as plt

def winVar(img, wlen):
    wmean, wsqrmean = (cv2.boxFilter(x, 11, (wlen, wlen),
                                     borderType=cv2.BORDER_REPLICATE) for x in (img,img))
    return wsqrmean - wmean * wmean

def lro(im_np):
    orientsmoothsigma = 4
    sobelx = cv2.Sobel(img, cv2.CV_8U, 1, 0, ksize=5)
    sobely = cv2.Sobel(img, cv2.CV_8U, 0, 1, ksize=5)
    Gxx = cv2.Sobel(im_np, -1, 2, 0)
    Gxy = cv2.Sobel(im_np, -1, 1, 1)
    Gyy = cv2.Sobel(im_np, -1, 0, 2)

    Gxx = ndimage.filters.gaussian_filter(Gxx, orientsmoothsigma)
    Gxy = ndimage.filters.gaussian_filter(Gxy, orientsmoothsigma)
    Gyy = ndimage.filters.gaussian_filter(Gyy, orientsmoothsigma)

    angle = math.pi/2. + np.divide(np.arctan2(np.multiply(Gxy,2), np.subtract(Gxx,Gyy)),2)
    # angle = np.arctan2(sobely, sobelx)
    return sobelx,sobely


# Checks if a matrix is a valid rotation matrix.
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


# skip = (slice(None, None, 3), slice(None, None, 3))
img = cv2.imread("C:\\Users\\Ojtek\\Documents\\DATABASE2\\001004.png")
print img.shape
img = img[:,:,0]
h, w = img.shape
print img.shape
X, Y = np.mgrid[0:h:500j, 0:w:500j]
U, V = lro(img)
# 2
plt.figure()
Q = plt.quiver(X, Y, U, V, units='width')
qk = plt.quiverkey(Q, 0.9, 0.95, 2, r'$2 \frac{m}{s}$',
                   labelpos='E',
                   coordinates='figure',
                   fontproperties={'weight': 'bold'})
plt.axis([-1, 7, -1, 7])
plt.title('scales with plot width, not view')
# windows1 = winVar(img,11)
# angs = []
# for win in windows1:
#     for w in win:
#         angs.append(lro(w))
# print angs[52]
# cv2.imshow('wind',angs[52])
# cv2.waitKey(0)
# plt.subplot(2,2,1),plt.imshow(windows1[1],cmap = 'gray')
# plt.title('Original'), plt.xticks([]), plt.yticks([])
# plt.subplot(2,2,2),plt.imshow(laplacian,cmap = 'gray')
# plt.title('Laplacian'), plt.xticks([]), plt.yticks([])
# plt.subplot(2,2,3),plt.imshow(sobelx,cmap = 'gray')
# plt.title('Sobel X'), plt.xticks([]), plt.yticks([])
# plt.subplot(2,2,4),plt.imshow(sobely,cmap = 'gray')
# plt.title('Sobel Y'), plt.xticks([]), plt.yticks([])
#
plt.show()