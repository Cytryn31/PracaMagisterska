import cv2
import numpy as np
from matplotlib import pyplot as plt
import argparse

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Gabor filter applied")
    parser.add_argument("image", nargs=1, help = "path to image")
    parser.add_argument("type", nargs=1, help = "THRESH_BINARY, ADAPTIVE_THRESH_MEAN_C, ADAPTIVE_THRESH_GAUSSIAN_C")
    parser.add_argument("threshold_left", nargs=1, help = "left border")
    parser.add_argument("threshold_right", nargs=1, help = "right border")
    args = parser.parse_args()

    img = cv2.imread(args.image[0], 0)
    img = cv2.medianBlur(img, 5)
    threshold_left = int(args.threshold_left[0])
    threshold_right = int(args.threshold_right[0])
    if args.type[0] == "THRESH_BINARY":
        ret,th1 = cv2.threshold(img,threshold_left,threshold_right,cv2.THRESH_BINARY)
    elif args.type[0] == "ADAPTIVE_THRESH_MEAN_C":
        th1 = cv2.adaptiveThreshold(img,threshold_right,cv2.ADAPTIVE_THRESH_MEAN_C,\
        cv2.THRESH_BINARY,11,2)
    elif args.type[0] == "ADAPTIVE_THRESH_GAUSSIAN_C":
        th1 = cv2.adaptiveThreshold(img,threshold_right,cv2.ADAPTIVE_THRESH_GAUSSIAN_C,\
        cv2.THRESH_BINARY,11,2)

    cv2.imwrite('processedImage.tiff', th1)
