import cv2
import argparse
import numpy as np
import matplotlib.pyplot as plt

from skimage import data
from skimage.restoration import inpaint


def winVar(img, wlen):
    wmean, wsqrmean = (cv2.boxFilter(x, 11, (wlen, wlen),
                                     borderType=cv2.BORDER_REPLICATE) for x in (img, img * img))
    return wsqrmean - wmean * wmean


def fast(image, template):
    for i in range(0, image.shape[0]):
        for j in range(0, image.shape[1]):
            image[i, j] = 0
            # print template[i,j]
            if (template[i, j] <= 0):
                break
        for j in reversed(range(0, image.shape[1])):
            image[i, j] = 0
            # print template[i,j]
            if (template[i, j] <= 0):
                break

    return image


def fillGaps(mask):
    for i in range(0, mask.shape[0]):
        for j in range(0, mask.shape[1]):
            t = True
            k = i
            p = 1
            while t:
                if k == 0:
                    break
                k += p
                if k == i or mask.shape[0] == k or k >= i + 20:
                    break
                if mask[k, j] <= 0:
                    p = -1
                if p == - 1:
                    mask[k, j] = 0

    return mask


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Segmentation")
    parser.add_argument("image", nargs=1, help="path to image")
    parser.add_argument("window_size", nargs=1)
    args = parser.parse_args()

    img = cv2.imread(args.image[0], 0)
    windows1 = winVar(img, int(args.window_size[0]))
    mask = fillGaps(windows1)
    cv2.imwrite('processedImage.tiff', fast(img, mask))
