import cv2
from PIL import Image
import numpy as np
import argparse
import matplotlib.pyplot as plt
from scipy import fftpack
import urllib2
# import IPython

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument("image", nargs=1, help = "path to image")
    # parser.add_argument("angle", nargs=1, help = "angle")
    # parser.add_argument("frq", nargs=1, help = "frequency")
    args = parser.parse_args()


    img = cv2.imread(args.image[0], 0)      # 1 chan, grayscale!
    imf = np.float32(img)/1.0  # float conversion/scale
    dst = cv2.dct(imf,1)           # the dct
    img = np.uint8(dst)*1.0

    cv2.imwrite('processedImage.tiff', img)

