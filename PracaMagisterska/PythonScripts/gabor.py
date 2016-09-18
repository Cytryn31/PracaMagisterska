import numpy as np
import cv2
import argparse
from os import path


def build_filters(sigma, gamma, ksize, thetaRange, lmdaRange, thetaDivider, lmdaDivider):
    filters = []
    for theta in np.arange(0, thetaRange, thetaRange / thetaDivider):
        for lmda in np.arange(1, lmdaRange, lmdaDivider):
            kern = cv2.getGaborKernel((ksize, ksize), sigma, theta, lmda, gamma, ktype=cv2.CV_64F)
            cv2.multiply(kern, 1 * kern, kern)
            filters.append(kern)
    return filters


def process(img, filters):
    accum = np.zeros_like(img)
    fimgRRy = []
    for kern in filters:
        fimgRRy.append(cv2.filter2D(img, cv2.CV_16U, kern))
    for fimg in fimgRRy:
        np.maximum(accum, np.asarray(fimg), accum)
    return accum


if __name__ == '__main__':
    parser = argparse.ArgumentParser(description="Gabor filter applied")
    parser.add_argument("image", nargs=1, help="path to image")
    parser.add_argument("sigma", nargs=1, help="sigma")
    parser.add_argument("gamma", nargs=1, help="gamma")
    parser.add_argument("ksize", nargs=1, help="ksize")
    parser.add_argument("thetaRange", nargs=1, help="thetaRange")
    parser.add_argument("lmdaRange", nargs=1, help="lmdaRange")
    args = parser.parse_args()
    img = cv2.imread(args.image[0], 0)
    dirName = path.dirname(args.image[0]) + "\\"
    filters = build_filters(float(args.sigma[0]), float(args.gamma[0]), int(args.ksize[0]), float(args.thetaRange[0]),
                            float(args.lmdaRange[0]), thetaDivider=32, lmdaDivider=0.15)
    # tiles = image_slicer.slice(args.image[0], 4)
    # dst = "C:\\Users\\Ojtek\\Documents\\PracaMagisterska\\PracaMagisterska\\PythonScripts\\tmp\\"
    # for tile in tiles:
    #     tileImg = cv2.imread(dirName + str(tile)[11:-1].strip())
    #     res = process(tileImg, filters)
    #     cv2.imwrite(dirName + str(tile)[11:-1].strip(), res)
    # v = image_slicer.join(tiles)
    # v = np.asarray(v)
    v = process(img, filters)
    # v = multi(v, 1.15)
    # cv2.addWeighted(v, 0.75, v, 0.75, 0, v)
    # v = np.invert(v)
    cv2.imwrite('processedImage.tiff', v)
