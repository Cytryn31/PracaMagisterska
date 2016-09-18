import cv2
import numpy as np
import argparse
import math
from  scipy import ndimage
import matplotlib.pyplot as plt

import numpy as np
def blockshaped(arr, nrows, ncols):
    """
    Return an array of shape (n, nrows, ncols) where
    n * nrows * ncols = arr.size

    If arr is a 2D array, the returned array should look like n subblocks with
    each subblock preserving the "physical" layout of arr.
    """
    h, w = arr.shape
    return (arr.reshape(h//nrows, nrows, -1, ncols)
               .swapaxes(1,2)
               .reshape(-1, nrows, ncols))

def lro(img, ksize):
    dx = cv2.Sobel(img, cv2.CV_8U, 1, 0, ksize=ksize)
    dy = cv2.Sobel(img, cv2.CV_8U, 0, 1, ksize=ksize)

    Gxx = np.power(dx,2)
    Gxy = np.multiply(dx,dy)
    Gyy = np.power(dy,2)

    angle = math.pi / 2. + np.divide(np.arctan2(np.multiply(Gxy, 2), np.subtract(Gxx, Gyy)), 2)
    return angle


# def EstimateOrientation(window, image, sobelWindowSize):
#     angle = math.pi/2. + np.divide(np.arctan2(np.multiply(Gxy,2), np.subtract(Gxx,Gyy)),2)

# def EstimateOrientation(window, image, sobelWindowSize):
#     img = image
#     dx = cv2.Sobel(image, cv2.CV_8U, 1, 0, ksize=sobelWindowSize)
#     dy = cv2.Sobel(image, cv2.CV_8U, 0, 1, ksize=sobelWindowSize)
#     theta =[]
#     for i in range(window / 2, img.shape[0] - window/2, 1):
#         for j in range(window / 2, img.shape[1] - window/2, 1):
#             Gxx = 0
#             Gyy = 0
#             Gxy = 0
#             thetaDim =[]
#             for u in range(i - (window / 2), i + (window / 2), 1):
#                 for v in range(j - (window / 2), j + (window / 2), 1):
#                     Gxx = np.power(dx[u][v],2)
#                     Gyy = np.power(dy[u][v],2)
#                     Gxy = np.multiply(dx[u][v],dy[u][v])
#             t = np.divide(np.pi,2) + np.divide(np.arctan2(np.multiply(Gxy,2), np.subtract(Gxx,Gyy)),2)
#             thetaDim.append(t)
#         theta.append(thetaDim)
#     print theta.shape
#     return theta

def CalculatePositionOfOrientationVector(currentPosition, orient, distance=5):
    x = distance * np.cos(orient);
    y = distance * np.sin(orient);
    return (currentPosition[0] + int(x), currentPosition[1] + int(y))

def FillArray(value,array,x,y,window):
    for i in range(x - window/2,x + window/2):
        for j in range(y - window/2,y + window/2):
            array[i][j] = value
    return array

def Avr(array,x,y, window):
    sum = 0
    for i in range(x - window/2,x + window/2):
        for j in range(y - window/2,y + window/2):
            sum += array[i][j]
    return sum/(window*window)



if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Gabor filter applied")
    # parser.add_argument("image", nargs=1, help="path to image")
    # parser.add_argument("sigma", nargs=1, help="sigma")
    # parser.add_argument("gamma", nargs=1, help="gamma")
    # parser.add_argument("ksize", nargs=1, help="ksize")
    # parser.add_argument("thetaRange", nargs=1, help="thetaRange")
    # parser.add_argument("lmdaRange", nargs=1, help="lmdaRange")
    image = cv2.imread("C:\\Users\\Ojtek\\Documents\\DATABASE2\\001001.png", 1)
    image_gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
    # print image_gray.shape[0]
    # print image_gray.shape[1]
    window = 10
    M0 = 100
    V0 = 100
    M = np.mean(image_gray)
    V = np.var(image_gray)
    # cv2.imshow("Original Image", image_gray)

    for i in range(0, image_gray.shape[0]):
        for j in range(0, image_gray.shape[1]):
            # print "i: " + str(i) + " j: " + str(j)
            if image_gray[i][j] > M:
                image_gray[i][j] = M0 + np.sqrt(np.divide(V0 * np.power(image_gray[i][j] - M, 2), V))
            else:
                image_gray[i][j] = M0 - np.sqrt(np.divide(V0 * np.power(image_gray[i][j] - M, 2), V))

    angles = lro(image_gray, 5)

    for i in range(window / 2, angles.shape[0] - window / 2, window):
            for j in range(window / 2, angles.shape[1] - window/2, window):
                val = Avr(angles,i,j,window)
                angles = FillArray(val,angles,i,j,window)

    print angles
    # angle = lro(image_gray, 11)
    # # for pixels in angle:
    # #     for pix in pixels:
    # #         print math.degrees(pix)
    # a = blockshaped(angle, 6, 6)
    # avr =[]
    # for b in a:
    #     avr.append(np.mean(b))
    # for b in avr:
    #     print math.degrees(b)
    # for i in range(0, image_gray.shape[0], 16):
    #     for j in range(0, image_gray.shape[1], 16):
    #         image_gray = cv2.line(image_gray, (i, j),
    #                               CalculatePositionOfOrientationVector((i - 8, j + 8), math.degrees(angle[i][j])),
    #                               (255, 255, 255), 1)
    # cv2.imshow("Normalized", image_gray)
    # print a
    #  cv2.waitKey(0)
    #   cv2.destroyAllWindows()
    # sigma = 0.5 * lmda
    # sin_gabor = cv2.getGaborKernel((ksize, ksize), sigma, theta * M_PI / 180.0, lmda , 1.0, M_PI / 2.0, cv2.CV_32F)
    # cos_gabor = cv2.getGaborKernel((ksize, ksize), sigma, theta * M_PI / 180.0, lmda , 1.0, 0.0, cv2.CV_32F)
    #
    # # image_gray.convertTo(image_f, cv2.CV_32F, 1.0 / 256.0)
    # sin_response = cv2.filter2D(image_gray, -1, sin_gabor)
    # cos_response = cv2.filter2D(image_gray, -1, cos_gabor)
    # cv2.multiply(sin_response, sin_response, sin_response)
    # cv2.multiply(cos_response, cos_response, cos_response)
    #
    # cv2.imshow("Original Image", image)
    # cv2.imshow("Grayscale Image", image_gray)
    # cv2.imshow("Sine Gabor Kernel", sin_gabor + 0.5)
    # cv2.imshow("Cosine Gabor Kernel", cos_gabor + 0.5)
    # cv2.imshow("Sine Response (CPU)", sin_response)
    # cv2.imshow("Cosine Response (CPU)", cos_response)
    #
    cv2.waitKey(0);
    cv2.destroyAllWindows();
