import cv2
import argparse
import numpy as np
from PIL import Image
import frequency
import utils
import math
import Filters
sobelOperator = [[-1, 0, 1], [-2, 0, 2], [-1, 0, 1]]

def apply_kernel_at(get_value, kernel, i, j):
    kernel_size = len(kernel)
    result = 0
    for k in range(0, kernel_size):
        for l in range(0, kernel_size):
            pixel = get_value[i + k - kernel_size / 2][j + l - kernel_size / 2]
            result += pixel * kernel[k][l]
    return result

def calculateOrientation(image,gausWindow, gaussSgima,window,ksize):
    smoothed_im = cv2.GaussianBlur(image, (gausWindow, gausWindow), gaussSgima, sigmaY=gaussSgima)
    # # dx = cv2.Sobel(smoothed_im, cv2.CV_8U, 1, 0, ksize=gausWindow)
    # # dy = cv2.Sobel(smoothed_im, cv2.CV_8U, 0, 1, ksize=gausWindow)
    # #
    # # Gxx = dx **2
    # # Gyy = dx **2
    # # Gxy = dy * dx
    # # Gxx = cv2.GaussianBlur(Gxx, (gausWindow, gausWindow), gaussSgima, sigmaY=gaussSgima)
    # # Gyy = cv2.GaussianBlur(Gyy, (gausWindow, gausWindow), gaussSgima, sigmaY=gaussSgima)
    # # Gxy = cv2.GaussianBlur(Gxy, (gausWindow, gausWindow), gaussSgima, sigmaY=gaussSgima)
    # #
    # # denom = np.sqrt(Gxy** 2 + (Gxx - Gyy)** 2)
    # # sin2theta = Gxy/ denom
    # # cos2theta = (Gxx - Gyy) / denom
    # #
    # # sin2theta = cv2.GaussianBlur(sin2theta, (gausWindow, gausWindow), gaussSgima, sigmaY=gaussSgima)
    # # cos2theta = cv2.GaussianBlur(cos2theta, (gausWindow, gausWindow), gaussSgima, sigmaY=gaussSgima)
    # #
    # # orientim = np.pi*0.5 + np.arctan2(sin2theta, cos2theta) * 0.5
    # # # smooth image with gaussian blur
    # smoothed_im = Filters.gaussFilter(image, 0.5, 3)
    #
    # # calculate gradients with sobel filter
    # dx, dy = Filters.sobelFilter(smoothed_im)
    #
    # # smooth gradients
    # Gx = Filters.gaussFilter(dx, 0.5, 3)
    # Gy = Filters.gaussFilter(dy, 0.5, 3)
    #
    # # compute gradient magnitude
    # Gxx = Gx ** 2
    # Gyy = Gy ** 2
    # G = np.sqrt(Gxx + Gyy)
    #
    # # calculate theta
    # theta = np.arctan2(Gy, Gx)
    #
    # # smooth theta
    # smoothed_theta = Filters.gaussFilter(theta, 0.5, 3)
    #
    # # calculate double sine and cosine on theta --> increases precision
    # Tx = (G ** 2 + 0.001) * (np.cos(smoothed_theta) ** 2 - np.sin(smoothed_theta) ** 2)
    # Ty = (G ** 2 + 0.001) * (2 * np.sin(smoothed_theta) * np.cos(smoothed_theta))
    #
    # denom = np.sqrt(Ty ** 2 + Tx ** 2)
    #
    # Tx = Tx / denom
    # Ty = Ty / denom
    #
    # # smooth theta x and y
    # smoothed_Tx = Filters.gaussFilter(Tx, 0.5, 3)
    # smoothed_Ty = Filters.gaussFilter(Ty, 0.5, 3)
    #
    # # calculate new value for theta
    # theta = np.pi + np.arctan2(smoothed_Ty, smoothed_Tx) * 0.5
    # kern = cv2.getGaussianKernel(gausWindow, gaussSgima)
    # smoothed_im = cv2.GaussianBlur(image,(gausWindow,gausWindow),gaussSgima,sigmaY=gaussSgima)
    # c_smoothed_im =smoothed_im.copy()
    ySobel = sobelOperator
    xSobel = np.transpose(sobelOperator)
    dx = cv2.Sobel(smoothed_im, cv2.CV_8U, 1, 0, ksize=5)
    dy = cv2.Sobel(smoothed_im, cv2.CV_8U, 0, 1, ksize=5)
    angles = []
    f = lambda x, y: x * y
    g = lambda x, y: (x ** 2)*(y ** 2)
    # # angles = [[] for i in range(0, image.shape[0], window)]
    # # angles = []
    for i in range(0, image.shape[0], window):
        for j in range(0, image.shape[1], window):
            gx = 0
            gy = 0
            gxy = 0
            for k in range(i, min(i + window, image.shape[0] - 1)):
                for l in range(j, min(j + window, image.shape[1] - 1)):
                    xP = dx[k][l]
                    yP = dy[k][l]
                    gx += np.multiply(xP,xP)
                    gy += np.multiply(yP,yP)
                    gxy += np.multiply(xP,yP)
            angle = 0.5 * np.pi + math.atan2(2 * gxy, gx - gy) * 0.5
            print math.degrees(angle)

            angles.append(angle)
    # angles = utils.smooth_angles(angles)
    # # angles = [x for sublist in angles for x in sublist]
    # # for i in range(0, image.shape[0], window):
    # #     for j in range(0, image.shape[1], window):
    # #         gx = 0
    # #         gy = 0
    # #         gxy = 0
    # #         for k in range(i, min(i + window, image.shape[0] - 1)):
    # #             for l in range(j, min(j + window, image.shape[1] - 1)):
    # #                 gx += np.multiply(dx[k][l], dx[k][l])
    # #                 gy += np.multiply(dy[k][l], dy[k][l])
    # #                 gxy += np.multiply(dx[k][l], dy[k][l])
    # #         angle = (0.5*np.pi) + 0.5 *math.atan2(gxy, gx-gy)
    # #         angles.append(angle)
    #
    # angles = cv2.GaussianBlur(np.asarray(angles),(gausWindow,gausWindow),gaussSgima,sigmaY=gaussSgima)
    #
    # #
    # # dx = cv2.Sobel(smoothed_im, cv2.CV_8U, 1, 0, ksize=gausWindow)
    # # dy = cv2.Sobel(smoothed_im, cv2.CV_8U, 0, 1, ksize=gausWindow)
    # #
    # # # smooth gradients
    # # Gx = cv2.filter2D(dx,cv2.CV_8U,kern)
    # # Gy = cv2.filter2D(dy,cv2.CV_8U,kern)
    # #
    # # Gxx = Gx ** 2
    # # Gyy = Gy ** 2
    # # G = np.sqrt(Gxx + Gyy)
    # #
    # # theta = np.arctan2(Gy, Gx)
    # # Tx = (G ** 2 + 0.001) * (np.cos(theta) ** 2 - np.sin(theta) ** 2)
    # # Ty = (G ** 2 + 0.001) * (2 * np.sin(theta) * np.cos(theta))
    # #
    # # denom = np.sqrt(Ty ** 2 + Tx ** 2)
    # #
    # # Tx = Tx / denom
    # # Ty = Ty / denom
    # # theta = np.pi + np.arctan2(Ty, Tx) / 2
    # # print theta.shape
    # # print smoothed_im.shape
    # # im = Image.open("C:\\Users\\Ojtek\\Documents\\DATABASE2\\001002.png")
    # # im = im.convert("L")
    # # fromArray = Image.fromarray(smoothed_im)
    # # f = lambda x, y: 2 * x * y
    # # g = lambda x, y: x ** 2 - y ** 2
    # # angles2 = utils.calculate_angles(im, window, f, g)
    # # freqs = frequency.freq(smoothed_im,window,angles)
    # # print freqs
    filtred = decomposeImage(image, angles, window,ksize)
    return filtred

