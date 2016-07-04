from PIL import Image
import argparse
import os
import cv2
import cv2.cv as cv
import numpy as np

def build_filters():
    """ returns a list of kernels in several orientations
    """
    filters = []
    ksize = 31
    for theta in np.arange(0, np.pi, np.pi / 32):
        params = {'ksize':(ksize, ksize), 'sigma':1.0, 'theta':theta, 'lambd':15.0,
                  'gamma':0.02, 'psi':0, 'ktype':cv2.CV_32F}
        kern = cv2.getGaborKernel(**params)
        kern /= 1.5*kern.sum()
        filters.append((kern,params))
    return filters

def process(img, filters):
    """ returns the img filtered by the filter list
    """
    accum = np.zeros_like(img)
    for kern,params in filters:
        fimg = cv2.filter2D(img, cv2.CV_8UC3, kern)
        np.maximum(accum, fimg, accum)
    return accum


#main
filters = build_filters()

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Gabor filter applied")
    parser.add_argument("image", nargs=1, help = "Path to image")
    parser.add_argument("block_size", nargs=1, help = "Block size")
    parser.add_argument("--save", action='store_true', help = "Save result image as src_image_enhanced.gif")
    args = parser.parse_args()

    im = Image.open(args.image[0])
    im = im.convert("L")  # covert to grayscale
    im.show()

    W = int(args.block_size[0])

    f = lambda x, y: 2 * x * y
    g = lambda x, y: x ** 2 - y ** 2


    # result = gabor(im, W, angles)
    # result.show()

    if args.save:
        base_image_name = os.path.splitext(os.path.basename(args.image[0]))[0]
        im.save(base_image_name + "_enhanced.gif", "GIF")
