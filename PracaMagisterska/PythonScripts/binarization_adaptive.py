import cv2
import numpy as np
from matplotlib import pyplot as plt
import argparse

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Gabor filter applied")
    parser.add_argument("image", nargs=1, help = "path to image")
    parser.add_argument("type", nargs=1, help = "ADAPTIVE_THRESH_MEAN_C, ADAPTIVE_THRESH_GAUSSIAN_C")
    parser.add_argument("maxVal", nargs=1, help = "maxVal")
    args = parser.parse_args()

    img = cv2.imread(args.image[0], 0)
    img = cv2.medianBlur(img, 5)
    maxVal = int(args.maxVal[0])
    if args.type[0] == "ADAPTIVE_THRESH_MEAN_C":
        th1 = cv2.adaptiveThreshold(img,maxVal,cv2.ADAPTIVE_THRESH_MEAN_C,\
        cv2.THRESH_BINARY,11,2)
    elif args.type[0] == "ADAPTIVE_THRESH_GAUSSIAN_C":
        th1 = cv2.adaptiveThreshold(img,maxVal,cv2.ADAPTIVE_THRESH_GAUSSIAN_C,\
        cv2.THRESH_BINARY,11,2)
    cv2.imwrite('processedImage.tiff', th1)
