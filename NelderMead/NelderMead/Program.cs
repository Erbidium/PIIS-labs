using MathNet.Numerics.LinearAlgebra;
using NelderMead;

const double distanceBetweenTwoPoints = 1;
const double precision = 0.01;
const int iterationsNumber = 500;

var startingPoint = Vector<double>.Build.Dense(new []{3.0, 3.0, 2.0});

NelderMeadMethod.Run(startingPoint, distanceBetweenTwoPoints, precision, iterationsNumber);