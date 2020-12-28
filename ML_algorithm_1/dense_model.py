from tensorflow import keras
import tensorflow as tf

class MCDropout(keras.layers.Dropout):
    def call(self, inputs):
        return super().call(inputs, training=True)

def get_model(shape = (30,)):
    tf.keras.backend.clear_session()
    tf.random.set_seed(42)
    model_dense = keras.models.Sequential([
    keras.layers.Dense(40, activation = 'relu', input_shape=shape),
    keras.layers.BatchNormalization(),
    MCDropout(0.3),
    keras.layers.Dense(40, activation = 'selu'),
    keras.layers.BatchNormalization(),
    keras.layers.Dense(5, activation = 'selu'),
    keras.layers.BatchNormalization(),
    keras.layers.Dense(3, activation = 'softmax')
])
    return model_dense