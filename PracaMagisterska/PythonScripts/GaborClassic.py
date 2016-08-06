from __future__ import print_function
import cv2
import matplotlib.pyplot as plt
import numpy as np
from scipy import ndimage as ndi
import argparse

from skimage import data
from skimage.util import img_as_float
from skimage.filters import gabor_kernel


def compute_feats(image, kernels):
    feats = np.zeros((len(kernels), 2), dtype=np.double)
    for k, kernel in enumerate(kernels):
        filtered = ndi.convolve(image, kernel, mode='wrap')
        feats[k, 0] = filtered.mean()
        feats[k, 1] = filtered.var()
    return feats


def match(feats, ref_feats):
    min_error = np.inf
    min_i = None
    for i in range(ref_feats.shape[0]):
        error = np.sum((feats - ref_feats[i, :])**2)
        if error < min_error:
            min_error = error
            min_i = i
    return min_i

def power(image, kernel):
    # Normalize images for better comparison.
    image = (image - image.mean()) / image.std()
    return np.sqrt(ndi.convolve(image, np.real(kernel), mode='wrap')**2 +
                   ndi.convolve(image, np.imag(kernel), mode='wrap')**2)

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Gabor filter applied")
    parser.add_argument("image", nargs=1, help = "path to image")
    parser.add_argument("angle", nargs=1, help = "angle")
    parser.add_argument("frq", nargs=1, help = "frequency")
    args = parser.parse_args()
    shrink = (slice(0, None, 3), slice(0, None, 3))
    # prepare filter bank kernels
    kernel = np.real(gabor_kernel(float(args.frq[0]), theta=float(args.angle[0])/180*np.pi))
    img = img_as_float(cv2.imread(args.image[0], 0))[shrink]

    # Plot a selection of the filter bank kernels and their responses.
    results = []
    kernel = gabor_kernel(float(args.frq[0]), theta=float(args.angle[0])/180*np.pi)
    params = 'angle=%d,\nfrequency=%.2f' % (float(args.angle[0]), float(args.frq[0]))
    # Save kernel and the power image for each image
    img = power(img, kernel)
    from PIL import Image
    im = Image.fromarray(img)
    im.save('processedImage.tiff')
