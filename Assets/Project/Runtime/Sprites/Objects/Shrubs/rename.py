import os

filepath = "S:\\Unity\\Projects\\Pylos\\Assets\\Project\\Runtime\\Sprites\\Objects\\Shrubs\\"

for count, file in enumerate(os.listdir(filepath)[1:]):
	source = filepath + file
	os.rename(source, f'Shrub {count + 1}.png')