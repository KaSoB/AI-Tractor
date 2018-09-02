# -*- coding: utf-8 -*-
import cv2
import numpy as np
from keras.preprocessing.image import ImageDataGenerator
from keras.models import model_from_yaml


class Predictor:
    IMG_SIZE = 150
    TEST_PATH = 'image_samples/test/'

    def __init__(self):
        model = self._load_model()
        test_generator = self._load_test_data_generator()
        results = model.predict_generator(test_generator)
        self.results_dict = self._create_prediction_dict(
            results,
            test_generator
        )
        print("Ready to predict.")

    def _load_model(self):
        with open('model.yaml', 'r') as yaml:
            model_yaml = yaml.read()
        model = model_from_yaml(model_yaml)
        model.load_weights("weights.h5")
        return model

    def _load_test_data_generator(self):
        test_datagen = ImageDataGenerator(rescale=1. / 255)
        test_generator = test_datagen.flow_from_directory(
            self.TEST_PATH,
            shuffle=False,
            target_size=(self.IMG_SIZE, self.IMG_SIZE),
            batch_size=1,
            class_mode='categorical'
        )
        return test_generator

    def _create_prediction_dict(self, prediction_results, test_generator):
        classes = [np.argmax(preds) for preds in prediction_results]
        labels = {v: k for k, v in test_generator.class_indices.items()}

        def name2path(s):
            return self.TEST_PATH + s.replace('\\', '/')

        return {
            name2path(test_generator.filenames[i]): labels[classes[i]]
            for i in range(len(classes))
        }

    def predict(self, path):
        return self.results_dict[path]

    def _show_acc(self):
        num_correct_preds = \
            sum(k.split('\\')[0] == v for k, v in self.results_dict.items())
        print(num_correct_preds / len(self.results_dict))

    def _old_predict(self, path):
        if not hasattr(self, 'model'):
            self.model = self._load_model()
        image = cv2.imread(path)
        image = cv2.cvtColor(image, cv2.COLOR_RGBA2RGB)
        image = image.astype(np.float32) * 1./255
        image = cv2.resize(image, (150, 150))
        image = np.expand_dims(image, axis=0)
        results = self.model.predict(image, batch_size=1)
        return self._num2label(int(results.argmax(axis=-1)))

    def _num2label(self, num):
        return {
            0: 'Carrot',
            1: 'Corn',
            2: 'Wheat'
        }[num]


if __name__ == "__main__":
    Predictor()._show_acc()
