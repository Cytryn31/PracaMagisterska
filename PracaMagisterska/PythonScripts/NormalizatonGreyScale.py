def weightedAverage(pixel):
    return 0.299*pixel[0] + 0.587*pixel[1] + 0.114*pixel[2]
import argparse
import cv2
import numpy as np

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Normalization")
    parser.add_argument("image", nargs=1, help = "path to image")

    args = parser.parse_args()
    image = cv2.imread(args.image[0], 1)




    grey = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    from PIL import Image

    im = Image.fromarray(grey)
    im.save('processedImage.tiff')