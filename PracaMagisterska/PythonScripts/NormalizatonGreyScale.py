def weightedAverage(pixel):
    return 0.299*pixel[0] + 0.587*pixel[1] + 0.114*pixel[2]
import argparse
import cv2
import numpy as np

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Normalization")
    parser.add_argument("image", nargs=1, help = "path to image")

    args = parser.parse_args()
    image = cv2.imread(args.image[0], 0)




    grey = np.zeros((image.shape[0], image.shape[1])) # init 2D numpy array
    # get row number
    for rownum in range(len(image)):
       for colnum in range(len(image[rownum])):
          grey[rownum][colnum] = weightedAverage(image[rownum][colnum])

    from PIL import Image

    im = Image.fromarray(grey)
    im.save('processedImage.tiff')