def vec_and_step(tang, W):
    (begin, end) = utils.get_line_ends(0, 0, W, tang)
    (x_vec, y_vec) = (end[0] - begin[0], end[1] - begin[1])
    length = math.hypot(x_vec, y_vec)
    (x_norm, y_norm) = (x_vec / length, y_vec / length)
    step = length / W
    return (x_norm, y_norm, step)

def coords(x,y,distance, orient):
    pointX = x + distance * math.cos(orient)
    pointY = y + distance * math.sin(orient)
    array = np.rint((pointY,pointX))
    return (int(array[0]),int(array[1]))

def copyArray(image, theta, x,y,window):
    im = np.empty([window,window]).astype('uint8')
    # o = np.empty([window,window])
    for j in range(0,window):
        if x + j >= image.shape[0]:
            return im
        for i in range(0,window):
            if y + i >= image.shape[1]:
                break
            im[j][i] = (image[x + j][y + i])
            # o[j][i] = (theta[x + j][y + i])
    return im

def copyToArray(image, windows, y, x, counter, window):
    # print  image.shape
    # print windows
    for j in range(0,window):
        if y + j >= image.shape[0]:
            return image
        for i in range(0,window):
            if x + i >= image.shape[1]:
                break
            if np.isnan(windows[counter][j][i]):
                windows[counter][j][i] = 0
            # print (windows[counter][j][i])
            image[y + j][x + i] = (windows[counter][j][i])
    return image


