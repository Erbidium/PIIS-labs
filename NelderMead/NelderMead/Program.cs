using NelderMead;

const double distanceBetweenTwoPoints = 1;
const double precision = 0.01;
const int iterationsNumber = 500;

var startingPoint = (x1: 3, x2: 3, x3: 2);
NelderMeadMethod.Run(startingPoint, distanceBetweenTwoPoints, precision, iterationsNumber);