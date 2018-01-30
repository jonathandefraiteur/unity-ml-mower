# Unity ML - Mower

gif

## Introduction

## The environment

## Machine Learning
We started our quest with the ppo jupyter notebook.

### The inputs
Our mower has 8 sensors around itself.

It has 3 types of sensors :
1. the distance of the first clod
2. the number of clods along the sensor
3. the distance of an obstable

So our inputs is just a vector of 24.

### The rewards and punishements
We tried different rewards and punishements for our mower. The best that we found for the model is this :
- each clod mowned : +0.1
- all clods mowned : +10
- a rock touched : -1

We will try to add a little punishement for each frame. 

### The decisions
A mower can go forward, backward, right and left. So the agent needs to return a vector of 2 (continious values). First value is for speed and the second for the rotation.
Examples :
- [-1, 0] : backward
- [0.5,0.75] : forward and right

### hyperparameters and model
For the training succeed, we tuned the hyperparameters.
- max_steps = 1e7
- num_layers = 4
- buffer_size = 10000
- learning_rate = 1e-6
- hidden_units = 1024
- batch_size = 2000

Increase the buffer_size and the batch_size was a good idea from this issue : https://github.com/Unity-Technologies/ml-agents/issues/288

### The training

### Conclusion

### Improvements
