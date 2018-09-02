# -*- coding: utf-8 -*-
import argparse

from keras.preprocessing.image import ImageDataGenerator
from keras.models import Sequential
from keras.layers import Conv2D, MaxPooling2D
from keras.layers import Activation, Dropout, Flatten, Dense

WIDTH, HEIGHT = 150, 150

TRAIN_PATH = './image_samples/train/'
VALIDATE_PATH = './image_samples/validate/'
SAVE_DIR = './image_samples/generated'

BATCH_SIZE = 16
INPUT_SHAPE = (WIDTH, HEIGHT, 3)

EPOCHS = 5
NUM_TRAIN_STEPS = 20
NUM_VALIDATE_STEPS = 10


def prepare_data(save):
    train_datagen = ImageDataGenerator(
        rescale=1./255,
        shear_range=0.2,
        zoom_range=0.2,
        width_shift_range=0.2,
        horizontal_flip=True
    )

    params = {
        'target_size': (WIDTH, HEIGHT),
        'batch_size': BATCH_SIZE,
        'class_mode': 'categorical'
    }

    if save:
        params.update({
            'save_format': 'jpeg',
            'save_to_dir': SAVE_DIR
        })

    train_generator = train_datagen.flow_from_directory(
        TRAIN_PATH,
        **params
    )

    validate_datagen = ImageDataGenerator(rescale=1./255)

    validate_generator = validate_datagen.flow_from_directory(
        VALIDATE_PATH,
        target_size=(WIDTH, HEIGHT),
        batch_size=BATCH_SIZE,
        class_mode='categorical'
    )

    return train_generator, validate_generator


def prepare_model():
    model = Sequential()

    model.add(Conv2D(32, (3, 3), input_shape=INPUT_SHAPE))
    model.add(Activation('relu'))
    model.add(MaxPooling2D(pool_size=(2, 2)))

    model.add(Conv2D(32, (3, 3)))
    model.add(Activation('relu'))
    model.add(MaxPooling2D(pool_size=(2, 2)))

    model.add(Flatten())
    model.add(Dense(32))
    model.add(Activation('relu'))
    model.add(Dropout(0.5))
    model.add(Dense(3))
    model.add(Activation('softmax'))

    model.compile(
        loss='categorical_crossentropy',
        optimizer='rmsprop',
        metrics=['accuracy']
    )
    return model


def train_model(model, train_generator, validate_generator):
    model.fit_generator(
        train_generator,
        steps_per_epoch=NUM_TRAIN_STEPS,
        epochs=EPOCHS,
        validation_data=validate_generator,
        validation_steps=NUM_VALIDATE_STEPS,
    )


def save_model(model):
    with open("model.yaml", "w") as yaml_file:
        yaml_file.write(model.to_yaml())
    model.save_weights('weights.h5')


if __name__ == '__main__':
    argparser = argparse.ArgumentParser()
    argparser.add_argument(
        '-s',
        '--save',
        help='Save generated images',
        action='store_true'
    )
    args = argparser.parse_args()
    train, validate = prepare_data(args.save)
    model = prepare_model()
    train_model(model, train, validate)
    print("Save? [y/n]")
    if input() == 'y':
        save_model(model)
