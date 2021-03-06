import cv2
import numpy as np
from matplotlib import pyplot as plt
import argparse

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Gabor filter applied")
    parser.add_argument("image", nargs=1, help = "path to image")
    parser.add_argument("type", nargs=1, help = "THRESH_BINARY, ADAPTIVE_THRESH_MEAN_C, ADAPTIVE_THRESH_GAUSSIAN_C")
    parser.add_argument("threshold", nargs=1, help = "threshold")
    parser.add_argument("maxVal", nargs=1, help = "maxVal")
    args = parser.parse_args()

    img = cv2.imread(args.image[0], 0)
    img = cv2.medianBlur(img, 5)
    threshold = int(args.threshold[0])
    maxVal = int(args.maxVal[0])
    if args.type[0] == "THRESH_BINARY":
        ret,th1 = cv2.threshold(img,threshold,maxVal,cv2.THRESH_BINARY)
    elif args.type[0] == "ADAPTIVE_THRESH_MEAN_C":
        th1 = cv2.adaptiveThreshold(img,maxVal,cv2.ADAPTIVE_THRESH_MEAN_C,\
        cv2.THRESH_BINARY,11,2)
    elif args.type[0] == "ADAPTIVE_THRESH_GAUSSIAN_C":
        th1 = cv2.adaptiveThreshold(img,maxVal,cv2.ADAPTIVE_THRESH_GAUSSIAN_C,\
        cv2.THRESH_BINARY,11,2)
    elif args.type[0] == "THRESH_OTSU":
        ret2, th1 = cv2.threshold(img, threshold, maxVal, cv2.THRESH_BINARY + cv2.THRESH_OTSU)

    cv2.imwrite('processedImage.tiff', th1)