def decomposeImage(image, theta, window,ksize):
    windows = []
    orient = theta
    median = []
    row_count = 0
    for x in range(0, image.shape[0], window):
        for y in range(0,  image.shape[1], window):
            w = copyArray(image, theta, x, y, window)
            windows.append(w)
            # orient.append(o)
            row_count += 1

    print str(row_count)

    for cr in range(0,row_count):
        # sum = 0
        # for i in range(0, orient[cr].shape[0]):
        #     for j in range(0, orient[cr].shape[1]):
        #         sum += orient[cr]

        len = 1.5
        # median.append(sum / orient[cr].shape[0] ** 2)
        x1 = 4 - len / 2 * math.cos(np.average(orient[cr])) * 10
        x1 = np.around(x1)
        x1 = x1.astype(int)
        y1 = 4 - len / 2 * math.sin(np.average(orient[cr])) * 10
        y1 = np.around(y1)
        y1 = y1.astype(int)

        x2 = 4 + math.cos(np.average(orient[cr])) * 10
        x2 = np.around(x2)
        x2 = x2.astype(int)
        y2 = 4 + math.sin(np.average(orient[cr])) * 10
        y2 = np.around(y2)
        y2 = y2.astype(int)

        point1 = (x1, y1)
        point2 = (x2, y2)

        # windows[cr] = cv2.line(windows[cr], point1, point2, (255,255, 255))
    # print windows
    # print str(len(freqs))
    # print str(len(freqs[0]))
    # print str(len(theta))
    # print str(len(theta[0]))
    count = 0
    for i in range(0, row_count):
            # print str(i)
            # print len(freqs[i])
        kern = cv2.getGaborKernel((7,7),4,np.average(orient[i]),35,22)
        windows[i] = cv2.filter2D(src=windows[i], ddepth=cv2.CV_32F, kernel=kern)
        count +=1


    final_image = reconstructImage(image, windows, window)
    return final_image


def reconstructImage(image, windows, window):
    c = 0
    for currenty in range(0, image.shape[0], window):
        for currentx in range(0,  image.shape[1], window):
            image = copyToArray(image, windows, currenty, currentx, c, window)
            c+=1
    return image

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Gabor filter applied")
    # parser.add_argument("image", nargs=1, help="path to image")
    # parser.add_argument("sigma", nargs=1, help="sigma")
    # parser.add_argument("gamma", nargs=1, help="gamma")
    # parser.add_argument("ksize", nargs=1, help="ksize")
    # parser.add_argument("thetaRange", nargs=1, help="thetaRange")
    # parser.add_argument("lmdaRange", nargs=1, help="lmdaRange")

    image_gray = cv2.imread("C:\\Users\\Ojtek\\Documents\\DATABASE2\\001001.png",0)
    M0 = 100
    V0 = 100
    M = np.mean(image_gray)
    V = np.var(image_gray)
    for i in range(0, image_gray.shape[0]):
        for j in range(0, image_gray.shape[1]):
            if image_gray[i][j] > M:
                image_gray[i][j] = M0 + np.sqrt(np.divide(V0 * np.power(image_gray[i][j] - M, 2), V))
            else:
                image_gray[i][j] = M0 - np.sqrt(np.divide(V0 * np.power(image_gray[i][j] - M, 2), V))

    cv2.imshow("image_gray", image_gray)

    gabor = calculateOrientation(image_gray, 3, 0.5,17,5)

    cv2.imshow("gabor", gabor)

    cv2.waitKey(0);
    cv2.destroyAllWindows();
